using System.Data;

namespace SharedDapper
{
	public partial class DatabaseConnection : IDbConnection
    {
        public void Dispose()
        {
            this.Close();
            this._instance.Dispose();
        }

        public IDbTransaction BeginTransaction()
        {
            return this._instance.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this._instance.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            this._instance.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            if (this._instance.State != ConnectionState.Closed)
            {
                this._instance.Close();
            }
        }

        public IDbCommand CreateCommand()
        {
            return this._instance.CreateCommand();
        }

        public void Open()
        {
            if (this._instance.State != ConnectionState.Open)
            {
                this._instance.Open();
            }
        }

        public string ConnectionString
        {
            get => this._instance.ConnectionString;
            set => this._instance.ConnectionString = value;
        }

        public int ConnectionTimeout => this._instance.ConnectionTimeout;

        public string Database => this._instance.Database;

        public ConnectionState State => this._instance.State;
    }
}
