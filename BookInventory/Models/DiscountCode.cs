namespace BookInventory.Models
{
    public class DiscountCode
    {
        public int DiscountCodeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double DiscountValue { get; set; }
    }
}