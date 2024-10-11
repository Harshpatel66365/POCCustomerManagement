using Microsoft.EntityFrameworkCore;
using POCCustomerManagement.Data;
using POCCustomerManagement.Entity;

namespace POCCustomerManagement.Interface
{
	public interface ICustomerService
	{
		Task<List<CustomerEntity>> GetCustomersAsync();
		Task<CustomerEntity> GetCustomerByIdAsync(int id);
		Task AddCustomerAsync(CustomerEntity customer);
		Task UpdateCustomerAsync(CustomerEntity customer);
		Task DeleteCustomerAsync(int id);
	}
	public class CustomerService : ICustomerService
	{
		private readonly ApplicationDbContext _context;

		public CustomerService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<CustomerEntity>> GetCustomersAsync()
		{
			return await _context.Customers.ToListAsync();
		}

		public async Task<CustomerEntity> GetCustomerByIdAsync(int id)
		{
			return await _context.Customers.FindAsync(id);
		}

        public async Task AddCustomerAsync(CustomerEntity customer)
        {
            customer.CreatedDate = DateTime.UtcNow;  // Set CreatedDate to current time
            customer.DataVersion = 1;  // Initialize DataVersion as 1 for new entries
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(CustomerEntity customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer != null)
            {
                _context.Entry(existingCustomer).State = EntityState.Detached;
				customer.CreatedDate = existingCustomer.CreatedDate;
                customer.DataVersion = existingCustomer.DataVersion + 1;  // Increment DataVersion
                customer.ModifiedDate = DateTime.UtcNow;  // Set ModifiedDate to current time

                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCustomerAsync(int id)
		{
			var customer = await _context.Customers.FindAsync(id);
			if (customer != null)
			{
				_context.Customers.Remove(customer);
				await _context.SaveChangesAsync();
			}
		}
	}
}
