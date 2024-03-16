using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class SoundManager : MonoBehaviour
    {
        private Slider m_soundSlider;
        private GameObject m_soundButton;

        private void Start()
        {
            m_soundSlider = GetComponent<Slider>();
            m_soundSlider.value = PlayerPrefs.GetInt("sound_on");
            m_soundButton = GameObject.Find("SoundButton/Button");
        }

        public void SwitchSound()
        {
            AudioListener.volume = m_soundSlider.value;
            PlayerPrefs.SetInt("sound_on", (int)m_soundSlider.value);
            if (m_soundButton != null)
            {
                m_soundButton.GetComponent<SoundButton>().ToggleSprite();
            }
        }
    }
}