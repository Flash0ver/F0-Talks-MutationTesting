using System;
using System.Threading;
using System.Threading.Tasks;
using F0.Talks.MutationTesting.Threading;
using F0.Testing;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Threading
{
	public class AsyncRandomTests
	{
		[Fact]
		public void ArgumentValidation()
		{
			using AsyncRandom random = new();

			Test.That(() => random.NextAsync(-1)).ThrowsSynchronously<ArgumentOutOfRangeException>();
			Test.That(() => random.NextAsync(1, 0)).ThrowsSynchronously<ArgumentOutOfRangeException>();
		}

		[Fact]
		public async Task NextAsync_WithoutCancellation()
		{
			using AsyncRandom random = new();

			Assert.Equal(0, await random.NextAsync(0));
			Assert.Equal(1, await random.NextAsync(1, 2));
		}

		[Fact]
		public async Task NextAsync_WithCancellation()
		{
			using AsyncRandom random = new();

			CancellationToken cancellationToken = new(true);

			await Assert.ThrowsAsync<TaskCanceledException>(() => random.NextAsync(0, cancellationToken));
			await Assert.ThrowsAsync<TaskCanceledException>(() => random.NextAsync(1, 2, cancellationToken));
		}

		[Fact]
		public async Task ReleaseResources()
		{
			using AsyncRandom random = new();

			random.Dispose();

			await Assert.ThrowsAsync<ObjectDisposedException>(() => random.NextAsync(0));
			await Assert.ThrowsAsync<ObjectDisposedException>(() => random.NextAsync(1, 2));
		}
	}
}
