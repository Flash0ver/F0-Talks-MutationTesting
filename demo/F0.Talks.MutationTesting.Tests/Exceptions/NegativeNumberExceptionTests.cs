using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using F0.Talks.MutationTesting.Exceptions;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Exceptions
{
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

		[Fact]
		public void Serialization_RoundTrip()
		{
			string message = "description";
			Exception innerException = new InvalidOperationException();
			var originalException = new NegativeNumberException(message, -3, innerException);

			var restoredException = (NegativeNumberException)RoundTrip(originalException);

			Assert.NotSame(originalException, restoredException);
			Assert.NotSame(innerException, restoredException.InnerException);
			Assert.Equal(message, restoredException.Message);
			Assert.Equal(-3, restoredException.Number);
			Assert.Equal(innerException.Message, restoredException.InnerException!.Message);

			static object RoundTrip(object graph)
			{
				using Stream stream = new MemoryStream();
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, graph);

				_ = stream.Seek(0, SeekOrigin.Begin);
				return formatter.Deserialize(stream);
			}
		}
	}
}
