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

    //void Start()
    //{
    //    SettartPosition();
    //}

    //private void SettartPosition()
    //{
    //    line.SetPosition(0, startPos);
    //    line.SetPosition(1, endPos);
    //}
    public void SetColor(Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }

    //void FixedUpdate()
    //{
    //    //Vector3[] oldPos = new Vector3[line.positionCount];
    //    //Vector3[] newPos = new Vector3[line.positionCount];
    //    //line.GetPositions(oldPos);
    //    //for (int i = 0; i < line.positionCount; i ++)
    //    //{
    //    //    newPos[i] = transform.localToWorldMatrix * new Vector4(oldPos[i].x, oldPos[i].y, 0, 1);
    //    //}
    //    //line.SetPositions( newPos);
    //    //Calculate new postion 
    //    Vector3 newBeginPos = transform.localToWorldMatrix * new Vector4(startPos.x, startPos.y, 0, 1);
    //    Vector3 newEndPos = transform.localToWorldMatrix * new Vector4(endPos.x, endPos.y, 0, 1);

    //    //Apply new position
    //    line.SetPosition(0, newBeginPos);
    //    line.SetPosition(1, newEndPos);
    //}

    //private void OnDisable()
    //{
    //    SettartPosition();
    //}

}
