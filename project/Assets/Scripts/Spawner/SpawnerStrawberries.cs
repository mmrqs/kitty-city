using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible to make strawberries spawn
/// </summary>
public class SpawnerStrawberries : MonoBehaviour
{
    /// <summary>
    /// Fruit to instanciate
    /// </summary>
    public GameObject fruit;

    /// <summary>
    /// Instanciate strawberries around the tree.
    /// </summary>
    public void spawnStrawberries()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(1, 5), transform.position.y, transform.position.z + Random.Range(1, 5));

        Instantiate(
            fruit,
            spawnPosition,
            gameObject.transform.rotation);
    }
}
