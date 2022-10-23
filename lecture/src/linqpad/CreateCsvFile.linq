<Query Kind="Program">
  <NuGetReference>ClosedXML</NuGetReference>
  <NuGetReference>CsvHelper</NuGetReference>
  <NuGetReference>Faker.Net</NuGetReference>
  <Namespace>ClosedXML.Excel</Namespace>
  <Namespace>CsvHelper</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var csvFilename = @"C:\Code\Talks\SoftwareDoneProperly\CustomersToImport.csv";
	var excelFilename = @"C:\Code\Talks\SoftwareDoneProperly\CustomersToImport.xlsx";

	var customers = Enumerable.Range(1, 10)
		.Select(x => new Customer
		{
			FirstName = Faker.Name.First(),
			LastName = Faker.Name.Last(),
			Birthdate = new DateTime(Faker.RandomNumber.Next(1950, 1990), Faker.RandomNumber.Next(1, 12), Faker.RandomNumber.Next(1, 28)),
			Company = Faker.Company.Name(),
			Title = Faker.Enum.Random<Title>().ToString(),
			WorkPhone = Faker.Phone.Number(),
			CellPhone = Faker.Phone.Number(),
			Email = Faker.Internet.Email(),
			Inactive = Faker.Boolean.Random()
		})
		.ToList();

	//customers.Dump();

	//var headers = GetPropertyNames<Customer>(
	//	x => x.FirstName,
	//	x => x.LastName,
	//	x => x.Birthdate,
	//	x => x.Company,
	//	x => x.Title,
	//	x => x.WorkPhone,
	//	x => x.CellPhone,
	//	x => x.Email,
	//	x => x.Inactive).ToList();
	//
	//headers.Dump();

	//CreateExcel(excelFilename.Replace("CustomersToImport.xlsx", "CustomersToImport-All.xlsx"), customers);
	//CreateExcel(excelFilename, customers, x => x.LastName, x => x.FirstName, x => x.Title);

	//CreateCsv(csvFilename.Replace("CustomersToImport.csv", "CustomersToImport-All.csv"), customers);
	//CreateCsv(csvFilename, customers, x => x.LastName, x => x.FirstName, x => x.Title);
	CreateCsv(csvFilename, customers);
}

public void CreateCsv(string filename, IList<Customer> customers)
{
	using(var writer = new StreamWriter(filename))
	using(var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
	{
		csvWriter.WriteRecords(customers);
	}
	return;
}

public void CreateCsv(string filename, IList<Customer> customers, params Expression<Func<Customer, object>>[] expressions)
{
	throw new NotImplementedException();
}

public void CreateExcel(string filename, IList<Customer> customers)
{
	var customerDictionaries = customers.Select(x => x.ToDictionary()).ToList();
	var propertyNames = customerDictionaries.FirstOrDefault()?.Keys.ToList();
	
	//var properties = typeof(Customer).GetProperties();

	using (var workbook = new XLWorkbook())
	{
		var worksheet = workbook.Worksheets.Add("Sheet1");

		//Write Headers
		for (var i = 0; i < propertyNames.Count(); i++)
		{
			worksheet.Cell(1, i + 1).Value = propertyNames[i];
			worksheet.Cell(1, i + 1).Style.Font.FontSize = 14;
			worksheet.Cell(1, i + 1).Style.Font.Bold = true;
		}

		//Write Rows
		var row = worksheet.RangeUsed().RowCount() + 1;

		foreach (var customerDictionary in customerDictionaries)
		{
			var column = 1;

			foreach (var propertyName in propertyNames)
			{
				worksheet.Cell(row, column).Value = customerDictionary[propertyName];
				column++;
			}

			row++;
		}

		worksheet.Columns().AdjustToContents();

		workbook.SaveAs(filename);
	}
}

public void CreateExcel(string filename, IList<Customer> customers, params Expression<Func<Customer, object>>[] expressions)
{
	using (var workbook = new XLWorkbook())
	{
		var worksheet = workbook.Worksheets.Add("Sheet1");

		//Write Headers
		for (var i = 0; i < expressions.Count(); i++)
		{
			worksheet.Cell(1, i + 1).Value = expressions[i].GetPropertyName();
			worksheet.Cell(1, i + 1).Style.Font.FontSize = 14;
			worksheet.Cell(1, i + 1).Style.Font.Bold = true;
		}

		//Write Rows
		var funcs = expressions.Select(x => x.Compile()).ToList();
		var row = worksheet.RangeUsed().RowCount() + 1;

		foreach (var customer in customers)
		{
			var column = 1;

			foreach (var func in funcs)
			{
				worksheet.Cell(row, column).Value = func(customer);
				column++;
			}

			row++;
		}

		worksheet.Columns().AdjustToContents();

		workbook.SaveAs(filename);
	}
}

public static IEnumerable<string> GetPropertyNames<T>(params Expression<Func<T, object>>[] expressions)
{
	foreach (var expression in expressions)
	{
		yield return expression.GetPropertyName<T>();
	}
}

public enum Title
{
	Unknown,
	Programmer,
	Baker,
	Engraver
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
}

public static class Extensions
{
	public static IList<string> GetPropertyNames(this Type type)
	{
		return type.GetProperties().Select(x => x.Name).ToList();
	}

	public static IEnumerable<string> GetPropertyNames<T>(this IEnumerable<Expression<Func<T, object>>> expressions)
	{
		foreach (var expression in expressions)
		{
			yield return GetPropertyName<T>(expression);
		}
	}

	public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
	{
		if (expression.Body is MemberExpression)
		{
			return (expression.Body as MemberExpression)?.Member?.Name;
		}

		else if (expression.Body is UnaryExpression)
		{
			return ((expression.Body as UnaryExpression).Operand as MemberExpression)?.Member?.Name;
		}

		return null;
	}
	
	//TODO: Replace with Expression Tree equivalent
	public static IDictionary<string, object> ToDictionary<T>(this T instance)
	{
		var dictionary = new Dictionary<string, object>();

		foreach (var property in instance.GetType().GetProperties())
			dictionary.Add(property.Name, property.GetValue(instance));
			
		return dictionary;
	}
}