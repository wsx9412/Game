using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler {
    private Transform iconListTr;

	// Use this for initialization
	void Start () {
        iconListTr = GameObject.Find("Slot").transform;
		
	}

    public void OnDrop(PointerEventData eventData)
    {
        if (this.transform.childCount == 1)
            Drag.draggedIcon.transform.SetParent(iconListTr);
        else
    
        Drag.draggedIcon.transform.SetParent(this.transform);
    }
}

