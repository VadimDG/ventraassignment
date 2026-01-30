namespace Application.Models
{
    public class SkuModel
    {
        public int Id { get; }
        public string Name { get; } = null!;
        public List<SubSkuModel> SubSkus { get; } = new();
        public SubSkuItemModel SkuSum { get; set; } = new();

        public SkuModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSubSku(SubSkuModel subSku)
        {
            SubSkus.Add(subSku);
            SkuSum.UnitsH0 += subSku.UnitsH0 * subSku.Ratio;
            SkuSum.UnitsY1 += subSku.UnitsY1 * subSku.Ratio;
            SkuSum.AmountH0 += subSku.AmountH0;
            SkuSum.AmountY1 += subSku.AmountY1;
        }

        public void CalculatePriceH0()
        {
            SkuSum.PriceH0 = SkuSum.UnitsH0 == 0 ? 0 : SkuSum.AmountH0 / SkuSum.UnitsH0;
        }

        public void CalculatePriceY1()
        {
            SkuSum.PriceY1 = SkuSum.UnitsY1 == 0 ? 0 : SkuSum.AmountY1 / SkuSum.UnitsY1;
        }
        public void CalculateContributionGrowth()
        {
            SkuSum.UnitsContributionGrowth = SkuSum.UnitsH0 == 0 ? 0 : (SkuSum.UnitsY1 - SkuSum.UnitsH0) / SkuSum.UnitsH0;
            SkuSum.AmountContributionGrowth = SkuSum.AmountH0 == 0 ? 0 : (SkuSum.AmountY1 - SkuSum.AmountH0) / SkuSum.AmountH0;
            SkuSum.PriceContributionGrowth =  SkuSum.PriceH0 == 0 ? 0 : (SkuSum.PriceY1 - SkuSum.PriceH0) / SkuSum.PriceH0;
        }
    }
}
