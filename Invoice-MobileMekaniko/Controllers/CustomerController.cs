﻿using Invoice_MobileMekaniko.Models;
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
            if(ModelState.IsValid)
            {
                await _customerRepository.AddCustomerAsync(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}
