using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "New Crystal Spawn Data", menuName = "Match3/Crystal Spawn Data")]
    public class CrystalSpawnData : ScriptableObject
    {
        public List<CrystalData> crystalTypes;
    }
}