using System;
using F0.Talks.MutationTesting.Exceptions;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Exceptions
{
	public class ThrowerTests
	{
		[Fact]
		public void Throw_Null_Throws()
		{
			Assert.Throws<ArgumentNullException>("exception", static () => Thrower.Throw(null!));
		}

		[Fact]
		public void Throw_NotNull_Throws()
		{
			Assert.Throws<InvalidOperationException>(static () => Thrower.Throw(new InvalidOperationException()));
		}
	}
}
