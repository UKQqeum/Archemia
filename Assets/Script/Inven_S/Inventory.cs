using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using static UnityEditor.Progress;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using TMPro;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Item[] items;

    [SerializeField]
    public Item[] Foods;

    public Slot[] GetSlots() { return Slots; }

    public void LoadToInven(int _arryNum, string _itemName, int _itemNum, bool _itemBool)// 로드용
    {// 순서대로 슬롯의 위치, 아이템 이름, 아이템의 개수, 아이템 사용 유무
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
            {
                Slots[_arryNum].AddItem(items[i], _itemNum);
                if (items[i].item_ != _itemBool)
                {
                    items[i].item_ = _itemBool;
                }
            }
            else if (i < 5)// 음식 리스트는 아이템 리스트와 크기가 다르기 때문에
            {
                if (Foods[i].itemName == _itemName)
                {
                    Slots[_arryNum].AddItem(Foods[i], _itemNum);
                }
            }
        }
    }

    public static bool InvenBool = false;// 인벤 활성화 확인, 이게 트루면 마우스 움직임 막아야함

    // 필요한 컴포넌트
    [SerializeField]
    public GameObject InvenPanel;// 인벤토리 전체 판넬
    [SerializeField]
    private GameObject All_Slot;// 모든 인벤 슬롯들을 관리하는 것. content

    [SerializeField]
    private GameManager game;// 게임 매니저

    [SerializeField]
    private CreSlot[] sslot;

    [SerializeField]
    private GameObject stuSlot;// 합성창의 재료 슬롯의 컨텐츠

    [SerializeField]
    private SynthesisMain SyMain;

    [SerializeField]
    private ItemList itemList;

    [SerializeField]
    private EatSlotManager EatManager;

    private StuSlot[] stu;// 합성창의 재료 슬롯

    private Slot[] Slots;// 인벤토리 슬롯
    private Item item;

    //public ItemClick itemclick;
    public string clickText = "";

    public GameObject ItemStatePanel;// 아이템의 상세 설명 판넬

    public AudioSource ItemSound;// 아이템 정보를 볼 때 소리

    void Start()
    {
        Slots = All_Slot.GetComponentsInChildren<Slot>();
        stu = stuSlot.GetComponentsInChildren<StuSlot>();

        ItemStatePanel.SetActive(false);

        for (int i = 0; i < Slots.Length; i++)// 슬롯의 개수만큼 반복.SetSlotColor(0);
        {
            Slots[i].SetColor(0);
            //Debug.Log("슬롯 전부 비활성화");
            Slots[i].gameObject.SetActive(false);// 슬롯 전부 시작하자 마자 안보이게 해주기
        }
    }
    public void OpenInven()
    {
        game.OpenPanel.Play();
        InvenPanel.SetActive(true);// 인벤토리 창 열기
        game.Open();// 게임 매니저의 오픈 함수
        //Vector3 uiV = new Vector3(960, 540, 0);
        //InvenPanel.GetComponent<RectTransform>().anchoredPosition = uiV;// 인벤창 위로
        //LeanTween.move(InvenPanel, uiV, 0.5f);// 0.5초만에 인벤 판넬을 위로 올리기
    }// 인벤 창 열기

    public void AII(Item _item, int count)// 파견 후 아이템을 받아서 인벤토리에 넣기
    {
        for (int i = 0; i < Slots.Length; i++)// 슬롯의 개수만큼 반복
        {
            if (Slots[i].item != null)// 칸에 아이템이 있을 때
            {
                if (Slots[i].item.itemName == _item.itemName)// 슬롯 안에 같은 아이템이 들어있음
                {
                    Slots[i].SetSlotCount(count);// 슬롯안의 아이템 갯수 증가
                    return;
                }
            }
            else// 칸에 아이템이 없을 때
            {
                Slots[i].SetColor(1);
                Slots[i].AddItem(_item, count);// 아이템을 넣어 줌
                Slots[i].gameObject.SetActive(true);// 아이템을 얻었으면 해당 슬롯 보이게 해주기
                return;
            }
        }
    }

    public void Stu_Inven()// 합성에 쓰이고 나면 인벤에서 개수를 차감시킬 함수
    {
        game.BoolPanelFalse();// 홈버튼, 뒤로가기 버튼 꺼주기
        for (int i = 2; i >= 0; i--)// 합성 재료만큼 검사
        {
            for (int j = 0; j < Slots.Length; j++)// 인벤 슬롯만큼 검사
            {
                if (sslot[i].item.itemName == Slots[j].item.itemName)
                {
                    clickText += Slots[j].item.itemNumber;
                    //Debug.Log(clickText + "완성품 번호 검사");
                    Slots[j].item.item_ = true;
                    Slots[j].SetSlotCount(-1);// 인벤토리의 재료 개수 빼기
                    sslot[i].TSlot();// 합성창의 재료칸에 아이템을 빼고 +를 다시 보여줄 코드
                    SyMain.Minus(j, -1);// 합성창의 재료에 들어갈 재료 슬롯의 개수 차감
                    break;
                    //stu[j].SetSlotCount(-1); 이 멍청한 놈이 이 코드가 들지 않아서 위의 코드로 경유함
                }
            }
        }
        itemList.AddBookCre(clickText);
        clickText = "";// 넘겨준 다음 초기화 시켜주기
    }

    public void SaveInvenToSyn()// 인벤토리에 저장된 재료가 합성창으로 가도록
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].item != null)// 슬롯 안에 아이템이 들어있을 때
            {
                if (Slots[i].item.itemType != Item.ItemType.Used)// 아이템이 음식이 아닐 때
                {
                    SyMain.StuAdd(Slots[i].item, Slots[i].itemCount);
                }
                else// 아이템이 모두 음식일 때
                {
                    EatManager.FoodPick(Slots[i].item, Slots[i].itemCount);
                }
            }
        }
    }
    public void CreFalse()
    {
        for (int i = 0; i < sslot.Length; i++)
        {
            sslot[i].TSlot();// 합성창의 재료칸에 아이템을 빼고 +를 다시 보여줄 코드
        }
    }
    //public void AddInvenItem(Item _item, int _count = 1)// 아이템이 인벤에 들어가는 함수
    //{
    //    if (Item.ItemType.Equipment != _item.itemType)// 아이템이 장비형이 아닐 때, 중복을 검색하기 위해
    //    {
    //        for (int i = 0; i < Slots.Length; i++)// 슬롯의 개수만큼 반복
    //        {
    //            if (Slots[i].item != null)
    //            {
    //                //Debug.Log(i + "번째 슬롯에 아이템이 들어있음");
    //                if (Slots[i].item.itemName == _item.itemName)// 슬롯 안에 같은 아이템이 들어있음
    //                {
    //                    //Debug.Log(i + "번째 슬롯에 같은 아이템이 들어있음");
    //                    Slots[i].SetSlotCount(_count);// 슬롯안의 아이템 갯수 증가
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //
    //    for (int i = 0; i < Slots.Length; i++)// 슬롯의 개수만큼 반복
    //    {
    //        if (Slots[i].item == null)// 슬롯에 빈자리가 있을 때
    //        {
    //            Slots[i].AddItem(_item, _count);// 아이템을 넣어 줌
    //            //Slots[i].gameObject.SetActive(true);// 아이템을 얻었으면 해당 슬롯 보이게 해주기
    //            return;
    //        }
    //    }
    //}
    public void ClearSlotItem()// 아이템을 전부 썼을 때 뒤의 아이템이 앞으로 오도록
    {
        for (int i = 0; i < Slots.Length - 1; i++)
        {
            //Debug.Log(i + " 번?");
            if (Slots[i].item == null)// i번째 슬롯이 비어있고
            {
                if (Slots[i + 1].item != null)// i의 다음 슬롯에 아이템이 있을 때
                {// 본인이 빈 슬롯이고 뒷슬롯이 차있을 때에는 뒷슬롯을 앞슬롯으로 옮기기
                    Slots[i].item = Slots[i + 1].item;
                    Slots[i].itemImage.sprite = Slots[i + 1].itemImage.sprite;
                    Slots[i].itemCount = Slots[i + 1].itemCount;
                    Slots[i].item.itemNumber = Slots[i + 1].item.itemNumber;
                    Slots[i].gameObject.SetActive(true);
                    Slots[i].SetColor(1);
                    Slots[i].TextS();
                    Slots[i + 1].item = null;
                    Slots[i + 1].itemImage.sprite = null;
                    Slots[i + 1].itemCount = 0;
                    Slots[i + 1].gameObject.SetActive(false);
                    Slots[i + 1].SetColor(0);

                    //Debug.Log(Slots[i].item.itemName + "어디 슬롯에 어느 아이템");
                }
                else
                {// 본인과 뒷슬롯 모두 비었을 때에는 비운 쪽을 지우기
                    //Slots[i].gameObject.SetActive(false);
                }
            }
            if (Slots[Slots.Length - 1].item == null)
            {
                //Slots[Slots.Length - 1].gameObject.SetActive(false);
            }
        }
    }
}
