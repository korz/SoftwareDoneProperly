using System.Threading.Tasks;
using Contracts;
using Infrastructure;
using SharedCsv;

namespace Domain
{
    public static class Processor
    {
        public static async Task Process()
        {
            //Notice how we separated orchestration from logic.
            //It is orchestration because it isn't doing anything other than facilitating calls to other methods

            await DatabaseRepository.TruncateCustomers();

            var customers = CsvSerializer.Read<Customer>(Settings.CsvFilename);
            customers = CustomerParser.Parse(customers);

            await DatabaseRepository.Insert(customers);

            var savedCustomers = await DatabaseRepository.FetchAll();
            savedCustomers.ForEach(x => System.Console.WriteLine(x.ToString()));

            //If this app had logging, and more tests I would completely feel good releasing into production
        }
    }
}
