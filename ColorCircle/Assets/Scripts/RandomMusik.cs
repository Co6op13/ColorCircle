using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusik : MonoBehaviour
{
    [SerializeField] private AudioSource musik1;
    [SerializeField] private AudioSource musik2;
    void Start()
    {
        int i = Random.Range(0, 2);
        if (i == 0)

            musik1.Play();

        else
            musik2.Play();
    }

    public void StopMusic()
    {
        musik1.Stop();
        musik2.Stop();
    }
}
