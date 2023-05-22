namespace Estimator.Application.Queries
{
    public interface IEstimatorQueries
    {
        Task<Estimate> GetEstimateAsync(int id);
    }
}