using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class ItemController : MonoBehaviour
{
    public static ItemController _ins;

    // json API
    private string api = "/data/";


    private ItemsList itemsList = new ItemsList();

    private void Awake()
    {
        _ins = this;

        // Start to get data
        StartCoroutine(GetData());
    }

    public void UpdateAct()
    {
        // Start to get data
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(MainController._rootAPI + api + "act" + MainController._act + "/items.json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            // Do nothing..
        }
        else
        {
            itemsList = JsonUtility.FromJson<ItemsList>(request.downloadHandler.text);
        }
    }


    public void ShowItemDetail(string itemName)
    {

        foreach (Item item in itemsList.Items)
        {
            if(item.name == itemName)
            {
                
                if(item.type == "image")
                {
                    // Add root api address
                    List<string> srcs = new List<string>();
                    foreach(string s in item.src)
                    {
                        srcs.Add(MainController._rootAPI + s);
                    }

                    ImageViewerController._ins.MultipleImage(srcs);
                }

                if(item.type == "sound")
                {
                    AudioController._ins.InvItemAudioPlay(MainController._rootAPI + item.src[0], itemName, -1, -1, item.length, false);
                }
                
            }
        }
    }


}
