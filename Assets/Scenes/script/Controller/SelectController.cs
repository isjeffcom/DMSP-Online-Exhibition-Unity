using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{

    public static SelectController _ins;

    public Vector3 OriginalScale = new Vector3(.8f, .8f, .8f);
    public Vector3 SelectedScale = new Vector3(1.6f, 1.6f, 1.6f);

    private GameObject NPCs;
    private GameObject UI_NPCName_Cont;
    private GameObject UI_NPCName_Sample;

    private List<string> allNPCName = new List<string>();

    private void Awake()
    {
        _ins = this;

        NPCs = GameObject.Find("NPCs");

        UI_NPCName_Cont = GameObject.Find("UI_NPC_Names_Cont");
        UI_NPCName_Sample = GameObject.Find("UI_NPC_Names");
    }

    private void Start()
    {
        GetAllNPCs();
    }

    private void GetAllNPCs()
    {
        
        foreach (Transform child in NPCs.transform)
        {
            allNPCName.Add(child.gameObject.name);
        }

        DisplayAllNPCs();
    }

    public void DisplayAllNPCs()
    {
        int i = 1;
        foreach(string item in allNPCName)
        {
            Vector3 posi = new Vector3(306, 0 + (35 * i), 0);
            GameObject single = Instantiate(UI_NPCName_Sample, posi, Quaternion.identity);
            single.GetComponentInChildren<Text>().text = item;
            single.transform.SetParent(UI_NPCName_Cont.transform); //Setting button parent
            i++;
        }
    }

    public bool TryMatch(string name)
    {
        if (MainController._selectedNPC != "")
        {
            if(name == MainController._selectedNPC)
            {
                
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
    
    public void ClearOthers(string name)
    {
        foreach(Transform child in NPCs.transform)
        {
            if(child.name != name)
            {
                RestoreSelectedNPC(child.name);
            }
        }
    }

    public void RenderSelectedNPC(string name)
    {
        if(name != "")
        {
            GameObject.Find(name).transform.localScale = SelectedScale;
        }
        
    }

    public void RestoreSelectedNPC(string name)
    {
        if (name != "")
        {
            GameObject.Find(name).transform.localScale = OriginalScale;
        }
    }
}
