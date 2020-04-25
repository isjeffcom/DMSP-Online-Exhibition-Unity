using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SortBlock : MonoBehaviour,IDropHandler
{
    public GameObject item
    {
        get
        {
            //Get the first child if there is
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        int childs = transform.childCount;

        //If there is no child, then receive the dropped one
        if (childs==0)
        {
            SortName.itemBeingDragged.transform.SetParent(transform);
            if (SortingController._ins.SortOut())
            {
                MissionController._ins.CompelteAvailable(true);
            }
            else
            {
                Debug.Log("jno");
            }
        }
    }
}
