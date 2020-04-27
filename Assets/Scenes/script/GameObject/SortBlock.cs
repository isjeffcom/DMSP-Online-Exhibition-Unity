using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SortBlock : MonoBehaviour,IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        int childs = transform.childCount;

        //If there is no child, then receive the dropped one
        if (childs==0)
        {
            SortName.itemBeingDragged.transform.SetParent(transform);

            if (MainController._act==2)
            {
                if (SortingController._ins.SortOut())
                {
                    MissionController._ins.CompelteAvailable(true);
                }
                else
                {
                    MissionController._ins.CompelteAvailable(false);
                 
                }
                
            }

            if(MainController._act==3)
            {
                if (DecisionController._ins.Decide())
                {
                    MissionController._ins.CompelteAvailable(true);
                }
                else
                {
                    MissionController._ins.CompelteAvailable(false);
                }
            }
            
        }
    }
}
