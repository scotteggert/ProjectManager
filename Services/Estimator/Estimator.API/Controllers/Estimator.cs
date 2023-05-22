using Estimator.API.ViewModel;
using Model = Estimator.Application.Models;
using Queries = Estimator.Application.Queries;
namespace Estimator.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
[ApiController]
public class EstimatorController : ControllerBase
    {
    private readonly EstimatorContext _estimatorContext;
    private readonly EstimatorSettings _settings;
    private readonly IEstimatorIntegrationEventService _estimatorIntegrationEventService;



    private readonly IMediator _mediator;
    private readonly IEstimatorQueries _estimateQueries;
    private readonly IIdentityService _identityService;
    private readonly ILogger<EstimatorController> _logger;

    public EstimatorController(
        IMediator mediator, 
        IEstimatorQueries estimatorQueries,
        IIdentityService identityService,
        ILogger<EstimatorController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            estimatorQueries = estimatorQueries ?? throw new ArgumentNullException(nameof(estimatorQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    [HttpGet]
    [Route("items")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<Model.Estimate>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Model.Estimate>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0, string ids = null)
    {
        if (!string.IsNullOrEmpty(ids))
        {
            var items = await GetItemsByIdsAsync(ids);

            if (!items.Any())
            {
                return BadRequest("ids value invalid. Must be comma-separated list of numbers");
            }

            return Ok(items);
        }

        var totalItems = await _estimatorContext.Estimates
            .LongCountAsync();

        var itemsOnPage = await _estimatorContext.Estimates
            .OrderBy(c => c.ClientName)
            .ThenBy(c => c.ProjectManager)
            .ThenBy(c => c.JobCode)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

        var model = new PaginatedItemsViewModel<Model.Estimate>(pageIndex, pageSize, totalItems, itemsOnPage);

        return Ok(model);
    }

    private List<Queries.Estimate> ChangeUriPlaceholder(List<Queries.Estimate> items)
    {
        var baseUri = _settings.PicBaseUrl;
        var azureStorageEnabled = _settings.AzureStorageEnabled;

        foreach (var item in items)
        {
            item.FillProductUrl(baseUri, azureStorageEnabled: azureStorageEnabled);
        }

        return items;
    }

    private async Task<List<Model.Estimate>> GetItemsByIdsAsync(string ids)
    {
        var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

        if (!numIds.All(nid => nid.Ok))
        {
            return new List<Model.Estimate>();
        }

        var idsToSelect = numIds
            .Select(id => id.Value);

        var items = await _estimatorContext.Estimates.Where(ci => idsToSelect.Contains(ci.Id)).ToListAsync();

        items = ChangeUriPlaceholder(items);

        return items;
    }








    [HttpPost]
    [ProducesResponseType(typeof(EstimateDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateEstimateAsync([FromBody] CreateEstimateCommand command)
    {
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetEstimateByIdAsync), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EstimateDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetEstimateByIdAsync(Guid id)
    {
        var rateCard = await _rateCardQueries.GetEstimateByIdAsync(id);

        if (rateCard == null)
        {
            return NotFound();
        }

        return Ok(rateCard);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsDto<EstimateDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetEstimatorAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var rateCards = await _rateCardQueries.GetEstimatorAsync(pageSize, pageIndex);

        var totalItems = rateCards.TotalCount;
        var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

        var model = new PaginatedItemsDto<EstimateDto>(pageIndex, pageSize, totalPages, rateCards.Items);

        return Ok(model);
    }

    [HttpPost("{id}/items")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddEstimateItemAsync(Guid id, [FromBody] AddEstimateItemCommand command)
    {
        command.EstimateId = id;

        await _mediator.Send(command);

        return CreatedAtAction(nameof(GetEstimateByIdAsync), new { id }, null);
    }
    
}