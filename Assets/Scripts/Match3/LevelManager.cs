using System.Collections.Generic;
using Match3.Settings;
using Settings;
using UI.Buttons;
using UnityEngine;
using Utils;

namespace Match3
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] private List<LevelButton> _levels;

        private LevelData _levelsSettings;

        public Level CurrentLevel { get; private set; }
        private void Start()
        {
            _levelsSettings = SettingsProvider.Get<LevelData>();
            int i = 0;
            foreach (var level in _levels)
            {
                level.Setup(new LevelButtonSettings()
                {
                    LevelNumber = i,
                    StarsCount = _levelsSettings.tasks[i].StarsCount,
                    LevelSprite = _levelsSettings.tasks[i].LevelButtonType != LevelButtonType.Current ? _levelsSettings.LevelImage : _levelsSettings.CurrentLevelImage,
                    StarsSprite = _levelsSettings.StarsSprite,
                    LevelButtonType = _levelsSettings.tasks[i].LevelButtonType,
                });
                i++;
            }
        }

        public void SetCurrentLevel(int levelNumber)
        {
            CurrentLevel = _levelsSettings.tasks[levelNumber];
        } 

    }
}
