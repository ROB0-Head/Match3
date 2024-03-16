using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class PlayPopup : Popup
    {
        public Color enabledColor;
        public Color disabledColor;

        public Image leftStarImage;
        public Image middleStarImage;
        public Image rightStarImage;

        public void SetAchievedStars(int starsObtained)
        {
            if (starsObtained == 0)
            {
                leftStarImage.color = disabledColor;
                middleStarImage.color = disabledColor;
                rightStarImage.color = disabledColor;
            }
            else if (starsObtained == 1)
            {
                leftStarImage.color = enabledColor;
                middleStarImage.color = disabledColor;
                rightStarImage.color = disabledColor;
            }
            else if (starsObtained == 2)
            {
                leftStarImage.color = enabledColor;
                middleStarImage.color = enabledColor;
                rightStarImage.color = disabledColor;
            }
            else if (starsObtained == 3)
            {
                leftStarImage.color = enabledColor;
                middleStarImage.color = enabledColor;
                rightStarImage.color = enabledColor;
            }
        }
    }
}