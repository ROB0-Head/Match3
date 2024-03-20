using Match3.Settings;
using SaveSystem;
using Settings;
using UnityEngine;
using Utils;

public class GameManager : MonoSingleton<GameManager>
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        SettingsProvider.Get<LevelsData>().Tasks = SaveManager.LoadLevelsData();
    }
}