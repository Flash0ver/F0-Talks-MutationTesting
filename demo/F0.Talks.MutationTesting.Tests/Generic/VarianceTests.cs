using System;
using System.CodeDom.Compiler;
using System.IO;
using F0.Talks.MutationTesting.Generic;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Generic
{
	public class VarianceTests
	{
		[Fact]
		public void Covariance_PreservesAssignmentCompatibility()
		{
			Type covariant = typeof(ICovariant<TextWriter>);

#if HAS_IS_ASSIGNABLE_TO
			Assert.True(covariant.IsAssignableTo(typeof(ICovariant<MarshalByRefObject>)));
			Assert.False(covariant.IsAssignableTo(typeof(ICovariant<IndentedTextWriter>)));
#else
			Assert.True(typeof(ICovariant<MarshalByRefObject>).IsAssignableFrom(covariant));
			Assert.False(typeof(ICovariant<IndentedTextWriter>).IsAssignableFrom(covariant));
#endif

		}

		[Fact]
		public void Contravariance_ReversesAssignmentCompatibility()
		{
			Type contravariant = typeof(IContravariant<TextWriter>);

#if HAS_IS_ASSIGNABLE_TO
			Assert.False(contravariant.IsAssignableTo(typeof(IContravariant<MarshalByRefObject>)));
			Assert.True(contravariant.IsAssignableTo(typeof(IContravariant<IndentedTextWriter>)));
#else
			Assert.False(typeof(IContravariant<MarshalByRefObject>).IsAssignableFrom(contravariant));
			Assert.True(typeof(IContravariant<IndentedTextWriter>).IsAssignableFrom(contravariant));
#endif

		}

		[Fact]
		public void Invariance_IsNeitherCovariantNorContravariant()
		{
			Type invariant = typeof(IInvariant<TextWriter>);

#if HAS_IS_ASSIGNABLE_TO
			Assert.False(invariant.IsAssignableTo(typeof(IInvariant<MarshalByRefObject>)));
			Assert.False(invariant.IsAssignableTo(typeof(IInvariant<IndentedTextWriter>)));
#else
			Assert.False(typeof(IInvariant<MarshalByRefObject>).IsAssignableFrom(invariant));
			Assert.False(typeof(IInvariant<IndentedTextWriter>).IsAssignableFrom(invariant));
#endif
		}
	}
}
