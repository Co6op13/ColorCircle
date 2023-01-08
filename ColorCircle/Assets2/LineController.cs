using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private LineRenderer circleRenderer;
    [SerializeField] private int steps;
    [SerializeField] private float radius;
    [SerializeField] private int fragments;
    [SerializeField] private int vertexCount;
    [SerializeField] private float startPosition;
    [SerializeField] public float endPosition;
    [SerializeField] private Color color;
    [SerializeField] private float speed;
    [SerializeField] private float acselerate;
    [SerializeField] private Collider2D collider;
    

    [SerializeField] List<Vector3> nodes;
    private LineRenderer lr;
    public bool isPassed = false;

    public void SetVariables (Color color, int fragments, float radius, float startPosition, float speed)
    {
        this.color = color;
        this.fragments = fragments;
        this.radius = radius;
        this.startPosition = startPosition;
        this.speed = speed;
        lr.startColor = color;
        lr.endColor = color;
        lr.positionCount = vertexCount + 1;
        DrawCircle();
    }
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        collider = GetComponent<Collider2D>();
       // lr.positionCount = vertexCount + 1;
       // DrawCircle();
    }

    //private void Update()
    //{
    //   // lr.SetPositions(nodes.ConvertAll(n => n).ToArray());
    //}

    public void DisableCollider()
    {
        collider.enabled = false;
    }

    private void FixedUpdate()
    {        
        DrawCircle();
        radius -= speed * Time.fixedDeltaTime;
        if (isPassed)
        {
            speed += acselerate * 2;
        }
        if (radius < 5)
        {
            speed += acselerate;
        }
        if (radius <= 0)
        {
            Destroy(gameObject);
        }
    }


    public Vector3[] GetPositions ()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth()
    {
        return lr.startWidth;
    }

    public void DrawCircle()
    {
        float deltaTheta = ((2f * Mathf.PI ) / (vertexCount) / (fragments));
        float theta = startPosition;
        //circleRenderer.positionCount = vertexCount;

        for (int i = 0; i < circleRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f) + transform.position;
            //nodes.Add(pos);
            circleRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
        endPosition =theta - deltaTheta;
        startPosition += rotateSpeed;
    }
}
