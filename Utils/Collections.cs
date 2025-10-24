using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story52_Stacking.Utils
{
    public class Collections
    {
        public class CrateCollection
        {
            public Dictionary<string, ServerPickupItemSpawner> crateDict =
                new Dictionary<string, ServerPickupItemSpawner>();
            public void Add(string name, ServerPickupItemSpawner crate)
            {
                crateDict[name] = crate;
            }
            public void Clear()
            {
                crateDict.Clear();
            }
            public string ListAllCrates()
            {
                return string.Join(", ", crateDict.Keys.ToArray());
            }
        }
        public class CounterCollection
        {
            public Dictionary<string, CounterData> counterDict;

            public CounterCollection()
            {
                counterDict = new Dictionary<string, CounterData>
                {
                    {"MeatLeft", new CounterData(new UnityEngine.Vector3(9.6f, 0.0f, 9.6f), null) },
                    {"FireExtLeft", new CounterData(new UnityEngine.Vector3(9.6f, 0.0f, 10.8f), null) },
                    {"PassingMid", new CounterData(new UnityEngine.Vector3(15.6f, 0.0f, 9.6f), null) },
                    {"BunBoxMid", new CounterData(new UnityEngine.Vector3(15.6f, 0.0f, 10.8f), null) },
                    {"LettuceTop", new CounterData(new UnityEngine.Vector3(16.8f, 0.0f, 12.0f), null) },
                    {"ChoppingRight", new CounterData(new UnityEngine.Vector3(21.6f, 0.0f, 8.4f), null) },
                    {"Chopping1N", new CounterData(new UnityEngine.Vector3(21.6f, 0.0f, 9.6f), null) },
                    {"Chopping2N", new CounterData(new UnityEngine.Vector3(21.6f, 0.0f, 10.8f), null) }
                };
            }
        }
        public class CounterData
        {
            public UnityEngine.Vector3 Position { get; set; }
            public ServerAttachStation Counter { get; set; }
            public CounterData(UnityEngine.Vector3 position, ServerAttachStation counter)
            {
                Position = position;
                Counter = counter;
            }
        }
    }
}
