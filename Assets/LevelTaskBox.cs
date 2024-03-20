using Match3;
using Match3.Core;
using Settings;
using TMPro;
using UI.Prefabs;
using UnityEngine;
using UnityEngine.UI;

public class LevelTaskBox : BasePrefabs
{
    [SerializeField] private Image _tileImage;
    [SerializeField] private TMP_Text _missionCountText;
    [SerializeField] private TMP_Text _currentTileCountText;
    [SerializeField] private GameObject _checkedIcon;

    private int _currentTileCount;
    private int _missionTileCount;
    private LevelTaskBoxSettings _settings;
   

    public void SetupCurrentTileText(int count)
    {
        _currentTileCount += count;
        _currentTileCountText.text = _currentTileCount.ToString();
        if (this != null)
        {
            if (_currentTileCount >= _missionTileCount && !_checkedIcon.activeSelf)
            {
                _checkedIcon.SetActive(true);
                LevelManager.SetupMission();
            }
        }
    } 
    
    public void Setup(LevelTaskBoxSettings settings)
    {
        _settings = settings;
        _checkedIcon.SetActive(false);
        _tileImage.sprite = SettingsProvider.Get<TileTypeDatas>().GetSpriteForTileType(settings.TileSprite) ;
        _currentTileCount = settings.CurrentTilerCount;
        _missionTileCount = settings.TileCount;
        _missionCountText.text = _missionTileCount.ToString();
        SetupCurrentTileText(_currentTileCount);
    }

    public void CheckTask(TileTypeData tileType, int destroyedCount)
    {
        if (tileType.TileType == _settings.TileSprite)
            SetupCurrentTileText(destroyedCount);
    }
    
}

public class LevelTaskBoxSettings
{
    public ETileType TileSprite;
    public int TileCount;
    public int CurrentTilerCount;
}
