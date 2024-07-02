using Invoice_MobileMekaniko.Data;
using Invoice_MobileMekaniko.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice_MobileMekaniko.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<tblCustomer>> GetAllCustomersAsync()
        {
            return await _context.tblCustomer
                .ToListAsync();
        }

        public async Task AddCustomerAsync(tblCustomer customer)
        {
            _context.tblCustomer.Add(customer);
            await _context.SaveChangesAsync();
        }
    }
}
