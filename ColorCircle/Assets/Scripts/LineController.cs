using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
   // [SerializeField] private AudioSource audioUpPoint;
    //[SerializeField] private float rotateSpeed;
    [SerializeField] private LineRenderer circleRenderer;
    [SerializeField] private int steps;
    [SerializeField] private float size;
    [SerializeField] private int fragments;
    [SerializeField] private int vertexCount;
    [SerializeField] private float startPosition;
    [SerializeField] public float endPosition;
    [SerializeField] private Color color;
   // [SerializeField] private float speed;
    //[SerializeField] private float acselerate;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private GameObject particle;
    

   // [SerializeField] List<Vector3> nodes;
    private LineRenderer lr;
    public bool isPassed = false;

    public void SetVariables (Color color, float size, int fragments,  float startPosition)
    {
        this.color = color;
        this.fragments = fragments;
        this.size = size;
        this.startPosition = startPosition;
        lr.startColor = color;
        lr.endColor = color;
        lr.positionCount = vertexCount + 1;
        DrawCircle();
    }
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        collider2d = GetComponent<Collider2D>();
        lr.positionCount = vertexCount + 1;
        
       // lr.positionCount = vertexCount + 1;
       // DrawCircle();
    }

    //private void Update()
    //{
    //   // lr.SetPositions(nodes.ConvertAll(n => n).ToArray());
    //}

    public void DisableCollider()
    {
        collider2d.enabled = false;
    }

    //private void FixedUpdate()
    //{        
    //    DrawCircle();
    //    radius -= speed * Time.fixedDeltaTime;
    //    if (isPassed)
    //    {
    //        speed += acselerate * 2;
    //    }
    //    if (radius < 5)
    //    {
    //        speed += acselerate;
    //    }
    //    if (radius <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}


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


    public void DisableFragment()
    {
        //audioUpPoint.Play();
        Vector3[] points = new Vector3[circleRenderer.positionCount];
        //Get old Positions
        circleRenderer.GetPositions(points);
        foreach ( var point in points)
        {
            int rand = Random.Range(0, 2);   
            if (rand == 1)
            {
                var p =ShapePool.Instance.GetFromPool(particle.name, point, transform.rotation);
                var pp = p.GetComponent<ParticleSystem>().main;
                pp.startColor = color;
            }
        }
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    public void DrawCircle()
    {
        float deltaTheta = ((2f * Mathf.PI ) / (vertexCount) / (fragments));
        float theta = startPosition;
        //circleRenderer.positionCount = vertexCount;

        for (int i = 0; i < circleRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(size * Mathf.Cos(theta), size * Mathf.Sin(theta), 0f) + transform.position;
            //nodes.Add(pos);
            circleRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
        endPosition =theta - deltaTheta;
        //startPosition += rotateSpeed;
    }
}
