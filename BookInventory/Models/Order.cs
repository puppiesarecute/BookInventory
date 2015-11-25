using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookInventory.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string Cashier { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}