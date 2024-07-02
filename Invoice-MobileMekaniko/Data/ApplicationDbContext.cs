using Invoice_MobileMekaniko.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice_MobileMekaniko.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<tblCustomer> tblCustomer { get; set; }
        public DbSet<tblInvoice> tblInvoice { get; set; }
        public DbSet<tblInvoiceItem> tblInvoiceItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCustomer>()
                .HasMany(c => c.tblInvoice)
                .WithOne(i => i.tblCustomer)
                .HasForeignKey(i => i.CarRego);

            modelBuilder.Entity<tblInvoice>()
                .HasMany(i => i.tblInvoiceItem)
                .WithOne(ii => ii.tblInvoice)
                .HasForeignKey(ii => ii.InvoiceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
