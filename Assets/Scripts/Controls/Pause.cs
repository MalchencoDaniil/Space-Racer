using UnityEngine;

public class Pause : MonoBehaviour
{
    public void OnPauseGame()
    {
        Time.timeScale = 0;
    }

    public void OffPauseGame()
    {
        Time.timeScale = 1;
    }
}