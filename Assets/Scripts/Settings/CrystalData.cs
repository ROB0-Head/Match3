using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "New Crystal Data", menuName = "Match3/Crystal Data")]
    public class CrystalData : ScriptableObject
    {
        public enum SpecialAbility
        {
            None,
            HorizontalLightning,
            VerticalLightning,
            CrossLightning,
            DestroySameColor
        }

        public string typeName;
        public Color color;
        public SpecialAbility specialAbility;
    }
}