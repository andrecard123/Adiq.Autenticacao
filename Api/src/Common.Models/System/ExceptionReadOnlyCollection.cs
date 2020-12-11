using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System
{
	public class ExceptionReadOnlyCollection : IReadOnlyCollection<Exception>
	{
		private readonly IEnumerable<Exception> _enumerable;

		public static ExceptionReadOnlyCollection Empty { get; }

		static ExceptionReadOnlyCollection()
		{
			Empty = new ExceptionReadOnlyCollection();
		}

		public ExceptionReadOnlyCollection(params Exception[] array)
			: this(array as IEnumerable<Exception>)
		{
		}

		public ExceptionReadOnlyCollection(IEnumerable<Exception> enumerable)
		{
			_enumerable = enumerable;
		}

		public int Count
			=> _enumerable.Count();

		public IEnumerator<Exception> GetEnumerator()
			=> _enumerable.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> _enumerable.GetEnumerator();

		public static implicit operator ExceptionReadOnlyCollection(Exception exception)
			=> new ExceptionReadOnlyCollection(exception);
	}
}