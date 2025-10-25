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
            public void Clear()
            {
                foreach (var counterName in counterDict.Keys)
                {
                    counterDict[counterName].Counter = null;
                }
            }
            public void Assign(string counterName, ServerAttachStation counter)
            {
                counterDict[counterName].Counter = counter;
            }
            public ServerAttachStation Retrieve(string counterName)
            {
                return counterDict[counterName].Counter;
            }
            public string ListFoundCrates()
            {
                StringBuilder sb = new StringBuilder();
                bool isFirst = true;

                foreach (var kvp in counterDict)
                {
                    if (kvp.Value.Counter != null)
                    {
                        if (!isFirst)
                        {
                            sb.Append(", ");
                        }
                        sb.Append(kvp.Key);
                        isFirst = false;
                    }
                }
                return sb.ToString();
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
        public class CookerCollection
        {
            public Dictionary<string, CookerData> cookerDict;

            public CookerCollection()
            {
                cookerDict = new Dictionary<string, CookerData>
                {
                    {"LeftPan", new CookerData(new UnityEngine.Vector3(9.6f, 0.6f, 8.4f), null, null) },
                    {"TopPan", new CookerData(new UnityEngine.Vector3(18.0f, 0.6f, 12.0f), null, null) },
                    {"RightPan", new CookerData(new UnityEngine.Vector3(21.6f, 0.6f, 6.0f), null, null) }
                };
            }
            public void Clear()
            {
                foreach (var cookerName in cookerDict.Keys)
                {
                    cookerDict[cookerName].Cook_Container = null;
                    cookerDict[cookerName].Ingr_Container = null;
                }
            }
            /*
            public void Assign<T>(string cookerName, T container) where T :class
            {
                if (typeof(T) == typeof(ServerCookableContainer))
                {
                    cookerDict[cookerName].Cook_Container = container as ServerCookableContainer;
                }
                else if (typeof(T) == typeof(ServerIngredientContainer))
                {
                    cookerDict[cookerName].Ingr_Container = container as ServerIngredientContainer;
                }
            }
            */
            public void Assign(string cookerName, ServerCookableContainer cook_container, ServerIngredientContainer ingr_container)
            {
                cookerDict[cookerName].Cook_Container = cook_container;
                cookerDict[cookerName].Ingr_Container = ingr_container;
            }
            public void Assign(string cookerName, ServerCookableContainer cook_container)
            {
                cookerDict[cookerName].Cook_Container = cook_container;
            }
            public void Assign(string cookerName, ServerIngredientContainer ingr_container)
            {
                cookerDict[cookerName].Ingr_Container = ingr_container;
            }
            public T Retrieve<T>(string cookerName) where T : class
            {
                // Usage
                // var cookContainer = cookers.Retrieve<ServerCookableContainer>("PanLeft");
                // var ingrContainer = cookers.Retrieve<ServerIngredientContainer>("PanLeft");

                if (!cookerDict.ContainsKey(cookerName))
                    return null;

                if (typeof(T) == typeof(ServerCookableContainer))
                {
                    return cookerDict[cookerName].Cook_Container as T;
                }
                else if (typeof(T) == typeof(ServerIngredientContainer))
                {
                    return cookerDict[cookerName].Ingr_Container as T;
                }

                return null;
            }
            public string ListFoundCookers()
            {
                StringBuilder sb = new StringBuilder();
                bool isFirst = true;

                foreach (var kvp in cookerDict)
                {
                    if (kvp.Value.Cook_Container != null && kvp.Value.Ingr_Container != null)
                    {
                        if (!isFirst)
                        {
                            sb.Append(", ");
                        }
                        sb.Append(kvp.Key);
                        isFirst = false;
                    }
                }
                return sb.ToString();
            }
        }
        public class CookerData
        {
            public UnityEngine.Vector3 Position { get; set; }
            public ServerCookableContainer Cook_Container { get; set; }
            public ServerIngredientContainer Ingr_Container { get; set; }
            public CookerData(UnityEngine.Vector3 position, 
                ServerCookableContainer cook_container,
                ServerIngredientContainer ingr_container)
            {
                Position = position;
                Cook_Container = cook_container;
                Ingr_Container = ingr_container;
            }
        }
    }
}
