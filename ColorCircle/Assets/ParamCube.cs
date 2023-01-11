using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public AudioPeer ap;
    public int band;
    public float startScale, scaleMultipler;


    private void FixedUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x, 
            (ap.freqBand[band] * scaleMultipler) + startScale, 0f);
        
    }
}
