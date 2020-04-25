using UnityEngine;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class NPCsController : MonoBehaviour
{

    public static NPCsController _ins;

    private NpcList npcs = new NpcList();
    private string itemsJson;

    // json API
    private string api = "/data/";

    private void Awake()
    {
        _ins = this;

        // Start to get data
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(MainController._rootAPI + api + "npcs.json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            itemsJson = File.ReadAllText(Application.dataPath + "/npcs.json");
        }
        else
        {
            npcs = JsonUtility.FromJson<NpcList>(request.downloadHandler.text);
        }
    }



    public NpcList GetAllNPCsList()
    {
        return npcs;
    }

    public List<string> GetAllNpcIntro()
    {
        List<string> res = new List<string>();

        foreach (Npc el in npcs.npcs)
        {
            res.Add(el.intro);
        }

        return res;
    }

    public string GetNpcIntro(string name)
    {
        string res = null;

        foreach(Npc el in npcs.npcs)
        {
            if(name == el.name)
            {
                res = el.intro;
            }
        }

        return res;
    }

    public void NPCVisualNight()
    {
        foreach (Transform child in GameObject.Find("NPCs").transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void NPCVisualDay()
    {
        foreach (Transform child in GameObject.Find("NPCs").transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
    }


}
