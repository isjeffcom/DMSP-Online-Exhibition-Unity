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
            //Debug.Log("Done");
            return;
        }

        string thisName = gameObject.GetComponentInChildren<Text>().text;

        if (SelectController._ins.TryMatch(thisName))
        {
            SelectController._ins.RestoreSelectedNPC(thisName);
            done = true;
            RightAni();
        } 
        else 
        {
            StartCoroutine(WrongAni());
            //Debug.Log("Wrong or not selected");
        }
    }

    IEnumerator WrongAni()
    {
        GetComponent<Animator>().SetBool("open", true);

        yield return new WaitForSeconds(2);

        GetComponent<Animator>().SetBool("open", false);
    }

    public void RightAni()
    {
        gameObject.transform.Find("UI_NPC_Name_Right").GetComponent<Animator>().SetBool("right", true);
    }

}
