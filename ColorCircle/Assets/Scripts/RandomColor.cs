using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private Camera cam;
    int i = 0;

    void Start()
    {
        cam.backgroundColor = colors[i];
        //cam.backgroundColor = colors[Random.Range(0, colors.Length)];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            cam.backgroundColor = colors[i++];
        }
    }
}
