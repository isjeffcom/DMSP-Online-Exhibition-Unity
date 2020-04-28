using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenes_Act3 : MonoBehaviour
{
    
    public bool audioChecked = false;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            if (Input.GetKey(KeyCode.A))
            {
                audioChecked = true;

                if (DecisionController._ins.allChecked())
                {
                    DecisionController._ins.addDragScript();
                }
            }
        }
    }
}
