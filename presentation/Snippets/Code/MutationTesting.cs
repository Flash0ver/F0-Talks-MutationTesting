namespace Snippets.Code
{
	internal static class MutationTesting
	{
		public static void Add()
		{
			#region MutationTesting_Add
			int number = Add(1, 2);
			Check.That(number).Equal(3);

			int Add(int left, int right)
			{
				return left + right;
			}
			#endregion
		}

		public static void Subtract()
		{
			#region MutationTesting_Subtract
			int number = Subtract(3, 2);
			Check.That(number).Equal(1);

			int Subtract(int left, int right)
			{
				return left - right;
			}
			#endregion
		}

		public static void Multiply()
		{
			#region MutationTesting_Multiply
			int number = Multiply(2, 3);
			Check.That(number).Equal(6);

			int Multiply(int left, int right)
			{
				return left * right;
			}
			#endregion
		}

		public static void Divide()
		{
			#region MutationTesting_Divide
			int number = Divide(6, 3);
			Check.That(number).Equal(2);

			int Divide(int left, int right)
			{
				return left / right;
			}
			#endregion
		}

		public static void Equality()
		{
			#region MutationTesting_Equality
			Check.That(IsOneDigit(9)).IsTrue();
			Check.That(IsOneDigit(10)).IsFalse();
			Check.That(IsOneDigit(-9)).IsTrue();
			Check.That(IsOneDigit(-10)).IsFalse();

			bool IsOneDigit(int number)
			{
				return -9 <= number && number <= 9;
			}
			#endregion
		}
	}
}
