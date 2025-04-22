using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;

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
                Vector3 target = player.transform.position + (Vector3)(radius * UnityEngine.Random.insideUnitCircle);
                Instantiate(enemyPrefab, target, Quaternion.identity);
            }
        }
    }
}
