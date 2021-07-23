using F0.Talks.MutationTesting.Mathematics;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Mathematics
{
	public class CalculatorTests
	{
		[Fact]
		public void Add()
		{
			double result = Calculator.Add(2, 2);

			Assert.Equal(4, result);
		}

		[Fact]
		public void Subtract()
		{
			double result = Calculator.Subtract(2, 2);

			Assert.Equal(0, result);
		}

		[Fact]
		public void Multiply()
		{
			double result = Calculator.Multiply(2, 2);

			Assert.Equal(4, result);
		}

		[Fact]
		public void Divide()
		{
			double result = Calculator.Divide(1, 1);

			Assert.Equal(1, result);
		}

		[Fact]
		public void DivideByZero()
		{
			double result = Calculator.Divide(0x_F0, 0);

			Assert.Equal(double.NaN, result);
		}

		[Fact]
		public void Remainder()
		{
			double result = Calculator.Remainder(2, 2);

			Assert.Equal(0, result);
		}
	}
}
