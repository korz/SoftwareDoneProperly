using System.Threading.Tasks;
using Contracts;
using SharedDapper;
using Domain;

namespace Console
{
    class Program
    {
        static Program()
        {
            Settings.CsvFilename = @"CustomersToImport.csv";
            Settings.CleanCsvFilename = @"CustomersToImport-Clean.csv";
            Settings.ConnectionString = @"Server=.\SQLEXPRESS;Database=SoftwareDoneProperly;Trusted_Connection=True;";
            DatabaseConnection.StaticConnectionString = Settings.ConnectionString;
        }

        static async Task Main(string[] args)
        {
            await Processor.Process();
        }
    }
}
