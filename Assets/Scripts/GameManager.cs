using UnityEngine;
using Utils;

public class GameManager : MonoSingleton<GameManager>
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}