using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationResult : MonoBehaviour
{
    public GameObject form;

    public Image background;

    public Image[] charRarityStars;
    public Animator charAnimator;
    public SpriteRenderer charSpriteRenderer;

    public Text charName;
    public Text charRarity;

    public Text charAttack;
    public Text charAttackSpeed;
    public Text charCritical;
    public Text charArmor;
    public Text charMaxHP;

    public Image circle;
    public ParticleSystem circleOra;

    public Text[] skillName;
    public Text[] skillDesc;

    private static CombinationResult instance = null;
    public static CombinationResult Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CombinationResult>();
            }
            return instance;
        }
    }


    public void ShowCombinationResult(CharacterInfo charInfo)
    {
        Color rarityColor = Database.Instance.colors[(int)charInfo.rarity];

        background.color = rarityColor;

        int index = 0;
        for (; index <= (int)charInfo.rarity; index++)
        {
            charRarityStars[index].gameObject.SetActive(true);
            charRarityStars[index].color = rarityColor;
        }

        int rarityLength = System.Enum.GetValues(typeof(Rarity)).Length;
        for (; index < rarityLength; index++)
        {
            charRarityStars[index].gameObject.SetActive(false);
        }

        charAnimator.runtimeAnimatorController = Database.Instance.unitAnimatorsDict[charInfo.unitName];
        charSpriteRenderer.material = Database.Instance.rarityMaterial[(int)charInfo.rarity];

        charName.text = charInfo.printName;
        charRarity.text = charInfo.rarity.ToString();
        charRarity.color = rarityColor;

        charAttack.text = ((int)charInfo.damage).ToString();
        charAttackSpeed.text = ((int)(charInfo.attackSpeed * 100)).ToString() + "%";
        charCritical.text = ((int)(charInfo.criticalPercent)).ToString() + "%";

        charArmor.text = ((int)charInfo.armor).ToString();
        charMaxHP.text = ((int)charInfo.maxHP).ToString();
        

        circle.color = rarityColor;

        var main = circleOra.main;
        rarityColor.a = 0.5f;
        main.startColor = rarityColor;
        var emission = circleOra.emission;
        emission.enabled = true;


        // 스킬
        var dataset = Database.Instance.GetUnitDataset(charInfo.unitName);
        var attack = dataset.character.myAttack;
        
        for(int i=0;i<2;i++)
        {
            skillName[i].gameObject.SetActive(false);
            skillDesc[i].gameObject.SetActive(false);
        }

        for(int i=0;i<attack.skills.Length;i++)
        {
            skillName[i].text = attack.skills[i].name;
            skillDesc[i].text = attack.skills[i].skillDesc;
            skillName[i].gameObject.SetActive(true);
            skillDesc[i].gameObject.SetActive(true);
        }

        form.SetActive(true);

        SoundManager.Instance.PlaySFX("combination");
    }
}
