using Invoice_MobileMekaniko.Models;

namespace Invoice_MobileMekaniko.Repository
{
    public interface ICustomerInvoiceRepository
    {
        Task AddCustomerInvoiceAsync(CustomerInvoiceViewModel model);

        Task<tblCustomer> GetCustomerByCarRegoAsync(string carRego);
    }
}
