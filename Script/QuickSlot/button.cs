using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {
    GameObject test;
    
    public void AddItem()
    {
        int random = Random.Range(0, Inventory.prefab.Count);
        for (int i = 0; i < Inventory.slot.Count; i++)
        {
            if (Inventory.slot[i].transform.childCount == 0)
            {
                test = Instantiate(Inventory.prefab[random], Inventory.slot[i].transform, Inventory.slot[i]);
                test.transform.SetParent(Inventory.slot[i].transform);
                break;
            }
        }
    }
}
