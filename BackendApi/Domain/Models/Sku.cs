namespace Models
{
    public class Sku
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public SubSku[] SubSkus { get; set; } = [];
    }
}
