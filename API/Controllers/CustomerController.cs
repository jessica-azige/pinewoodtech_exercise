using API.Database;
using API.Database.Service;
using Common.Schema;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API.Controllers
{
    [Route("api/customer/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerService _customerService;
        private MainDbContext _mainDbContext;
        public CustomerController(CustomerService customerService, MainDbContext dbContext) 
        { 
            _customerService = customerService;
            _mainDbContext = dbContext;
        }

        // NOTE: for more complex projects/requirements, I'd probably prefer using a custom error handling middleware
        // than the constant try/catch, but would do a similar job

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            try
            {
                var customers = await _customerService.GetCustomers(_mainDbContext);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(500, "Unable to retrieve customers due to an internal error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            var validationString = customer.IsValid();

            if (validationString != null)
                return StatusCode(400, validationString);

            try
            {
                var response = await _customerService.CreateCustomer(_mainDbContext, customer);

                if (response != null)
                    return StatusCode((int)response.StatusCode, response.ErrorDetail);

                return Created();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(500, "Unable to create customer due to an internal error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Put(int id, [FromBody] Customer customer)
        {
            var validationString = customer.IsValid();

            if (validationString != null)
                return StatusCode(400, validationString);

            try
            {
                var response = await _customerService.UpdateCustomer(_mainDbContext, customer);

                if (response != null)
                    return StatusCode((int)response.StatusCode, response.ErrorDetail);

                return Ok();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(500, "Unable to update customer due to an internal error");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            try
            {
                var response = await _customerService.DeleteCustomer(_mainDbContext, id);

                if (response != null)
                    return StatusCode((int)response.StatusCode, response.ErrorDetail);

                return Ok();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(500, "Unable to delete customer due to an internal error");
            }
            
        }
    }
}
