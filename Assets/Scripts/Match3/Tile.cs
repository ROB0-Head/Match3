using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public sealed class Tile : MonoBehaviour
    {
        public Image Icon;
        public Button Button;

        private int x;
        private int y;
        private TileTypeAsset _type;

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }
        
        
        public TileTypeAsset Type
        {
            get => _type;

            set
            {
                if (_type == value)
                    return;

                _type = value;

                Icon.sprite = _type.sprite;
            }
        }

        public TileData Data => new TileData(x, y, _type.id);

        
    }
}