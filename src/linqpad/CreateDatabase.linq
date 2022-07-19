<Query Kind="Program">
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Faker.Net</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	DatabaseConnection.StaticConnectionString = Settings.ConnectionString;
	
	await CreateDatabaseIfNotExists();
	await DropTablesIfExists();
	await CreateTablesIfNotExists();
}

public static class Settings
{
	public static string ConnectionString = @"Server=.\SQLEXPRESS;Database=SoftwareDoneProperly;Trusted_Connection=True;";
}

public static async Task CreateDatabaseIfNotExists(string databaseName = null)
{
	//If no database name is passed in then it is retreived from the connection string
	
	var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(Settings.ConnectionString);

	if (databaseName == null)
	{
		databaseName = sqlConnectionStringBuilder.InitialCatalog.Dump("Database Name");
	}

	// Change name to master to check.
	sqlConnectionStringBuilder.InitialCatalog = "master";

	using (var connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
	{
		connection.Open();
		var result = await connection.ExecuteScalarAsync<int?>(@"SELECT DB_ID(@databaseName);", new { databaseName }).Dump("DB_ID");

		if (!result.HasValue)
		{
			await connection.ExecuteAsync($@"CREATE DATABASE {databaseName}");
		}

		if (connection.State != ConnectionState.Closed)
		{
			connection.Close();
		}
	}
}

public static async Task DropTablesIfExists()
{
	await DatabaseConnection.ExecuteAsync(@"DROP TABLE IF EXISTS [SoftwareDoneProperly].[dbo].[Customer]");
}

public static async Task CreateTablesIfNotExists()
{
	var sql = @"
		USE [SoftwareDoneProperly]
		--GO

		/****** Object:  Table [dbo].[Customer]    Script Date: 7/7/2022 11:35:29 PM ******/
		SET ANSI_NULLS ON
		--GO

		SET QUOTED_IDENTIFIER ON
		--GO

		CREATE TABLE [dbo].[Customer](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[FirstName] [nvarchar](1000) NOT NULL,
			[LastName] [nvarchar](1000) NOT NULL,
			[Birthdate] [datetime] NOT NULL,
			[Company] [nvarchar](1000) NULL,
			[Title] [nvarchar](1000) NULL,
			[WorkPhone] [nvarchar](100) NULL,
			[CellPhone] [nvarchar](100) NULL,
			[Email] [nvarchar](400) NOT NULL,
			[Inactive] [bit] NOT NULL,
		 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]
		--GO

		ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_Inactive]  DEFAULT ((0)) FOR [Inactive]
		--GO";

	await DatabaseConnection.ExecuteAsync(sql);
}

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
}

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

	public IDbTransaction BeginTransaction(System.Data.IsolationLevel il)
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
