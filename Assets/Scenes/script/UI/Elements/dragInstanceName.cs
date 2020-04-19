using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class dragInstanceName : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject nameCardPre;
    GameObject nameCard;

    //destroy if there has another nameCard
    public string btnName;
    //get audio clip
    AudioClip myAudioClip;

    private void Start()
    {
        myAudioClip = GetComponentInChildren<AudioSource>().clip;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        nameCard = Instantiate(nameCardPre) as GameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (nameCard != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = 1 << 8 ;

            if (Physics.Raycast(pos, -Vector3.back * 10, out hit, Mathf.Infinity, mask))
            {
                nameCard.transform.position = hit.transform.position;
            }
            else
            {
                Vector3 off = new Vector3(0, 0, 10);
                nameCard.transform.position = pos + off;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (nameCard != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = 1 << 8;

            if (Physics.Raycast(pos, -Vector3.back * 10, out hit, Mathf.Infinity, mask))
            {
                nameCard.transform.position = hit.transform.position;
                nameCard.transform.parent = hit.transform;
                myAudioClip = hit.transform.GetComponent<AudioSource>().clip;
            }
            else
            {
                Destroy(nameCard);
                
            }
        }
    }
    
}