using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Common.Schema
{
    public class Customer
    {
        public long Id { get; set; } 
        public string Title { get; set; } 
        public string FirstName { get; set; } 
        public string? MiddleName { get; set; } 
        public string LastName { get; set; } 
        public DateTime DateOfBirth { get; set; }  

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public DateTime CreatedDttm  { get; set; } 

        public string FullName()
        {
            return FirstName + " " +  (MiddleName == null ? "" : MiddleName + " ") + LastName;
        }

        public void Update(Customer that)
        {
            this.Title = that.Title;
            this.FirstName = that.FirstName;
            this.LastName = that.LastName;
            this.MiddleName = that.MiddleName;

            this.Email = that.Email;
            this.PhoneNumber = that.PhoneNumber;
        }

        public string? IsValid()
        {
            if (CreatedDttm.Equals(DateTime.MinValue))
                CreatedDttm = DateTime.Now;

            if (FirstName == null)
                return "First name cannot be empty";
                    
            if (LastName == null)
                return "Last name cannot be empty";

            if (Title == null)
                return "Title cannot be empty";

            if (DateOfBirth.Equals(DateTime.MinValue))
                return "Date of Birth cannot be empty";

            // very basic for this example
            if (Email != null && !Email.Contains("@")) 
                return "Invalid format for email: " + Email;

            // int's max value is too small to use here
            if (PhoneNumber != null && (PhoneNumber.Length != 11 || !long.TryParse(PhoneNumber.Substring(1), out long result)))
                return "Invalid format for phone number: " + PhoneNumber;

            return null;
        }
    }
}
