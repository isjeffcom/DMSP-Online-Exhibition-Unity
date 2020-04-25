using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static MainController _ins;


    // Global Vars
    public static int _act = 1; 
    public static string _selectedNPC;

    public static string _rootAPI = "https://playground.eca.ed.ac.uk/~s1888009/dmspassets";

    private void Awake()
    {
        _ins = this;
    }


    public void ToAct(int toAct)
    {
        _act = toAct;
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
        //NPCsController._ins.NPCVisualNight(); //There will be no npc in act2
    }

    public void MapToDay()
    {
        GameObject.Find("bg_base").GetComponent<Animator>().SetBool("night", false);
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bg_act1_day");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_act1_day");
        NPCsController._ins.NPCVisualDay();
    }

    public void MapToAct3()
    {
        GameObject.Find("bg_base").GetComponent<Animator>().SetBool("night", false);
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bg_act3_day");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_act3_night");
        NPCsController._ins.NPCVisualDay();
    }

    public void AudioPlayByAct(string name)
    {
        switch (_act)
        {
            case 1:
                AudioController._ins.PlayAllNPCsAudio();
                break;
            case 2:
                AudioController._ins.PlayNPCAudio(name);
                break;
        }
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
        }
        
    }
}
