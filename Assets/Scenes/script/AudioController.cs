using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class AudioController : MonoBehaviour
{
    public static AudioController _audioIns;

    AudioSource audioSource_A;
    AudioSource audioSource_B;
    AudioSource audioSource_C;
    AudioSource audioSource_D;
    //media one
    AudioSource audioSource;
    
    string filePath;
    int currentPlayer;
    string audioName;
    int nextPlayer;

    // All Audios array container
    private string audiosJson;

    //private GameObject[] All_Audios;
    private AudiosList AudiosList = new AudiosList();

    // json API
    private string api = "https://playground.eca.ed.ac.uk/~s1925755/DMSP/audios.json";

    void Awake()
    {
        // For global access
        _audioIns = this;

        //Get AudioSource of each NPC
        audioSource_A = GameObject.Find("Joe").GetComponentInChildren<AudioSource>();
        audioSource_B = GameObject.Find("Tom").GetComponentInChildren<AudioSource>();
        audioSource_C = GameObject.Find("Blake").GetComponentInChildren<AudioSource>();
        audioSource_D = GameObject.Find("Shawn").GetComponentInChildren<AudioSource>();
        audioSource = null;

        filePath = "file://" + Application.streamingAssetsPath + "/Sound/";
        StartCoroutine(GetData());
        //getDataByFile();
    }

    // Get audios data file (.json format)
    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(api);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            audiosJson = File.ReadAllText(Application.dataPath + "/audios.json");
        }
        else
        {
            AudiosList = JsonUtility.FromJson<AudiosList>(request.downloadHandler.text);
        }
        Debug.Log("audioDone");

    }

    //void getDataByFile()
    //{
    //    audiosJson = File.ReadAllText(Application.dataPath + "/audios.json");
    //    AudiosList = JsonUtility.FromJson<AudiosList>(audiosJson);
    //    Debug.Log("audioDone");
    //}

    //Load and play audio clips
    //Differ audios by acts number
    public void LoadAudio(string actNumber, int toId)
    {
        StopCoroutine(nextAudio(audioSource.clip.length, toId,  actNumber));

        // Define what to do next
        int to = -1;

        // Find audio by NPC name
        foreach (Audios audios in AudiosList.Audios)
        {
            if (actNumber == audios.actNumber)
            {
                // Get audio name
                foreach (AudiosActs acts in audios.acts)
                {

                    if (toId == acts.id)
                    {
                        currentPlayer = acts.currentPlayer;
                        audioName = acts.audioName;
                        nextPlayer = acts.nextPlayer;
                        to = acts.to;
                    }
                }
            }
        }

        //Could be replaced by audioName, if audioName save the audio path
        //But now I have problems connecting campus server, can only use the dataPath
        string soundPath = filePath + actNumber + "/";
        WWW request = GetAudioFromFile(soundPath, audioName);

        
        switch (currentPlayer)
        {
            case 1:
                audioSource = audioSource_A;
                break;
            case 2:
                audioSource = audioSource_B;
                break;
            case 3:
                audioSource = audioSource_C;
                break;
            case 4:
                audioSource = audioSource_D;
                break;
            case 0:
                audioSource = null;
                break;
        }
        
        audioSource.clip = request.GetAudioClip();
        audioSource.clip.name = audioName;

        PlayAudio();
        StartCoroutine(nextAudio(audioSource.clip.length, to, actNumber));
    }

    
    IEnumerator nextAudio(float time, int to, string actNumber)
    {
        //Stop play audio after it finish
        yield return new WaitForSeconds(time);
        audioSource.Stop();
        if (to == -1)
        {
            yield return null;
        }
        else
        {
            LoadAudio(actNumber, to);
        }
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    WWW GetAudioFromFile (string path, string fielname)
    {
        string audioToLoad = string.Format(path + fielname);
        WWW request = new WWW(audioToLoad);
        return request;
    }

}
