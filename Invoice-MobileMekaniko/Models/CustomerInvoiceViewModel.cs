namespace Invoice_MobileMekaniko.Models
{
    public class CustomerInvoiceViewModel
    {
        // Customer details
        public string CarRego { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CarMake { get; set; }
        public string? CarModel { get; set; }
        public string? PaymentStatus { get; set; }

        // Invoice details
        public DateTime? DateAdded { get; set; }
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

        // InvoiceItem details
        public List<InvoiceItemViewModel> InvoiceItems { get; set; }
    }
}
