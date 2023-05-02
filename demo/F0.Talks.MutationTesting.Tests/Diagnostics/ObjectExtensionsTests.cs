using System;
using F0.Talks.MutationTesting.Diagnostics;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Diagnostics;

public class ObjectExtensionsTests
{
	[Fact]
	public void ThrowIfNull_DoesNotThrowIfNotNull_WithArgumentName()
	{
		string text = string.Empty;

		text.ThrowIfNull(nameof(text));

		Assert.Empty(text);
	}

	[Fact]
	public void ThrowIfNull_DoesNotThrowIfNotNull_WithoutArgumentName()
	{
		string text = string.Empty;

		text.ThrowIfNull();

		Assert.Empty(text);
	}

	[Fact]
	public void ThrowIfNull_DoesNotThrowIfNotNull_NullArgumentName()
	{
		string text = string.Empty;

		text.ThrowIfNull(null);

		Assert.Empty(text);
	}

	[Fact]
	public void ThrowIfNull_DoesThrowIfNull_WithArgumentName()
	{
		string? text = null;

		Assert.Throws<ArgumentNullException>("text", () => text.ThrowIfNull(nameof(text)));
	}

	[Fact]
	public void ThrowIfNull_DoesThrowIfNull_WithoutArgumentName()
	{
		string? text = null;

		Assert.Throws<ArgumentNullException>("text", () => text.ThrowIfNull());
	}

	[Fact]
	public void ThrowIfNull_DoesThrowIfNull_NullArgumentName()
	{
		string? text = null;

		Assert.Throws<ArgumentNullException>(null, () => text.ThrowIfNull(null));
	}
}
