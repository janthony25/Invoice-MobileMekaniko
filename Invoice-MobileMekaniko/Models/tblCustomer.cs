using System.ComponentModel.DataAnnotations;

namespace Invoice_MobileMekaniko.Models
{
    public class tblCustomer
    {
        [Key]
        public string CarRego { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? PaymentStatus { get; set; }

    }
}
