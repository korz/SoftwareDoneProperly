<Query Kind="Program" />

void Main()
{
	var items = new List<Item>
	{
		new Item { Id = 1 },
		new Item { Id = 2 },
		new Item { Id = 3 },
		new Item { Id = 1 },
		new Item { Id = 4 },
		new Item { Id = 5 }
	};

	items.MinBy(x => x.Id).ToList().Dump();
	items.MaxBy(x => x.Id).ToList().Dump();
}

public class Item
{
	public int Id { get; set; }
	public string Name => $"Item{this.Id}";
}

public static class Extensions
{
	public static IEnumerable<T1> MinBy<T1, T2>(this IEnumerable<T1> elements, Func<T1, T2> aggregate) where T2 : IComparable, IComparable<T2>
	{
		var minValue = aggregate(elements.ElementAt(0));
		var minElements = new List<T1>();

		foreach (var element in elements)
		{
			var currentValue = aggregate(element);
			var comparison = currentValue.CompareTo(minValue);

			if(comparison == 0)
			{
				minElements.Add(element);
			}
			else if (comparison == -1)
			{
				minValue = currentValue;
				minElements.Clear();
				minElements.Add(element);
			}
		}

		return minElements;
	}

	public static IEnumerable<T1> MaxBy<T1, T2>(this IEnumerable<T1> elements, Func<T1, T2> aggregate) where T2 : IComparable, IComparable<T2>
	{
		var maxValue = aggregate(elements.ElementAt(0));
		var maxElements = new List<T1>();

		foreach (var element in elements)
		{
			var currentValue = aggregate(element);
			var comparison = currentValue.CompareTo(maxValue);

			if (comparison == 0)
			{
				maxElements.Add(element);
			}
			else if (comparison == 1)
			{
				maxValue = currentValue;
				maxElements.Clear();
				maxElements.Add(element);
			}
		}

		return maxElements;
	}
}