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

    private Coroutine currentNext = null;

    // Status
    public static bool _isPlaying = false;
    public static int _pausedNext = -1;
    public static float _pausedRestTime = 0;

    // All Audios array container
    private string audiosJson;

    //private GameObject[] All_Audios;
    private AudiosList AudiosList = new AudiosList();

    // Current AudioPlayer
    private AudioSource audioPlayer = null;

    // json API
    private string api = "/data/";
    private string api_audio = "/audios/";

    void Awake()
    {
        // For global access
        _ins = this;

        audioSource = null;

        StartCoroutine(GetData());
    }

    public void UpdateAct()
    {
        StartCoroutine(GetData());
    }

    // Get audios data file (.json format)
    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(MainController._rootAPI + api + "act" + MainController._act + "/audios.json?d=" + (Random.value * 10).ToString());

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            // Do nothing...
        }
        else
        {
            AudiosList = JsonUtility.FromJson<AudiosList>(request.downloadHandler.text);

            if (autoStart)
            {
                LoadAudio("", -1, true, true, false);
            }

            Debug.Log(request.downloadHandler.text);

        }

    }

    public void PlayNPCAudio(string name)
    {
        LoadAudio(name, -1, false, false, false);
    }

    public void PlayAudioById(int id)
    {
        
        LoadAudio("", id, false, true, false);
    }


    //Load and play audio clips
    //Differ audios by acts number
    public void LoadAudio(string objectName, int toId, bool init, bool hasNext, bool hasAlways)
    {
        
        // End
        if (objectName == "" && toId == -1 && !init)
        {
            _isPlaying = false;
            _pausedNext = -1;
            _pausedRestTime = 0;
            audioPlayer = null;
            currentNext = null;
            return;
        }

        if (init)
        {
            toId = AudiosList.audios.start;
        }

        // Get audio name
        foreach (AudiosActs acts in AudiosList.audios.acts)
        {
            
            // Search by name
            if (toId == -1)
            {
                if (objectName == acts.objectName)
                {
                    
                    // Check if overwrited by alwaysPopTo
                    if (hasAlways == false && acts.alwaysPopTo != -1)
                    {
                        LoadAudio("", acts.alwaysPopTo, false, true, true);
                        return;
                    } else
                    {
                        
                        StartCoroutine(DownloadAudio(MainController._rootAPI + api_audio + "act" + MainController._act + "/" + acts.audioName, acts.objectName, acts.id, acts.to, acts.length, hasNext));
                    }
                    
                }
            } 
            // Search by id
            else
            {
                
                if (toId == acts.id)
                {
                    // Check if overwrited by alwaysPopTo
                    if (hasAlways == false && acts.alwaysPopTo != -1)
                    {
                        Debug.Log("aaa");
                        LoadAudio("", acts.alwaysPopTo, false, true, true);
                        return;
                    }
                    else
                    {
                        StartCoroutine(DownloadAudio(MainController._rootAPI + api_audio + "act" + MainController._act + "/" + acts.audioName, acts.objectName, acts.id, acts.to, acts.length, hasNext));
                    }
                }
            }


            currentObject = acts.objectName;

        }
            
    }

    public bool CheckHasAudio(string name)
    {

        bool res = false;

        for (int i = 0; i < AudiosList.audios.acts.Count; i++)
        {
            AudiosActs acts = AudiosList.audios.acts[i];
            if (name == acts.objectName)
            {
                res = true;
            }
        }

        return res;
    }

    IEnumerator DownloadAudio(string url, string npc, int id, int next, float nextLength, bool hasNext)
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
            PlayAudio(audioClip, npc, id, next, nextLength, hasNext);

            
        }

    }

    public void PlayAudio(AudioClip audio, string npc, int id, int nextId, float nextLength, bool hasNext)
    {
        

        List<GameObject> targets = FindMultipleObjectByName(npc);

        GameObject target = new GameObject();
        target.name = null;

        if (targets.Count > 1)
        {
            target = FindMultipleObjectByPlayId(id);
        }

        if(target.name == null || target.name == "")
        {
            target = targets[0];
        }

        // Get Audio Player
        audioPlayer = target.GetComponent<AudioSource>();

        // If audio player found, and is not playing
        if (audioPlayer && !_isPlaying)
        {
            audioPlayer.clip = audio;
            audioPlayer.Play();

            _isPlaying = true;

            if (hasNext)
            {
                // Wait and play next
                currentNext = StartCoroutine(NextAudio(nextLength, nextId));
            } else
            {
                // If no next, wait and set audio isPlaying to false
                currentNext = StartCoroutine(NextAudio(nextLength, -1));
            }
            
        }
        
    }

    public void PauseAudio()
    {
        if (audioPlayer != null)
        {
            audioPlayer.Pause();
            _isPlaying = false;

            if (currentNext != null)
            {
                StopCoroutine(currentNext);
            }
        }
        
    }

    public void StopAudio()
    {
        if (audioPlayer != null)
        {
            audioPlayer.Stop();
            _isPlaying = false;

            if (currentNext != null)
            {
                StopCoroutine(currentNext);
            }
        }
    }

    public void ContinueAudio()
    {
        if(audioPlayer != null)
        {

            audioPlayer.Play();

            _isPlaying = true;

            currentNext = StartCoroutine(NextAudio(_pausedRestTime, _pausedNext));
        }
        
    }

    IEnumerator NextAudio(float delay, int next)
    {

        _pausedNext = next;
        _pausedRestTime = delay;

        yield return new WaitForSeconds(delay + 0.5f);

        audioPlayer.clip = null;
        audioPlayer.Stop();

        // Could be a problem if delay is 0
        LoadAudio("", next, false, true, true);
        _isPlaying = false;

    }

    public void InvItemAudioPlay(string url, string npc, int id, int next, float nextLength, bool hasNext)
    {
        StartCoroutine(DownloadAudio(url, npc, id, next, nextLength, hasNext));
    }

    private List<GameObject> FindMultipleObjectByName(string name)
    {
        List<GameObject> list = new List<GameObject>();

        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.name == name)
            {
                list.Add(gameObj);
                //Do what you want...
            }
        }

        return list;
    }

    private GameObject FindMultipleObjectByPlayId(int id)
    {

        GameObject res = new GameObject();
        res.name = null;

        

        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.GetComponent<NPC>())
            {
                if (gameObj.GetComponent<NPC>().PlayIDs.Contains(id))
                {
                    res = gameObj;
                }
            }
            
        }

        return res;

    }


}
