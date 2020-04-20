using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class AudioController : MonoBehaviour
{
    public static AudioController _audioIns;

    AudioSource audioSource;
    AudioClip audioClip;
    public string filePath;
    string audioName;

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

        audioSource = gameObject.AddComponent<AudioSource>();
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
        LoadAudio("Joe", 101);

    }

    //void getDataByFile()
    //{
    //    audiosJson = File.ReadAllText(Application.dataPath + "/audios.json");
    //    AudiosList = JsonUtility.FromJson<AudiosList>(audiosJson);
    //    Debug.Log("audioDone");
    //}

    //Load and play audio clips
    public void LoadAudio(string character, int toId)
    {
        // If to Id is -1 then stop
        if (toId == -1)
        {
            audioSource.Stop();
            return;
        }

        // Define what to do next
        int to = -1;

        // Find audio by NPC name
        foreach (Audios audios in AudiosList.Audios)
        {
            if (character == audios.name)
            {
                // Get answer
                foreach (AudiosActs acts in audios.acts)
                {

                    if (toId == acts.id)
                    {
                        audioName = acts.audioName;
                        Debug.Log(audioName);
                        to = acts.to;
                        
                    }
                }
            }
        }

        string soundPath = filePath + character + "/";
        Debug.Log(soundPath);

        WWW request = GetAudioFromFile(soundPath, audioName);

        audioClip = request.GetAudioClip();
        audioSource.clip = audioClip;
        audioSource.clip.name = audioName;

        PlayAudio();
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
