using Invoice_MobileMekaniko.Models;

namespace Invoice_MobileMekaniko.Repository
{
    public interface ICustomerRepository
    {
        Task<List<tblCustomer>> GetAllCustomersAsync();
    }
}
