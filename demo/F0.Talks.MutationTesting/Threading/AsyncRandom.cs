using System;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.MutationTesting.Threading;

public sealed class AsyncRandom : IDisposable
{
	private readonly Random random;
	private readonly SemaphoreSlim mutex = new(1);

	public AsyncRandom()
	{
		random = new Random();
	}

	public Task<int> NextAsync(int maxValue, CancellationToken cancellationToken = default)
	{
		_ = maxValue >= 0 ? maxValue : throw new ArgumentOutOfRangeException(nameof(maxValue), $"'{nameof(maxValue)}' must be greater than zero.");

		return NextAsync(maxValue, cancellationToken);

		async Task<int> NextAsync(int maxValue, CancellationToken cancellationToken)
		{
			try
			{
				await mutex.WaitAsync(cancellationToken).ConfigureAwait(false);
				return random.Next(maxValue);
			}
			finally
			{
				mutex.Release();
			}
		}
	}

	public Task<int> NextAsync(int minValue, int maxValue, CancellationToken cancellationToken = default)
	{
		_ = minValue <= maxValue ? minValue : throw new ArgumentOutOfRangeException(nameof(minValue), $"'{nameof(minValue)}' cannot be greater than {nameof(maxValue)}.");

		return NextAsync(minValue, maxValue, cancellationToken);

		async Task<int> NextAsync(int minValue, int maxValue, CancellationToken cancellationToken)
		{
			try
			{
				await mutex.WaitAsync(cancellationToken).ConfigureAwait(false);
				return random.Next(minValue, maxValue);
			}
			finally
			{
				mutex.Release();
			}
		}
	}

	public void Dispose()
	{
		mutex.Dispose();
	}
}
