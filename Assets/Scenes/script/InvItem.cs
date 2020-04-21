using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvItem : MonoBehaviour
{
    private Text tip;

    private bool itemDetailEnabled = false;
    private void Awake()
    {
        // Find tip
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            tip.text = "E to check " + gameObject.name;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.5f);
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
                //dialogAns.GetComponent<Text>().text = "Hello";
                itemDetailEnabled = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tip.text = "";
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        itemDetailEnabled = false;
    }
}
