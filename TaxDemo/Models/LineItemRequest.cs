namespace Taxdemo.Models
{
    public class LineItemRequest
    {
        public int quantity { get; set; }
        public double unit_price { get; set; }
        public string product_tax_code { get; set; }
    }
}
