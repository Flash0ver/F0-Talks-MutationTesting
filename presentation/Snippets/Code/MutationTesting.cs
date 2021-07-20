using System.Collections.Generic;
using System.Linq;

namespace Snippets.Code
{
	internal static class MutationTesting
	{
		#region MutationTesting_Add
		public static int Add(int left, int right)
		{
			return left + right;
		}

		[Test]
		public static void Add_Test()
		{
			int number = Add(1, 2);
			Check.That(number).Equal(3);
		}
		#endregion

		#region MutationTesting_Subtract
		public static int Subtract(int left, int right)
		{
			return left - right;
		}

		[Test]
		public static void Subtract_Test()
		{
			int number = Subtract(3, 2);
			Check.That(number).Equal(1);
		}
		#endregion

		#region MutationTesting_Multiply
		public static int Multiply(int left, int right)
		{
			return left * right;
		}

		[Test]
		public static void Multiply_Test()
		{
			int number = Multiply(2, 3);
			Check.That(number).Equal(6);
		}
		#endregion

		#region MutationTesting_Divide
		public static int Divide(int left, int right)
		{
			return left / right;
		}

		[Test]
		public static void Divide_Test()
		{
			int number = Divide(6, 3);
			Check.That(number).Equal(2);
		}
		#endregion

		#region MutationTesting_Equality
		public static bool IsOneDigit(int number)
		{
			return -9 <= number && number <= 9;
		}

		[Test]
		public static void Equality_Test()
		{
			Check.That(IsOneDigit(9)).IsTrue();
			Check.That(IsOneDigit(10)).IsFalse();
			Check.That(IsOneDigit(-9)).IsTrue();
			Check.That(IsOneDigit(-10)).IsFalse();
		}
		#endregion

		#region MutationTesting_Linq
		public static bool AreEven(IEnumerable<int> numbers)
		{
			return numbers.All(number => number % 2 == 0);
		}

		[Test]
		public static void Linq_Test()
		{
			Check.That(AreEven(new int[] { 2, 4 })).IsTrue();
			Check.That(AreEven(Enumerable.Range(2, 3))).IsFalse();
		}
		#endregion
	}
}
