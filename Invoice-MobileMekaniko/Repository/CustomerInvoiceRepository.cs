using Invoice_MobileMekaniko.Data;
using Invoice_MobileMekaniko.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice_MobileMekaniko.Repository
{
    public class CustomerInvoiceRepository : ICustomerInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerInvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCustomerInvoiceAsync(CustomerInvoiceViewModel model)
        {
            // Fetch the customer based on CarRego, including related invoices
            var customer = await _context.tblCustomer
                 .Include(c => c.tblInvoice) // Include related invoices for the customer
                 .FirstOrDefaultAsync(c => c.CarRego == model.CarRego); // Find the first customer with the matching CarRego

            if (customer == null)
            {
                // If the customer is not found, create a new customer entity
                customer = new tblCustomer
                {
                    CarRego = model.CarRego,
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CarMake = model.CarMake,
                    CarModel = model.CarModel,
                    PaymentStatus = model.PaymentStatus
                };
                _context.tblCustomer.Add(customer); // Add the new customer entity to the context
            }
            else
            {
                // If the customer is found, update the existing customer's details
                customer.CustomerName = model.CustomerName;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CarMake = model.CarMake;
                customer.CarModel = model.CarModel;
                customer.PaymentStatus = model.PaymentStatus;
            }

            // Create a new invoice entity for the customer
            var invoice = new tblInvoice
            {
                DateAdded = model.DateAdded ?? DateTime.Now, // Set DateAdded to now if not provided
                DueDate = model.DueDate,
                IssueName = model.IssueName,
                PaymentTerm = model.PaymentTerm,
                Notes = model.Notes,
                LaborPrice = model.LaborPrice,
                Discount = model.Discount,
                ShippingFee = model.ShippingFee,
                SubTotal = model.SubTotal,
                TaxAmount = model.TaxAmount,
                TotalAmount = model.TotalAmount,
                AmountPaid = model.AmountPaid,
                CarRego = model.CarRego, // Set the CarRego for the invoice
                tblCustomer = customer // Link the invoice to the customer
            };

            // Add the new invoice entity to the context
            _context.tblInvoice.Add(invoice);

            // Save changes to the context to generate the InvoiceId for the invoice
            await _context.SaveChangesAsync();

            // Create a list of invoice item entities, setting the InvoiceId for each item
            var invoiceItems = model.InvoiceItems.Select(item => new tblInvoiceItem
            {
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                ItemPrice = item.ItemPrice,
                InvoiceId = invoice.InvoiceId // Set the InvoiceId to link to the parent invoice
            }).ToList();

            // Add the invoice item entities to the context
            _context.tblInvoiceItem.AddRange(invoiceItems);

            // Save all changes to the context, including the invoice items
            await _context.SaveChangesAsync();
        }

        public async Task<tblCustomer> GetCustomerByCarRegoAsync(string carRego)
        {
            return await _context.tblCustomer
                .Include(c => c.tblInvoice)
                .FirstOrDefaultAsync(c => c.CarRego == carRego);
        }
    }
}
