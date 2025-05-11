using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;

    public int maxEnemySpawn = 5;

    public float radius = 10f;
    public float spawnTime = 0;
    public float maxSpawnTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= maxSpawnTime)
            {
                spawnTime = 0;
                int spanwNumber = Random.Range(1, maxEnemySpawn);
                for(int i = 0; i < spanwNumber; i++)
                {
                    Vector3 spawnPosition = (Vector3)(radius * UnityEngine.Random.insideUnitCircle);
                    spawnPosition.y = player.transform.position.y -10;
                    Vector3 target = player.transform.position + spawnPosition;
                    Instantiate(enemyPrefab, target, Quaternion.identity);
                }
            }
        }
    }
}
