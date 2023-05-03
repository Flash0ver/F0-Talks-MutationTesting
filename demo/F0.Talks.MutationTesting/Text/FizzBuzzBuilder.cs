using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace F0.Talks.MutationTesting.Text;

public static class FizzBuzzBuilder
{
	public static IEnumerable<string> EnumerateFizzBuzz(int start, int count)
	{
		IEnumerable<int> numbers = Enumerable.Range(start, count);
		return EnumerateFizzBuzz(numbers);

		static IEnumerable<string> EnumerateFizzBuzz(IEnumerable<int> numbers)
		{
			Debug.Assert(numbers != null);

			foreach (int integer in numbers)
			{
				bool isFizz = integer % 3 == 0;
				bool isBuzz = integer % 5 == 0;

				if (isFizz && isBuzz)
				{
					yield return "FizzBuzz";
				}
				else if (isFizz)
				{
					yield return "Fizz";
				}
				else if (isBuzz)
				{
					yield return "Buzz";
				}
				else
				{
					yield return integer.ToString(NumberFormatInfo.InvariantInfo);
				}
			}
		}
	}

	public static string GetFizzBuzz(int start, int count)
	{
		return GetFizzBuzz(start, count, ", ");
	}

	public static string GetFizzBuzz(int start, int count, string separator)
	{
		var builder = new StringBuilder();

		foreach (string text in EnumerateFizzBuzz(start, count))
		{
			builder.Append(text);
			builder.Append(separator);
		}

		int length = builder.Length;
		return length == 0
			? string.Empty
			: builder.ToString(0, length - separator.Length);
	}
}
