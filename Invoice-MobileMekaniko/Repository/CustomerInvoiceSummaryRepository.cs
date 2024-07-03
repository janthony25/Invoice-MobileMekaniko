using Invoice_MobileMekaniko.Data;
using Invoice_MobileMekaniko.Models;
using Invoice_MobileMekaniko.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Invoice_MobileMekaniko.Repository
{
    public class CustomerInvoiceSummaryRepository : ICustomerInvoiceSummaryRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerInvoiceSummaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<List<CustomerInvoiceSummaryDto>> GetCustomerInvoiceSummaryAsync()
        //{
        //    return await _context.tblCustomer
        //        .Include(c => c.tblInvoice)
        //        .SelectMany(c => c.tblInvoice.Select(i => new CustomerInvoiceSummaryDto
        //        {
        //            CarRego = c.CarRego,
        //            CustomerName = c.CustomerName,
        //            CustomerEmail = c.CustomerEmail,
        //            CarMake = c.CarMake,
        //            CarModel = c.CarModel,
        //            PaymentStatus = c.PaymentStatus,
        //            InvoiceId = i.InvoiceId,
        //            DateAdded = i.DateAdded,
        //            IssueName = i.IssueName,
        //            TotalAmount = i.TotalAmount,
        //            AmountPaid = i.AmountPaid
        //        })).ToListAsync();
        //}


        public async Task<List<CustomerInvoiceSummaryDto>> GetCustomerInvoiceSummaryAsync(string carRego)
        {
            var leftJoinQuery = from customer in _context.tblCustomer
                                join invoice in _context.tblInvoice
                                on customer.CarRego equals invoice.CarRego
                                into customerInvoices
                                from customerInvoice in customerInvoices.DefaultIfEmpty()
                                where customer.CarRego == carRego
                                select new
                                {
                                    CarRego = customer.CarRego,
                                    CustomerName = customer.CustomerName,
                                    CustomerEmail = customer.CustomerEmail,
                                    CarMake = customer.CarMake,
                                    CarModel = customer.CarModel,
                                    PaymentStatus = customer.PaymentStatus,
                                    InvoiceId = customerInvoice != null ? customerInvoice.InvoiceId : 0,
                                    DateAdded = customerInvoice.DateAdded,
                                    IssueName = customerInvoice.IssueName,
                                    DueDate = customerInvoice.DueDate,
                                    TotalAmount = customerInvoice.TotalAmount,
                                    AmountPaid = customerInvoice.AmountPaid
                                };

            List<CustomerInvoiceSummaryDto> resultList = await leftJoinQuery
                .Select(x => new CustomerInvoiceSummaryDto
                {
                    CarRego = x.CarRego,
                    CustomerName = x.CustomerName,
                    CustomerEmail = x.CustomerEmail,
                    CarMake = x.CarMake,
                    CarModel = x.CarModel,
                    PaymentStatus = x.PaymentStatus,
                    InvoiceId = x.InvoiceId,
                    DateAdded = x.DateAdded,
                    IssueName = x.IssueName,
                    DueDate = x.DueDate,
                    TotalAmount = x.TotalAmount,
                    AmountPaid = x.AmountPaid
                })
                .ToListAsync();

            return resultList;
        }

    }
}
