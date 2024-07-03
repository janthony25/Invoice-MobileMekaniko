namespace Invoice_MobileMekaniko.Models.Dto
{
    public class CustomerInvoiceSummaryDto
    {
        //From tblCustomer
        public string CarRego { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CarMake { get; set; }
        public string? CarModel { get; set; }
        public string? PaymentStatus { get; set; }

        //From tblInvoice
        public int? InvoiceId { get; set; }
        public DateTime? DateAdded { get; set; }
        public string? IssueName { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AmountPaid { get; set; }
    }
}
