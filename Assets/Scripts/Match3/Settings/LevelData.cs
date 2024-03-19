using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Settings
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Match3/Levels Data")]
    public class LevelData : ScriptableObject
    {
        public List<Level> tasks;

        public Sprite LockedLevelImage;
        public Sprite UnlockedLevelImage;
        public Sprite CurrentLevelImage;

    }
    
    [Serializable]
    public class Level
    {
        public List<Mission> Missions;
        public int TotalMoves;
        public int Score;
        public bool IsLocked;
    }
    
    [Serializable]
    public class Mission
    {
        public ETileType TileType;
        public int TargetCount;
    }
}