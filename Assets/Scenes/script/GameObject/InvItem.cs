using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvItem : MonoBehaviour
{
    private Text tip;

    private bool itemDetailEnabled = false;

    public bool itemChecked = false;

    private void Awake()
    {
        // Find tip
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            if (!AudioController._isPlaying)
            {
                tip.text = "E to check " + gameObject.name;
            } else
            {
                tip.text = "Wait for audio to finished...";
            }
            
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            // Check if dialog has already enabled
            if (Input.GetKey(KeyCode.E) && !itemDetailEnabled)
            {
                //clear tip
                tip.text = "";
                
                // Display dialog container
                ItemController._ins.ShowItemDetail(gameObject.name);
                itemDetailEnabled = true;

                //Play check audio
                InteractiveAudio._ins.clipChange("clue_click");

                itemChecked = true;

                if (SortingController._ins.allChecked())
                {
                    SortingController._ins.addDragScript();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tip.text = "";
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        itemDetailEnabled = false;
    }
}
