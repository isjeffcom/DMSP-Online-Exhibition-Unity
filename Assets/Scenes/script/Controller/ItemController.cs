using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public static ItemController _ins;

    // json API
    private string api = "/data/";


    private ItemsList itemsList = new ItemsList();
    private string itemsJson;

    private void Awake()
    {
        _ins = this;

        // Start to get data
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(MainController._rootAPI + api + "act" + MainController._act + "/items.json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            itemsJson = File.ReadAllText(Application.dataPath + "/Items.json");
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
                    ImageViewerController._ins.SingleImage(item.src);
                }

                if(item.type == "sound")
                {
                    AudioController._ins.InvItemAudioPlay(item.src, itemName, -1, false);
                }
                
            }
        }
    }


}
