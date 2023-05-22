using Model = Estimator.UI.Model;

namespace Estimator.UI.Services
{
    public class RateCard : IRateCard
    {
        private Model.RateCard rateCard = new Model.RateCard();

        public RateCard()
        {

            rateCard = new Model.RateCard("1", "Default", new List<RateCardItem>());

            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Executive Creative Director", Rate = 350, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Group Creative Director", Rate = 300, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Creative Director", Rate = 250, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Associate Creative Director", Rate = 200, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior UX Designer/Researcher", Rate = 184, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Art Director", Rate = 180, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Designer", Rate = 177, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Art Director", Rate = 165, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Motion Design/Editor", Rate = 165, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Copywriter", Rate = 160, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Production Designer", Rate = 150, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Copywriter", Rate = 142, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Designer", Rate = 140, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "UX Designer/Researcher", Rate = 140, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Motion Design/Editor", Rate = 140, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Copy Editor/QA Specialist", Rate = 136, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Junior Designer", Rate = 105, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Production Designer", Rate = 105, GroupName = "Design & Creative Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Practice Lead", Rate = 350, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Technology Strategy Director", Rate = 350, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Technology Strategy Director", Rate = 245, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Discipline Lead", Rate = 195, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Technology Manager", Rate = 185, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Solution Architect", Rate = 177, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Data And Analytics Manager", Rate = 177, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Developer", Rate = 170, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Data Analyst", Rate = 163, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "QA Lead", Rate = 160, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Senior Data Engineer", Rate = 149, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Devops Engineer", Rate = 142, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Data Analyst", Rate = 130, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Developer", Rate = 128, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "System Admin", Rate = 121, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Data Engineer", Rate = 120, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Data Coordinator", Rate = 115, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "QA Analyst", Rate = 106, GroupName = "Technology & Data Practice" });
            rateCard.RateCardItems.Add(new RateCardItem() { RoleName = "Junior Developer", Rate = 99, GroupName = "Technology & Data Practice" });


        }

        public Model.RateCard GetRateCard()
        {
            return rateCard;
        }

    }

}