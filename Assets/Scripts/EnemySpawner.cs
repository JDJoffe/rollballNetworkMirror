using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTimer = 1f;

    // Start is called before the first frame update
    public override void OnStartServer()
    {
        InvokeRepeating("SpawnEnemy", this.spawnTimer, this.spawnTimer);
    }

   void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4f, 4f), this.transform.position.y, Random.Range(-4f,4f));
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(enemy);
        Destroy(enemy, 10);
    }
}
