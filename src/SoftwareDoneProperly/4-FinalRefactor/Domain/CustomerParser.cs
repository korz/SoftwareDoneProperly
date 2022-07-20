using System.Collections.Generic;
using Contracts.Interfaces;
using Contracts.Models;

namespace Domain
{
    public class CustomerParser : ICustomerParser
    {
        public List<Customer> Parse(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                this.Parse(customer);
            }

            return customers;
        }

        public Customer Parse(Customer customer)
        {
            customer.WorkPhone = PhoneNumberParser.Parse(customer.WorkPhone);
            customer.CellPhone = PhoneNumberParser.Parse(customer.CellPhone);
            customer.Title = customer.Title.ToUpper().Trim() == "UNKNOWN" ? "SCRUM MASTER" : customer.Title.ToUpper().Trim();

            return customer;
        }
    }
}
