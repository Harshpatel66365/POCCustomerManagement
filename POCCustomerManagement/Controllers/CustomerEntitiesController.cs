using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POCCustomerManagement.Entity;
using POCCustomerManagement.Interface;
using System.Threading.Tasks;

namespace POCCustomerManagement.Controllers
{
	[Authorize(AuthenticationSchemes = "Cookies")]
	public class CustomerEntitiesController : Controller
	{
		private readonly ICustomerService _customerRepository;

		public CustomerEntitiesController(ICustomerService customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task<IActionResult> Index()
		{
			List<CustomerEntity> customers = new List<CustomerEntity>();
			try
			{
				customers = await _customerRepository.GetCustomersAsync(); // Fetch the customers
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
			}
			return View(customers);
		}

		// GET: CustomerEntities/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			CustomerEntity customerEntity = new CustomerEntity();
			try
			{
				if (id == null)
				{
					return NotFound();
				}

				customerEntity = await _customerRepository.GetCustomerByIdAsync(id.Value);
				if (customerEntity == null)
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
			}
			return View(customerEntity);
		}

		// GET: CustomerEntities/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: CustomerEntities/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone,CreatedDate,ModifiedDate,DataVersion")] CustomerEntity customerEntity)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await _customerRepository.AddCustomerAsync(customerEntity);
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
			}
			return View(customerEntity);
		}

		// GET: CustomerEntities/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			CustomerEntity customerEntity = new CustomerEntity();
			try
			{
				if (id == null)
				{
					return NotFound();
				}
				customerEntity = await _customerRepository.GetCustomerByIdAsync(id.Value);
				if (customerEntity == null)
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
			}
			return View(customerEntity);
		}

		// POST: CustomerEntities/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone,CreatedDate,ModifiedDate,DataVersion")] CustomerEntity customerEntity)
		{
			try
			{
				if (id != customerEntity.Id)
				{
					return NotFound();
				}

				if (ModelState.IsValid)
				{
					await _customerRepository.UpdateCustomerAsync(customerEntity);
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
			}
			return View(customerEntity);
		}

		// GET: CustomerEntities/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			CustomerEntity customerEntity = new CustomerEntity();
			try
			{
				if (id == null)
				{
					return NotFound();
				}

				customerEntity = await _customerRepository.GetCustomerByIdAsync(id.Value);
				if (customerEntity == null)
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
			}
			return View(customerEntity);
		}

		// POST: CustomerEntities/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			try
			{
				await _customerRepository.DeleteCustomerAsync(id);
			}
			catch (Exception ex)
			{
				TempData["strMessage"] = ex.Message;
				TempData["strMessageCode"] = "0";
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
