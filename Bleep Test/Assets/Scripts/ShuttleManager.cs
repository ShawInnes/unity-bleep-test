using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

/*
  {
    "level": 1,
    "laps": 7,
    "distance": 20,
    "speed": 8,
    "totalDistance": 140,
    "lapTime": 9,
    "totalTime": 63
  }
 */

[Serializable]
public class ShuttleLevel
{
    public int Level { get; set; }
    public int Laps { get; set; }
    public int Distance { get; set; }
    public double LapTime { get; set; }
    public double Speed { get; set; }
    public int TotalDistance { get; set; }
    public double TotalTime { get; set; }
}

public class ShuttleManager : MonoBehaviour
{
    public bool isLoaded = false;
    public int totalLevels;
    public int currentLevel;

    public int levelLaps;
    public int currentLap;

    private List<ShuttleLevel> _shuttles = new List<ShuttleLevel>();

    public void Initialize()
    {
        if (!isLoaded)
        {
            Debug.LogWarning("Cannot do Initialize, data not loaded");
            return;
        }

        totalLevels = _shuttles.Count;
        currentLevel = _shuttles.Min(p => p.Level);
        levelLaps = _shuttles.Single(p => p.Level == currentLevel).Laps;
        currentLap = 1;
    }

    public void LoadShuttles(string fileName)
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            _shuttles = JsonConvert.DeserializeObject<List<ShuttleLevel>>(dataAsJson);
            isLoaded = true;
        }
        else
        {
            Debug.LogError("Cannot load shuttle data!");
        }
    }

    public void NextLevel()
    {
        if (!isLoaded)
        {
            Debug.LogWarning("Cannot do NextLevel, data not loaded");
            return;
        }

        if (currentLevel == totalLevels) {
            Debug.LogWarning("Cannot do NextLevel, already at highest level");
            return;
        }

        currentLevel++;
        levelLaps = _shuttles.Single(p => p.Level == currentLevel).Laps;
        currentLap = 1;
    }

    public void NextLap()
    {

    }
}
