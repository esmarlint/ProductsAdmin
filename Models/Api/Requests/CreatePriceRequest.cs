namespace ProductsAdmin.Models.Api.Requests
{
    public class CreatePriceRequest
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public bool IsDefaultPrice { get; set; }
    }

    public class UpdatePriceRequest
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public bool IsDefaultPrice { get; set; }
    }

}
