using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageViewerController : MonoBehaviour
{

    public static ImageViewerController _ins;

    public static int _currentImgsLength;
    public static int _currentImgIndex;

    // UI Container
    private GameObject itemCont;
    private GameObject itemImg;
    private GameObject imgLast;
    private GameObject imgNext;

    private List<Texture2D> allTextures = new List<Texture2D>();

    private void Awake()
    {
        _ins = this;

        // Find UI Container for display item content
        itemCont = GameObject.Find("UI_Item_Cont");
        itemImg = GameObject.Find("UI_Item_Img_Cont");
        imgLast = GameObject.Find("Img_Last");
        imgNext = GameObject.Find("Img_Next");
    }
    private void Start()
    {
        itemCont.SetActive(false);

        /*List<string> srcs = new List<string>();
        srcs.Add("https://api.isjeff.com/main/upload/imgs/1571410371_919.png");
        srcs.Add("https://api.isjeff.com/main/upload/imgs/1570240752_838.png");
        MultipleImage(srcs);*/
    }


    public void SingleImage(string src)
    {
        itemCont.SetActive(true);
        StartCoroutine(DownloadImage(src, false, -1, false));
        DisableBothBtns();
    }

    public void MultipleImage(List<string> srcs)
    {
        int i = 0;

        foreach (string src in srcs)
        {

            if (i == srcs.Count - 1)
            {
                
                StartCoroutine(DownloadImage(src, true, i, true));
                itemCont.SetActive(true);
            } 
            else
            {
                StartCoroutine(DownloadImage(src, false, i, true));
            }
            
            i++;
        }

        _currentImgsLength = srcs.Count;
    }

    public void CloseImage()
    {
        itemCont.SetActive(false);

        _currentImgsLength = 0;
        _currentImgIndex = 0;

        //MainController._ins.CheckActStatus();
    }

    IEnumerator DownloadImage(string url, bool isEnd, int idx, bool multiple)
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
            if (!multiple)
            {
                DisplayImage(texture);
            } else
            {
                allTextures.Add(texture);
                if (isEnd)
                {
                    
                    DisplayImages(allTextures, 0);
                }
                
            }
        }

    }

    void DisplayImage(Texture2D texture)
    {
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        itemImg.GetComponent<Image>().sprite = spriteToUse;
    }

    public void NextImage()
    {
        if(allTextures.Count > 0)
        {
            if(_currentImgIndex < _currentImgsLength)
            {
                DisplayImages(allTextures, _currentImgIndex + 1);
            }
        }
    }

    public void LastImage()
    {
        if (allTextures.Count > 0)
        {
            if (_currentImgIndex < _currentImgsLength)
            {
                DisplayImages(allTextures, _currentImgIndex - 1);
            }
        }
    }

    void DisplayImages(List<Texture2D> textures, int idx)
    {

        _currentImgIndex = idx;

        Texture2D texture = textures[idx];
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        itemImg.GetComponent<Image>().sprite = spriteToUse;

        SetButtonsStyle();

    }

    // Set Button Available
    void SetButtonsStyle()
    {

        if (_currentImgIndex == 0)
        {
            imgNext.SetActive(true);
            imgLast.SetActive(false);
        }

        else if(_currentImgIndex == _currentImgsLength - 1)
        {
            imgLast.SetActive(true);
            imgNext.SetActive(false);
        }

        else
        {
            imgNext.SetActive(true);
            imgLast.SetActive(true);
        }
    }

    void DisableBothBtns()
    {
        imgNext.SetActive(false);
        imgLast.SetActive(false);
    }

    
}
