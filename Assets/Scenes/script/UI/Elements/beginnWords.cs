using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class beginnWords : MonoBehaviour
{
    //text control
    Text _beginnWords;
    float _duration = 0.5f;
    string[] words = new string[10];
    [SerializeField] GameObject startButton;
    [SerializeField] Image startBG;

    //audio control
    [SerializeField] AudioClip[] narraClips = new AudioClip[10];
    AudioSource audioSource;

    void Awake()
    {
        words[0] = "Welcome detective";
        words[1] = "I am sure you are well aware of \n the current global situation";
        words[2] = "Everything is far from straightforward";
        words[3] = "It is a possibility that the virus was \n deliberately created and spread";
        words[4] = "We will arrange for you to go undercover as a doctor in charge of a vaccine research unit";
        words[5] = "So that you can work alongside \n them in the laboratory";
        words[6] = "You are the perfect person for the job, \n to help us to get to the truth";
        words[7] = "To begin, we would like you to put on \n your headphones";
        words[8] = "Now, be careful, take care not to \n expose your identity";
        words[9] = "We will be ready to start";  
    }
    // Start is called before the first frame update
    void Start()
    {
        //audio
        audioSource = GetComponent<AudioSource>();

        //text
        _beginnWords = gameObject.GetComponent<Text>();
        StartCoroutine(TextFade());
        StartCoroutine(BGflicker());


    }


    IEnumerator BGflicker()
    {
        yield return new WaitForSeconds(0.1f);
        Color originalColor = startBG.color;
        int t=1;
        while(true)
        {
            if (t % 2 == 0)
            {
                startBG.color = new Color(166, 164, 164, 255);
            }
            else if(t % 2 == 1)
            {
                startBG.color = originalColor;
            }
            t++;
            yield return new WaitForSeconds(0.002f);
        }
        
        
    }

    IEnumerator TextFade()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 10; i++)
        {
            //play audio
            audioSource.clip = narraClips[i];
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            //text
            _beginnWords.text = words[i];
            Color originalColor = _beginnWords.color;
            for ( float t = 0.01f; t < _duration; t += Time.deltaTime)
            {
                _beginnWords.color = Color.Lerp(originalColor, new Color(255, 255, 255, 0), Mathf.Min(1, t / _duration));
                yield return null;
            }
            if (i < 9)
            {
                _beginnWords.color = originalColor;
            }
            else
            {
                startButton.SetActive(true);
            }
            //stop audio
            audioSource.Stop();
        }
        
    }
}
