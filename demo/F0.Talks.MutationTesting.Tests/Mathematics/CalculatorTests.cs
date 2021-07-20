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
			double result = Calculator.Subtract(0x_F0, 0x_F0);

			Assert.Equal(0, result);
		}

		[Fact]
		public void Multiply()
		{
			double result = Calculator.Multiply(0x_F0, 2);

			Assert.Equal(480, result);
		}

		[Fact]
		public void Divide()
		{
			double result = Calculator.Divide(0x_F0, 10);

			Assert.Equal(24, result);
		}

		[Fact]
		public void DivideByZero()
		{
			double result = Calculator.Divide(0x_F0, 0);

			Assert.Equal(double.NaN, result);
		}
	}
}
