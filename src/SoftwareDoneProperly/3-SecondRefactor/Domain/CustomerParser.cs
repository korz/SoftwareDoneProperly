using System.Collections.Generic;
using Contracts;

namespace Console
{
    public static class CustomerParser
    {
        public static List<Customer> ParseAll(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                var workPhoneParts = customer.WorkPhone.Split(' ');
                customer.WorkPhone = workPhoneParts[0]
                    .Replace(".", "-")
                    .Replace("(", "")
                    .Replace(")", "-");

                if (customer.WorkPhone.StartsWith("+1-"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                if (customer.WorkPhone.StartsWith("+1 -"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(4);
                }

                if (customer.WorkPhone.StartsWith("+1- "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(4);
                }

                if (customer.WorkPhone.StartsWith("+1- "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(5);
                }

                if (customer.WorkPhone.StartsWith("+1 - "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(6);
                }

                if (customer.WorkPhone.StartsWith("1-"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(2);
                }

                if (customer.WorkPhone.StartsWith("1 -"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                if (customer.WorkPhone.StartsWith("1- "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                if (customer.WorkPhone.StartsWith("1 - "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                var cellPhoneParts = customer.CellPhone.Split(' ');
                customer.CellPhone = cellPhoneParts[0]
                    .Replace(".", "-")
                    .Replace("(", "")
                    .Replace(")", "-");

                if (customer.CellPhone.StartsWith("+1-"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }
                if (customer.CellPhone.StartsWith("+1 -"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(4);
                }
                if (customer.CellPhone.StartsWith("+1- "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(4);
                }
                if (customer.CellPhone.StartsWith("+1- "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(5);
                }

                if (customer.CellPhone.StartsWith("+1 - "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(6);
                }

                if (customer.CellPhone.StartsWith("1-"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(2);
                }

                if (customer.CellPhone.StartsWith("1 -"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }

                if (customer.CellPhone.StartsWith("1- "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }

                if (customer.CellPhone.StartsWith("1 - "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }

                customer.Title = customer.Title.ToUpper().Trim() == "UNKNOWN" ? "SCRUM MASTER" : customer.Title.ToUpper().Trim();
            }

            return customers;
        }
    }
}
