using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the spawners for the strawberries
/// </summary>
public class SpawnerManagementStrawberries : MonoBehaviour
{
    /// <summary>
    /// List of the strawberries spawners
    /// </summary>
    private List<GameObject> spawns = new List<GameObject>();

    /// <summary>
    /// Start method
    /// Get all the strawberries spawners and add them to the list
    /// then start the coroutine.
    /// </summary>
    void Start()
    {
        // Add all the spawn spots
        foreach (Transform child in transform)
            spawns.Add(child.gameObject);
        // launch the fruit generation
        StartCoroutine(GenerateFruit());
    }

    /// <summary>
    /// Generate the fruits
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateFruit()
    {
        // while the program is running
        while (true)
        {
            // suspends the execution during 5 seconds
            yield return new WaitForSeconds(5);
            // determine randomly in which spawner the strawberry will spawn
            var random = new System.Random();
            // call the method responsible of instantiating a fruit
            spawns[random.Next(spawns.Count)].GetComponent<SpawnerStrawberries>().spawnStrawberries();
            yield return new WaitForSeconds(5);
        }
    }
}