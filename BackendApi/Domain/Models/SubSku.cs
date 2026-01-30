namespace Models
{
    public class SubSku
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SkuId { get; set; }
        public decimal Price { get; set; }
        public int Ratio { get; set; }
        public HistoryY0 HistoryY0s { get; set; } = null!;
        public PlaningY1 PlaningY1s { get; set; } = null!;
    }
}
