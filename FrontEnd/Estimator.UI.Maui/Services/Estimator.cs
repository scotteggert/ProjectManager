
namespace Estimator.UI.Services
{
    public class Estimator : IEstimator
    {
        public List<Estimate> GetEstimates()
        {
            var temp = new List<Estimate>();
            var e = new Estimate();
            e.ClientName = "Client 1";
            e.EstimatedStartDate = DateTime.Now;
            e.EstimatedEndDate = DateTime.Now.AddDays(30);
            e.JobCode = "1111";
            e.ProjectManager = "Joel Smith";
            temp.Add(e);

            e = new Estimate();
            e.ClientName = "Client 2";
            e.EstimatedStartDate = DateTime.Now.AddDays(5);
            e.EstimatedEndDate = DateTime.Now.AddDays(40);
            e.JobCode = "2222";
            e.ProjectManager = "Frank Smith";
            temp.Add(e);

            return temp;
        }

        public Estimate GetEstimate(int Id)
        {
            var e = new Estimate();
            e.ClientName = "Client 1";
            e.EstimatedStartDate = DateTime.Now;
            e.EstimatedEndDate = DateTime.Now.AddDays(30);
            e.JobCode = "1111";
            e.ProjectManager = "Joel Smith";

            return e;
        }

        public void AddEstimate(Estimate estimate)
        {

        }

        public void RemoveEstimate(Estimate estimate)
        {

        }

      
    }
}
