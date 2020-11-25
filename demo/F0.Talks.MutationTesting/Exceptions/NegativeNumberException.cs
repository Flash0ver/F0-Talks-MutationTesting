using System;
using System.Runtime.Serialization;

namespace F0.Talks.MutationTesting.Exceptions
{
	[Serializable]
	public class NegativeNumberException : Exception
	{
		public NegativeNumberException()
			: base("A number was negative.")
		{
		}

		public NegativeNumberException(string message)
			: base(message)
		{
		}

		public NegativeNumberException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public NegativeNumberException(string message, int number)
			: base(message)
		{
			Number = number;
		}

		public NegativeNumberException(string message, int number, Exception innerException)
			: base(message, innerException)
		{
			Number = number;
		}

		public int? Number { get; }

		protected NegativeNumberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			Number = (int?)info.GetValue("NegativeNumber_Number", typeof(int?));
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("NegativeNumber_Number", Number, typeof(int?));
		}
	}
}
