using System;
using F0.Talks.MutationTesting.Exceptions;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Exceptions;

public class NegativeNumberExceptionTests
{
	[Fact]
	public void Create()
	{
		var exception = new NegativeNumberException();

		Assert.Equal("A number was negative.", exception.Message);
		Assert.False(exception.Number.HasValue);
	}

	[Fact]
	public void Create_Message()
	{
		string message = "description";
		var exception = new NegativeNumberException(message);

		Assert.Equal(message, exception.Message);
		Assert.False(exception.Number.HasValue);
	}

	[Fact]
	public void Create_Message_InnerException()
	{
		string message = "description";
		Exception innerException = new InvalidOperationException();
		var exception = new NegativeNumberException(message, innerException);

		Assert.Equal(message, exception.Message);
		Assert.Equal(innerException, exception.InnerException);
		Assert.False(exception.Number.HasValue);
	}

	[Fact]
	public void Create_Message_Number()
	{
		string message = "description";
		var exception = new NegativeNumberException(message, -1);

		Assert.Equal(message, exception.Message);
		Assert.Equal(-1, exception.Number);
	}

	[Fact]
	public void Create_Message_Number_InnerException()
	{
		string message = "description";
		Exception innerException = new InvalidOperationException();
		var exception = new NegativeNumberException(message, -2, innerException);

		Assert.Equal(message, exception.Message);
		Assert.Equal(-2, exception.Number);
		Assert.Equal(innerException, exception.InnerException);
	}
}
