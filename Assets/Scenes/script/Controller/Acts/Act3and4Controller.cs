using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Act3and4Controller : MonoBehaviour
{
    public static Act3and4Controller _ins;

    private GameObject toNext;
    private Text tip;
    private GameObject completeButton;
    private GameObject[] npcAct4;

    private List<int> audioCheck = new List<int>();
    private List<int> audioPlayed = new List<int>();


    private void Awake()
    {
        _ins = this;
        toNext = GameObject.Find("toNext3");
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
        completeButton = GameObject.Find("Mission_Complete_Button");
    }

    private void Start()
    {
        toNext.SetActive(false);
        addAudioID();
    }

    private void addAudioID()
    {
        for(int i = 0; i< 20; i++)
        {
            audioCheck.Add(i);
        }
    }

    //To check whether each scene has been played
    public void audioPlayedCheck(int id)
    {
        audioPlayed.Add(id);

        int j=0;
        foreach (int existedID in audioCheck)
        {
            if (audioPlayed.Contains(existedID))
            {
                j++;

                if (j == audioCheck.Count)
                {
                   
                    DecisionController._ins.addDragScript();
                }
            }
            
        }
    }

    public void ActCheck()
    {
        AudioController._ins.StopAudio();

        toNext.SetActive(true);

        tip.text = "Now You Can Go Upstairs";
    }

    public void enterNext()
    {
        EnterAct4();
    }


    public void EnterAct4()
    {
        completeButton.SetActive(false);
        
        NPCsController._ins.ClearAllNPCName();
        ColliderController._ins.SwitchCollider(4);
        MainController._ins.ToAct(4);
        NPCsController._ins.NPCswitch(4);
        NPCsController._ins.DisplayAllNameOnScreen(4);

        //Set Shawn,Tom and Joe to be active false
        //npcAct4 = GameObject.FindGameObjectsWithTag("NPC_Act4");
        //for (int i = 0; i < npcAct4.Length; i++)
        //{
        //    npcAct4[i].SetActive(false);
        //}
        MainController._ins.MapToAct4();

        MissionController._ins.MissionText(4);
        MissionController._ins.MissionContent(4);

        // Show dialog
        StartCoroutine(PlayAct4Dialog());
    }

    IEnumerator PlayAct4Dialog()
    {

        yield return new WaitForSeconds(3);

        DialogController._ins.ShowDialog("Blake", 0);
    }


}
