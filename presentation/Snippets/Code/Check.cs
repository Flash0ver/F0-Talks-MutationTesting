namespace Snippets.Code
{
	internal static class Check
	{
		public static LogicalChecker That(bool actual)
		{
			return new LogicalChecker(actual);
		}

		public static EqualityChecker<T> That<T>(T actual) where T : notnull
		{
			return new EqualityChecker<T>(actual);
		}
	}
}
