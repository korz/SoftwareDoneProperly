using System.Threading.Tasks;
using SharedDapper;
using Console.Composition;
using Contracts.Models;
using Contracts.Interfaces;

namespace Console
{
    class Program
    {
        public static IProcessor Processor;

        static Program()
        {
            Settings.CsvFilename = @"CustomersToImport.csv";
            Settings.CleanCsvFilename = @"CustomersToImport-Clean.csv";
            Settings.ConnectionString = @"Server=.\SQLEXPRESS;Database=SoftwareDoneProperly;Trusted_Connection=True;";
            DatabaseConnection.StaticConnectionString = Settings.ConnectionString;

            Processor = CompositionRoot.Get<IProcessor>();
        }

        static async Task Main(string[] args)
        {
            await Processor.Process();
        }
    }
}
