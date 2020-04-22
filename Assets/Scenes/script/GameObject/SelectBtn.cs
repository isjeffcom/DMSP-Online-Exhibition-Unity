using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBtn : MonoBehaviour
{
    private bool done = false;


    public void GetSelect()
    {

        if (done)
        {
            Debug.Log("Done");
            return;
        }

        string thisName = gameObject.GetComponentInChildren<Text>().text;
        Debug.Log(thisName);

        if (SelectController._ins.TryMatch(thisName))
        {
            gameObject.GetComponentInChildren<Text>().color = new Color(0.20f, 0.67f, 0.45f);
            done = true;
        } 
        else 
        {
            Debug.Log("Wrong or not selected");
        }
    }
}
