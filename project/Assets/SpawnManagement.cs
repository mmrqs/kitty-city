using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManagement : MonoBehaviour
{
    private List<GameObject> spawns = new List<GameObject>();
    public int numberOfEnnemies;

    // Start is called before the first frame update
    void Start()
    {
        // Add all the spawn spots
        foreach (Transform child in transform)
            spawns.Add(child.gameObject);
        StartCoroutine(GenerateEnnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GenerateEnnemy()
    {
        while(true)
        {
            if(GetNumberOfEnnemies() < numberOfEnnemies)
            {
                yield return new WaitForSeconds(5);
                var random = new System.Random();
                spawns[random.Next(spawns.Count)].GetComponent<Spawner>().spawnEnnemy();
                yield return new WaitForSeconds(5);
            }           
            yield return null;
        }       
    }

    private int GetNumberOfEnnemies()
    {
        return GameObject.FindGameObjectsWithTag("CatEnnemy").Length;
    }
}
