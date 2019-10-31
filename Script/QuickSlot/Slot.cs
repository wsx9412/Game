using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Slot : MonoBehaviour {
    public List<GameObject> slotScripts = new List<GameObject>();
    public List<GameObject> prefab = new List<GameObject>();

    public InfoForm infoForm;

    public Dictionary<CharacterInfo, GameObject> viewList = new Dictionary<CharacterInfo, GameObject>();
    public int Count
    {
        get
        {
            return viewList.Count;
        }
    }


    public GameObject content;
    public GameObject attackRange;

    private static Slot instance;
    public static Slot Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Slot>();
            return instance;
        }
    }

    void Start()
    {
        Inventory.slot = slotScripts;
        Inventory.prefab = prefab;
    }
	

	public void AddItem (CharacterInfo charInfo) {

        UnitName unitName = charInfo.unitName;

        GameObject temp = MasterObjectPool.Instance.GetFromPoolOrNull("QuickSlotIcon", gameObject);
        temp.transform.localScale = Vector3.one;

        GameObject icon = temp.transform.GetChild(2).gameObject;
        icon.GetComponent<Image>().sprite = Database.Instance.unitIconSpritesDict[unitName]; // 아이콘에 맞는 스프라이트

        var quickSlotIcon = temp.GetComponent<QuickSlotIcon>();
        
        quickSlotIcon.unitName = charInfo.unitName;
        quickSlotIcon.characterInfo = charInfo;
        quickSlotIcon.transform.GetChild(0).GetComponent<Image>().color = Database.Instance.colors[(int)charInfo.rarity];

        temp.transform.SetSiblingIndex(999);
        temp.SetActive(true);
        viewList.Add(quickSlotIcon.characterInfo, temp);
    }
    public void DeleteItem(GameObject item)
    {
        viewList.Remove(item.GetComponent<QuickSlotIcon>().characterInfo);
        //Destroy(item);
        item.SetActive(false);
    }

    public void SetInfoForm(CharacterInfo charInfo)
    {
        // 등급에 따라 마름모갯수
        int index = 0;
        for (index = 0; index <= (int)charInfo.rarity; index++)
            infoForm.stars[index].SetActive(true);
        for (; index < System.Enum.GetValues(typeof(Rarity)).Length; index++)
            infoForm.stars[index].SetActive(false);

        infoForm.nameText.text = charInfo.printName;
        infoForm.nameText.color = Database.Instance.colors[(int)charInfo.rarity];

        //infoForm.rarityText.text = charInfo.rarity.ToString();
        //infoForm.rarityText.color = Database.Instance.colors[(int)charInfo.rarity];

        infoForm.attackText.text = ((int)charInfo.damage).ToString() + "(+" + charInfo.addedDamage + ")";
        infoForm.attackSpeedText.text = ((int)(charInfo.attackSpeed * 100)).ToString() + "%";
        infoForm.criticalText.text = ((int)(charInfo.criticalPercent)).ToString() + "%";

        infoForm.armorText.text = ((int)charInfo.armor).ToString() + "(+" + charInfo.addedArmor + ")"; ;
        infoForm.hpText.text = ((int)charInfo.currentHP) + " / " + ((int)charInfo.maxHP);
        infoForm.hpSlider.value = (float)charInfo.currentHP / charInfo.maxHP;
    }

    public void ShowInfo(Vector3 position)
    {
        infoForm.form.transform.position = position;
        infoForm.form.SetActive(true);
    }

    public void CloseInfo()
    {
        infoForm.form.SetActive(false);
    }

    public void deleteItemByQuickSlot(QuickSlotIcon item)
    {
        viewList.Remove(item.characterInfo);
        //Destroy(viewList[item]);
        viewList[item.characterInfo].SetActive(false);
    }

    public void OnClickSort_Grade()
    {
        int index = 0;
        for(int i = Enum.GetValues(typeof(Rarity)).Length; i>=0; i--)
        {
            foreach(var go in viewList)
            {
                
                if (go.Key.rarity == (Rarity)i)
                {
                    go.Value.transform.SetSiblingIndex(index++);
                }
            }
        }

    }
    public void OnClickSort_Class()
    {
        int index = 0;
        for (int unitClass = 0; unitClass < Enum.GetValues(typeof(UnitName)).Length; unitClass++)
        {
            for (int i = Enum.GetValues(typeof(Rarity)).Length - 1; i >= 0; i--)
            {
                foreach (var go in viewList)
                {
                    if(go.Key.rarity == (Rarity)i && go.Key.unitName == (UnitName)unitClass)
                    {
                        go.Value.transform.SetSiblingIndex(index++);
                    }
                }
            }
        }


    }

   
}
