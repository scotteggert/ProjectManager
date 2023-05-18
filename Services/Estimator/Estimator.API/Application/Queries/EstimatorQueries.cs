namespace Estimator.Application.Queries
{
    public class EstimatorQueries
    {
        private string _connectionString = string.Empty;

        public EstimatorQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<Estimate> GetEstimateAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(
                @"select e.Name, e.Description, e.ClientName, e.JobCode, e.ProjectManager, e.TotalEstimate, e.CreatedDate, e.ModifiedDate,
                    ep.Id, ep.Name, ep.Description, ep.TotalPhaseEstimate
                    eprci.EstimatePhaseRateCardItemId, eprci.RateCardItemId, eprci.RoleName, eprci.Rate, eprc.Count,
                    rc.Id, rc.Name, rc.Description,
                    rci.RateCardItemId, rci.RoleName, rci.Rate, rci.EffectiveFrom, rci.EffectiveTo,
                    from Estimates e, 
                    inner join RateCard rc on e.RateCardId = rc.Id
                    inner join RateCardItems rci on rc.Id = rci.RateCardId
                    inner join EstimatePhases ep on e.Id = ep.EstimateId
                    inner join EstimatePhaseRateCardItems eprci on ep.Id = eprci.EstimatePhaseId
                    WHERE e.Id=@id"
                    , new { id }
                );

            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();

            return MapEstimateItems(result);
        }


        private Estimate MapEstimateItems(dynamic result)
        {
            var estimate = new Estimate
            {
                ClientName = result[0].clientname,
                JobCode = result[0].jobcode,
                ProjectManager = result[0].projectmanager,
                TotalEstimate = 0,
                RateCard = new RateCard
                {
                    RateCardId = result[0].ratecard.ratecardid,
                    Name = result[0].ratecard.name,
                    Description = result[0].ratecard.description,
                    RateCardItems = new List<RateCardItem>()
                },
                EstimatePhases = new List<EstimatePhase>()
            };

            foreach (dynamic item in result.EstimatePhases)
            {
                var estimatePhase = new EstimatePhase
                {
                    EstimatePhaseId = item.id,
                    Name = item.name,
                    Description = item.description,
                    EstimatePhaseRateCardItems = new List<EstimatePhaseRateCardItem>()
                };

                foreach (dynamic rateCardItem in item.EstimatePhaseRateCardItems)
                {
                    var estimatePhaseRateCardItem = new EstimatePhaseRateCardItem
                    {
                        EstimatePhaseRateCardItemId = rateCardItem.EstimatePhaseRateCardItemId,
                        RateCardItemId = rateCardItem.RateCardItemId,
                        RoleName = rateCardItem.RoleName,
                        Rate = rateCardItem.Rate,
                        Count = rateCardItem.Count
                    };
                    estimatePhase.EstimatePhaseRateCardItems.Add(estimatePhaseRateCardItem);
                }

                estimate.EstimatePhases.Add(estimatePhase);
            }

            return estimate;
        }

    }
}
