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

    //Mission for Acts
    private GameObject Mission_Content_act1;
    private GameObject Mission_Content_act2;
    private GameObject Mission_Content_act3;

    private List<GameObject> AllMissions = new List<GameObject>();

    private void Awake()
    {
        _ins = this;

        Mission_Text = GameObject.Find("Mission_Text").GetComponent<Text>();
        Mission_Content = GameObject.Find("Mission_Content");
        Mission_Complete = GameObject.Find("Mission_Complete_Button").GetComponent<Button>();

        Mission_Content_act1 = GameObject.Find("UI_NPC_Names_Cont");
        Mission_Content_act2 = GameObject.Find("Mission_Content_Act2");
        Mission_Content_act3 = GameObject.Find("Mission_Content_Act3");

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
    }

    public void CompleteClick()
    {
        MainController._ins.CheckActStatus();
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
                Mission_Text.text = "Sort the Suspects by Drag Their Name";
                break;
            case 3:
                Mission_Text.text = "Make Your Final Desicion \n Who Is the One?";
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
        }
    }
}
