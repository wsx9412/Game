using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationInfo : MonoBehaviour
{
    public CharacterInfo unitInfo;
    public Character character;
    public Toggle toggle;
    public GameObject go;

    void Awake()
    {
        if(go != null)
            toggle = go.GetComponent<Toggle>();
    }
    public void ShowInfoInCombinationList(Text text)
    {
        CharacterInfo characterInfo = unitInfo;
        string infoText = "이름: " + characterInfo.characterName +
              "  등급: " + characterInfo.rarity.ToString() + "\n" +
              "공격력: " + (int)characterInfo.damage +
              "  공격속도: " + characterInfo.attackSpeed +
              "  체력: " + (int)characterInfo.currentHP + "/" + (int)characterInfo.maxHP +
              "  방어력: " + (int)characterInfo.armor;
        text.text = infoText;
    }
}
