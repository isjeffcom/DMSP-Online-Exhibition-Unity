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

    private GameObject NPC_Act1;
    private GameObject NPC_Act2;
    private GameObject NPC_Act3;
    private GameObject NPC_Act4;

    private void Awake()
    {
        _ins = this;

        // Start to get data
        StartCoroutine(GetData());

        NPC_Act1 = GameObject.Find("NPC_Act1");
        NPC_Act2 = GameObject.Find("NPC_Act2");
        NPC_Act3 = GameObject.Find("NPC_Act3");
        NPC_Act4 = GameObject.Find("NPC_Act4");
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
        foreach (Transform child in GameObject.Find("NPC_Act1").transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void NPCVisualDay()
    {
        string npc_name = "NPC_Act" + MainController._act;
        foreach (Transform child in GameObject.Find(npc_name).transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
    }

    public void NPCswitch(int act)
    {
        foreach (Transform child in GameObject.Find("NPCs").transform)
        {
            child.gameObject.SetActive(false);
        }

        switch (act)
        {
            case 1:
                NPC_Act1.SetActive(true);
                break;
            case 2:
                NPC_Act2.SetActive(true);
                break;
            case 3:
                NPC_Act3.SetActive(true);
                break;
            case 4:
                NPC_Act4.SetActive(true);
                break;
        }

    }

}
