namespace Application.Dtos
{
    public class SkuDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<SubSkuItemDto> SubSkus { get; set; } = new();
        public SubSkuItemDto SkuSum { get; set; } = new();
    }
}
