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
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bg_2_night");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_main_night");
        NPCsController._ins.NPCVisualNight();
    }

    public void MapToDay()
    {
        GameObject.Find("bg_base").GetComponent<Animator>().SetBool("night", false);
        GameObject.Find("bg").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("floorplan_main_day");
        GameObject.Find("UI_Tip").GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject.Find("UI_Floor_Cont").GetComponent<Image>().sprite = Resources.Load<Sprite>("floorinfo_main_night");
        NPCsController._ins.NPCVisualDay();
    }


    public void CheckActStatus()
    {
        /*
         1. Image Viewer Controler -> NextImage()
         2. Select Controller -> TryMatch()
        */


        switch (_act)
        {
            case 1:
                Act1and2Controller._ins.ActCheck();
                break;
        }
        
    }
}
