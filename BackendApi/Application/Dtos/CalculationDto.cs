namespace Application.Dtos
{
    public class CalculationDto
    {
        public List<SkuDto> Skus { get; set; } = new();
        public SubSkuItemDto Totals { get; set; } = new();
    }
}
