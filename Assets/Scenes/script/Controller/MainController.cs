using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public static MainController _ins;
    
    // Global Vars
    public static int _act = 1;
    public static int _actCount = 4;
    public static string _selectedNPC;
    public static bool playerMovable = true;

    private GameObject shadowCont;
    private Animator shadow;

    public List<GameObject> allObjsWithActTag = new List<GameObject>();

    public static string _rootAPI = "https://playground.eca.ed.ac.uk/~s1888009/dmspassets";
    

    private void Awake()
    {
        _ins = this;
        shadowCont = GameObject.Find("UI_Shade_Cont");
        shadow = shadowCont.GetComponent<Animator>();
    }

    private void Start()
    {
        GetAllObjsHasActTag();
        shadowCont.SetActive(false);


        Act1and2Controller._ins.EnterAct1();
    }

    public void GetAllObjsHasActTag()
    {

        for (int i = 1; i <= _actCount; i++)
        {
            GameObject[] tmps = GameObject.FindGameObjectsWithTag("Act_" + i);
            
            if (tmps != null)
            {
                foreach(GameObject item in tmps)
                {
                    allObjsWithActTag.Add(item);
                }
                
            }
        }
    }


    public void ToAct(int toAct)
    {
        _act = toAct;

        ActObjectsControl();

        // Update all act data
        AudioController._ins.UpdateAct();
        ItemController._ins.UpdateAct();
        DialogController._ins.UpdateAct();
        
    }

    public void SelectNPC(string name)
    {
        _selectedNPC = name;
    }

    public void MapToNight()
    {
        GameObject.Find("bg_base").GetComponent<Animator>().SetBool("night", true);
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bg_act2_night");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_act2_night");
        PlayerController_Mouse._ins.playerVisualNight();
        playerMovable = true;
        shadowCont.SetActive(false);
        shadow.SetBool("isAct4", false);
    }

    public void MapToDay()
    {
        GameObject.Find("bg_base").GetComponent<Animator>().SetBool("night", false);
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bg_act1_day");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_act1_day");
        //NPCsController._ins.NPCVisualDay();
        PlayerController_Mouse._ins.playerVisualDay();
        playerMovable = true;
        shadowCont.SetActive(false);
        shadow.SetBool("isAct4", false);
    }

    public void MapToAct3()
    {
        GameObject.Find("bg_base").GetComponent<Animator>().SetBool("night", false);
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bg_act3_day");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_act3_day");
        //NPCsController._ins.NPCVisualDay();
        PlayerController_Mouse._ins.playerVisualDay();
        playerMovable = true;
        shadowCont.SetActive(false);
        shadow.SetBool("isAct4", false);
    }

    public void MapToAct4()
    {
        GameObject.Find("bg_base").GetComponent<Animator>().SetBool("night", false);
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bg_act4_day");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_act4_day");
        PlayerController_Mouse._ins.playerVisualDay();
        playerMovable = false;
        //enterAct4
        shadowCont.SetActive(true);
        shadow.SetBool("isAct4", true);
    }

    public void ActObjectsControl()
    {

        foreach (GameObject item in allObjsWithActTag)
        {
            if (item.tag == "Act_" + _act)
            {
                
                if (item.GetComponent<AudioSource>())
                {
                    item.GetComponent<AudioSource>().Play();
                }
                item.SetActive(true);
            }
            else
            {
                if (item.GetComponent<AudioSource>())
                {
                    item.GetComponent<AudioSource>().Stop();
                }
                item.SetActive(false);
            }
        }
        
    }

    public void AudioPlayByActName(string name)
    {
        /*switch (_act)
        {
            case 1:
                AudioController._ins.PlayAllNPCsAudio();
                break;
            case 2:
                AudioController._ins.PlayNPCAudio(name);
                break;
        }*/

        AudioController._ins.PlayNPCAudio(name);
    }

    public void AudioPlayByActId(int id)
    {
        AudioController._ins.PlayAudioById(id);
    }

  

    public void CheckActStatus()
    {
        /*
         1. Select Controller -> TryMatch()
        */


        switch (_act)
        {
            case 1:
                Act1and2Controller._ins.ActCheck();
                break;
            case 2:
                Act2and3Controller._ins.ActCheck();
                break;
            case 3:
                Act3and4Controller._ins.ActCheck();
                break;
        }
        
    }

    public void DetectEnding(int to)
    {
        if (_act == _actCount)
        {
            if (to >= 900)
            {
                if(to == 999)
                {
                    ItemController._ins.ShowItemDetail("Blake");
                } 
                else if(to == 900)
                {
                    ItemController._ins.ShowItemDetail("Detective");
                }

                //SceneManager.LoadScene(0);
            }
        }
    }


}
