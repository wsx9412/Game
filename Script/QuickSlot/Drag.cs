using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    private Transform inventoryTr;
    public static GameObject draggedIcon;

    void Start()
    {
        inventoryTr = GameObject.Find("Inventory").transform;
    }
	public void OnDrag (PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnBeginDrag (PointerEventData eventData)
    {
        draggedIcon = this.gameObject;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        this.transform.SetParent(inventoryTr);
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        draggedIcon = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
