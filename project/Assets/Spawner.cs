using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] ennemies;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    public void spawnEnnemy()
    {
        randEnemy = Random.Range(0, 1);

        Vector3 spawnPosition = new Vector3(spawnValues.x, 0, spawnValues.z);

        Instantiate(
            ennemies[randEnemy],
            spawnPosition,
            gameObject.transform.rotation);
    }
}
