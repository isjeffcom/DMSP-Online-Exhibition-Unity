using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Instance for cross cs access
    public static UIController _ins;

    // Mode
    public static int _mode = 0;

    // All UI Array Container
    private GameObject[] All_Pages;

    private void Awake()
    {
        _ins = this;

        // Get All Pages
        All_Pages = GameObject.FindGameObjectsWithTag("UI_Page");
    }

    public void switchUIView(string pageName, bool cursor)
    {

        switchCursor(cursor);
        
        foreach (GameObject page in All_Pages)
        {
            if (pageName == page.name)
            {
                page.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                page.GetComponent<Canvas>().enabled = false;
            }
        }

    }

    public void switchCursor(bool bol)
    {
        Cursor.visible = bol;
        Cursor.lockState = bol ? CursorLockMode.None : CursorLockMode.Locked;
    }

}
