using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using Mirror;

public class Bomb : NetworkBehaviour
{
    public float explosionRadius = 2f;
    public float explosionDelay = 2f;
    public float destroyDelay = 1f;

    public GameObject linePrefab;

    private Animation anim;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    void Awake()
    {
        anim = GetComponent<Animation>();
    }
    void Start()
    {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);

        Explode(transform.position, explosionRadius);
        CmdExplode(transform.position, explosionRadius);

        anim.Play("Floating");

        yield return new WaitForSeconds(destroyDelay);

        NetworkServer.Destroy(gameObject);
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject clone = Instantiate(linePrefab, transform);
        LineRenderer line = clone.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    [Command]
    void CmdExplode(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        foreach (var hit in hits)
        {
            NetworkIdentity networkId = hit.GetComponent<NetworkIdentity>();
            if (networkId && hit.name.Contains("Enemy"))
            {
                NetworkServer.Destroy(hit.gameObject);
            }
        }
    }

    void Explode(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        foreach (var hit in hits)
        {
            NetworkIdentity networkId = hit.GetComponent<NetworkIdentity>();
            if (networkId && hit.name.Contains("Enemy"))
            {
                CreateLine(transform.position, hit.transform.position);
            }
        }
    }


}

