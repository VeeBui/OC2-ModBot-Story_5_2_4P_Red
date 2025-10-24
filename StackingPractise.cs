using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using UnityEngine;
using Story52_Stacking.Utils;
using Team17.Online.Multiplayer.Messaging;
using HarmonyLib;

namespace Story52_Stacking
{
    [BepInPlugin("MyGUID", "Story 5-2 Stacking", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        // Bring in functions
        private Actions actions = new Actions();
        private LevelInfo levelInfo = new LevelInfo();
        // Constants for comparison
        Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f); // items are usually 0.5 above counters
        const float tol = 0.01f;

        // global variables
        bool setLettuce = false;
        int meatCount = 0;
        int phase = 0;
        private void Awake()
        {
            // Plugin startup logic
            Debug.Log($"Plugin {"MyGUID"} is loaded!");
            Debug.Log("Hello world from Stacking Practise Mod/Bot (Story 5-2)");

        }
        private void Update()
        {
            // always check if we are in the level
            var controller = FindObjectOfType<ServerKitchenFlowControllerBase>();
            try
            {
                levelInfo.CheckLevelState(controller);
                if (levelInfo.inLevel)
                {
                    levelInfo.UpdateLevelData(controller);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error finding controller/level config: {ex}");
            }

            // Now with level info being tracked properly, start doing stuff
            if (levelInfo.name == "Balloon_5_2_2P" && levelInfo.timer > 0.1f)
            {
                Logger.LogInfo("In level");

                switch (phase)
                {
                    case 0:
                        // Phase 0 = starting sequence
                        break;
                    case 1:
                        // Phase 1 = normal run, stack and bun
                        break;
                    case 2:
                        // Phase 2 = ending sequence
                        break;
                    default:
                        break;
                }
            }
            
        }
    }
}