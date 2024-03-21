using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using SaveSystem;
using UI.Buttons;
using UnityEngine;

namespace Match3.Settings
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Match3/Levels Data")]
    public class LevelsData : ScriptableObject
    {
        public List<Level> Tasks;

        public Sprite LevelImage;
        public Sprite CurrentLevelImage;
        public List<Sprite> StarsSprite;
        
        private int _selectedLevel;
        
        public void SelectLevel(int levelNumber) => _selectedLevel = levelNumber;
        
        public Level GetUnlockedTask()
        {
            if (Tasks[_selectedLevel].LevelButtonType == ELevelButtonType.Current)
                return Tasks.FirstOrDefault(x => x.LevelButtonType == ELevelButtonType.Current);
            else
                return Tasks[_selectedLevel];
        }
        public void SetNextLevelTypes(int starsCount,int score)
        {
            var currentLevelIndex = Tasks.FindIndex(x => x.LevelButtonType == ELevelButtonType.Current);
            if (currentLevelIndex != -1 && currentLevelIndex < Tasks.Count - 1)
            {
                Tasks[currentLevelIndex].LevelButtonType = ELevelButtonType.Unlocked;
                Tasks[currentLevelIndex].StarsCount = starsCount;
                Tasks[currentLevelIndex].Score = score;
                
                Tasks[currentLevelIndex + 1].LevelButtonType = ELevelButtonType.Current;
            }
            SaveManager.SaveLevelsData(Tasks);
        }
    }
    
    [Serializable]
    public class Level
    {
        public List<Mission> Missions;
        public int TotalMoves;
        public int StarsCount;
        public int Score;
        public ELevelButtonType LevelButtonType;
    }
    
    [Serializable]
    public class Mission
    {
        public ETileType TileType;
        public int TargetCount;
    }
}