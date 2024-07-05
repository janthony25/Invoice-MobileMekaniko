using Invoice_MobileMekaniko.Models;
using Invoice_MobileMekaniko.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_MobileMekaniko.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerInvoiceSummaryRepository _customerSummary;
        private readonly ICustomerInvoiceRepository _customerInvoiceRepository;
        public CustomerController(ICustomerRepository customerRepository, ICustomerInvoiceSummaryRepository customerSummary, ICustomerInvoiceRepository customerInvoiceRepository)
        {
            _customerRepository = customerRepository;
            _customerSummary = customerSummary;
            _customerInvoiceRepository = customerInvoiceRepository;
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

        // GET - Add CustomerInvoice
        public async Task<IActionResult> CreateCustomerInvoice(string carRego)
        {
            var customer = await _customerInvoiceRepository.GetCustomerByCarRegoAsync(carRego);

            if (customer == null)
            {
                return NotFound(); // or handle the case where customer is not found
            }

            var model = new CustomerInvoiceViewModel
            {
                CarRego = customer.CarRego,
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CarMake = customer.CarMake,
                CarModel = customer.CarModel,
                PaymentStatus = customer.PaymentStatus,
                InvoiceItems = new List<InvoiceItemViewModel> { new InvoiceItemViewModel() }
            };

            return View(model);
        }

        // POST - Add new Customer Invoice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomerInvoice(CustomerInvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _customerInvoiceRepository.AddCustomerInvoiceAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
