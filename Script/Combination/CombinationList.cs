using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class CombinationList : MonoBehaviour
{
    public GameObject combinationList;
    public GameObject combinationBoard;
    public GameObject combinationBoard2;
    public GameObject combinationEntity;
    public GameObject combinationContentEntity;
    public GameObject combineAgreeBox;
    public GameObject combineDeniedBox;
    public GameObject unitListParent;
    public GameObject combinationContent;
    public GameObject combinationTemp;
    public Text recipeBox;
    public QuickSlotIcon combineResult;
    public List<GameObject> prefabs;
    public List<Transform> combinationIcon;
    public List<GameObject> rarityButton;
    public GameObject test;

    List<CombinationInfo> unitList = new List<CombinationInfo>();
    List<CombinationInfo> unitCheckList = new List<CombinationInfo>();
    List<CharacterInfo> selectedUnitList = new List<CharacterInfo>();
    List<CharacterInfo> checkList = new List<CharacterInfo>();
    List<Character> selectedCharacter = new List<Character>();
    List<int> selectedNumber = new List<int>();
    List<int> checkNumber = new List<int>();
    List<Recipe> combineList = new List<Recipe>();
    List<Character> characters = new List<Character>();
    //private Gacha gacha = Gacha.Instance;
    private int iconNumber;
    private int i;
    private Combination combination;
    private bool makeContent = true;
    private GameObject selectedRarityIcon;
    private static CombinationList instance;
    public static CombinationList Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CombinationList>();
            return instance;
        }
    }

    private void Start()
    {
        iconNumber = 0;
    }

    public void GetRecipes()
    {
        combination = combinationTemp.GetComponent<Combination>();
        if(makeContent)
        {
            foreach (var go in combination.combinationRecipes)
            {
                GameObject gameObject = Instantiate(combinationContentEntity, combinationContent.transform);
                Recipe recipe = gameObject.GetComponent<Recipe>();
                QuickSlotIcon quickSlotIcon = gameObject.GetComponent<QuickSlotIcon>();
                recipe.neededArtifact = go.neededArtifact;
                quickSlotIcon.characterInfo.unitName = go.result;
                quickSlotIcon.unitName = go.result;
                recipe.minRarity = go.minRarity;
                if(recipe.minRarity == Rarity.Normal)
                {
                    recipe.combinationNumber = 1;
                    recipe.maxRarity = Rarity.Legend;
                }
                else
                {
                    recipe.combinationNumber = 0;
                    recipe.maxRarity = Rarity.Epic;
                }
                gameObject.GetComponent<Image>().sprite = Database.Instance.unitIconSpritesDict[quickSlotIcon.unitName];
                recipe.materialsName.Add(go.material0);
                recipe.materialsName.Add(go.material1);
                recipe.materialsName.Add(go.material2);
                recipe.materialsName.Add(go.material3);
                recipe.name = SetUnitName(quickSlotIcon.unitName, gameObject, recipe,quickSlotIcon);
                recipe.SetText();
                combineList.Add(recipe);
            }
            makeContent = false;
        }
        foreach(var go in combineList)
        {
            CombineCheck2(go);
        }
    }
    public string SetUnitName(UnitName unitName)
    {
        switch (unitName)
        {
            case UnitName.unit_warrior:
                return "전사";
            case UnitName.unit_archer:
                return "궁수";
            case UnitName.unit_halberdier:
                return "창병";
            case UnitName.unit_barbarian:
                return "야만전사";
            case UnitName.unit_monk:
                return "수도승";
            case UnitName.unit_swordmaster:
                return "검객";
            case UnitName.unit_highwarrior:
                return "상급 전사";
            case UnitName.unit_higharcher:
                return "상급 궁수";
            case UnitName.unit_highhalberdier:
                return "상급 창병";
            case UnitName.unit_highbarbarian:
                return "상급 야만전사";
            case UnitName.unit_highmonk:
                return "상급 수도승";
            case UnitName.unit_highswordmaster:
                return "상급 검객";
            case UnitName.unit_ultimatewarrior:
                return "불꽃전사";
            case UnitName.unit_ultimatearcher:
                return "날개궁수";
            case UnitName.unit_ultimatehalberdier:
                return "금빛미늘창병";
            case UnitName.unit_ultimatebarbarian:
                return "악마야만";
            case UnitName.unit_ultimatemonk:
                return "변이수도승";
            case UnitName.unit_ultimateswordmaster:
                return "가면검객";
        }
        return "알수없음";
    }
    public string SetUnitName(UnitName unitName,GameObject go,Recipe recipe,QuickSlotIcon quickSlotIcon)
    {
        switch (unitName)
        {
            case UnitName.unit_warrior:
                recipe.skillInfo = "근력이 뛰어난 전사이다.";
                return "전사";
            case UnitName.unit_archer:
                recipe.skillInfo = "하나의 화살을 신중하게 쏠 수 있는 궁수이다.";
                return "궁수";
            case UnitName.unit_halberdier:
                recipe.skillInfo = "창술이 뛰어난 병사이다.";
                return "창병";
            case UnitName.unit_barbarian:
                recipe.skillInfo = "동물의 감각을 지닌 전사이다.";
                return "야만전사";
            case UnitName.unit_monk:
                recipe.skillInfo = "수행을 통해 기를 사용할 수 있게 된 수도승이다.";
                return "수도승";
            case UnitName.unit_swordmaster:
                recipe.skillInfo = "검술수련에 열중하는 검객이다.";
                return "검객";
            case UnitName.unit_highwarrior:
                recipe.skillInfo = "망치를 불꽃으로 휘감아 적을 강타한다.";
                return "상급 전사";
            case UnitName.unit_higharcher:
                recipe.skillInfo = "적을 관통하는 화살 2발을 쏜다.";
                return "상급 궁수";
            case UnitName.unit_highhalberdier:
                recipe.skillInfo = "적을 빠르게 3회 공격하고 마지막 공격에는 검기를 내뿜는다.";
                return "상급 창병";
            case UnitName.unit_highbarbarian:
                recipe.skillInfo = "대지를 불태워 적을 공격한다.";
                return "상급 야만전사";
            case UnitName.unit_highmonk:
                recipe.skillInfo = "기를 모아 거대한 기의 구체를 적에게 투척한다.";
                return "상급 수도승";
            case UnitName.unit_highswordmaster:
                recipe.skillInfo = "적을 빠르게 내려친다.";
                return "상급 검객";
            case UnitName.unit_ultimatewarrior:
                recipe.skillInfo = "빠르게 두번 공격하고 추가로 강한 공격을 가한다.";
                return "불꽃전사";
            case UnitName.unit_ultimatearcher:
                recipe.skillInfo = "화살에 기를 모아 발사한다.";
                return "날개궁수";
            case UnitName.unit_ultimatehalberdier:
                recipe.skillInfo = "빠르게 3회 검기를 내뿜는다.";
                return "금빛미늘창병";
            case UnitName.unit_ultimatebarbarian:
                recipe.skillInfo = "2회 연속으로 검기를 내뿜는다.";
                return "악마야만";
            case UnitName.unit_ultimatemonk:
                recipe.skillInfo = "빠르게 3회 공격하고 기를 모아 거대한 기를 투척한다.";
                return "변이수도승";
            case UnitName.unit_ultimateswordmaster:
                recipe.skillInfo = "분신을 만들어서 적을 2회 공격하고 강하게 내려친다.";
                return "가면검객";
        }
        return "알수없음";
    }
    public string SetRarityName(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Normal:
                return "일반";
            case Rarity.Rare:
                return "희귀";
            case Rarity.Unique:
                return "유일";
            case Rarity.Legend:
                return "전설";
            case Rarity.Epic:
                return "신화";
        }
        return "알수없음";
    }
    public bool ReturnContent()
    {
        return makeContent;
    }
    public void GetCombinationList()//조합리스트 받아오기
    {
        foreach(var ul in unitList)
        {
            Destroy(ul.gameObject);
        }
        unitList.Clear();
        Recipe recipe = combineResult.gameObject.GetComponent<Recipe>();
        foreach (var character in UnitPool.Instance.GetActiveCharacters())
        {
                for(int i = 0; i < recipe.materialsName.Count; i++)
                {
                    if (character.characterInfo.rarity == recipe.materialsRarity && character.characterInfo.unitName == recipe.materialsName[i] && !selectedUnitList.Contains(character.characterInfo))
                    {
                        GameObject game = Instantiate(combinationEntity, unitListParent.transform);
                        CombinationInfo cInfo = game.GetComponent<CombinationInfo>();
                        cInfo.toggle.isOn = false;
                        cInfo.unitInfo = character.characterInfo;
                        cInfo.ShowInfoInCombinationList(game.GetComponentInChildren<Text>());
                        cInfo.character = character;
                        unitList.Add(cInfo);
                        break;
                    }
                }
        }
        foreach (var go in Slot.Instance.viewList)
        {
            if (recipe.materialsName.Count > 0)
            {
                for(int i = 0; i < recipe.materialsName.Count; i++)
                {
                    if (go.Key.rarity == recipe.materialsRarity && go.Key.unitName == recipe.materialsName[i] && !selectedUnitList.Contains(go.Key))
                    {
                        GameObject game = Instantiate(combinationEntity, unitListParent.transform);
                        CombinationInfo cInfo = game.GetComponent<CombinationInfo>();
                        cInfo.toggle.isOn = false;
                        cInfo.unitInfo = go.Key;
                        cInfo.ShowInfoInCombinationList(game.GetComponentInChildren<Text>());
                        unitList.Add(cInfo);
                        break;
                    }
                }
            }
        }
    }
    public bool MenualFilling()//수동조합창 채우기
    {
        foreach (var ul in unitList)
        {
            if (ul.toggle.isOn)
            {
                unitCheckList.Add(ul);

                /*
                GameObject go = Instantiate(prefabs, combinationIcon[iconNumber]);
                Image image = go.GetComponent<Image>();
                CombinationPrefabs prefab = go.GetComponent<CombinationPrefabs>();
                var material = Database.Instance.rarityMaterial[(int)ul.unitInfo.characterInfo.rarity];
                image.sprite = Database.Instance.unitIconSpritesDict[ul.unitInfo.unitName];
                image.material = material;
                prefab.cbInfo = ul;
                prefab.iconNumber = iconNumber++;
                prefab.unitName = ul.unitInfo.unitName;
                selectedUnitList.Add(ul.unitInfo);
                */
            }
            else if (unitCheckList.Contains(ul))
            {
                unitCheckList.Remove(ul);
            }
        }
        if(unitCheckList.Count + selectedUnitList.Count > 7)
        {
            unitCheckList.Clear();
            return false;
        }
        else
        {
            foreach(var ul in unitCheckList)
            {
                for (int j = 0; j <= 5; j++)
                {
                    if (prefabs[j].GetComponent<CombinationPrefabs>().unitName == ul.unitInfo.unitName)
                    {
                        GameObject go = Instantiate(prefabs[j], combinationIcon[iconNumber]);
                        var material = Database.Instance.rarityMaterial[(int)ul.unitInfo.rarity];
                        go.GetComponent<Image>().material = material;
                        go.GetComponent<CombinationPrefabs>().cbInfo = ul;
                        go.GetComponent<CombinationPrefabs>().iconNumber = iconNumber++;
                        selectedUnitList.Add(ul.unitInfo);
                        if(ul.character != null)
                            selectedCharacter.Add(ul.character);
                    }
                }
            }
            unitCheckList.Clear();
            return true;
        }
    }
    public void AutoFilling()
    {
        Recipe recipe = combineResult.gameObject.GetComponent<Recipe>();
        int count = 0;
        int i = 0;
        int endCount = recipe.materialsName.Count;
        selectedNumber.Clear();
        foreach(var va in Slot.Instance.viewList)
        {
            for(i = 0; i < endCount; i++)
            {
                if (selectedNumber.Contains(i))
                {
                    continue;
                }
                if (va.Key.unitName == recipe.materialsName[i] && va.Key.rarity == recipe.materialsRarity)
                {
                    for (int j = 0; j <= 5; j++)
                    {
                        if (prefabs[j].GetComponent<CombinationPrefabs>().unitName == va.Key.unitName)
                        {
                            GameObject go = Instantiate(prefabs[j], combinationIcon[iconNumber]);
                            CombinationInfo cbInfo = new CombinationInfo();
                            cbInfo.unitInfo = va.Key;
                            var material = Database.Instance.rarityMaterial[(int)va.Key.rarity];
                            go.GetComponent<Image>().material = material;
                            go.GetComponent<CombinationPrefabs>().cbInfo = cbInfo;
                            go.GetComponent<CombinationPrefabs>().iconNumber = iconNumber++;
                            selectedUnitList.Add(cbInfo.unitInfo);
                            break;
                        }
                    }
                    selectedNumber.Add(i);
                    count++;
                    break;
                }
            }
            if (count == endCount)
                break;
        }

        foreach(var character in UnitPool.Instance.GetActiveCharacters())
        {
            for (i = 0; i < endCount; i++)
            {
                if (selectedNumber.Contains(i))
                {
                    continue;
                }
                if (character.characterInfo.unitName == recipe.materialsName[i] && character.characterInfo.rarity == recipe.materialsRarity)
                {
                    for (int j = 0; j <= 5; j++)
                    {
                        if (prefabs[j].GetComponent<CombinationPrefabs>().unitName == character.characterInfo.unitName)
                        {
                            GameObject instanceGo = Instantiate(prefabs[j], combinationIcon[iconNumber]);
                            CombinationInfo cbInfo = new CombinationInfo();
                            cbInfo.unitInfo = character.characterInfo;
                            cbInfo.character = character;
                            var material = Database.Instance.rarityMaterial[(int)character.characterInfo.rarity];
                            instanceGo.GetComponent<Image>().material = material;
                            instanceGo.GetComponent<CombinationPrefabs>().cbInfo = cbInfo;
                            instanceGo.GetComponent<CombinationPrefabs>().iconNumber = iconNumber++;
                            selectedUnitList.Add(character.characterInfo);
                            selectedCharacter.Add(character);
                            break;
                        }
                    }
                    selectedNumber.Add(i);
                    count++;
                    break;
                }
            }
            if (count == endCount)
                break;
        }

    }
    public void EmptyOne(GameObject gameObject)//아이콘 한칸 비우기
    {

        CombinationPrefabs cbInfo = gameObject.GetComponent<CombinationPrefabs>();
        int number = cbInfo.iconNumber;
        selectedUnitList.Remove(cbInfo.cbInfo.unitInfo);
        selectedCharacter.Remove(cbInfo.cbInfo.character);
        Destroy(gameObject);
        for (int i = number; i < iconNumber - 1; i++)
        {
            if (/*i < iconNumber + 1 && */i < 6)
            {
                combinationIcon[i + 1].GetChild(0).GetComponent<CombinationPrefabs>().iconNumber = i;
                combinationIcon[i + 1].GetChild(0).parent = combinationIcon[i].transform;
            }
        }
        iconNumber--;
    }
    public void EmptyAll()//조합 아이콘 전체 비우기
    {
        for(int i = 0; i <= prefabs.Count; i++)
        {
            if(combinationIcon[i].childCount != 0)
            {
                Destroy(combinationIcon[i].GetChild(0).gameObject);
            }
        }
        selectedUnitList.Clear();
        selectedCharacter.Clear();
        iconNumber = 0;
    }

    public void Combine()//조합
    {
        Recipe recipe = combineResult.gameObject.GetComponent<Recipe>();
        foreach (var go in selectedUnitList)
        {
            if(Slot.Instance.viewList.ContainsKey(go))
                Slot.Instance.DeleteItem(Slot.Instance.viewList[go]);
        }
        foreach(var go in selectedCharacter)
        {
            go.DisableCharacter();
        }
        if(recipe.neededArtifact != "")
        {
            if (ArtifactManager.Instance.FindArtifact(recipe.neededArtifact))
                ArtifactManager.Instance.DeleteArtifact(recipe.neededArtifact);
        }
        selectedUnitList.Clear();
        selectedCharacter.Clear();

        Prefix prefix = Gacha.Instance.RandomPrefix();
        var charInfo = RandomCharacterInfoCreator.Instance.CreateRandomStat(combineResult.unitName, combineResult.characterInfo.rarity, prefix);
        Slot.Instance.AddItem(charInfo);

        //결과창
        CombinationResult.Instance.ShowCombinationResult(charInfo);

        CombineInit();

        QuestEventManager.Instance.ReceiveEvent(null,QuestEvent.Combination,1);
    }

    public int CombineCheck()//조합조건을 충족하는지 확인
    {
        checkList.Clear();
        checkNumber.Clear();
        Recipe recipe = combineResult.gameObject.GetComponent<Recipe>();
        i = recipe.materialsName.Count;
        foreach(var go in selectedUnitList)
        {
            if (!checkList.Contains(go))
            {
                for(int j = 0; j < i; j++)
                {
                    if (recipe.materialsName[j] == go.unitName && recipe.materialsRarity == go.rarity && !checkNumber.Contains(j))
                    {
                        checkNumber.Add(j);
                        checkList.Add(go);
                        break;
                    }
                }
            }
        }
        if (i == checkList.Count)
        {
            if(checkList.Count < selectedUnitList.Count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        return 2;
    }
    public void CombineCheck2(Recipe recipe)
    {
        if (recipe.neededArtifact == "")
        {
            recipe.button.enabled = true;
            recipe.disAble.SetActive(false);
        }
        else if (!ArtifactManager.Instance.FindArtifact(recipe.neededArtifact))
        {
            recipe.button.enabled = false;
            recipe.disAble.SetActive(true);
        }
        else if(ArtifactManager.Instance.FindArtifact(recipe.neededArtifact))
        {
            recipe.button.enabled = true;
            recipe.disAble.SetActive(false);
        }
    }
    public void CombineInit()//조합 초기화
    {
        checkNumber.Clear();
        checkList.Clear();
        iconNumber = 0;
    }
    public void RarityChange(Rarity rarity)
    {
        if (combineResult.GetComponent<Recipe>().combinationNumber == 1)
        {
            combineResult.characterInfo.rarity = rarity + 1;
        }
        else
            combineResult.characterInfo.rarity = rarity;
        Recipe recipe = combineResult.GetComponent<Recipe>();
        recipe.SetMaterialsRarity(rarity);
        SelectRarity(recipe);
    }
    public void RaritySetActive(Recipe recipe)
    {
        RarityButton rb;
        recipe.materialsRarity = recipe.minRarity;
        foreach(var go in rarityButton)
        {
            rb = go.GetComponent<RarityButton>();
            if (rb.rarity >= recipe.minRarity && rb.rarity <= recipe.maxRarity)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        SelectRarity(recipe);
    }
    public void SelectRarity(Recipe recipe)
    {
        var material = Database.Instance.rarityMaterial[0];
        foreach(var go in rarityButton)
        {
            if(recipe.materialsRarity == go.GetComponent<RarityButton>().rarity)
            {
                go.GetComponent<Image>().material = material;
            }
            else
            {
                go.GetComponent<Image>().material = null;
            }
        }
    }
    public void SetRecipe()
    {
        StringBuilder builder = new StringBuilder();
        recipeBox.text = null;
        Recipe recipe = combineResult.GetComponent<Recipe>();
        for(int i = 0; i < recipe.materialsName.Count;i++)
        {
            if (i != 0)
                builder.Append(" + ");
            builder.AppendFormat("{0} {1}", SetRarityName(recipe.materialsRarity), SetUnitName(recipe.materialsName[i]));
        }
        if (recipe.neededArtifact != "")
            builder.AppendFormat(" + {0}", recipe.neededArtifact);
        recipeBox.text = builder.ToString();
        switch (recipe.materialsRarity)
        {
            case Rarity.Normal:
                recipeBox.color = Color.white;
                break;
            case Rarity.Rare:
                recipeBox.color = new Color32(0x49, 0xE3, 0xFF, 0xFF);
                break;
            case Rarity.Unique:
                recipeBox.color = new Color32(0xB4, 0x9C, 0xFF, 0xFF);
                break;
            case Rarity.Legend:
                recipeBox.color = new Color32(0xFF, 0xD8, 0x5A, 0xFF);
                break;
            case Rarity.Epic:
                recipeBox.color = new Color32(0xFF, 0x92, 0xA4, 0xFF);
                break;
        }
    }
}