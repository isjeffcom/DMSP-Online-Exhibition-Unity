using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
{
    private Texture2D cursorTexture;
    private Texture2D cursorDragable;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot;

    private void Awake()
    {
        cursorTexture = Resources.Load<Texture2D>("cursor");
        cursorDragable = Resources.Load<Texture2D>("cursor_dragable");
        hotSpot = new Vector2(20, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*if (gameObject.GetComponent<Button>().interactable)
        {
            if (MainController._act == 2 && gameObject.tag == "NameBlock")
            {
                Cursor.SetCursor(cursorDragable, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            }
        }*/
        
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.name == "Close_Button")
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    public void ToNextUI(string name)
    {
        UIController._ins.switchUIView(name, true);
    }

    public void CloseItemWindow()
    {
        ImageViewerController._ins.CloseImage();
    }

    public void LastImage()
    {
        Debug.Log("a");
        ImageViewerController._ins.LastImage();
    }

    public void NextImage()
    {
        ImageViewerController._ins.NextImage();
    }

    public void Complete()
    {
        MissionController._ins.CompleteClick();
    }

    public void toActs(int act)
    {
        if (act == 2)
        {
            Act1and2Controller._ins.EnterAct2();
        }
        if (act == 3)
        {
            Act2and3Controller._ins.EnterAct3();
        }
        if (act == 4)
        {
            Act3and4Controller._ins.EnterAct4();
        }
    }

    public void addAudioClip(string name)
    {
        InteractiveAudio._ins.clipChange(name);
    }

    
}
