using System;
using System.Collections.Generic;
using UI.Buttons;
using UnityEngine;

namespace Match3.Settings
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Match3/Levels Data")]
    public class LevelData : ScriptableObject
    {
        public List<Level> tasks;

        public Sprite LevelImage;
        public Sprite CurrentLevelImage;
        public List<Sprite> StarsSprite;

    }
    
    [Serializable]
    public class Level
    {
        public List<Mission> Missions;
        public int TotalMoves;
        public int StarsCount;
        public int Score;
        public LevelButtonType LevelButtonType;
    }
    
    [Serializable]
    public class Mission
    {
        public ETileType TileType;
        public int TargetCount;
    }
}