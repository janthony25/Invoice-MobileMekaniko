using System.ComponentModel.DataAnnotations;

namespace Invoice_MobileMekaniko.Models
{
    public class tblInvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal ItemPrice { get; set; }

        // Foreign Key 
        public int InvoiceId { get; set; }
        public tblInvoice? tblInvoice { get; set; }
    }
}
