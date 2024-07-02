using System.ComponentModel.DataAnnotations;

namespace Invoice_MobileMekaniko.Models
{
    public class tblInvoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime? DateAdded { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public string? IssueName { get; set; }
        public string? PaymentTerm { get; set; }
        public string? Notes { get; set; }
        public decimal? LaborPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AmountPaid { get; set; }

        // Foreign key
        public string CarRego { get; set; }
        public tblCustomer? tblCustomer { get; set; }

        // Has many InvoiceItem
        public ICollection<tblInvoiceItem>? tblInvoiceItem { get; set; }
    }
}
