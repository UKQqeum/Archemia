using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EatSlotManager : MonoBehaviour
{
    [SerializeField]
    private GameObject All_Slot;// 모든 먹이 슬롯들을 관리하는 것. content

    private EatSlot[] eatSlots;

    // Start is called before the first frame update
    void Start()
    {
        eatSlots = All_Slot.GetComponentsInChildren<EatSlot>();

        for (int i = 0; i < eatSlots.Length; i++)// 슬롯의 개수만큼 반복.SetSlotColor(0);
        {
            eatSlots[i].SetColor(0);
            eatSlots[i].gameObject.SetActive(false);// 슬롯 전부 시작하자 마자 안보이게 해주기
        }
    }

    public void FoodPick(Item _item, int count)
    {
        for (int i = 0; i < eatSlots.Length; i++)
        {
            if (eatSlots[i].item != null)// 칸에 아이템이 있을 때
            {
                if (eatSlots[i].item.itemName == _item.itemName)// 슬롯 안에 같은 아이템이 들어있음
                {
                    //Debug.Log(i + "번째 슬롯에 같은 아이템이 들어있음");
                    eatSlots[i].SetFoodCount(count);// 슬롯안의 아이템 갯수 증가
                    return;
                }
            }
            else
            {
                eatSlots[i].SetColor(1);
                eatSlots[i].AddPickFood(_item, count);
                eatSlots[i].gameObject.SetActive(true);// 슬롯 전부 시작하자 마자 안보이게 해주기
                return;
            }
        }
    }
    public void ClearSlotItem()// 아이템을 전부 썼을 때 뒤의 아이템이 앞으로 오도록
    {
        for (int i = 0; i < eatSlots.Length - 1; i++)
        {
            //Debug.Log(i + " 번?");
            if (eatSlots[i].item == null)// i번째 슬롯이 비어있고
            {
                if (eatSlots[i + 1].item != null)// i의 다음 슬롯에 아이템이 있을 때
                {// 본인이 빈 슬롯이고 뒷슬롯이 차있을 때에는 뒷슬롯을 앞슬롯으로 옮기기
                    eatSlots[i].item = eatSlots[i + 1].item;
                    eatSlots[i].itemImage.sprite = eatSlots[i + 1].itemImage.sprite;
                    eatSlots[i].itemCount = eatSlots[i + 1].itemCount;
                    eatSlots[i].item.itemNumber = eatSlots[i + 1].item.itemNumber;
                    eatSlots[i].gameObject.SetActive(true);
                    eatSlots[i].SetColor(1);
                    eatSlots[i].SetFoodCount(0);
                    eatSlots[i + 1].item = null;
                    eatSlots[i + 1].itemImage.sprite = null;
                    eatSlots[i + 1].itemCount = 0;
                    eatSlots[i + 1].gameObject.SetActive(false);
                    eatSlots[i + 1].SetColor(0);
                }
            }
        }
    }
}
