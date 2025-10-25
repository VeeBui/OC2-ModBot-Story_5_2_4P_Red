using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Story52_Stacking.Utils
{
    public class LevelInfo
    {
        public string name = "No Level";
        public float timer = -1.0f;
        public float timeleft = -1.0f;
        //public int levelID = -1;
        //public int DLC = -42;

        public Collections.CrateCollection crates = new Collections.CrateCollection();
        public Collections.CounterCollection counters = new Collections.CounterCollection();
        public Collections.CookerCollection cookers = new Collections.CookerCollection();

        public bool inLevel = false;
        private bool foundStations = false;
        public void CheckLevelState()
        {
            bool wasInLevel = inLevel;

            var controller = UnityEngine.Object.FindObjectOfType<ServerKitchenFlowControllerBase>();
            inLevel = (controller != null);
            // check if state changed
            if (inLevel && !wasInLevel)
            {
                // if level state changed to be in level
                GetLevelData();
                
            }
            else if (!inLevel && wasInLevel)
            {
                // if level state changed to have exited level
                name = "No Level";
                timer = -1.0f;
                timeleft = -1.0f;
                foundStations = false;
                crates.Clear();
                counters.Clear();
                cookers.Clear();
                //levelID = -1;
                //DLC = -42;
            }

            // get crates and counters
            if (inLevel && !foundStations)
            {
                // keep trying until found crates and counters

                // get crates
                var levelCrates = UnityEngine.Object.FindObjectsOfType<ServerPickupItemSpawner>();
                var foundCrates = false;
                if (levelCrates != null && levelCrates.Length > 0)
                {
                    int foundCratePrefabsCount = 0;
                    foreach (var crate in levelCrates)
                    {
                        var prefab = crate.GetItemPrefab();
                        if (prefab != null)
                        {
                            string crateName = prefab.name;
                            crates.Add(crateName, crate);
                            //UnityEngine.Debug.Log($"[Story 5-2]: Found {crateName} crate");
                            foundCratePrefabsCount++;
                        }
                    }
                    if (foundCratePrefabsCount == levelCrates.Length) { foundCrates = true; }
                }

                // get counters
                var levelCounters = UnityEngine.Object.FindObjectsOfType<ServerAttachStation>();
                var foundCounters = false;
                // assume if crates are found, then counters can be found too
                if (levelCounters != null && levelCounters.Length > 0 && foundCrates)
                {
                    int foundCountersCount = 0;
                    foreach (var counter in levelCounters)
                    {
                        var counterPosition = counter.transform.position;
                        // check against wanted positions
                        foreach(var kvp in counters.counterDict)
                        {
                            if ((counterPosition - kvp.Value.Position).sqrMagnitude < 0.01f)
                            {
                                //kvp.Value.Counter = counter;
                                counters.Assign(kvp.Key, counter);
                                //UnityEngine.Debug.Log($"[Story 5-2]: Found {kvp.Key} counter at ({kvp.Value.Position})");
                                foundCountersCount++;
                            }
                        }
                    }
                    if (foundCountersCount == counters.counterDict.Count) { foundCounters = true; }
                }

                // get cookers
                var levelCookConts = UnityEngine.Object.FindObjectsOfType<ServerCookableContainer>();
                var levelIngrConts = UnityEngine.Object.FindObjectsOfType<ServerIngredientContainer>();
                var foundCookers = false;
                // assume if crates are found, then cookers can be found too
                if (foundCrates)
                {
                    int foundCookContCount = 0;
                    foreach (var cookCont in levelCookConts)
                    {
                        var cookContPosition = cookCont.transform.position;
                        // check against wanted positions
                        foreach (var kvp in cookers.cookerDict)
                        {
                            if ((cookContPosition - kvp.Value.Position).sqrMagnitude < 0.01f)
                            {
                                // Assign the cook_container
                                cookers.Assign(kvp.Key, cookCont);
                                foundCookContCount++;
                            }
                        }
                    }
                    int foundIngrContCount = 0;
                    foreach (var ingrCont in levelIngrConts)
                    {
                        var ingrContPosition = ingrCont.transform.position;
                        // check against wanted positions
                        foreach (var kvp in cookers.cookerDict)
                        {
                            if ((ingrContPosition - kvp.Value.Position).sqrMagnitude < 0.01f)
                            {
                                // Assign the ingr_container
                                cookers.Assign(kvp.Key, ingrCont);
                                foundIngrContCount++;
                            }
                        }
                    }
                    if (foundCookContCount == cookers.cookerDict.Count && foundIngrContCount == cookers.cookerDict.Count) { foundCookers = true; }
                }

                foundStations = foundCrates && foundCounters && foundCookers;
            }
        }
        public void GetLevelData()
        {
            // get the level name
            var levelConfig = GameUtils.GetLevelConfig();
            if (levelConfig != null)
            {
                name = levelConfig.name;                
            }
            // some other stuff
            /*
            GameSession gameSession = GameUtils.GetGameSession();
            if (gameSession != null)
            {
                DLC = gameSession.DLC;
            }
            levelID = GameUtils.GetLevelID();
            */
        }
        public void UpdateLevelData()
        {
            var controller = UnityEngine.Object.FindObjectOfType<ServerKitchenFlowControllerBase>();
            if (controller != null)
            {
                timer = controller?.RoundTimer?.TimeElapsed ?? -1.0f;
            }
        }

    }
}
