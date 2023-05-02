namespace F0.Talks.MutationTesting.Generic;

public interface IInvariant<T>
{
	T Get();
	void Set(T value);
}
