<Query Kind="Program" />

void Main()
{
	var numbers = Enumerable.Range(1, 1_000).Select(x =>
	{	
		if (x % 3 == 0 && x % 5 == 0) { return "FizzBuzz"; }
		else if (x % 3 == 0) { return "Fizz"; }
		else if (x % 5 == 0) { return "Buzz"; }
		else {return x.ToString();}
	}).ToList().Dump();
}
