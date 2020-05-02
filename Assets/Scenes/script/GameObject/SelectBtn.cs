using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectBtn : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    private bool done = false;

    private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot;

    private void Awake()
    {
        cursorTexture = Resources.Load<Texture2D>("cursor");
        hotSpot = new Vector2(20, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void GetSelect()
    {

        if (done)
        {
            //Debug.Log("Done");
            return;
        }

        string thisName = gameObject.GetComponentInChildren<Text>().text;

        if (SelectController._ins.TryMatch(thisName))
        {
            SelectController._ins.RestoreSelectedNPC(thisName);
            done = true;
            RightAni();
        } 
        else 
        {
            StartCoroutine(WrongAni());
            //Debug.Log("Wrong or not selected");
        }
    }

    IEnumerator WrongAni()
    {
        GetComponent<Animator>().SetBool("open", true);

        yield return new WaitForSeconds(2);

        GetComponent<Animator>().SetBool("open", false);
    }

    public void RightAni()
    {
        gameObject.transform.Find("UI_NPC_Name_Right").GetComponent<Animator>().SetBool("right", true);
    }


}
