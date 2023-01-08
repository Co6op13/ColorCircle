using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    
    public Vector2 startPos, endPos;
    //public Color color;
    //private float speed;
    //private float rotateSpeed;

    void Start()
    {     
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    public void SetColor(Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }

    void Update()
    {
        //Calculate new postion 
        Vector3 newBeginPos = transform.localToWorldMatrix * new Vector4(startPos.x, startPos.y, 0, 1);
        Vector3 newEndPos = transform.localToWorldMatrix * new Vector4(endPos.x, endPos.y, 0, 1);

        //Apply new position
        line.SetPosition(0, newBeginPos);
        line.SetPosition(1, newEndPos);
    }
    
}
