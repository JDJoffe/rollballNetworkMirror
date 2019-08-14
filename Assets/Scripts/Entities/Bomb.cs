using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Bomb : NetworkBehaviour
{
    public float explosionRadius = 2f;
    public float bombTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());
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
    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(bombTimer);
        CmdExplode(transform.position, explosionRadius);
    }
    [Command]
    void CmdExplode(Vector3 pos, float rad)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            NetworkServer.Destroy(hit.gameObject);
        }
    }
}
