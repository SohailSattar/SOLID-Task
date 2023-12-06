namespace CoverGo.Task.Domain
{
    public class Discount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual ProductAmount? Product { get; set; }
        public int RequiredAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
