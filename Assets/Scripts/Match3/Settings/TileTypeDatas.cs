using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = "TileTypeDatas", menuName = "Match3/Tile/Tile Type Datas", order = 0)]
    public sealed class TileTypeDatas : ScriptableObject
    {
        public List<TileTypeData> TileDatas;
        
        public Sprite GetSpriteForTileType(ETileType tileType)
        {
            foreach (var tileData in TileDatas)
            {
                if (tileData.TileType == tileType)
                {
                    return tileData.Sprite;
                }
            }
            
            return null;
        }
    }
}