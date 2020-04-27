using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Act2and3Controller : MonoBehaviour
{
    public static Act2and3Controller _ins;

    private GameObject toNext;
    private Text tip;
    
    private void Awake()
    {
        _ins = this;
        toNext = GameObject.Find("toNext2");
        tip= GameObject.Find("UI_Tip").GetComponent<Text>();
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
        EnterAct3();
    }

    public void EnterAct3()
    {
        ColliderController._ins.SwitchCollider(3);
        toNext.SetActive(false);
        MainController._ins.ToAct(3);
        NPCsController._ins.NPCswitch(3);//npc need to be active before MaptoAct3()
        NPCsController._ins.DisplayAllNameOnScreen();
        MainController._ins.MapToAct3();

        MissionController._ins.MissionText(3);
        MissionController._ins.MissionContent(3);

        
    }


}
