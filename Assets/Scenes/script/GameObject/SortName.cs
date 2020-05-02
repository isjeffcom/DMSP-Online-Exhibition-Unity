using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SortName : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Store the item being dragged
    public static GameObject itemBeingDragged;

    //Original Position
    Vector3 startPosition;

    //Original Parent
    Transform startParent;

    //Cursor
    private Texture2D cursorDragging;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot;

    private void Start()
    {
        gameObject.isStatic = true;

        cursorDragging = Resources.Load<Texture2D>("cursor_dragging");
        hotSpot = new Vector2(20, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        //Set cursor
        Cursor.SetCursor(cursorDragging, hotSpot, cursorMode);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }

        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
