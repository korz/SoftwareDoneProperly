using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace SoftwareDoneProperly.FirstRefactor.Final.Console
{
    public static class DatabaseRepository
    {
        public static void TruncateCustomers()
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
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

        public static void Insert(IList<Customer> customers)
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                foreach (var customer in customers)
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
        }

        public static List<Customer> FetchAll()
        {
            var savedCustomers = new List<Customer>();

            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                var sql = "SELECT * FROM Customer";

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                savedCustomers = connection.Query<Customer>(sql).ToList();

                connection.Close();
            }

            return savedCustomers;
        }
    }
}
