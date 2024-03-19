using System;
using System.Collections.Generic;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class LevelButton : BaseButton
    {
        [SerializeField] private Button _button;
        [SerializeField] private List<GameObject> _stars;
        [SerializeField] private TMP_Text _levelNumber;

        private LevelButtonSettings _settings;
        private AudioSource _audioSource;
        private Animator _animator;
        private int _starsCount;
        private bool _interactable;

        public void Setup(LevelButtonSettings buttonSettings,Action onButtonClick = null)
        {
            if(buttonSettings is not LevelButtonSettings actionButtonSettings)
                return;
            
            _starsCount = buttonSettings.StarsCount;
            _levelNumber.text = buttonSettings.LevelNumber.ToString();
            for (int i = 0; i < _starsCount; i++)
            {
                _stars[i].SetActive(true);
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
        public bool Interactable;
    }
}