using System.Collections.Generic;
using Match3.Settings;
using Settings;
using UI.Buttons;
using UnityEngine;

namespace Match3
{
    public class LevelSelectionManager : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levels;

        private LevelsData _levelsesSettings;
        private void Start()
        {
            _levelsesSettings = SettingsProvider.Get<LevelsData>();
            int i = 0;
            foreach (var level in _levels)
            {
                level.Setup(new LevelButtonSettings()
                {
                    LevelNumber = i,
                    StarsCount = _levelsesSettings.Tasks[i].StarsCount,
                    LevelSprite = _levelsesSettings.Tasks[i].LevelButtonType != ELevelButtonType.Current ? _levelsesSettings.LevelImage : _levelsesSettings.CurrentLevelImage,
                    StarsSprite = _levelsesSettings.StarsSprite,
                    LevelButtonType = _levelsesSettings.Tasks[i].LevelButtonType,
                });
                i++;
            }
        }

       

    }
}
