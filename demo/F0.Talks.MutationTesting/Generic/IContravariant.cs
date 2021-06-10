namespace F0.Talks.MutationTesting.Generic
{
	public interface IContravariant<in T>
	{
		void Set(T value);
	}
}
