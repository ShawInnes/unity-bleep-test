using System;
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
    public float LapTime { get; set; }
    public double Speed { get; set; }
    public int TotalDistance { get; set; }
    public float TotalTime { get; set; }
}

public class ShuttleManager : MonoBehaviour
{
    public bool isLoaded = false;
    public int totalLevels;
    public int currentLevelNumber;

    public int levelLaps;
    public int currentLap;

    public bool isStarted;

    public AudioManager audioManager;

    private List<ShuttleLevel> _shuttles = new List<ShuttleLevel>();
    private ShuttleLevel _currentLevel;

    private int _totalDistance;
    private float _targetTime;
    private float _totalTime;

    public void Initialize()
    {
        if (!isLoaded)
        {
            Debug.LogWarning("Cannot do Initialize, data not loaded");
            return;
        }

        totalLevels = _shuttles.Count;

        _currentLevel = _shuttles.OrderBy(p => p.Level).First();
        currentLevelNumber = _currentLevel.Level;
        levelLaps = _currentLevel.Laps;
        _targetTime = _currentLevel.LapTime;

        currentLap = 1;
        _totalDistance = 0;
        _totalTime = 0;
    }

    public void LoadShuttles(string fileName)
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            Debug.Log($"Loading shuttle data from {fileName}");
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

        if (currentLevelNumber == totalLevels)
        {
            Debug.LogWarning("Cannot do NextLevel, already at highest level");
            return;
        }

        currentLevelNumber++;
        currentLap = 1;

        StartLevel();
    }

    public void StartLevel()
    {
        _currentLevel = _shuttles.Single(p => p.Level == currentLevelNumber);
        levelLaps = _currentLevel.Laps;

        audioManager.DoLevel(currentLevelNumber);
    }

    public int GetTotalDistance()
    {
        return _totalDistance;
    }

    public float GetLapProgress()
    {
        return 1 - (_targetTime / _currentLevel.LapTime);
    }

    public float GetLevelProgress()
    {
        return (currentLap - 1.0f) / _currentLevel.Laps;
    }

    public float GetTotalTime()
    {
        return _totalTime;
    }

    public void NextLap()
    {
        currentLap++;
        _totalDistance += _currentLevel.Distance;

        if (currentLap > _currentLevel.Laps)
        {
            NextLevel();
        }
        else
        {
            audioManager.DoBeep();
        }
    }

    public void DoStart()
    {
        isStarted = true;

        StartLevel();

        audioManager.DoLevel(currentLevelNumber);
    }

    public void DoStop()
    {
        isStarted = false;
    }

    private void Update()
    {
        if (_currentLevel != null && isStarted)
        {
            _targetTime -= Time.deltaTime;
            _totalTime += Time.deltaTime;

            if (_targetTime <= 0)
            {
                _targetTime = _currentLevel.LapTime;

                NextLap();
            }
        }
    }
}
