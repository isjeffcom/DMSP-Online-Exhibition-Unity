using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Act1and2Controller : MonoBehaviour
{

    public static Act1and2Controller _ins;

    private GameObject invesCont;

    private bool realAll = false;

    private void Awake()
    {
        _ins = this;

        invesCont = GameObject.Find("Inves");
    }

    private void Start()
    {
        EnterAct1();
    }

    public void ActCheck()
    {
        if(SelectController._matchedCount == SelectController._allNPCsLength)
        {
            Debug.Log(realAll);
            if (!realAll)
            {
                AudioController._ins.StopAudio();
                EnteringAct2();
                realAll = true;
                MissionController._ins.CompelteAvailable(true);
            }
            else
            {
                EnterAct2();
                MissionController._ins.CompelteAvailable(false);
            }
            
        }
    }

    public void EnterAct1()
    {
        invesCont.SetActive(false);
        MainController._ins.MapToDay();

        MissionController._ins.MissionText(1);
        MissionController._ins.MissionContent(1);

        NPCsController._ins.NPCswitch(1);

        ColliderController._ins.SwitchCollider(1);
    }

    public void EnteringAct2()
    {
        ImageViewerController._ins.MultipleImage(NPCsController._ins.GetAllNpcIntro());
    }

    public void EnterAct2()
    {
        MainController._act = 2;
        NPCsController._ins.NPCswitch(2);
        MainController._ins.MapToNight();
        invesCont.SetActive(true);

        MissionController._ins.MissionText(2);
        MissionController._ins.MissionContent(2);

        ColliderController._ins.SwitchCollider(2);

    }
}
