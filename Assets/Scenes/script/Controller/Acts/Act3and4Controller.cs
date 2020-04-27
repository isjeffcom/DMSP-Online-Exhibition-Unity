using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Act3and4Controller : MonoBehaviour
{
    public static Act3and4Controller _ins;

    private GameObject toNext;
    private Text tip;

    private void Awake()
    {
        _ins = this;
        toNext = GameObject.Find("toNext3");
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
    }

    private void Start()
    {
        toNext.SetActive(false);
    }

    public void ActCheck()
    {
        
        AudioController._ins.StopAudio();

        toNext.SetActive(true);
        MissionController._ins.CompelteAvailable(false);

        tip.text = "Now You Can Go Upstairs";
    }

    public void enterNext()
    {
        EnterAct4();
    }

    public void EnterAct4()
    {
        ColliderController._ins.SwitchCollider(4);

        MainController._ins.ToAct(4);
        //NPCsController._ins.NPCswitch(4);//npc need to be active before MaptoAct3()
        MainController._ins.MapToAct4();

        MissionController._ins.MissionText(4);
        MissionController._ins.MissionContent(4);
        
    }


}
