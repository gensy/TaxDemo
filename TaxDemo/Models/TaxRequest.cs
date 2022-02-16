using System.Collections.Generic;

namespace Taxdemo.Models
{
    public class TaxRequest
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public double amount { get; set; }
        public double shipping { get; set; }
        public List<LineItemRequest> line_items { get; set; }
    }
}