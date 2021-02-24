using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerStrawberries : MonoBehaviour
{
    public GameObject fruit;

    public void spawnStrawberries()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(1, 5), transform.position.y, transform.position.z + Random.Range(1, 5));

        Instantiate(
            fruit,
            spawnPosition,
            gameObject.transform.rotation);
    }
}
