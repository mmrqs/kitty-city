using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagementStrawberries : MonoBehaviour
{
    private List<GameObject> spawns = new List<GameObject>();

    void Start()
    {
        // Add all the spawn spots
        foreach (Transform child in transform)
            spawns.Add(child.gameObject);

        StartCoroutine(GenerateFruit());
    }

    IEnumerator GenerateFruit()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            var random = new System.Random();
            spawns[random.Next(spawns.Count)].GetComponent<SpawnerStrawberries>().spawnStrawberries();
            yield return new WaitForSeconds(5);
        }
    }
}