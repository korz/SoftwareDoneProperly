using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace SoftwareDoneProperly.FirstRefactor.Final.Console
{
    public static class CsvSerializer
    {
        public static List<Customer> Read()
        {
            var customers = new List<Customer>();

            using (var reader = new StreamReader(Settings.CsvFilename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                customers = csv.GetRecords<Customer>().ToList();
            }

            return customers;
        }

        public static void Write(IList<Customer> customers)
        {
            using (var writer = new StreamWriter(Settings.CleanCsvFilename))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(customers);
            }
        }

        public static List<Customer> ReadClean()
        {
            var customers = new List<Customer>();

            using (var reader = new StreamReader(Settings.CleanCsvFilename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                customers = csv.GetRecords<Customer>().ToList();
            }

            return customers;
        }
    }
}
