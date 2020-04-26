using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SortingController : MonoBehaviour
{
    public static SortingController _ins;

    private GameObject NPC_Act2;
    private GameObject Mission_Content_Act2;
    private GameObject Mission_Sorting_Block;
    private GameObject Mission_Sorting_NameBlock;

    private List<string> allNPCName = new List<string>();

    private GameObject[] allSortingBlank;

    private void Awake()
    {
        _ins = this;

        Mission_Content_Act2 = GameObject.Find("Mission_Content_Act2");
        Mission_Sorting_Block = GameObject.Find("Mission_Sorting_Block");
        Mission_Sorting_NameBlock = GameObject.Find("Mission_Sorting_NameBlock");

        NPC_Act2 = GameObject.Find("NPC_Act2");
        
    }

    private void Start()
    {
        GetAllNPCs();
        
    }

    private void GetAllNPCs()
    {

        foreach (Transform child in NPC_Act2.transform)
        {
            allNPCName.Add(child.gameObject.name);
        }

        DisplayAll();
    }

    public void GetAllBlanks()
    {

        allSortingBlank = GameObject.FindGameObjectsWithTag("SortingBlank");
    }

    public void DisplayAll()
    {
        //Blocks
        for(int i = 1; i < 5; i++)
        {
            // re positioning
            Vector3 posi = new Vector3(Screen.width - 440, Screen.height - (80 * i) -340, 0);

            // Instantiate
            GameObject block = Instantiate(Mission_Sorting_Block, posi, Quaternion.identity);

            // Set Text
            block.GetComponentInChildren<Text>().text = i.ToString();

            // Set button parent
            block.transform.SetParent(Mission_Content_Act2.transform);

            block.transform.localScale = new Vector3(1, 1, 1);
        }

        //Names
        int j = 1;
        foreach (string item in allNPCName)
        {
            // re positioning
            Vector3 posi = new Vector3(Screen.width - 220, Screen.height - (80 * j) - 340, 0);

            // Instantiate
            GameObject name = Instantiate(Mission_Sorting_NameBlock, posi, Quaternion.identity);

            // Set Text
            name.GetComponentInChildren<Text>().text = item;

            // Set button parent
            name.transform.SetParent(Mission_Content_Act2.transform);

            name.transform.localScale = new Vector3(1, 1, 1);

            // Counter
            j++;
        }

    }

    public bool SortOut()
    {
        GetAllBlanks();

        int j = 0;
        for(int i = 0; i < allSortingBlank.Length; i++)
        {
            if (allSortingBlank[i].transform.childCount>0)
            {
                j++;
            }
        }
        if (j==4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
