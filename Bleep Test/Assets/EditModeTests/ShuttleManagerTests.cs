using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ShuttleManagerTests
    {
        [Test]
        public void ShuttleManager_LoadsData()
        {
            var shuttleManager = new ShuttleManager();

            shuttleManager.LoadShuttles("shuttles.json");

            Assert.IsTrue(shuttleManager.isLoaded);
        }

        [Test]
        public void ShuttleManager_Initializes()
        {
            var shuttleManager = new ShuttleManager();

            shuttleManager.LoadShuttles("shuttles.json");

            Assert.AreEqual(0, shuttleManager.currentLevelNumber);
            Assert.AreEqual(0, shuttleManager.levelLaps);
            Assert.AreEqual(0, shuttleManager.currentLap);

            shuttleManager.Initialize();

            Assert.AreEqual(21, shuttleManager.totalLevels);
            Assert.AreEqual(1, shuttleManager.currentLevelNumber);
            Assert.AreEqual(7, shuttleManager.levelLaps);
            Assert.AreEqual(1, shuttleManager.currentLap);
        }

        [Test]
        public void ShuttleManager_NextLevel()
        {
            var shuttleManager = new ShuttleManager();

            shuttleManager.LoadShuttles("shuttles.json");
            shuttleManager.Initialize();

            Assert.AreEqual(1, shuttleManager.currentLevelNumber);
            Assert.AreEqual(7, shuttleManager.levelLaps);
            Assert.AreEqual(1, shuttleManager.currentLap);

            shuttleManager.NextLevel();

            Assert.AreEqual(2, shuttleManager.currentLevelNumber);
            Assert.AreEqual(8, shuttleManager.levelLaps);
            Assert.AreEqual(1, shuttleManager.currentLap);
        }

        [Test]
        [TestCase(6, 1, 120)]
        [TestCase(7, 2, 140)]
        [TestCase(15, 3, 300)]
        public void ShuttleManager_NextLap(int laps, int expectedLevel, int expectedDistance)
        {
            var shuttleManager = new ShuttleManager();

            shuttleManager.LoadShuttles("shuttles.json");
            shuttleManager.Initialize();

            for (int i = 0; i < laps; i++)
                shuttleManager.NextLap();

            Assert.AreEqual(expectedLevel, shuttleManager.currentLevelNumber);
            Assert.AreEqual(expectedDistance, shuttleManager.GetTotalDistance());
        }
    }
}
