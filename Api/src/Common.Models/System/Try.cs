namespace System
{
	public struct Try<TSuccess>
	{
		public ExceptionReadOnlyCollection Failure { get; }
		public TSuccess Success { get; }

		public bool IsFailure { get; }
		public bool IsSuccess => !IsFailure;

		internal Try(ExceptionReadOnlyCollection failure)
		{
			IsFailure = true;
			Failure = failure;
			Success = default;
		}

		internal Try(TSuccess success)
		{
			IsFailure = false;
			Failure = default;
			Success = success;
		}

		public TResult Resume<TResult>(
			Func<ExceptionReadOnlyCollection, TResult> failure,
			Func<TSuccess, TResult> success
		) => IsFailure ? failure(Failure) : success(Success);

		public Return Resume(
			Action<ExceptionReadOnlyCollection> failure,
			Action<TSuccess> success
		) => Resume(ToFunc(failure), ToFunc(success));

		public static implicit operator Try<TSuccess>(Exception exception)
			=> new ExceptionReadOnlyCollection(exception);

		public static implicit operator Try<TSuccess>(ExceptionReadOnlyCollection failure)
			=> new Try<TSuccess>(failure);

		public static implicit operator Try<TSuccess>(TSuccess success)
			=> new Try<TSuccess>(success);

		private static Func<T, Return> ToFunc<T>(Action<T> action)
			=> o =>
			{
				action(o);
				return Return.Empty;
			};
	}
}
