﻿using UnityEngine;

namespace Scripts
{ 
    public class MuteBackgroundMusic : MonoBehaviour
    {
        private BackgroundMusic m_bgMusic;

        private void Awake()
        {
            var backgroundMusic = GameObject.Find("BackgroundMusic");
            if (backgroundMusic != null)
            {
                m_bgMusic = backgroundMusic.GetComponent<BackgroundMusic>();
                if (m_bgMusic != null)
                    m_bgMusic.FadeOut();
            }
        }

        private void OnDestroy()
        {
            if (m_bgMusic != null)
                m_bgMusic.FadeIn();
        }
    }
}