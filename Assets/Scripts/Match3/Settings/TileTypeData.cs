using UnityEngine;

namespace Match3
{
	[CreateAssetMenu(fileName = "TileTypeData", menuName = "Match3/Tile/Tile Type Data", order = 0)]
	public sealed class TileTypeData : ScriptableObject
	{
		public ETileType TileType;
		public Sprite Sprite;
	}
}
