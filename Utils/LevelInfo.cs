using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story52_Stacking.Utils
{
    public class LevelInfo
    {
        public string name = "No Level";
        public float timer = -1.0f;
        //public int levelID = -1;
        //public int DLC = -42;

        public bool inLevel = false;
        public void CheckLevelState(ServerKitchenFlowControllerBase controller)
        {
            bool wasInLevel = inLevel;
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
                //levelID = -1;
                //DLC = -42;
            }

            //
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
        public void UpdateLevelData(ServerKitchenFlowControllerBase controller)
        {
            if (controller != null)
            {
                timer = controller?.RoundTimer?.TimeElapsed ?? -1.0f;
            }
        }

    }
}
