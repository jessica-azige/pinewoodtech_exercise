using Common.Schema;
using MudBlazor;
using System.Diagnostics;
using System.Net.Http.Json;

namespace UI.Service
{
    public class CustomerService
    {
        private HttpClient _httpClient;
        private ISnackbar _snackbar;
        public CustomerService(HttpClient client, ISnackbar snackbar) 
        { 
            _httpClient = client;
            _snackbar = snackbar;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>("api/customer") ?? new HashSet<Customer>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error attempting to get customer: " + ex.Message);
                _snackbar.Add("Unable to return customers. Please contact administrator for more details", Severity.Error);
            }

            return new HashSet<Customer>();
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<Customer>("api/customer", customer);

                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add(customer.FullName() + " has been created successfully!", Severity.Success);
                    return true;
                }
                
                _snackbar.Add("Unable to create customer: " + response.Content, Severity.Error);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                _snackbar.Add("Unable to create customer. Please contact administrator for more details", Severity.Error);
            }
            return false;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                var response =  await _httpClient.PutAsJsonAsync<Customer>("api/customer/" + customer.Id, customer);

                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add(customer.FullName() + " has been updated successfully!", Severity.Success);
                    return true;
                }

                _snackbar.Add("Unable to update customer: " + response.Content, Severity.Error);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                _snackbar.Add("Unable to update customer. please contact administrator for more details ", Severity.Error);
            }
            return false;
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("api/customer/" + customer.Id);

                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add(customer.FullName() + " has been deleted successfully!", Severity.Success);
                    return true;
                }

                _snackbar.Add("Unable to delete customer: " + response.Content, Severity.Error);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                _snackbar.Add("Unable to delete customer. please contact administrator for more details ", Severity.Error);
            }

            return false;
        }
    }
}
