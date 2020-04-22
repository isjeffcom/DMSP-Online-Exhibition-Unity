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
    private string api = "https://playground.eca.ed.ac.uk/~s1888009/dmspassets/data/";

    // UI Container
    private GameObject itemCont;
    private GameObject itemImg;

    private ItemsList itemsList = new ItemsList();
    private string itemsJson;

    private void Awake()
    {
        _ins = this;

        // Find UI Container for display item content
        itemCont = GameObject.Find("UI_Item_Cont");
        itemImg = GameObject.Find("UI_Item_Img_Cont");

        // Start to get data
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(api + "act" + MainController._act + "/items.json");

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

    private void Start()
    {
        itemCont.SetActive(false);
    }

    public void ShowItemDetail(string itemName)
    {
        

        foreach (Item item in itemsList.Items)
        {
            if(item.name == itemName)
            {

                
                if(item.type == "image")
                {
                    StartCoroutine(DownloadImage(item.src));
                    itemCont.SetActive(true);
                }

                if(item.type == "sound")
                {
                    AudioController._ins.InvItemAudioPlay(item.src, itemName, -1, false);
                }
                
            }
        }
    }

    public void CloseItemDetail()
    {
        itemCont.SetActive(false);
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
  
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
            SetDetailImg(texture);
        }
     
    }

    void SetDetailImg(Texture2D texture)
    {
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        itemImg.GetComponent<Image>().sprite = spriteToUse;
    }

}
