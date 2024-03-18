using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = "TileTypeDatas", menuName = "Match3/Tile/Tile Type Datas", order = 0)]
    public sealed class TileTypeDatas : ScriptableObject
    {
        public List<TileTypeData> TileDatas;
    }
}