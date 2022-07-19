using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Dapper;
using SharedDapper;

namespace Infrastructure
{
    public static class DatabaseRepository
    {
        public static async Task TruncateCustomers()
        {
            await DatabaseConnection.ExecuteAsync(@"TRUNCATE TABLE [Customer]");
        }

        public static async Task Insert(IList<Customer> customers)
        {
            using (var connection = new DatabaseConnection())
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

                    var customerId = await connection.QuerySingleAsync<int>(sql, parameters);
                }
            }
        }

        public static async Task<IList<Customer>> FetchAll()
        {
            return await DatabaseConnection.QueryAsync<Customer>(@"SELECT * FROM Customer");

            //which would you rather write?

            //var savedCustomers = new List<Customer>();

            //using (var connection = new SqlConnection(Settings.ConnectionString))
            //{
            //    var sql = "SELECT * FROM Customer";

            //    if (connection.State != ConnectionState.Open)
            //    {
            //        connection.Open();
            //    }

            //    savedCustomers = connection.Query<Customer>(sql).ToList();

            //    connection.Close();
            //}

            //return savedCustomers;
        }
    }
}
