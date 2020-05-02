using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Act1and2Controller : MonoBehaviour
{

    public static Act1and2Controller _ins;

    private GameObject invesCont;

    private bool realAll = false;

    private GameObject NPC_Act1;
    private List<GameObject> allNPC = new List<GameObject>();

    private void Awake()
    {
        _ins = this;

        invesCont = GameObject.Find("Inves");
        NPC_Act1 = GameObject.Find("NPC_Act1");

    }

    private void Start()
    {
        
        GetAllNPCs();
    }




    private void GetAllNPCs()
    {

        foreach (Transform child in NPC_Act1.transform)
        {
            allNPC.Add(child.gameObject);
        }
        
    }

    public void HideNPCNameOnScreen()
    {
        foreach (GameObject item in allNPC)
        {
            item.GetComponent<NPC>().hideNameOnScreen();
           
        }
    }

    public void ActCheck()
    {
        if(SelectController._matchedCount == SelectController._allNPCsLength)
        {
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
            }
            
        }
    }

    public void EnterAct1()
    {
        MissionController._ins.CompelteAvailable(false);
        invesCont.SetActive(false);
        MainController._ins.ToAct(1);
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

        MissionController._ins.CompelteAvailable(false);
        NPCsController._ins.ClearAllNPCName();
        MainController._ins.ToAct(2);
        HideNPCNameOnScreen();
        NPCsController._ins.NPCswitch(2);
        MainController._ins.MapToNight();
        invesCont.SetActive(true);

        MissionController._ins.MissionText(2);
        MissionController._ins.MissionContent(2);

        ColliderController._ins.SwitchCollider(2);
        
    }
}
