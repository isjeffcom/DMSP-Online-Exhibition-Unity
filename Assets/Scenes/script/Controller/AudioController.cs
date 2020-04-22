using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class AudioController : MonoBehaviour
{
    public static AudioController _ins;

    // Current audio
    AudioSource audioSource;

    private string currentObject;

    public bool autoStart = true;

    // Status
    public static bool _isPlaying = false;

    // All Audios array container
    private string audiosJson;

    //private GameObject[] All_Audios;
    private AudiosList AudiosList = new AudiosList();

    // json API
    private string api = "https://playground.eca.ed.ac.uk/~s1888009/dmspassets/data/";
    private string baseUrl = "https://playground.eca.ed.ac.uk/~s1888009/dmspassets/audios/";

    void Awake()
    {
        // For global access
        _ins = this;

        audioSource = null;

        StartCoroutine(GetData());
    }

    // Get audios data file (.json format)
    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(api + "act" + MainController._act + "/audios.json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            audiosJson = File.ReadAllText(Application.dataPath + "/audios.json");
            AudiosList = JsonUtility.FromJson<AudiosList>(audiosJson);
        }
        else
        {
            AudiosList = JsonUtility.FromJson<AudiosList>(request.downloadHandler.text);

            if (autoStart)
            {
                LoadAudio("", -1, true, true);
            }
            
        }

    }

    public void PlayNPCAudio(string name)
    {
        LoadAudio(name, -1, false, false);
    }


    //Load and play audio clips
    //Differ audios by acts number
    public void LoadAudio(string objectName, int toId, bool init, bool hasNext)
    {
        // End
        if (objectName == "" && toId == -1 && !init)
        {
            _isPlaying = false;
            return;
        }

        if (init)
        {
            toId = AudiosList.audios.start;
        }

        // Get audio name
        foreach (AudiosActs acts in AudiosList.audios.acts)
        {

            
            if (toId == -1)
            {
                if(objectName == acts.objectName)
                {
                    StartCoroutine(DownloadAudio(baseUrl + "act" + MainController._act + "/" + acts.audioName, acts.objectName, acts.to, hasNext));
                }
            } 
            else
            {
                
                if (toId == acts.id)
                {
                    
                    StartCoroutine(DownloadAudio(baseUrl + "act" + MainController._act + "/" + acts.audioName, acts.objectName, acts.to, hasNext));
                }
            }


            currentObject = acts.objectName;

        }
            
    }



    IEnumerator DownloadAudio(string url, string npc, int next, bool hasNext)
    {
        

        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            // Download Music File
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);

            // Send to play
            PlayAudio(audioClip, npc, next, hasNext);
        }

    }

    public void PlayAudio(AudioClip audio, string npc, int nextId, bool hasNext)
    {
        // Get Audio Player
        AudioSource audioPlayer = GameObject.Find(npc).GetComponent<AudioSource>();

        // If audio player found, and is not playing
        if (audioPlayer && !_isPlaying)
        {
            audioPlayer.clip = audio;
            audioPlayer.Play();

            _isPlaying = true;

            if (hasNext)
            {
                // Wait and play next
                StartCoroutine(NextAudio(audio.length, nextId));
            } else
            {
                // If no next, wait and set audio isPlaying to false
                StartCoroutine(NextAudio(audio.length, -1));
            }
            
        }
        
    }

    IEnumerator NextAudio(float delay, int next)
    {
        yield return new WaitForSeconds(delay);
        LoadAudio("", next, false, true);
        _isPlaying = false;

    }

    public void InvItemAudioPlay(string url, string npc, int next, bool hasNext)
    {
        StartCoroutine(DownloadAudio(url, npc, next, hasNext));
    }


}
