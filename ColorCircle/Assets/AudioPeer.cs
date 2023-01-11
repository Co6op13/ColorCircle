using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [SerializeField] private float[] samples = new float[512];
    [SerializeField] public float[] freqBand = new float[8];

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBand();
    }


    void GetSpectrumAudioSource()
    {
        audioS.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        
    }

    void MakeFrequencyBand()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }
    }
}
