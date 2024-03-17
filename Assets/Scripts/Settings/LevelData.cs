using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = "Match3/Level Data")]
    public class LevelData : ScriptableObject
    {
        [System.Serializable]
        public class Task
        {
            public CrystalData crystalType;
            public int targetCount;
        }

        public Task[] tasks;
        public GameObject[] obstacles;
    }
}