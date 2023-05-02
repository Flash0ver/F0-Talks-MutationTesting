namespace F0.Talks.MutationTesting.Generic;

public interface ICovariant<out T>
{
	T Get();
}
