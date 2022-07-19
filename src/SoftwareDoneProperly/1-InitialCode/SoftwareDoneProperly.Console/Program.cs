using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Dapper;

namespace SoftwareDoneProperly.Console
{
    class Program
    {
        static Program()
        {
            //Buried Intent: Delete all rows of the customer table
            using (var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=SoftwareDoneProperly;Trusted_Connection=True;"))
            {
                var sql = @"TRUNCATE TABLE [Customer]";

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                connection.Execute(sql);

                connection.Close();
            }
        }

        static void Main(string[] args)//179-line method
        {
            var customers = new List<Customer>();
            var loadedCustomers = new List<Customer>();

            using (var reader = new StreamReader(@"CustomersToImport.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                customers = csv.GetRecords<Customer>().ToList();
            }

            foreach (var customer in customers)
            {
                var workPhoneParts = customer.WorkPhone.Split(' ');
                customer.WorkPhone = workPhoneParts[0]
                    .Replace(".", "-")
                    .Replace("(", "")
                    .Replace(")", "-");

                if (customer.WorkPhone.StartsWith("+1-"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                if (customer.WorkPhone.StartsWith("+1 -"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(4);
                }

                if (customer.WorkPhone.StartsWith("+1- "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(4);
                }

                if (customer.WorkPhone.StartsWith("+1- "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(5);
                }

                if (customer.WorkPhone.StartsWith("+1 - "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(6);
                }

                if (customer.WorkPhone.StartsWith("1-"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(2);
                }

                if (customer.WorkPhone.StartsWith("1 -"))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                if (customer.WorkPhone.StartsWith("1- "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                if (customer.WorkPhone.StartsWith("1 - "))
                {
                    customer.WorkPhone = customer.WorkPhone.Substring(3);
                }

                var cellPhoneParts = customer.CellPhone.Split(' ');
                customer.CellPhone = cellPhoneParts[0]
                    .Replace(".", "-")
                    .Replace("(", "")
                    .Replace(")", "-");

                if (customer.CellPhone.StartsWith("+1-"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }
                if (customer.CellPhone.StartsWith("+1 -"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(4);
                }
                if (customer.CellPhone.StartsWith("+1- "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(4);
                }
                if (customer.CellPhone.StartsWith("+1- "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(5);
                }

                if (customer.CellPhone.StartsWith("+1 - "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(6);
                }

                if (customer.CellPhone.StartsWith("1-"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(2);
                }

                if (customer.CellPhone.StartsWith("1 -"))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }

                if (customer.CellPhone.StartsWith("1- "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }

                if (customer.CellPhone.StartsWith("1 - "))
                {
                    customer.CellPhone = customer.CellPhone.Substring(3);
                }

                customer.Title = customer.Title.ToUpper().Trim() == "UNKNOWN" ? "SCRUM MASTER" : customer.Title.ToUpper().Trim();
            }

            using (var writer = new StreamWriter(@"CustomersToImport-Clean.csv"))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(customers);
            }

            using (var reader = new StreamReader(@"CustomersToImport-Clean.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                loadedCustomers = csv.GetRecords<Customer>().ToList();
            }

            var connectionString = @"Server=.\SQLEXPRESS;Database=SoftwareDoneProperly;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                foreach (var customer in loadedCustomers)
                {
                    var sql = @"INSERT INTO Customer(FirstName, LastName, Birthdate, Company, Title, WorkPhone, CellPhone, Email, Inactive)
                                VALUES(@FirstName, @LastName, @Birthdate, @Company, @Title, @WorkPhone, @CellPhone, @Email,@Inactive)
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                    var parameters = new
                    {
                        customer.FirstName,
                        customer.LastName,
                        customer.Birthdate,
                        customer.Company,
                        customer.Title,
                        customer.WorkPhone,
                        customer.CellPhone,
                        customer.Email,
                        customer.Inactive
                    };

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    var customerId = connection.QuerySingle<int>(sql, parameters);

                    connection.Close();
                }
            }

            var savedCustomers = new List<Customer>();

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM Customer";

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                savedCustomers = connection.Query<Customer>(sql).ToList();

                connection.Close();
            }

            savedCustomers.ForEach(System.Console.WriteLine);
        }

        public class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthdate { get; set; }
            public string Company { get; set; }
            public string Title { get; set; }
            public string WorkPhone { get; set; }
            public string CellPhone { get; set; }
            public string Email { get; set; }
            public bool Inactive { get; set; }

            public override string ToString()
            {
                return $"{this.FirstName}, {this.LastName}, {this.Birthdate}, {this.Company}, {this.Title}, {this.WorkPhone}, {this.CellPhone}, {this.Email}, {this.Inactive}";
                //return $"{this.WorkPhone},      {this.CellPhone}";
            }
        }
    }
}
