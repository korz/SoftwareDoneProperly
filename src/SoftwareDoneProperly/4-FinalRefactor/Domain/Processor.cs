using System.Threading.Tasks;
using Contracts.Interfaces;
using Contracts.Models;
using Infrastructure;
using SharedCsv;

namespace Domain
{
    public class Processor : IProcessor
    {
        private readonly ICustomerParser CustomerParser;
        private readonly IDatabaseRepository DatabaseRepository;

        public Processor(ICustomerParser customerParser, IDatabaseRepository databaseRepository)
        {
            this.CustomerParser = customerParser;
            this.DatabaseRepository = databaseRepository;
        }

        public async Task Process()
        {
            //Notice how we separated orchestration from logic.
            //It is orchestration because it isn't doing anything other than facilitating calls to other methods

            await this.DatabaseRepository.TruncateCustomers();

            var customers = CsvSerializer.Read<Customer>(Settings.CsvFilename);
            customers = this.CustomerParser.Parse(customers);

            await this.DatabaseRepository.Insert(customers);

            var savedCustomers = await this.DatabaseRepository.FetchAll();
            savedCustomers.ForEach(x => System.Console.WriteLine(x.ToString()));

            //If this app had logging, and more tests I would completely feel good releasing into production
        }
    }
}
