namespace TestApi.Model
{
    public class PurchaseOrders
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Instock {  get; set; }

        public decimal Price { get; set; }

        public string Username { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
