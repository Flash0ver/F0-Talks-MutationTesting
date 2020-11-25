using System;

namespace Snippets.Code
{
	internal static class Checker
	{
		public static void Pass()
		{
			Console.Write("✅ ");
			Console.WriteLine($"Test passed.");
		}

		public static void Fail(object expected, object actual)
		{
			Console.Write("❌ ");
			Console.WriteLine($"Expected to be '{expected}', but actually was '{actual}'.");
		}
	}
}
