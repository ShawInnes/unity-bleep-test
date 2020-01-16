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
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            var shuttleManager = new ShuttleManager();

            shuttleManager.LoadShuttles("shuttles.json");

            Assert.IsTrue(shuttleManager.isLoaded);
        }

        [Test]
        public void ShuttleManager_Initializes()
        {
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            var shuttleManager = new ShuttleManager();

            shuttleManager.LoadShuttles("shuttles.json");

            Assert.AreEqual(0, shuttleManager.currentLevel);
            Assert.AreEqual(0, shuttleManager.levelLaps);
            Assert.AreEqual(0, shuttleManager.currentLap);

            shuttleManager.Initialize();

            Assert.AreEqual(21, shuttleManager.totalLevels);
            Assert.AreEqual(1, shuttleManager.currentLevel);
            Assert.AreEqual(7, shuttleManager.levelLaps);
            Assert.AreEqual(1, shuttleManager.currentLap);
        }
    }
}
