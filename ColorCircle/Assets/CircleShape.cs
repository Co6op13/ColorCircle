using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShape : MonoBehaviour, IShape
{
    [SerializeField] private GameObject[] fragments;
    [SerializeField] private Palette palette;
    [SerializeField] private float lengthLine;
    [SerializeField] private float speedReduction, speedRotate;
    private float angle;
    public void SetVariable(int[] colorIndexs, float speedReduction, float speedRotate)
    {
        for (int i = 0; i < colorIndexs.Length; i++)
        {
            //fragments[i].SetColor(palette.colors[colorIndexs[i]]);
            //fragments[i].gameObject.layer = colorIndexs[i] + 10;
        }
        this.speedReduction = speedReduction;
        this.speedRotate = speedRotate;

        angle = Random.Range(0, 360);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
