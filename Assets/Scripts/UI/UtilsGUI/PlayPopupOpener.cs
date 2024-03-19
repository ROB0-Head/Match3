using UnityEngine;

namespace Scripts
{
    public class PlayPopupOpener : PopupOpener
    {
        private int starsObtained = 0;

        public override void OpenPopup()
        {
            var popup = Instantiate(popupPrefab, m_canvas.transform, false) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;

            var playPopup = popup.GetComponent<PlayPopup>();
            playPopup.Open();
            playPopup.SetAchievedStars(starsObtained);
        }
    }
}