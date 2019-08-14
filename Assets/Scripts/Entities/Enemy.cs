﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using NaughtyAttributes;
using Mirror;
public class Enemy : NetworkBehaviour
{
   [Tag] public string targetTag = "Target";
    public Transform target;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
