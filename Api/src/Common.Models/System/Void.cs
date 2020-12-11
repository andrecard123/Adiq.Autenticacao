namespace System
{
	public struct Return
	{
		public static Return Empty { get; }

		static Return()
		{
			Empty = new Return();
		}
	}
}