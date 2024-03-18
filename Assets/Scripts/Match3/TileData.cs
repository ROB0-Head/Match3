namespace Match3
{
	public readonly struct TileData
	{
		public readonly int X;
		public readonly int Y;

		public readonly ETileType TypeId;

		public TileData(int x, int y, ETileType typeId)
		{
			X = x;
			Y = y;

			TypeId = typeId;
		}
	}
	public enum ETileType
	{
		Blue,
		LightBlue,
		Pink,
		Purple,
		Yellow
	}
}
