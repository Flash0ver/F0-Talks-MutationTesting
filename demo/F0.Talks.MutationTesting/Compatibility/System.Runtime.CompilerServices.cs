using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.CompilerServices;

#if NETSTANDARD2_0
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
[ExcludeFromCodeCoverage]
internal sealed class CallerArgumentExpressionAttribute : Attribute
{
	public CallerArgumentExpressionAttribute(string parameterName)
	{
		ParameterName = parameterName;
	}

	public string ParameterName { get; }
}
#endif
