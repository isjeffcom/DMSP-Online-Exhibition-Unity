using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class audioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    Transform audioVFX;
    public static float[] _samples = new float[512];
    public float[] _freqBand = new float[8];
    public float scale;

    private void Awake()
    {
        audioVFX = gameObject.transform.Find("NPC_Sound_VFX");
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        getSpectrumAudioSource();
        makeFrequencyBands();
    }

    void getSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void makeFrequencyBands()
    {
        /*
         * about 22050Hz in total of the audio, 22050 / 512 = 43 Hz per sample
         * 
         * if we try to arrange then into 8 bands, then it should be 
         * 20 - 60 Hz
         * 60 - 250
         * 250 - 500 
         * 500 - 2000
         * 2000-4000
         * 4000-6000
         * 6000-20000
         * 
         * 0 - 2 =86 Hz
         * 1 - 4 = 172   87-258
         * 2 - 8 = 344   259-602
         * .
         * .
         * .
         * total 510
        */

        //current number of the bands
        int count = 0;

        for(int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for(int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }

        
        if (audioVFX)
        {
            float s = _freqBand[0] * 30 * scale;

            // Min size
            if(s < 0.1)
            {
                s = 0.1f;
            }

            // Max size
            if(s > 3)
            {
                s = 3;
            }
            audioVFX.localScale = new Vector3(s, s, s);
        }

    }

}
