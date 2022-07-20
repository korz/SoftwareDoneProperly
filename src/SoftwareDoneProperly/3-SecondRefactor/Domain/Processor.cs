using System.Collections.Generic;
using Console;
using Contracts;
using Infrastructure;

namespace Domain
{
    public static class Processor
    {
        public static void Process()
        {
            DatabaseRepository.TruncateCustomers();

            //Notice how we separated orchestration from logic

            var customers = new List<Customer>();
            var loadedCustomers = new List<Customer>();

            customers = CsvSerializer.Read();
            customers = CustomerParser.ParseAll(customers);

            CsvSerializer.Write(customers);

            loadedCustomers = CsvSerializer.ReadClean();

            DatabaseRepository.Insert(loadedCustomers);

            var savedCustomers = DatabaseRepository.FetchAll();
            savedCustomers.ForEach(System.Console.WriteLine);
        }
    }
}
