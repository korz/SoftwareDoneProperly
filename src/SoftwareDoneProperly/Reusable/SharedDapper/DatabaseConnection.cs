using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SharedDapper
{
    public partial class DatabaseConnection : IDbConnection
    {
        private readonly IDbConnection _instance;
        public static string StaticConnectionString { get; set; }

        public DatabaseConnection() : this(StaticConnectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">if null uses the connection string from the Settings</param>
        public DatabaseConnection(string connectionString = null)
        {
            this._instance = new SqlConnection(connectionString);
            this.Open();
        }

        //TODO: Implement all of the other missing methods
        public static void Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var connection = new DatabaseConnection(StaticConnectionString))
            {
                Dapper.SqlMapper.Execute(connection, sql, param, transaction, commandTimeout, commandType);
            }
        }

        public static async Task ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var connection = new DatabaseConnection(StaticConnectionString))
            {
                await Dapper.SqlMapper.ExecuteAsync(connection, sql, param, transaction, commandTimeout, commandType);
            }
        }

        public static IList<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var connection = new DatabaseConnection(StaticConnectionString))
            {
                return Dapper.SqlMapper.Query<T>(connection, sql, param, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }

        public static async Task<IList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var connection = new DatabaseConnection(StaticConnectionString))
            {
                var results = await Dapper.SqlMapper.QueryAsync<T>(connection, sql, param, transaction, commandTimeout, commandType);
                return results.ToList();
            }
        }
    }
}
