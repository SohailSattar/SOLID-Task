namespace CoverGo.Task.Domain
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<ProductAmount>? Products { get; set; }
        public decimal Total { get; set; } 
    }
}
