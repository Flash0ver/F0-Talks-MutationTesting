using System;
using System.Runtime.Serialization;

namespace F0.Talks.MutationTesting.Exceptions;

public partial class NegativeNumberException : Exception
{
	public NegativeNumberException()
		: base("A number was negative.")
	{
	}

	public NegativeNumberException(string message)
		: base(message)
	{
	}

	public NegativeNumberException(string message, Exception inner)
		: base(message, inner)
	{
	}

	public NegativeNumberException(string message, int number)
		: base(message)
	{
		Number = number;
	}

	public NegativeNumberException(string message, int number, Exception innerException)
		: base(message, innerException)
	{
		Number = number;
	}

	public int? Number { get; }
}
