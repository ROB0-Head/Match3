using System;
using System.Collections.Generic;
using Match3;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class LevelButton : BaseButton
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _levelSprite;
        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private Color _currentStateColor;
        [SerializeField] private GameObject _shine;
        [SerializeField] private GameObject _lockedState;
        [SerializeField] private List<Image> _stars;

        private LevelButtonSettings _settings;
        private AudioSource _audioSource;
        private Animator _animator;
        
        private int _starsCount;
        private ELevelButtonType _levelButtonType;
        
        public void Setup(LevelButtonSettings buttonSettings, Action onButtonClick = null)
        {
            if(buttonSettings is not LevelButtonSettings actionButtonSettings)
                return;
            
            _levelNumber.text = buttonSettings.LevelNumber.ToString();
            _starsCount = buttonSettings.StarsCount;
            _levelButtonType = buttonSettings.LevelButtonType;
            
            switch (_levelButtonType)
            {
                case ELevelButtonType.Current:
                    _levelSprite.sprite = buttonSettings.LevelSprite;
                    _shine.SetActive(true);
                    _lockedState.SetActive(false);
                    
                    _levelNumber.color = _currentStateColor;
                    _button.interactable = true;
                    
                    _stars.ForEach(star => star.gameObject.SetActive(true));
                    for (int i = 0; i < _starsCount; i++)
                    {
                        _stars[i].sprite = buttonSettings.StarsSprite[i];
                    }
                    break;
                
                case ELevelButtonType.Unlocked:
                    _lockedState.SetActive(false);
                    _button.interactable = true;
                    
                    _stars.ForEach(star => star.gameObject.SetActive(true));
                    for (int i = 0; i < _starsCount; i++)
                    {
                        _stars[i].sprite = buttonSettings.StarsSprite[i];
                    }
                    
                    break;
            }
            
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _button.onClick.AddListener(OpenStartGamePopup);    
        }

        private void OpenStartGamePopup()
        {
            _animator.SetTrigger("Pressed");
            
            if (_audioSource != null)
                _audioSource.Play();
            
            PopupSystem.ShowPopup<StartGamePopup>(new StartGamePopupSettings()
            {
                StarsObtained =  _starsCount
            });
        }
    }
    
    [Serializable]
    public class LevelButtonSettings : ButtonSettings
    {
        public int LevelNumber;
        public int StarsCount;
        public Sprite LevelSprite;
        public List<Sprite> StarsSprite;
        public ELevelButtonType LevelButtonType;
    }

    public enum ELevelButtonType
    {
        None,
        Locked,
        Unlocked,
        Current
    }
}