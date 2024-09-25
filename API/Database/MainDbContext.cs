using Common.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace API.Database
{
    public class MainDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
   
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            //no-op
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(GetCustomers());
        }

        private Customer[] GetCustomers()
        {
            return
            [
                new Customer {
                    Id = 1,  
                    Title = "Mr",
                    FirstName = "Joseph", 
                    LastName = "Joestar", 
                    CreatedDttm = DateTime.Now, 
                    DateOfBirth = new DateTime(1970, 9, 27) 
                },
                new Customer { 
                    Id = 2,
                    Title = "Miss", 
                    FirstName = "Samus", 
                    LastName = "Aran", 
                    CreatedDttm = DateTime.Now,
                    DateOfBirth = new DateTime(1989, 3, 15) 
                }
            ];
        }
    }
}
