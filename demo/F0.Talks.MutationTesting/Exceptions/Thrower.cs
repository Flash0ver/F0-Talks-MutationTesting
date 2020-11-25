using System;

namespace F0.Talks.MutationTesting.Exceptions
{
	public static class Thrower
	{
		public static void Throw(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException(nameof(exception));
			}

			throw exception;
		}
	}
}
