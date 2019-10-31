using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCount : MonoBehaviour
{
    public Text countText;

    void Start()
    {
        StartCoroutine(characterCountUpdateCoroutine());
    }
    
    IEnumerator characterCountUpdateCoroutine()
    {
        while(true)
        {
            countText.text = CharacterInfoScrollView.Instance.CharacterCount.ToString() + "/10";
            yield return new WaitForSeconds(0.1f);
        }
    }
}
