using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using UnityEngine;
using Team17.Online.Multiplayer.Messaging;

namespace Story52_Stacking.Utils
{
    internal class Actions
    {
        /// <summary>
        /// Empties all cooking containers such as pots and pans,
        /// once they are finished cooking
        /// i.e. once the cooking progress > 12.0
        /// </summary>
        /// <param name="ck_containers">
        /// Array of cooking containers to check.
        /// </param>
        /// <param name="in_containers">
        /// Array of ingredient containers to check.
        /// </param>
        public void EmptyAllPots(ServerCookableContainer[] ck_containers, ServerIngredientContainer[] in_containers)
        {
            foreach (var pot in ck_containers)
            {
                // values
                var cookingProgress = pot.GetCookingHandler().GetCookingProgress();
                var pan_position = pot.transform.position;

                // if finished cooking
                if (cookingProgress > 12.0)
                {
                    // Check against the containers
                    foreach (var container in in_containers)
                    {
                        var containerPosition = container.transform.position;

                        // then empty it
                        if (containerPosition == pan_position)
                        {
                            container.Empty();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Returns an ingredient crate.
        /// </summary>
        /// <param name="ingredientName">
        /// The name of the ingredient wanted.
        /// </param>
        /// <param name="spawnables">
        /// Array of ingredient containers to check.
        /// </param>
        public ServerPickupItemSpawner FindCrate(
            string ingredientName,
            ServerPickupItemSpawner[] spawnables)
        {
            foreach (var crate in spawnables)
            {
                if (crate.GetItemPrefab().name == ingredientName)
                {
                    return crate;
                }
            }
            return null;
        }
        /// <summary>
        /// Returns an ingredient with a rigid body.
        /// </summary>
        /// <param name="ingredientName">
        /// The name of the ingredient wanted.
        /// </param>
        /// <param name="spawnables">
        /// Array of ingredient containers to check.
        /// </param>
        /// <param name="position">
        /// Where to spawn the item.
        /// </param>
        /// <param name="worked">
        /// true if worked/chopped, false if not.
        /// </param>
        public GameObject SpawnIngredient(
            string ingredientName,
            ServerPickupItemSpawner[] spawnables,
            Vector3 position,
            Boolean worked = false)
        {
            ServerPickupItemSpawner crate = FindCrate(ingredientName, spawnables);
            if (crate != null)
            {
                var spawnedEntry = NetworkUtils.ServerSpawnPrefab(
                    crate.gameObject,
                    crate.GetItemPrefab(),
                    position,
                    Quaternion.identity);
                if (spawnedEntry != null)
                {
                    if (worked)
                    {
                        // get the worked item (assume first index)
                        var spawnerEntity = EntitySerialisationRegistry.GetEntry(spawnedEntry);
                        var spawnableEntities = spawnerEntity.m_GameObject.GetComponent<SpawnableEntityCollection>();
                        var spawnablesField = typeof(SpawnableEntityCollection).GetField(
                            "m_spawnables",
                            BindingFlags.NonPublic |
                            BindingFlags.Instance);
                        var spawnableList = (List<GameObject>)spawnablesField.GetValue(spawnableEntities);
                        var choppedPrefab = spawnableList[0];

                        // spawn the worked item
                        spawnedEntry = NetworkUtils.ServerSpawnPrefab(
                                spawnerEntity.m_GameObject,
                                choppedPrefab,
                                position,
                                Quaternion.identity
                                );

                        // destroy the non-worked item
                        NetworkUtils.DestroyObject(spawnerEntity.m_GameObject);
                    }
                    // give rigid body
                    if (spawnedEntry.GetComponent<ServerPhysicalAttachment>() is ServerPhysicalAttachment spa)
                    {
                        spa.ManualEnable();
                        return spawnedEntry;
                    }
                }
            }
            return null;
        }
    }
}
