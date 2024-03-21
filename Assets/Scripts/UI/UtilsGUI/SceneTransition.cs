using UnityEngine;

namespace Scripts
{
    public class SceneTransition : MonoBehaviour
    {
        public string scene = "<Insert scene name>";
        public float duration = 1.0f;
        public Color color = Color.black;

        public void PerformTransition()
        {
            Transition.LoadLevel(scene, duration, color);
        }
        
        public void QuitGame()
        {
            Debug.Log("Выход из игры...");
            Application.Quit(); // Закрыть приложение
        }
    }
}