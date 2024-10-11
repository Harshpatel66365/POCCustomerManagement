using Microsoft.EntityFrameworkCore;
using POCCustomerManagement.Entity;

namespace POCCustomerManagement.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<CustomerEntity> Customers { get; set; }
	}
}
