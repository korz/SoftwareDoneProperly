using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace SharedCsv
{
    public static class CsvSerializer
    {
        public static List<T> Read<T>(string filename)
        {
            List<T> records;

            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<T>().ToList();
            }

            return records;
        }

        public static void Write<T>(string filename, IList<T> records)
        {
            using (var writer = new StreamWriter(filename))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(records);
            }
        }
    }
}
