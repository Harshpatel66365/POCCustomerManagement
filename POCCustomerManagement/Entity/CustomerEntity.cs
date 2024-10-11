using System.ComponentModel.DataAnnotations;

namespace POCCustomerManagement.Entity
{
	public class CustomerEntity
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public int? DataVersion { get; set; }
	}
}
