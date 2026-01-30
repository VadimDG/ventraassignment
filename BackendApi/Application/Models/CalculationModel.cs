namespace Application.Models
{
    public class CalculationModel
    {
        public List<SkuModel> Skus { get; } = new();
        public SubSkuItemModel Totals { get; } = new();

        public void AddSku(SkuModel sku)
        {
            Skus.Add(sku);
            Totals.UnitsH0 += sku.SkuSum.UnitsH0;
            Totals.UnitsY1 += sku.SkuSum.UnitsY1;

            Totals.AmountH0 += sku.SkuSum.AmountH0;
            Totals.AmountY1 += sku.SkuSum.AmountY1;
        }

        public void CalculateTotalPrice()
        {
            Totals.PriceH0 = Totals.UnitsH0 == 0 ? 0 : Totals.AmountH0 / Totals.UnitsH0;
            Totals.PriceY1 = Totals.UnitsY1 == 0 ? 0 : Totals.AmountY1 / Totals.UnitsY1;
        }

        public void CalculateTotalContributionGrowth()
        {
            Totals.PriceContributionGrowth = Totals.PriceH0 == 0 ? 0 :(Totals.PriceH0 - Totals.PriceY1) / Totals.PriceH0;
            Totals.AmountContributionGrowth = Totals.AmountY1 == 0 ? 0 : (Totals.AmountH0 - Totals.AmountY1) / Totals.AmountH0;
            Totals.UnitsContributionGrowth = Totals.UnitsY1 == 0 ? 0 : (Totals.UnitsH0 - Totals.UnitsY1) / Totals.UnitsH0;
        }
    }
}
