using System;

namespace SoftwareDoneProperly.FirstRefactor.Final.Console
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public bool Inactive { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName}, {this.LastName}, {this.Birthdate}, {this.Company}, {this.Title}, {this.WorkPhone}, {this.CellPhone}, {this.Email}, {this.Inactive}";
        }
    }
}
