using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ShuttleManager shuttleManager;

    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI currentLap;
    public TextMeshProUGUI totalDistance;
    public TextMeshProUGUI totalTime;
    public ProgressBar levelProgress;
    public ProgressBar lapProgress;

    private void Awake()
    {
        shuttleManager.LoadShuttles("shuttles.json");
        shuttleManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel.text = $"Level {shuttleManager.currentLevelNumber}";
        currentLap.text = $"Lap {shuttleManager.currentLap} / {shuttleManager.levelLaps}";

        totalDistance.text = $"{shuttleManager.GetTotalDistance()}m";

        var ts = TimeSpan.FromSeconds(shuttleManager.GetTotalTime());
        totalTime.text = $"{ts.Minutes:00}:{ts.Seconds:00}";

        levelProgress.currentAmount = shuttleManager.GetLevelProgress();
        lapProgress.currentAmount = shuttleManager.GetLapProgress();
    }
}
