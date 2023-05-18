using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

//******************

public partial class Program
{

    public static string Namespace = typeof(Program).Assembly.GetName().Name;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}

static class CustomExtensionsMethods
{
    public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationInsightsTelemetry(configuration);
        services.AddApplicationInsightsKubernetesEnricher();

        return services;
    }

    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        // Add framework services.
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        })
            // Added for functional tests
            .AddApplicationPart(typeof(Estimator.API.Controllers.Estimator).Assembly)
            .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        hcBuilder
            .AddSqlServer(
                configuration["ConnectionString"],
                name: "EstimatorDB-check",
                tags: new string[] { "estimatordb" });

        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            hcBuilder
                .AddAzureServiceBusTopic(
                    configuration["EventBusConnection"],
                    topicName: "eshop_event_bus",
                    name: "estimator-servicebus-check",
                    tags: new string[] { "servicebus" });
        }
        else
        {
            hcBuilder
                .AddRabbitMQ(
                    $"amqp://{configuration["EventBusConnection"]}",
                    name: "estimator-rabbitmqbus-check",
                    tags: new string[] { "rabbitmqbus" });
        }

        return services;
    }

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EstimatorContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionString"],
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
        },
                    ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                );

        services.AddDbContext<IntegrationEventLogContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionString"],
                                    sqlServerOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
        });

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "eShopOnContainers - Estimator HTTP API",
                Version = "v1",
                Description = "The Estimator Service HTTP API"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
                        TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
                        Scopes = new Dictionary<string, string>()
                        {
                            { "orders", "Estimator API" }
                        }
                    }
                }
            });
            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });
        return services;
    }

    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
            sp => (DbConnection c) => new IntegrationEventLogService(c));

        services.AddTransient<IEstimatorIntegrationEventService, EstimatorIntegrationEventService>();

        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                var serviceBusConnectionString = configuration["EventBusConnection"];

                var subscriptionClientName = configuration["SubscriptionClientName"];

                return new DefaultServiceBusPersisterConnection(serviceBusConnectionString);
            });
        }
        else
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
        }

        return services;
    }

    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<EstimatorSettings>(configuration);
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json", "application/problem+xml" }
                };
            };
        });

        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            {
                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                string subscriptionName = configuration["SubscriptionClientName"];

                return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                    eventBusSubcriptionsManager, sp, subscriptionName);
            });
        }
        else
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });
        }

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // prevent from mapping "sub" claim to nameidentifier.
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        var identityUrl = configuration.GetValue<string>("IdentityUrl");

        services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = "orders";
            options.TokenValidationParameters.ValidateAudience = false;
        });

        return services;
    }
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "orders");
            });
        });
        return services;
    }
}