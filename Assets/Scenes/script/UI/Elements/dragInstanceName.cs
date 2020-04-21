using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class dragInstanceName : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject nameCardPre;
    GameObject nameCard;

    //Switch button images in different situations, rather than use 'Sprite Swap'
    [SerializeField]  Sprite[] images;
   
    //Destroy if there has another nameCard
    public string btnName;
    int isOne = 0;

    //Swap audio clip when mark a voice, to show the specific audio spectrum
    AudioSource myAudio;

    private void Start()
    {
        //Set default sprite
        gameObject.GetComponent<Image>().sprite = images[0];

        myAudio = gameObject.transform.Find("UI_Audio").GetComponent<AudioSource>();
        
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
            LayerMask mask = 1 << 8 ; //Layer 8 is for NPC

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
                myAudio.Play();
                //Swap sprite
                gameObject.GetComponent<Image>().sprite = images[1];
                //Delete existed name card
                nameCard.name = btnName + isOne;
                if (isOne > 0)
                {
                    //Find exsited one
                    int preOne = isOne - 1;
                    GameObject preCard = GameObject.Find(btnName + preOne);
                    Destroy(preCard);
                }
                isOne++;
                //Delete audio spectrum on the NPC
                for(int i = 0; i < 8; i++)
                {
                    Destroy(hit.transform.Find("sampleCube" + i).gameObject);
                }
                
            }
            else
            {
                Destroy(nameCard);
                gameObject.GetComponent<Image>().sprite = images[0];
            }
        }
    }
    
}