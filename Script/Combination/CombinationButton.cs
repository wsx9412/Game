using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationButton : MonoBehaviour
{
    private void getCombinationList(QuickSlotIcon icon, Recipe recipe)
    {
        CombinationList.Instance.combineResult = icon;
        CombinationList.Instance.RaritySetActive(recipe);
        CombinationList.Instance.SetRecipe();
    }

    public void SetRecipe()
    {
        //QuickSlotIcon icon = this.gameObject.GetComponentInParent<QuickSlotIcon>();
        QuickSlotIcon icon = this.gameObject.GetComponent<QuickSlotIcon>();
        CombinationList.Instance.combinationBoard.SetActive(false);
        CombinationList.Instance.combinationBoard2.SetActive(true);
        //버튼 활성화 넣기
        getCombinationList(icon, icon.gameObject.GetComponent<Recipe>());
        CombinationList.Instance.RarityChange(CombinationList.Instance.combineResult.GetComponent<Recipe>().minRarity);
    }
    public void AutoFilling()
    {
        EmptyAll();
        CombinationList.Instance.AutoFilling();
    }
    public void EmptyAll()
    {
        CombinationList.Instance.EmptyAll();
    }
    public void Exit()
    {
        EmptyAll();
        Recipe recipe = CombinationList.Instance.combineResult.GetComponent<Recipe>();
        recipe.materialsRarity = recipe.minRarity;
    }
    public void EmptyOne()
    {
        CombinationList.Instance.EmptyOne(this.gameObject);
    }
    public void ManualFilling()
    {
        if (CombinationList.Instance.MenualFilling())
        {
            CombinationList.Instance.combinationBoard2.SetActive(true);
            CombinationList.Instance.combinationList.SetActive(false);
        }
        else
        {
            CombinationList.Instance.combineDeniedBox.GetComponent<DeniedBox>().text.text = "재료가 너무 많습니다!";
            CombinationList.Instance.combineDeniedBox.SetActive(true);
        }
    }
    public void Combine()
    {
        CombinationList.Instance.Combine();
        CombinationList.Instance.EmptyAll();
        CombinationList.Instance.combinationBoard2.SetActive(false);
    }
    public void CombineCheck()
    {
        int i = CombinationList.Instance.CombineCheck();
        if(i == 0)
        {
            Combine();
        }
        else if(i == 1)
        {
            CombinationList.Instance.combineDeniedBox.GetComponent<DeniedBox>().text.text = "재료가 많거나 적습니다!";
            CombinationList.Instance.combineDeniedBox.SetActive(true);
        }
        else if(i == 2)
        {
            CombinationList.Instance.combineDeniedBox.GetComponent<DeniedBox>().text.text = "재료가 많거나 적습니다!";
            CombinationList.Instance.combineDeniedBox.SetActive(true);
        }
    }
    public void GetList()
    {
        CombinationList.Instance.combinationList.SetActive(true);
        CombinationList.Instance.combinationBoard2.SetActive(false);
        CombinationList.Instance.GetCombinationList();
    }
    
    public void RarityChange()
    {
        EmptyAll();
        CombinationList.Instance.RarityChange(this.gameObject.GetComponent<RarityButton>().rarity);
        CombinationList.Instance.SetRecipe();
    }
    public void GetContent()
    {
        CombinationList.Instance.GetRecipes();
    }
}
