﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionController : MonoBehaviour
{
    public static DecisionController _ins;

    //For add script
    public SortName sortName;

    // Publish to global for act check
    public static int _allNPCsLength = 0;
    public static int _sortedCount = 0;
    
    private GameObject Mission_Content_Act3;
    private GameObject Mission_Desicion_Blank;
    private GameObject Mission_Sorting_NameBlock;

    private List<string> allNPCName = new List<string>();

    private GameObject[] allSortingBlank;
    //private GameObject[] allScenes;
    private GameObject[] allNameBlocks;

    private void Awake()
    {
        _ins = this;

        Mission_Content_Act3 = GameObject.Find("Mission_Content_Act3");
        Mission_Desicion_Blank = GameObject.Find("Mission_Decision_Blank");
        Mission_Sorting_NameBlock = GameObject.Find("Mission_Sorting_NameBlock");
        
    }

    
    private void GetNameBlocks()
    {
        allNameBlocks = GameObject.FindGameObjectsWithTag("NameBlock");
    }
    

    public void addDragScript()
    {
        GetNameBlocks();

        foreach (GameObject nameBlock in allNameBlocks)
        {
            sortName = nameBlock.AddComponent<SortName>();
            nameBlock.GetComponent<Button>().interactable = true;
        }

    }

    public bool Decide()
    {
        if (Mission_Desicion_Blank.transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
