namespace CoverGo.Task.Application.DTO.Discounts
{
    public class CreateDiscountDto
    {
        public int ProductId { get; set; }
        public int RequiredAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
