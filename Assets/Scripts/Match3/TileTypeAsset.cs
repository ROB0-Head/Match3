using UnityEngine;

namespace Match3
{
	[CreateAssetMenu(fileName = "TileTypeAsset", menuName = "Match3/Tile Type Asset", order = 0)]
	public sealed class TileTypeAsset : ScriptableObject
	{
		public int id;
		
		public Sprite sprite;
	}
}
