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
    [BepInPlugin("MyGUID", "Story 5-2 Stacking", "1.0.1")]
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
            //var controller = FindObjectOfType<ServerKitchenFlowControllerBase>();
            try
            {
                levelInfo.CheckLevelState();
                if (levelInfo.inLevel)
                {
                    levelInfo.UpdateLevelData();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error finding controller/level config: {ex}");
            }

            // Now with level info being tracked properly, start doing stuff
            if (levelInfo.name == "Balloon_5_2_2P" && levelInfo.timer > 0.1f)
            {
                /*
                 * Sequences and timing are based on the following video/run
                 * https://www.bilibili.com/video/BV1Ru4y1j7bU/
                 */

                switch (phase)
                {
                    case 0:
                        // Phase 0 = starting sequence

                        // 0) Set up lettuce
                        if (!setLettuce)
                        {

                        }
                        else
                        {
                            // 1) Player passes one meat to left
                            //    Detect then destroy it
                            //    Then spawn a chopped meat into the left pan at 3:58 seconds
                            //      i.e. timer = 2.0f.... maybe 2.25f

                            // 2) Player chops one meat at normal speed
                            //    When chopped, destroy it and spawn a chopped meat into right pan

                            // 3) Player chops one meat at double speed
                            //    When chopped, destroy it and spawn a chopped meat into top pan

                            // 4) Player starts chopping one meat (numStages = 1)
                            //    In game, the yellow player will finish chopping this one
                            //    When chopped, destroy it and spawn a chopped meat on +1N counter

                            // 5) While "yellow is chopping", 
                            //    player moves fire extinguisher and throws meat
                            //    Detect the fire extinguisher, use takeitem

                            // 6) Player chops one meat at double speed
                            //    When chopped, destroy it and spawn it above top pan

                            // 7) Player will chop one meat, stacking on left pan
                            //    Left pan emptying will need to start from here
                            //    Increase meat count

                            // 8) Player will chop one meat, stacking bottom pan
                            //    and bun bottom pan, then top pan
                            //    Burger removal will need to start from here

                            // 9) Player will chop one meat at double speed
                            //    When chopped, destroy it and spawn a chopped meat on +2N counter

                            // 10)Player will quarter chop one meat (num stages = 2) then stack +1N meat
                            //    When +1N meat removed, destroy meat 9 and move to +1N counter

                            // 11)Player will place down meat
                            //    When detected, destroy it, spawn a chopped meat
                            //    When +1N meat removed, destroy meat 9 and move to +1N counter
                        }



                        break;
                    case 1:
                        // Phase 1 = normal run, stack and bun
                        break;
                    case 2:
                        // Phase 2 = ending sequence

                        // last meat at around 20 seconds

                        // chop two lettuce (another one will need to be spawned)

                        // use fire extinguisher (returned on bun box at 12-11 seconds left, i.e timer = 228.5f)

                        // bun top burger, then bottom burger, then pass lettuce

                        // help wash (detect be in washing area?)
                        break;
                    default:
                        break;
                }
            }
            
        }
    }
}