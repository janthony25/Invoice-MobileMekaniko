using Invoice_MobileMekaniko.Models.Dto;

namespace Invoice_MobileMekaniko.Repository
{
    public interface ICustomerInvoiceSummaryRepository
    {
        //Task<List<CustomerInvoiceSummaryDto>> GetCustomerInvoiceSummaryAsync();

        Task<List<CustomerInvoiceSummaryDto>> GetCustomerInvoiceSummaryAsync(string Rego);
    }
}
