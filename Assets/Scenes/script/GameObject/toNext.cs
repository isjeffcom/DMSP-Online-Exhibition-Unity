using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toNext : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("sd");
        Debug.Log(MainController._act);
        if (MainController._act == 2)
        {
            Act2and3Controller._ins.enterNext();
        }
        else if (MainController._act == 3)
        {
            Act3and4Controller._ins.enterNext();
        }
    }
    
}
