using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ShuttleManager shuttleManager;

    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI currentLap;
    public TextMeshProUGUI currentSpeed;
    public TextMeshProUGUI totalDistance;
    public TextMeshProUGUI totalTime;
    public ProgressBar levelProgress;
    public ProgressBar lapProgress;

    public GameObject startButton;
    public GameObject stopButton;

    private void Awake()
    {
        shuttleManager.LoadShuttles("shuttles.json");
        shuttleManager.Initialize();

        startButton.SetActive(true);
        stopButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel.text = $"Level {shuttleManager.currentLevelNumber}";
        currentSpeed.text = $"{shuttleManager.currentSpeed:0.0} km/h";
        currentLap.text = $"Lap {shuttleManager.currentLap} / {shuttleManager.levelLaps}";

        totalDistance.text = $"{shuttleManager.GetTotalDistance()}m";

        var ts = TimeSpan.FromSeconds(shuttleManager.GetTotalTime());
        totalTime.text = $"{ts.Minutes:00}:{ts.Seconds:00}";

        levelProgress.currentAmount = shuttleManager.GetLevelProgress();
        lapProgress.currentAmount = shuttleManager.GetLapProgress();

        if (shuttleManager.isStarted)
        {
            levelProgress.useSmoothing = true;
            startButton.SetActive(false);
            stopButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(true);
            stopButton.SetActive(false);
        }
    }
}
