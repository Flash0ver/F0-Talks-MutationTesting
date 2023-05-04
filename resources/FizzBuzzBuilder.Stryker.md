# Mutant Schemata (Mutation Switching)

```diff
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace F0.Talks.MutationTesting.Text;

public static class FizzBuzzBuilder
{
	public static IEnumerable<string> EnumerateFizzBuzz(int start, int count)
	{
+		if (Stryker.MutantControl.IsActive(46)) { }
+		else
+		{
			IEnumerable<int> numbers = Enumerable.Range(start, count);
			return EnumerateFizzBuzz(numbers);

			static IEnumerable<string> EnumerateFizzBuzz(IEnumerable<int> numbers)
			{
+				if (Stryker.MutantControl.IsActive(47)) { }
+				else
+				{
+					if (Stryker.MutantControl.IsActive(48)) {;}
+					else
+					{
-						Debug.Assert((Stryker.MutantControl.IsActive(49) ? numbers == null : numbers != null));
+					}

					foreach (int integer in numbers)
					{
+						if (Stryker.MutantControl.IsActive(50)) { }
+						else
+						{
-							bool isFizz = (Stryker.MutantControl.IsActive(51) ? integer % 3 != 0 : (Stryker.MutantControl.IsActive(52) ? integer * 3 : integer % 3) == 0);
-							bool isBuzz = (Stryker.MutantControl.IsActive(53) ? integer % 5 != 0 : (Stryker.MutantControl.IsActive(54) ? integer * 5 : integer % 5) == 0);

-							if ((Stryker.MutantControl.IsActive(56) ? !(isFizz && isBuzz) : (Stryker.MutantControl.IsActive(55) ? isFizz || isBuzz : isFizz && isBuzz)))
							{
+								if (Stryker.MutantControl.IsActive(57)) { }
+								else
+								{
+									if (Stryker.MutantControl.IsActive(58)) {;}
+									else
+									{
-										yield return (Stryker.MutantControl.IsActive(59) ? "" : "FizzBuzz");
+									}
+								}
							}
-							else if ((Stryker.MutantControl.IsActive(60) ? !(isFizz) : isFizz))
							{
+								if (Stryker.MutantControl.IsActive(61)) { }
+								else
+								{
+									if (Stryker.MutantControl.IsActive(62)) {;}
+									else
+									{
-										yield return (Stryker.MutantControl.IsActive(63) ? "" : "Fizz");
+									}
+								}
							}
-							else if ((Stryker.MutantControl.IsActive(64) ? !(isBuzz) : isBuzz))
							{
+								if (Stryker.MutantControl.IsActive(65)) { }
+								else
+								{
+									if (Stryker.MutantControl.IsActive(66)) {;}
+									else
+									{
-										yield return (Stryker.MutantControl.IsActive(67) ? "" : "Buzz");
+									}
+								}
							}
							else
							{
+								if (Stryker.MutantControl.IsActive(68)) { }
+								else
+								{
+									if (Stryker.MutantControl.IsActive(69)) {;}
+									else
+									{
-										yield return integer.ToString(NumberFormatInfo.InvariantInfo);
+									}
+								}
							}
+						}
					}
+				}
			}
+		}

+		throw new Exception("Unreachable");
	}
}

+internal static class Stryker
+{
+	internal static class MutantControl
+	{
+		internal static bool IsActive(int id) => throw new NotImplementedException();
+	}
+}

```
