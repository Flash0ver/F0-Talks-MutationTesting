// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Snippets.Code;

namespace Snippets
{
	public static class Program
	{
		private static void Main(
			string region = null,
			string session = null,
			string package = null,
			string project = null,
			string[] args = null)
		{
			#region Main
			switch (region)
			{
				case "MutationTesting_Add":
					MutationTesting.Add_Test();
					break;
				case "MutationTesting_Subtract":
					MutationTesting.Subtract_Test();
					break;
				case "MutationTesting_Multiply":
					MutationTesting.Multiply_Test();
					break;
				case "MutationTesting_Divide":
					MutationTesting.Divide_Test();
					break;
				case "MutationTesting_Equality":
					MutationTesting.Equality_Test();
					break;
				case "MutationTesting_Linq":
					MutationTesting.Linq_Test();
					break;
				case "Q_A":
					QuestionAndAnswer.PrintMotto();
					break;
			}
			#endregion
		}
	}
}
