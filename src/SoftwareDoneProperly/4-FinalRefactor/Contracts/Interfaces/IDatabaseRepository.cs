using Contracts.Models;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IDatabaseRepository
    {
        Task<System.Collections.Generic.IList<Customer>> FetchAll();
        Task Insert(System.Collections.Generic.IList<Customer> customers);
        Task TruncateCustomers();
    }
}
