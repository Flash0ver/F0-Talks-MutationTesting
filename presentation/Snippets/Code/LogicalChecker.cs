namespace Snippets.Code
{
	internal class LogicalChecker
	{
		private readonly bool actual;

		public LogicalChecker(bool actual)
		{
			this.actual = actual;
		}

		public void IsTrue()
		{
			if (actual)
			{
				Checker.Pass();
			}
			else
			{
				Checker.Fail(true, actual);
			}
		}

		public void IsFalse()
		{
			if (!actual)
			{
				Checker.Pass();
			}
			else
			{
				Checker.Fail(false, actual);
			}
		}
	}
}
