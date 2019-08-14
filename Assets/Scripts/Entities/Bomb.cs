using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Bomb : NetworkBehaviour
{
    public float explosionRadius = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
