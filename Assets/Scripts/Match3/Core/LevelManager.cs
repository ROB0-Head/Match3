using System.Collections;
using System.Collections.Generic;
using Match3.Settings;
using Scripts;
using Settings;
using TMPro;
using UnityEngine;

namespace Match3.Core
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _movesCountText;
        [SerializeField] private Transform _tasksParent;
        [SerializeField] private SceneTransition _levelComplete;

        private Level _currentLevel;
        private bool _levelCompleted;
        private static int _missionCompetedCount;

        private int _movesCount;
        private List<LevelTaskBox> _taskBoxes = new List<LevelTaskBox>();

        public void SetupCurrentMovesText()
        {
            _movesCount -= 1;
            _movesCountText.text = _movesCount.ToString();
        }

        public static void SetupMission() => _missionCompetedCount++;

        private void Awake()
        {
            _currentLevel = SettingsProvider.Get<LevelsData>().GetCurrentTask();
            foreach (var mission in _currentLevel.Missions)
            {
                var taskBox = Instantiate(SettingsProvider.Get<PrefabSet>().GetPrefab<LevelTaskBox>(), _tasksParent);
                taskBox.Setup(new LevelTaskBoxSettings
                {
                    TileSprite = mission.TileType,
                    TileCount = mission.TargetCount,
                    CurrentTilerCount = 0
                });
                _taskBoxes.Add(taskBox);
            }

            _movesCount = _currentLevel.TotalMoves;
            _movesCountText.text = _movesCount.ToString();
        }

        private void Update()
        {
            if (!_levelCompleted)
            {
                CheckLevelCompletion();
            }
        }

        public void UpdateDestroyedTilesCount(TileTypeData tileType, int destroyedCount)
        {
            foreach (var taskBox in _taskBoxes)
            {
                taskBox.CheckTask(tileType, destroyedCount);
            }

            if (_movesCount <= 0)
            {
                _levelCompleted = true;
                StartCoroutine(ShowLevelUncompletionScreen());
            }
        }

        private void CheckLevelCompletion()
        {
            if (_missionCompetedCount == _currentLevel.Missions.Count)
            {
                _levelCompleted = true;
                StartCoroutine(ShowLevelCompletionScreen());
            }
        }

        private IEnumerator ShowLevelCompletionScreen()
        {
            yield return new WaitForSeconds(2f);
            _levelComplete.PerformTransition();
            SettingsProvider.Get<LevelsData>().SetNextLevelTypes(3,1500);
            _missionCompetedCount = 0;
            Debug.Log("Level Completed!");
        }
        
        private IEnumerator ShowLevelUncompletionScreen()
        {
            yield return new WaitForSeconds(2f);
            _levelComplete.PerformTransition();
            _missionCompetedCount = 0;
            Debug.Log("Level Uncompleted!");
        }
    }
}