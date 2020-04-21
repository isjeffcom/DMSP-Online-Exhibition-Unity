using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadFile : MonoBehaviour
{
    public string urlString;
    string url;
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadURL()
    {
        url = urlString;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            Debug.Log("load");
            ((DownloadHandlerAudioClip)www.downloadHandler).streamAudio = true;
            yield return www.SendWebRequest();//比较耗时间
            Debug.Log("send");

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                yield break;
            }
            
            else
            {
                Debug.Log("done");
                _audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                _audioSource.Play();
            }
        }
    }

    public void StartClick()
    {
        //for start button
        StartCoroutine(LoadURL());
        Debug.Log("start");
    }
}
