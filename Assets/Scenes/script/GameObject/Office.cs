using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Office : MonoBehaviour
{
    private Text tip;

    private void Awake()
    {
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            tip.text = gameObject.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            tip.text = "";
        }
    }
}
