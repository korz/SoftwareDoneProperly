using Contracts.Models;
using System.Collections.Generic;

namespace Contracts.Interfaces
{
    public interface ICustomerParser
    {
        Customer Parse(Customer customer);
        List<Customer> Parse(List<Customer> customers);
    }
}
