using Invoice_MobileMekaniko.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_MobileMekaniko.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var customerList = await _customerRepository.GetAllCustomersAsync();
            return View(customerList);
        }
    }
}
