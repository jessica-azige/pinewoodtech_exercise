
using Common.Helpers;
using Common.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Validation;
using System.Diagnostics;
using static MudBlazor.Icons;

namespace API.Database.Service
{
    public class CustomerService 
    {
        private bool _inUse;
        public CustomerService()
        {
            //no-op
        }    

        public async Task<IEnumerable<Customer>> GetCustomers(MainDbContext dbContext) 
        {
            return await dbContext.Customers.ToListAsync();
        }

        public async Task<APIValidation?> UpdateCustomer(MainDbContext dbContext, Customer customer)
        {
            try
            {
                var prevCustomer = await dbContext.Customers.FirstOrDefaultAsync(o => o.Id == customer.Id);
                if ( prevCustomer == null)
                    return new APIValidation(System.Net.HttpStatusCode.BadRequest, "No existing customer with found with ID: " + customer.Id);

                prevCustomer.Update(customer);
                //dbContext.Log update the logs in the same txn, that way if there is a fail, everything is rolled back together
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // need to tell the user what validation was triggered
                await dbContext.DisposeAsync();
                Console.WriteLine(ex);
                return new APIValidation(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                // something else went wrong
                await dbContext.DisposeAsync();
                Console.WriteLine(ex);
                return new APIValidation(System.Net.HttpStatusCode.BadRequest, "Unable to save changes. Please contact administrator");
            }
            return null;
        }

        public async Task<APIValidation?> CreateCustomer(MainDbContext dbContext, Customer customer)
        {
            try
            {
                var prevCustomer = await dbContext.Customers.FirstOrDefaultAsync(o => o.Id == customer.Id);
                if (prevCustomer != null)
                    return new APIValidation(System.Net.HttpStatusCode.BadRequest, "Existing customer with found with ID: " + customer.Id);

                dbContext.Customers.Add(customer);
                //dbContext.Log update the logs in the same txn, that way if there is a fail, everything is rolled back together
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // need to tell the user what validation was triggered
                await dbContext.DisposeAsync();
                Console.WriteLine(ex);
                return new APIValidation(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                // something else went wrong
                await dbContext.DisposeAsync();
                Console.WriteLine(ex);
                return new APIValidation(System.Net.HttpStatusCode.BadRequest, "Unable to save changes. Please contact administrator");
            }
            return null;
        }

        public async Task<APIValidation?> DeleteCustomer(MainDbContext dbContext, long id)
        {
            try
            {
                var prevCustomer = await dbContext.Customers.FirstOrDefaultAsync(o => o.Id == id);
                if (prevCustomer == null)
                    return new APIValidation(System.Net.HttpStatusCode.BadRequest, "No existing customer with found with ID: " + id);

                dbContext.Customers.Remove(prevCustomer);
               dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // something else went wrong
                await dbContext.DisposeAsync();
                Console.WriteLine(ex);
                return new APIValidation(System.Net.HttpStatusCode.BadRequest, "Unable to save changes. Please contact administrator");
            }

            return null;
        }

    }
}
