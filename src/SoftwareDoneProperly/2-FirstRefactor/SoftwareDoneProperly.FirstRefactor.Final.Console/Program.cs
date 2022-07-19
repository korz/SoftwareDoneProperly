using System.Collections.Generic;

namespace SoftwareDoneProperly.FirstRefactor.Final.Console
{
    class Program
    {
        static Program()
        {
            DatabaseRepository.TruncateCustomers();
        }

        static void Main(string[] args)
        {
            //Notice how we sepated orchestration from logic

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
