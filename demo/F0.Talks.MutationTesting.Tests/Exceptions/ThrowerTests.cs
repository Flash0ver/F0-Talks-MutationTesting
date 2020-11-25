using System;
using F0.Talks.MutationTesting.Exceptions;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Exceptions
{
	public class ThrowerTests
	{
		[Fact]
		public void Throw_Throws()
		{
			Assert.Throws<InvalidOperationException>(() => Thrower.Throw(new InvalidOperationException()));
		}
	}
}
