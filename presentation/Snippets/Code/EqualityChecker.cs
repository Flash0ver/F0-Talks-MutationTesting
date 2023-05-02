using System;

namespace Snippets.Code
{
	internal class EqualityChecker<T> where T : notnull
	{
		private readonly T actual;

		public EqualityChecker(T actual)
		{
			this.actual = actual;
		}

		public void Equal(IEquatable<T> expected)
		{
			if (expected.Equals(actual))
			{
				Checker.Pass();
			}
			else
			{
				Checker.Fail(expected, actual);
			}
		}
	}
}
