using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum QuickSlotState
{
    Placement,
    Scroll,
}

public class IconDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    QuickSlotIcon icon;
    Slot slot;
    public GameObject cam;
    Vector3 mousePosition;
    bool impossible;

    bool dragCheck = false;
    

    private void Awake()
    {
        cam = GameObject.Find("CameraMovement");
        icon = GetComponent<QuickSlotIcon>();
        slot = Slot.Instance;
        init();
    }

    private void init()
    {
        slot.CloseInfo();

        mousePosition = Vector3.zero;

        impossible = true;

        //ObjectPlacement.Instance.unitInfo.SetActive(false);
        ObjectPlacement.Instance.greenCircle.SetActive(false);
        ObjectPlacement.Instance.redCircle.SetActive(false);
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragCheck = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        ObjectPlacement.Instance.greenCircle.transform.position = mousePosition;
        ObjectPlacement.Instance.redCircle.transform.position = mousePosition;
        ObjectPlacement.Instance.attackRange.transform.position = mousePosition;

        int mask = 1 << LayerMask.NameToLayer("unit") | 1 << LayerMask.NameToLayer("wall");

        Collider2D unitCol = Physics2D.OverlapCircle(mousePosition, 5.0f, mask);
        //Collider2D hitQuickSlot = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("quickSlot"));
        Collider2D hitScrollView = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("scrollView"));

        QuickSlotState state = QuickSlotState.Placement;

        if (hitScrollView != null)
            state = QuickSlotState.Scroll;

        

        switch (state)
        {
            case QuickSlotState.Placement:
                //ObjectPlacement.Instance.unitInfo.SetActive(false);
                slot.CloseInfo();
                ObjectPlacement.Instance.attackRange.SetActive(true);
                // 설치시 방해되는 유닛이나 벽이 있는지 확인
                if (unitCol != null)
                {
                    impossible = true;
                    ObjectPlacement.Instance.greenCircle.SetActive(false);
                    ObjectPlacement.Instance.redCircle.SetActive(true);
                }
                else
                {
                    impossible = false;
                    ObjectPlacement.Instance.greenCircle.SetActive(true);
                    ObjectPlacement.Instance.redCircle.SetActive(false);
                }
                break;

            case QuickSlotState.Scroll:
                impossible = true;
                
                eventData.scrollDelta = eventData.delta/3;
                ObjectPlacement.Instance.scrollView.GetComponent<ScrollRect>().OnScroll(eventData);
                //ObjectPlacement.Instance.scrollView.GetComponent<ScrollRect>().OnDrag(eventData);
                slot.ShowInfo(mousePosition);
                ObjectPlacement.Instance.attackRange.SetActive(false);

                //ObjectPlacement.Instance.unitInfo.transform.position = transform.position + new Vector3(30, 30);
                //ObjectPlacement.Instance.unitInfo.SetActive(true);

                ObjectPlacement.Instance.greenCircle.SetActive(false);
                ObjectPlacement.Instance.redCircle.SetActive(false);
                
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        ObjectPlacement.Instance.attackRange.SetActive(false);

        if (impossible)
        {
            EndClickIcon(gameObject);
        }
        else if(CharacterInfoScrollView.Instance.CharacterCount >= 10)
        {
            Notice.Instance.ShowNotice("배치할 수 있는 최대인원을 초과했습니다.");
            EndClickIcon(gameObject);
        }
        else
        {
            Character character = UnitPool.Instance.GetFromPool(icon.characterInfo);
            character.transform.position = mousePosition;
            //character.quickSlotIcon = icon;
            character.EnableCharacter();
            //UnitPool.Instance.EnableCharacter(character);

            Slot.Instance.DeleteItem(gameObject);
        }

        init();
    }

    public void OnClickIcon(GameObject item)
    {
        item.transform.GetChild(0).gameObject.SetActive(false);
        item.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void EndClickIcon(GameObject item)
    {
        item.transform.GetChild(1).gameObject.SetActive(false);
        item.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragCheck = false;

        init();

        OnClickIcon(gameObject);
        Slot.Instance.SetInfoForm(icon.characterInfo);
        Slot.Instance.ShowInfo(transform.position);

        ObjectPlacement.Instance.attackRange.transform.localScale = new Vector3(icon.characterInfo.attackDist * 2.3f* cam.GetComponent<CameraMovement>().CamRatio(), icon.characterInfo.attackDist * 2.3f* cam.GetComponent<CameraMovement>().CamRatio(), 1);
        //ObjectPlacement.Instance.unitInfo.transform.position = transform.position + new Vector3(30, 30);
        //ObjectPlacement.Instance.unitInfo.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EndClickIcon(gameObject);

        if (!dragCheck)
        {
            ObjectPlacement.Instance.attackRange.SetActive(false);

            init();
        }
    }
}
