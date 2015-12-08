namespace BookInventory.Models
{
    public class OrderLine
    {
        private double price;
        private double subTotal;
        private double total;

        public int OrderLineId { get; set; }
        public int SalesQuantity { get; set; }

        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (this.Book != null)
                    this.price = Book.SalesPrice;
                else
                    throw new System.Exception("Book is null!!!");
            }
        }

        public double SubTotal
        {
            get
            {
                return this.subTotal;
            }
            set
            {
                if (this.DiscountCode != null)
                    this.subTotal = this.Price * this.SalesQuantity * (1 + DiscountCode.DiscountValue);
                else
                    this.subTotal = this.Price * this.SalesQuantity;
            }
        }

        public double Total
        {
            get
            {
                return this.total;
            }
            set
            {
                this.total = this.SubTotal * (1 + VAT.Vat);
            }
        }

        public DiscountCode DiscountCode { get; set; }
        public Book Book { get; set; }
    }
}