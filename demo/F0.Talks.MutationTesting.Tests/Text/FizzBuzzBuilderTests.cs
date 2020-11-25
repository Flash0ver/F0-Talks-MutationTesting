using System;
using System.Collections.Generic;
using F0.Talks.MutationTesting.Text;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Text
{
	public class FizzBuzzBuilderTests
	{
		[Fact]
		public void GetFizzBuzz_CountIsLessThanZero_Throws()
		{
			Assert.Throws<ArgumentOutOfRangeException>("count", () => FizzBuzzBuilder.GetFizzBuzz(0, -1));
		}

		[Fact]
		public void EnumerateFizzBuzz_CountIsLessThanZero_ThrowsImmediately()
		{
			Assert.Throws<ArgumentOutOfRangeException>("count", () => FizzBuzzBuilder.EnumerateFizzBuzz(0, -1));
		}

		[Fact]
		public void GetFizzBuzz_StartPlusCountMinusOneIsLargerThanMaxValue_Throws()
		{
			Assert.Throws<ArgumentOutOfRangeException>("count", () => FizzBuzzBuilder.GetFizzBuzz(int.MaxValue, 2));
			Assert.Throws<ArgumentOutOfRangeException>("count", () => FizzBuzzBuilder.GetFizzBuzz(2, int.MaxValue));
		}

		[Fact]
		public void EnumerateFizzBuzz_StartPlusCountMinusOneIsLargerThanMaxValue_ThrowsImmediately()
		{
			Assert.Throws<ArgumentOutOfRangeException>("count", () => FizzBuzzBuilder.EnumerateFizzBuzz(int.MaxValue, 2));
			Assert.Throws<ArgumentOutOfRangeException>("count", () => FizzBuzzBuilder.EnumerateFizzBuzz(2, int.MaxValue));
		}

		[Theory(Skip = "demo")]
		[MemberData(nameof(GetFizzBuzzTestData))]
		public void GetFizzBuzz_DivisibleByThreeIsFizz_DivisibleByFiveIsBuzz_BothIsFizzBuzz_NeitherIsNumber(int start, int count, string expected)
		{
			string actual = FizzBuzzBuilder.GetFizzBuzz(start, count);

			Assert.Equal(expected, actual);
		}

		[Theory(Skip = "demo")]
		[MemberData(nameof(GetEmptyTestData))]
		public void GetFizzBuzz_NoRange_Empty(int start, int count)
		{
			string empty = FizzBuzzBuilder.GetFizzBuzz(start, count);

			Assert.Empty(empty);
		}

		[Theory]
		[MemberData(nameof(GetSeparatorTestData))]
		public void GetFizzBuzz_Separator(int start, int count, string separator, string expected)
		{
			string actual = FizzBuzzBuilder.GetFizzBuzz(start, count, separator);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[MemberData(nameof(GetEnumerableTestData))]
		public void EnumerateFizzBuzz(int start, int count, IEnumerable<string> expected)
		{
			IEnumerable<string> actual = FizzBuzzBuilder.EnumerateFizzBuzz(start, count);

			Assert.Equal(expected, actual);
		}

		public static TheoryData<int, int, string> GetFizzBuzzTestData()
		{
			return new()
			{
				{ 1, 4, "1, 2, Fizz, 4" },
				{ 4, 4, "4, Buzz, Fizz, 7" },
				{ 14, 3, "14, FizzBuzz, 16" },
				{ 1, 36, "1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz, 11, Fizz, 13, 14, FizzBuzz, 16, 17, Fizz, 19, Buzz, Fizz, 22, 23, Fizz, Buzz, 26, Fizz, 28, 29, FizzBuzz, 31, 32, Fizz, 34, Buzz, Fizz" },
			};
		}

		public static TheoryData<int, int> GetEmptyTestData()
		{
			return new()
			{
				{ 0, 0 },
				{ 1, 0 },
				{ -1, 0 },
			};
		}

		public static TheoryData<int, int, string, string> GetSeparatorTestData()
		{
			return new()
			{
				{ 1, 5, " ", "1 2 Fizz 4 Buzz" },
				{ 13, 3, ";", "13;14;FizzBuzz" },
			};
		}

		public static TheoryData<int, int, IEnumerable<string>> GetEnumerableTestData()
		{
			return new()
			{
				{ 28, 5, new string[] { "28", "29", "FizzBuzz", "31", "32" } },
				{ 19, 8, new string[] { "19", "Buzz", "Fizz", "22", "23", "Fizz", "Buzz", "26" } },
				{ 36, 0, Array.Empty<string>() },
			};
		}
	}
}
