using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareShape : MonoBehaviour, IShape
{
    [SerializeField] private LineRenderer[] lines;
    [SerializeField] private Palette palette;
    [SerializeField] private float size;
    [SerializeField] private float speedReduction, speedRotate;
    [SerializeField] private GameObject particle;
    private float angle;

  
    //private void SetSize()
    //{
    //    lines[0].startPos = new Vector2(size, -size);
    //    lines[0].endPos = new Vector2(size, size);
    //    lines[1].startPos = new Vector2(size, size);
    //    lines[1].endPos = new Vector2(-size, size);
    //    lines[2].startPos = new Vector2(-size, size);
    //    lines[2].endPos = new Vector2(-size, -size);
    //    lines[3].startPos = new Vector2(-size, -size);
    //    lines[3].endPos = new Vector2(size, -size);
    //}


    private void FixedUpdate()
    {
        var scale = transform.localScale.x -  speedReduction * Time.fixedDeltaTime;
        angle += speedRotate * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        transform.localScale = new Vector3(scale, scale, 1f);
        if(scale < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetVariable(int[] colorIndexs, float size, float speedReduction, float speedRotate)
    {

        //this.size = size;
        //SetSize();
        for(int i = 0; i < lines.Length; i++)
        {
            
            lines[i].startColor = palette.colors[colorIndexs[i]];
            lines[i].endColor = palette.colors[colorIndexs[i]];
            lines[i].gameObject.layer = colorIndexs[i] + 10;
            
        }
        this.speedReduction = speedReduction;
        this.speedRotate = speedRotate;
        angle = Random.Range(0, 360);
    }

    public void DisableShape()
    {
        foreach (var line in lines)
        {
            Vector3[] points = new Vector3[line.positionCount];
            //Get old Positions
            line.GetPositions(points);
            foreach (var point in points)
            {
                int rand = Random.Range(0, 2);
                if (rand == 1)
                {
                    var p = ShapePool.Instance.GetFromPool(particle.name, point, transform.rotation);
                    var pp = p.GetComponent<ParticleSystem>().main;
                    pp.startColor = line.startColor;
                }
            }
        }
        //gameObject.SetActive(false);
    }

    //private void OnDisable()
    //{
    //    SetSize();
    //    transform.localScale = Vector3.one;
    //}

}
