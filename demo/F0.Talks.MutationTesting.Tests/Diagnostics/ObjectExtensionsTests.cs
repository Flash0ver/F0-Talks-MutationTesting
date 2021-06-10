using System;
using F0.Talks.MutationTesting.Diagnostics;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Diagnostics
{
	public class ObjectExtensionsTests
	{
		[Fact]
		public void ThrowIfNull_DoesNotThrowIfNotNull()
		{
			string text = string.Empty;

			text.ThrowIfNull(nameof(text));

			Assert.Empty(text);
		}

		[Fact]
		public void ThrowIfNull_DoesThrowIfNull()
		{
			string? text = null;

			Assert.Throws<ArgumentNullException>("text", () => text.ThrowIfNull(nameof(text)));
		}
	}
}
