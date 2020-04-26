using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{

    public static SelectController _ins;

    // Publish to global for act check
    public static int _allNPCsLength = 0;
    public static int _matchedCount = 0;

    public Vector3 OriginalScale = new Vector3(.8f, .8f, .8f);
    public Vector3 SelectedScale = new Vector3(1.6f, 1.6f, 1.6f);

    private GameObject NPC_Act1;
    private GameObject UI_NPCName_Cont;
    private GameObject UI_NPCName_Sample;

    private List<string> allNPCName = new List<string>();

    private void Awake()
    {
        _ins = this;

        NPC_Act1 = GameObject.Find("NPC_Act1");

        UI_NPCName_Cont = GameObject.Find("UI_NPC_Names_Cont");
        UI_NPCName_Sample = GameObject.Find("UI_NPC_Names");
    }

    private void Start()
    {
        GetAllNPCs();
    }

    private void GetAllNPCs()
    {
        
        foreach (Transform child in NPC_Act1.transform)
        {
            allNPCName.Add(child.gameObject.name);
        }

        _allNPCsLength = allNPCName.Count;

        DisplayAllNPCs();
    }

    public void DisplayAllNPCs()
    {
        int i = 1;

        foreach(string item in allNPCName)
        {
            // re positioning
            Vector3 posi = new Vector3(Screen.width - 220, (Screen.height - 120) - (80 * i)-190, 0);

            Debug.Log(Screen.height);
            Debug.Log(Screen.width);

            // Instantiate
            GameObject single = Instantiate(UI_NPCName_Sample, posi, Quaternion.identity);

            // Set Text
            single.GetComponentInChildren<Text>().text = item;

            // Set button parent
            single.transform.SetParent(UI_NPCName_Cont.transform);

            // Counter
            i++;
        }
    }

    public bool TryMatch(string name)
    {
        if (MainController._selectedNPC != "")
        {
            if(name == MainController._selectedNPC)
            {
                _matchedCount++;
                MainController._ins.CheckActStatus();
                return true;
            } else
            {
                // Play Wrong Audio
                return false;
            }
        } else
        {
            // Play Wrong Audio
            return false;
        }
    }

    public void Select(string name)
    {
        if(name == MainController._selectedNPC)
        {
            // Restore
            MainController._ins.SelectNPC("");
            RestoreSelectedNPC(name);
        }
        else
        {
            // New
            MainController._ins.SelectNPC(name);
            RenderSelectedNPC(name);
            ClearOthers(name);
        }

        
    }
    
    // Clear other selected NPC
    public void ClearOthers(string name)
    {
        foreach(Transform child in NPC_Act1.transform)
        {
            if(child.name != name)
            {
                RestoreSelectedNPC(child.name);
            }
        }
    }

    // Enlarge Selected NPC
    public void RenderSelectedNPC(string name)
    {
        if(name != "")
        {
            GameObject.Find(name).transform.localScale = SelectedScale;
        }
        
    }

    // Restore Selected NPC from enlarge
    public void RestoreSelectedNPC(string name)
    {
        if (name != "")
        {
            GameObject.Find(name).transform.localScale = OriginalScale;
        }
    }
}
