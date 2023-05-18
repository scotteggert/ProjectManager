namespace ProjectManager.Services.Estimator.API;

public class EstimatorSettings
{
    public bool UseCustomizationData { get; set; }

    public string ConnectionString { get; set; }

    public string EventBusConnection { get; set; }

    public int GracePeriodTime { get; set; }

    public int CheckUpdateTime { get; set; }
}
