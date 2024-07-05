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
            var customer = await _context.tblCustomer
                 .Include(c => c.tblInvoice)
                 .FirstOrDefaultAsync(c => c.CarRego == model.CarRego);

            if (customer == null)
            {
                customer = new tblCustomer
                {
                    CarRego = model.CarRego,
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CarMake = model.CarMake,
                    CarModel = model.CarModel,
                    PaymentStatus = model.PaymentStatus
                };
                _context.tblCustomer.Add(customer);
            }
            else
            {
                customer.CustomerName = model.CustomerName;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CarMake = model.CarMake;
                customer.CarModel = model.CarModel;
                customer.PaymentStatus = model.PaymentStatus;
            }

            var invoice = new tblInvoice
            {
                DateAdded = model.DateAdded ?? DateTime.Now,
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
                CarRego = model.CarRego,
                tblCustomer = customer
            };

            _context.tblInvoice.Add(invoice);

            await _context.SaveChangesAsync();

            var invoiceItems = model.InvoiceItems.Select(item => new tblInvoiceItem
            {
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                ItemPrice = item.ItemPrice,
                InvoiceId = invoice.InvoiceId
            }).ToList();

            _context.tblInvoiceItem.AddRange(invoiceItems);

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
