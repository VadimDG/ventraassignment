namespace Application.Models
{
    public class SubSkuModel : SubSkuItemModel
    {
        public int Ratio { get; set; }
        public SubSkuItemModel Sums { get; set; } = new();

        public SubSkuModel(decimal price, int ratio, int h0Units, decimal h0Amount, int y1Units, decimal y1Amount)
        {
            Ratio = ratio;

            PriceY1 = price;
            PriceH0 = h0Amount / h0Units;
            PriceContributionGrowth = (PriceY1 - PriceH0) / PriceH0;

            AmountH0 = h0Units * h0Amount;
            AmountY1 = y1Units * y1Amount;
            AmountContributionGrowth = (AmountY1 - AmountH0) / AmountH0;

            UnitsH0 = h0Units;
            UnitsY1 = y1Units;
            UnitsContributionGrowth = (decimal)(UnitsY1 - UnitsH0) / UnitsH0;
        }
    }
}
