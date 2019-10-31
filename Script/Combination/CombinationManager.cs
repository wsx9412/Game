using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombinationManager : MonoBehaviour
{ 
    public GameObject parent;
    public GameObject combinationEntity;
    private CombinationButton combinationButton;
    private Vector3 mousePosition;
    private static Collider2D hitCombinationBoard;
    private static Collider2D hitCombinationList;
    private QuickSlotIcon icon;
    private GameObject list;
    private void Awake()
    {
        init();
    }

    private void init()
    {
        icon = null;
        hitCombinationBoard = null;
        hitCombinationList = null;
    }
    private void getCombinationList(QuickSlotIcon icon,Recipe recipe)
    {
        CombinationList.Instance.combineResult = icon;
        CombinationList.Instance.RaritySetActive(recipe);
        CombinationList.Instance.SetRecipe();
    }
    private void openCombinationBoard2()
    {
        CombinationList.Instance.combinationList.SetActive(false);
        CombinationList.Instance.combinationBoard.SetActive(false);
        CombinationList.Instance.combinationBoard2.SetActive(true);
    }
    private void openCombinationList()
    {
        CombinationList.Instance.combinationBoard.SetActive(false);
        CombinationList.Instance.combinationBoard2.SetActive(false);
        CombinationList.Instance.combinationList.SetActive(true);
    }
    private void listSelect(Toggle item)
    {
        ColorBlock cb;
        //Toggle ClickedToggle = item.GetComponent<Toggle>();
        /*하나만 선택하게 할때 코드
        if (toggle != null&& toggle != ClickedToggle)
        {
            cb = toggle.colors;
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.white;
            toggle.colors = cb;
            toggle.isOn = false;
        }
        */
        cb = item.colors;
        if (item.isOn)
        {
            cb.normalColor = Color.gray;
            cb.highlightedColor = Color.gray;
        }
        else
        {
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.white;
        }
        item.colors = cb;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            init();

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            //hitCombinationBoard = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("combination"));
            hitCombinationList = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("combinationList"));/*
            if (hitCombinationBoard != null && hitCombinationBoard.tag == "quickSlot" && hitCombinationList== null)
            {
                icon = hitCombinationBoard.gameObject.GetComponent<QuickSlotIcon>();
            }*/
            if(hitCombinationList != null && hitCombinationList.tag == "combinationList")
            {
                list = hitCombinationList.gameObject;
            }
        }

        if (Input.GetMouseButtonUp(0)) //Drop
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            hitCombinationBoard = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("combination"));/*
            hitCombinationList = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("combinationList"));
            if (hitCombinationBoard != null && hitCombinationBoard.tag == "quickSlot" && hitCombinationList == null)
            {
                if (icon == hitCombinationBoard.gameObject.GetComponent<QuickSlotIcon>())
                {
                    CombinationList.Instance.combinationBoard.SetActive(false);
                    CombinationList.Instance.combinationBoard2.SetActive(true);
                    //버튼 활성화 넣기
                    getCombinationList(icon,icon.gameObject.GetComponent<Recipe>());
                }
            }*/
            if (hitCombinationList != null && hitCombinationList.tag == "combinationList")
            {
                if (list == hitCombinationList.gameObject)
                {
                    //Debug.Log(list.gameObject.transform.GetChild(0).GetComponent<Toggle>().name);
                    listSelect(list.GetComponent<CombinationInfo>().toggle);
                }
            }
        }
    }
}
    
