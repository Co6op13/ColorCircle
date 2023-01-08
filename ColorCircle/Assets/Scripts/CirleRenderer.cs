using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirleRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer circleRenderer;
    [SerializeField] private int steps;
    [SerializeField] private float radius;
    [SerializeField] private int fragments;
    [SerializeField] private float angle;
    [SerializeField] private int vertexCount;

    private void FixedUpdate()
    {        
        DrawCircle();

    }


    public float DrawCircle()
    {
        float deltaTheta = ((2f * Mathf.PI) / vertexCount / (fragments - 0.1f));
        float theta = angle;
        circleRenderer.positionCount = vertexCount;

        for( int i = 0; i < circleRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f) + transform.position;
            circleRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
        return theta;
    }
}
