<Query Kind="Program">
  <NuGetReference>Faker.Net</NuGetReference>
</Query>

//Faker.Net

void Main()
{
	//(CustomerGenerator.GenerateCustomers(0).GetType()).Dump();

	//var customers = CustomerGenerator.GenerateCustomers(0).ToList();
	//(customers is IEnumerable<Customer>).Dump();
	//customers.Count().Dump();
	//"".Dump();

	var customers = CustomerGenerator.GenerateCustomers(10).ToList();
	//(customers is IList<Customer>).Dump();
	//(customers is IEnumerable<Customer>).Dump();
	//customers.Count().Dump();

	customers[0].Dump();
	customers.Dump();
}

#region Contracts
public class Customer
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string FullName => $"{this.FirstName.Trim()} {this.LastName.Trim()}";
	public string CellPhone { get; set; }
	public string Email { get; set; }
	public IList<Address> Addresses { get; set; }

	public Customer()
	{
		this.Addresses = new List<Address>();
	}
}

public class Address
{
	public AddressType AddressType { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Zip { get; set; }
}

public enum AddressType
{
	Work,
	Home,
	SecondHome,
	Emergency,
	Other
}
#endregion

#region Generator
public static class CustomerGenerator
{
	public static Customer GenerateCustomer()
	{
		var addressCount = Faker.RandomNumber.Next(1, 5);

		var customer = new Customer
		{
			FirstName = Faker.Name.First(),
			LastName = Faker.Name.Last(),
			CellPhone = Faker.Phone.Number(),
			Email = Faker.Internet.Email(),
		};

		for (var i = 0; i < addressCount; i++)
		{
			customer.Addresses.Add(new Address
			{
				AddressType = (AddressType)i,
				Street = Faker.Address.StreetAddress(false),
				City = Faker.Address.City(),
				State = Faker.Address.UsStateAbbr(),
				Zip = Faker.Address.ZipCode()
			});
		}

		return customer;
	}

	public static IEnumerable<Customer> GenerateCustomers(int count = 10)
	{
		if (count < 1)
		{
			yield break; //return empty IEnumerable<Customer> instance
		}

		for (var i = 0; i < count; i++)
		{
			yield return CustomerGenerator.GenerateCustomer(); //return IEnumerable<Customer> instance with customers added
		}
	}

	public static IEnumerable<Customer> GenerateCustomers_Equivalent(int count = 10)
	{
		if (count < 1)
		{
			return Enumerable.Empty<Customer>(); //return empty Customer[] array
		}

		var customers = new List<Customer>();

		for (var i = 0; i < count; i++)
		{
			customers.Add(CustomerGenerator.GenerateCustomer());
		}

		return customers;
	}
}
#endregion
