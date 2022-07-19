<Query Kind="Program" />

//Even Fibonacci Numbers (https://projecteuler.net/problem=2)

//Each new term in the Fibonacci sequence is generated by adding the previous two terms.
//	By starting with 1 and 2, the first 10 terms will be: 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
//	By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.

//TL;DR: Sum the Even Fibonacci numbers less than 4 MILLION
void Main()
{
	var maxValue = 4_000_000;
	var skipInitial = true; //skip the 0 and 1 from the start of the sequence since the actual Fibonacci sequence is 0, 1, 1, 2, 3, 5, ...
	var expectedResult = skipInitial ?
		new List<int> { 2, 8, 34, 144, 610, 2584, 10946, 46368, 196418, 832040, 3524578 } :
		new List<int> { 0, 2, 8, 34, 144, 610, 2584, 10946, 46368, 196418, 832040, 3524578 };
	//expectedResult.Dump();

	Func<IList<int>, bool> evaluation = result => Enumerable.SequenceEqual(expectedResult, result);

	var runResults = new List<RunResult<IList<int>>>
	{
		Runner.Run("WhileLoop", () => WhileLoop(maxValue, skipInitial), evaluation),
		Runner.Run("Recursive", () => Recursive(maxValue, skipInitial), evaluation),
		Runner.Run("LinkedList", () => LinkedList(maxValue, skipInitial), evaluation)
	};

	//Can this be done using only Linq???

	runResults.Select(x => new { x.Label, x.Evaluation, x.ElapsedMilliseconds }).Dump();

	var bestOnes = runResults.MinBy(x => x.ElapsedMilliseconds).ToList();
	var worstOnes = runResults.Except(bestOnes).MaxBy(x => x.ElapsedMilliseconds).ToList();

	bestOnes.ToBestOnesLabel().Dump();
	"".Dump();
	worstOnes.ToWorstOnesLabel().Dump();
}

public class RunResult<T>
{
	public string Label { get; set; }
	public T Result { get; set; }
	public bool Evaluation { get; set; }
	public long ElapsedMilliseconds { get; set; }
}

public static class Runner
{
	public static RunResult<T> Run<T>(Func<T> function, Func<T, bool> evaluation)
	{
		return Runner.Run(function, evaluation);
	}

	public static RunResult<T> Run<T>(string label, Func<T> function, Func<T, bool> evaluation)
	{
		var stopwatch = new Stopwatch();

		stopwatch.Start();

		var result = function();
		var isValid = evaluation(result);

		stopwatch.Stop();

		return new RunResult<T>
		{
			Label = label,
			Result = result,
			Evaluation = isValid,
			ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
		};
	}
}

public static IList<int> WhileLoop(int maxValue, bool skipInitial = false)
{
	var values = new List<int>
	{
		0,
		1
	};

	var previousIndex = 0;
	var currentIndex = 1;
	var currentValue = 0;

	while (currentValue <= maxValue)
	{
		currentValue = values[previousIndex] + values[currentIndex];

		if (currentValue <= maxValue)
		{
			values.Add(currentValue);
		}

		previousIndex++;
		currentIndex++;
	}

	return values.SkipConditionally(2, skipInitial).Where(x => x % 2 == 0).ToList();
}

#region Recursive
public static IList<int> Recursive(int maxValue, bool skipInitial = false)
{
	var values = new Queue<int>();
	var previousValue = 0;
	var currentValue = 1;

	values.Enqueue(previousValue);
	values.Enqueue(currentValue);

	RecurseNext(previousValue, currentValue, maxValue, values);

	return values.SkipConditionally(2, skipInitial).Where(x => x % 2 == 0).ToList();
}

private static void RecurseNext(int previousValue, int currentValue, int maxValue, Queue<int> values)
{
	//My 5 rules to recursive methods:
	//1. Never expose the actual recursive method, always make them call a wrapper function, as it isn't safe to assume they know how to properly use it
	//2. Recursive method should have a void return type
	//3. Any data needed or to be persisted needs to be passed as an argument otherwise the values will be lost on the next call, otherwise the methods get hard to write and maintain
	//4. The recursive method needs to recurse (call itself with updated values)
	//5. Termination condition(s) needs to be used within the recursive method, otherwise it won't know when to stop and you will get a StackOverflow error	

	if (currentValue <= maxValue)
	{
		var nextValue = previousValue + currentValue;

		if (nextValue <= maxValue)
		{
			try
			{
				values.Enqueue(nextValue);
			}
			catch (Exception e)
			{
				values.Dump();
				nextValue.Dump();
				e.Dump();
			}

		}

		previousValue = currentValue;
		currentValue = nextValue;

		RecurseNext(previousValue, currentValue, maxValue, values);
	}
}
#endregion

#region Linked List
public class FibonacciLinkedListNode
{
	public int Value { get; set; }
	public bool IsEven => this.Value % 2 == 0;

	public FibonacciLinkedListNode PreviousPrevious { get; set; }
	public FibonacciLinkedListNode Previous { get; set; }
	public FibonacciLinkedListNode Next { get; set; }
}

public class FibonacciLinkedList //Singly-Linked List with custom pointers
{
	//Nodes
	public IList<int> Values => GetValues();
	public FibonacciLinkedListNode Root { get; internal set; }

	//Pointers
	public FibonacciLinkedListNode End { get; internal set; }
	public FibonacciLinkedListNode Previous { get; internal set; }
	public FibonacciLinkedListNode Current { get; internal set; }

	public FibonacciLinkedList()
	{
		this.Root = new FibonacciLinkedListNode { Value = 0 };
		this.End = new FibonacciLinkedListNode
		{
			Value = 1,
			Previous = this.Root
		};

		this.Root.Next = this.End;

		this.Previous = this.Root;
		this.Current = this.End;
	}

	public void Add(int value)
	{
		var newNode = new FibonacciLinkedListNode
		{
			Value = value,
			PreviousPrevious = this.End.Previous,
			Previous = this.End,
		};

		this.End.Next = newNode;
		this.End = newNode;

		this.Previous = this.Previous.Next;
		this.Current = this.Current.Next;
	}

	private IList<int> GetValues()
	{
		var currentNode = this.Root;
		var values = new List<int>();

		while (currentNode.Next != null)
		{
			values.Add(currentNode.Value);
			currentNode = currentNode.Next;
		}

		values.Add(currentNode.Value);

		return values;
	}
}

public static IList<int> LinkedList(int maxValue, bool skipInitial = false)
{
	var linkedList = new FibonacciLinkedList();

	var currentValue = 0;

	while (currentValue <= maxValue)
	{
		currentValue = linkedList.Previous.Value + linkedList.Current.Value;

		if (currentValue <= maxValue)
		{
			linkedList.Add(currentValue);
		}
	}

	return linkedList.Values.SkipConditionally(2, skipInitial).Where(x => x % 2 == 0).ToList();
}
#endregion

public static class Extensions
{
	public static IEnumerable<int> WhereEven(this IEnumerable<int> elements)
	{
		return elements.Where(x => x % 2 == 0);
	}
	
	public static IEnumerable<T> WhereEven<T>(this IEnumerable<T> elements, Func<T, int> selector)
	{
		return elements.Where(x => selector(x) % 2 ==0);
	}

	public static IEnumerable<T> WhereEven<T>(this IEnumerable<T> elements, Func<T, decimal> selector)
	{
		return elements.Where(x => selector(x) % 2 == 0);
	}

	public static IEnumerable<T> WhereEven<T>(this IEnumerable<T> elements, Func<T, double> selector)
	{
		return elements.Where(x => selector(x) % 2 == 0);
	}

	public static IEnumerable<T> WhereEven<T>(this IEnumerable<T> elements, Func<T, Single> selector)
	{
		return elements.Where(x => selector(x) % 2 == 0);
	}

	public static IEnumerable<T> WhereEven<T>(this IEnumerable<T> elements, Func<T, byte> selector)
	{
		return elements.Where(x => selector(x) % 2 == 0);
	}

	//Skips elements only when given condition is met, it condition is not met, all elements will be returned
	public static IEnumerable<T> SkipConditionally<T>(this IEnumerable<T> elements, int count, bool condition)
	{
		return condition ? elements.Skip(count) : elements;

		//if(condition)
		//{
		//	return elements.Skip(count);
		//}
		//
		//return elements;
	}

	//Returns all elements of the list with the min value based on the aggregate
	public static IEnumerable<T1> MinBy<T1, T2>(this IEnumerable<T1> elements, Func<T1, T2> aggregate) where T2 : IComparable, IComparable<T2>
	{
		if (elements == null || elements.NotAny())
		{
			return elements;
		}

		var minValue = aggregate(elements.ElementAt(0));
		var minElements = new List<T1>();

		foreach (var element in elements)
		{
			var currentValue = aggregate(element);
			var comparison = currentValue.CompareTo(minValue);

			if (comparison == 0)
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

	//Returns all elements of the list with the max value based on the aggregate
	public static IEnumerable<T1> MaxBy<T1, T2>(this IEnumerable<T1> elements, Func<T1, T2> aggregate) where T2 : IComparable, IComparable<T2>
	{
		if (elements == null || elements.NotAny())
		{
			return elements;
		}

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

	public static bool NotAny<T>(this IEnumerable<T> elements)
	{
		return !elements.Any();
	}

	public static bool NotAny<T>(this IEnumerable<T> elements, Func<T, bool> predicate)
	{
		return !elements.Any(predicate);
	}

	public static string ToBestOnesLabel(this IList<RunResult<IList<int>>> bestOnes)
	{
		return bestOnes.ToSelectedOnesLabel("best");
	}

	public static string ToWorstOnesLabel(this IList<RunResult<IList<int>>> worstOnes)
	{
		return worstOnes.ToSelectedOnesLabel("worst");
	}

	public static string ToSelectedOnesLabel(this IList<RunResult<IList<int>>> selectedOnes, string selectionLabel)
	{
		if (selectedOnes == null || selectedOnes.NotAny())
		{
			return "";
		}

		var stringBuilder = new StringBuilder();

		if (selectedOnes.Count() == 1)
		{
			stringBuilder.Append($"The {selectionLabel} algorithm is {selectedOnes.Single().Label}");
		}
		else
		{
			stringBuilder.Append($"The {selectionLabel} algorithm is ");

			for (var i = 0; i < selectedOnes.Count(); i++)
			{
				if (i < selectedOnes.Count() - 2)
				{
					stringBuilder.Append($"{selectedOnes[i].Label}, ");
				}
				else if (i < selectedOnes.Count() - 1)
				{
					stringBuilder.Append($"{selectedOnes[i].Label}, or ");
				}
				else
				{
					stringBuilder.Append($"{selectedOnes[i].Label}");
				}
			}
		}

		stringBuilder.Append(".");

		return stringBuilder.ToString();
	}
}