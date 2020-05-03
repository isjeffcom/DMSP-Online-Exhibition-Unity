using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public static MissionController _ins;
    
    private Text  Mission_Text;
    private GameObject Mission_Content;
    private Button  Mission_Complete;
    private Animator Complete_Ani;

    //Mission for Acts
    private GameObject Mission_Content_act1;
    private GameObject Mission_Content_act2;
    private GameObject Mission_Content_act3;
    private GameObject Mission_Content_act4;

    private List<GameObject> AllMissions = new List<GameObject>();

    private Text tip;

    private void Awake()
    {
        _ins = this;

        Mission_Text = GameObject.Find("Mission_Text").GetComponent<Text>();
        Mission_Content = GameObject.Find("Mission_Content");
        Mission_Complete = GameObject.Find("Mission_Complete_Button").GetComponent<Button>();
        Complete_Ani = GameObject.Find("Mission_Complete_Button").GetComponent<Animator>();

        Mission_Content_act1 = GameObject.Find("UI_NPC_Names_Cont");
        Mission_Content_act2 = GameObject.Find("Mission_Content_Act2");
        Mission_Content_act3 = GameObject.Find("Mission_Content_Act3");
        Mission_Content_act4 = GameObject.Find("Mission_Content_Act4");

        tip = GameObject.Find("UI_Tip").GetComponent<Text>();

        GetAllContents();

        Mission_Complete.interactable = false;
    }

    private void GetAllContents()
    {

        foreach (Transform child in Mission_Content.transform)
        {
            AllMissions.Add(child.gameObject);
        }
        
    }

    //Complete button available or not
    public void CompelteAvailable(bool available)
    {
        Mission_Complete.interactable = available;
        Complete_Ani.SetBool("isAvailable", available);

        if (available == true)
        {
            if (MainController._act == 1)
            {
                tip.text = "Click Compelete to Find More";
            }
            else
            {
                tip.text = "Click Compelete to Confirm Your Choice";
            }
        }
        else
        {
            tip.text = "";
        }
    }

    public void CompleteClick()
    {
        MainController._ins.CheckActStatus();
        CompelteAvailable(false);
    }

    //Switch mission name for acts
    public void MissionText(int act)
    {
        switch (act)
        {
            case 1:
                Mission_Text.text = "Match Voices by Selecting the Person and Their Name";
                break;
            case 2:
                Mission_Text.text = "Check all evidences \n Drag names and sort them by suspicion";
                break;
            case 3:
                Mission_Text.text = "Listen all audio records \n Who Is the One?";
                break;
            case 4:
                Mission_Text.text = "Remember Your Mission \n Find the Truth";
                break;

        }
    }

    //Switch mission board for acts
    public void MissionContent(int act)
    {

        foreach (GameObject item in AllMissions)
        {
            item.SetActive(false);
        }
            switch (act)
        {
            case 1:
                Mission_Content_act1.SetActive(true);
                break;
            case 2:
                Mission_Content_act2.SetActive(true);
                break;
            case 3:
                Mission_Content_act3.SetActive(true);
                break;
            case 4:
                Mission_Content_act4.SetActive(true);
                break;
        }
    }
}
