namespace Application.Models
{
    public class SubSkuItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UnitsH0 { get; set; }
        public int UnitsY1 { get; set; }
        public decimal? UnitsContributionGrowth { get; set; }
        public decimal PriceH0 { get; set; }
        public decimal PriceY1 { get; set; }
        public decimal PriceContributionGrowth { get; set; }
        public decimal AmountH0 { get; set; }
        public decimal AmountY1 { get; set; }
        public decimal AmountContributionGrowth { get; set; }
    }
}
