using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class Recipe : MonoBehaviour
{
    public string name;
    public List<UnitName> materialsName;
    public List<GameObject> rarityIcon;
    public GameObject disAble;
    public Button button;
    public Rarity materialsRarity;
    public Rarity minRarity;
    public Rarity maxRarity;
    public Text nameText;
    public Text materialsText;
    public Text characterInfoText;
    public string neededArtifact;
    public string skillInfo;
    public int combinationNumber;
    public void Start()
    {
        materialsRarity = minRarity;
    }
    public void onDisable()
    {
        materialsRarity = minRarity;
    }
    public void SetMaterialsRarity(Rarity rarity)
    {
        materialsRarity = rarity;
    }
    public void SetText()
    {
        StringBuilder builder = new StringBuilder();
        nameText.text = name;
        builder.AppendFormat("{0} + {1} + {2} + {3}", 
            CombinationList.Instance.SetUnitName(materialsName[0]), 
            CombinationList.Instance.SetUnitName(materialsName[1]),
            CombinationList.Instance.SetUnitName(materialsName[2]), 
            CombinationList.Instance.SetUnitName(materialsName[3]));
        if (neededArtifact != "")
            builder.AppendFormat(" + {0}",neededArtifact);
        materialsText.text = builder.ToString();
        characterInfoText.text = skillInfo;
        if ((int)minRarity != 0)
        {
            for (int i = 0; i < (int)minRarity; i++)
            {
                //rarityIcon[i].SetActive(false);
                rarityIcon[i].GetComponent<Image>().color = new Color32(0x52, 0x52, 0x52, 0xFF);
            }
        }
        if (maxRarity != Rarity.Epic)
        {
            for (int i = (int)Rarity.Epic; i > (int)maxRarity; i--)
            {
                //rarityIcon[i].SetActive(false);
                rarityIcon[i].GetComponent<Image>().color = new Color32(0x52, 0x52, 0x52, 0xFF);
            }
        }
    }
}
