namespace Estimator.UI.Services
{
    public interface IEstimator
    {
        void AddEstimate(Estimate estimate);
        Estimate GetEstimate(int Id);
        List<Estimate> GetEstimates();
        void RemoveEstimate(Estimate estimate);
    }
}