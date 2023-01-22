using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Spawn Enemy 
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyContainer;

    // Spawn Power-up
    [SerializeField]
    private GameObject[] powerups;

    private bool stopSpawning = false;

    public void Spawnings()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);

        while (stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 5, 0);

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(2.5f);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3);

        while (stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 5, 0);

            GameObject tripleshot = Instantiate(powerups[Random.Range(0,3)], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3,7));
        }
    }

}
