using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CustomNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        //spawn instance of enemy on client
        GameObject enemyPrefab = spawnPrefabs[0];
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(0,1,0),Quaternion.identity);
        NetworkServer.Spawn(enemy);
    }
}
