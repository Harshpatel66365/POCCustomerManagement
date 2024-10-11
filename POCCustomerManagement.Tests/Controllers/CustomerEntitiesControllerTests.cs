using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using POCCustomerManagement.Controllers;
using POCCustomerManagement.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using POCCustomerManagement.Interface;

public class CustomerEntitiesControllerTests
{
	private readonly Mock<ICustomerService> _mockCustomerService;

	public CustomerEntitiesControllerTests()
	{
		_mockCustomerService = new Mock<ICustomerService>();
	}

	[Fact]
	public async Task Index_ReturnsViewWithCustomers()
	{
		// Arrange
		var customers = new List<CustomerEntity>
		{
			new CustomerEntity { Id = 1, FirstName = "John", LastName = "Doe", CreatedDate = DateTime.UtcNow, DataVersion = 1, Email = "test@gmail.com", Phone = "7984304065" },
			new CustomerEntity { Id = 2, FirstName = "Jane", LastName = "Smith", CreatedDate = DateTime.UtcNow, DataVersion = 1, Email = "test1@gmail.com", Phone = "7984304064" }
		};

		// Set up the mock to return the customers
		_mockCustomerService.Setup(service => service.GetCustomersAsync()).ReturnsAsync(customers);

		var controller = new CustomerEntitiesController(_mockCustomerService.Object);

		// Act
		var result = await controller.Index();

		// Assert
		var viewResult = Assert.IsType<ViewResult>(result);
		var model = Assert.IsAssignableFrom<List<CustomerEntity>>(viewResult.ViewData.Model);
		Assert.Equal(2, model.Count);
	}

	// Additional test cases can be added here...
}
