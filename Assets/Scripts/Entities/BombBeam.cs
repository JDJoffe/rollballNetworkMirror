﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bypasser script for allowing us to animate the scake if a LineRenderer

[RequireComponent(typeof(LineRenderer))]
public class BombBeam : MonoBehaviour
{
    public float scale = 1.0f;
    private LineRenderer lineRenderer;

    private void OnDrawGizmos()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = scale;
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.widthMultiplier = scale;
    }
}
