using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class StartGamePopup : BasePopup
    {
        private StartGamePopupSettings _settings;

        [SerializeField] private Color _enabledColor;
        [SerializeField] private Color _disabledColor;
        
        [SerializeField] private Image _leftStarImage; 
        [SerializeField] private Image _middleStarImage;
        [SerializeField] private Image _rightStarImage;
        
        public override void Setup(PopupSettings popupSettings)
        {
            base.Setup(popupSettings);
            _settings = (StartGamePopupSettings)popupSettings;
            
            if (_settings.StarsObtained == 0)
            {
                _leftStarImage.color = _disabledColor;
                _middleStarImage.color = _disabledColor;
                _rightStarImage.color = _disabledColor;
            }
            else if (_settings.StarsObtained == 1)
            {
                _leftStarImage.color = _enabledColor;
                _middleStarImage.color = _disabledColor;
                _rightStarImage.color = _disabledColor;
            }
            else if (_settings.StarsObtained == 2)
            {
                _leftStarImage.color = _enabledColor;
                _middleStarImage.color = _enabledColor;
                _rightStarImage.color = _disabledColor;
            }
            else if (_settings.StarsObtained == 3)
            {
                _leftStarImage.color = _enabledColor;
                _middleStarImage.color = _enabledColor;
                _rightStarImage.color = _enabledColor;
            }
        }
    }

    public class StartGamePopupSettings : PopupSettings
    {
        public int StarsObtained;
    }
    
}