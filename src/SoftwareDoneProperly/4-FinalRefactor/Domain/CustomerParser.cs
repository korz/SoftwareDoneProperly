using System.Collections.Generic;
using Contracts;

namespace Domain
{
    public static class CustomerParser
    {
        public static List<Customer> Parse(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                CustomerParser.Parse(customer);
            }

            return customers;
        }

        public static Customer Parse(Customer customer)
        {
            customer.WorkPhone = PhoneNumberParser.Parse(customer.WorkPhone);
            customer.CellPhone = PhoneNumberParser.Parse(customer.CellPhone);
            customer.Title = customer.Title.ToUpper().Trim() == "UNKNOWN" ? "SCRUM MASTER" : customer.Title.ToUpper().Trim();

            return customer;
        }
    }
}
