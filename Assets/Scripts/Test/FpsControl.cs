using TMPro;
using UnityEngine;

public class FpsControl : MonoBehaviour
{
    private float accum = 0, frames = 0, timeleft;
    private float currentFPS, highestFPS = 0, lowestFPS = 1000;

    [Header("FPS Display")]
    [SerializeField] private float updateInterval = 0.5f;
    [SerializeField] private TextMeshProUGUI fpsText;

    [Space(8)]
    [SerializeField] private bool showAverage = true;
    [SerializeField] private bool showPeak = true;

    [Header("FPS Limit")]
    [SerializeField] private bool limitFPS = false;
    [SerializeField] private int targetFPS = 60;

    private void Start()
    {
        timeleft = updateInterval;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

    private void Update()
    {
        if (limitFPS && Application.targetFrameRate != targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }
        else if (!limitFPS && Application.targetFrameRate != -1)
        {
            Application.targetFrameRate = -1;
        }

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            currentFPS = accum / frames;
            string _format = System.String.Format("{0:F2} FPS", currentFPS);

            if (currentFPS > highestFPS)
            {
                highestFPS = currentFPS;
            }

            if (currentFPS < lowestFPS)
            {
                lowestFPS = currentFPS;
            }

            if (fpsText != null)
            {
                fpsText.text = _format;
            }

            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}