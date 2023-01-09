using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShape : MonoBehaviour, IShape
{
    [SerializeField] private GameObject fragment;
    [SerializeField] private Palette palette;
    [SerializeField] private float lengthLine;
    [SerializeField] private float speedReduction, speedRotate;
    private float startPosition = 0;
    private float angle;
    


    //private void Start()
    //{
    //    int[] aa = new int[] { 1, 2, 3, 5 };

    //    SetVariable(aa, speedReduction, speedRotate);
    //}

    public void SetVariable(int[] colorIndexs, float speedReduction, float speedRotate)
    {
        for (int i = 0; i < colorIndexs.Length; i++)
        {
            GameObject frag = ShapePool.Instance.GetFromPool(fragment.name, Vector3.zero, transform.rotation);
            //var frag = Instantiate(fragmentCircle, Vector3.zero, transform.rotation);            
            var lc =  frag.GetComponent<LineController>();   
            frag.layer = colorIndexs[i] + 10;
            lc.SetVariables(palette.colors[colorIndexs[i]], colorIndexs.Length, startPosition);
            startPosition = lc.endPosition;
            //Debug.Log(startPosition);
            frag.SetActive(true);
        }
        this.speedReduction = speedReduction;
        this.speedRotate = speedRotate;
        angle = Random.Range(0, 360);
    }

    private void FixedUpdate()
    {
        var scale = transform.localScale.x - speedReduction * Time.fixedDeltaTime;
        angle += speedRotate * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        transform.localScale = new Vector3(scale, scale, 1f);
        if (scale < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
