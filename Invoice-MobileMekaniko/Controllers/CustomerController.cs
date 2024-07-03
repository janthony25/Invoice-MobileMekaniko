using Invoice_MobileMekaniko.Models;
using Invoice_MobileMekaniko.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_MobileMekaniko.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerInvoiceSummaryRepository _customerSummary;
        public CustomerController(ICustomerRepository customerRepository, ICustomerInvoiceSummaryRepository customerSummary)
        {
            _customerRepository = customerRepository;
            _customerSummary = customerSummary;

        }

        // Customer List Page
        public async Task<IActionResult> Index()
        {
            var customerList = await _customerRepository.GetAllCustomersAsync();
            return View(customerList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        // Direct to ApplicationDbContext - faster but for smaller project
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CarRego, CustomerName, CustomerEmail, CarMake, CarModel, PaymentStatus")] tblCustomer customer)
        //{

        //}

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(tblCustomer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddCustomerAsync(customer);
                return Json(new { success = true, message = "Customer Added" });
            }
            return Json(new { success = false, message = "Customer Validation Failed" });
        }

        // GET
        public async Task<IActionResult> GetCustomerInvoiceSummary(string carRego)
        {
            if (string.IsNullOrEmpty(carRego))
            {
                return NotFound();
            }

            var invoiceSummary = await _customerSummary.GetCustomerInvoiceSummaryAsync(carRego);
            if (invoiceSummary == null || !invoiceSummary.Any())
            {
                return NotFound();
            }

            return View(invoiceSummary);
        }
    }
}
