using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StuSlot : MonoBehaviour// 합성에 필요한 재료를 넣을 인벤토리 칸
{
    [SerializeField]
    private GameObject inven;

    [SerializeField]
    private SynthesisMain synthesisMain;

    [SerializeField]
    private ItemList list;

    public Item item;

    public int itemCount;// 획득한 아이템의 갯수
    public Image itemImage;// 아이템의 이미지

    public TextMeshProUGUI text_Count;// 재료 아이템의 개수

    public Image[] stu_image;// 합성 재료의 이미지

    public CreSlot[] creSlot;// 합성 재료칸

    public GameObject creslots;// 합성 재료칸의 컨텐츠

    public bool item_ = false;

    public AudioSource InputStu;// 재료 넣는 소리

    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.Find("Inventory");
        //StuffSlot = GameObject.Find("Stuff_Content");
        creSlot = creslots.GetComponentsInChildren<CreSlot>();
    }
    public void SetColor(float _alpha)// 이미지 색의 알파값, 투명도 조절 관련 함수
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
        //if (_alpha == 1)
        //    item_ = true;
    }

    public void AddStu(Item _item, int _count)// 아이템 획득
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        text_Count.text = itemCount.ToString();

        SetColor(1);// 아이템이 들어왔다면 1로 바꿔서 보이도록 해주기
        gameObject.SetActive(true);
    }
    public void SetSlotCount(int _count)// 아이템 개수 조정
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    public void ClearSlot()// 슬롯 초기화
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        text_Count.text = " ";
        gameObject.SetActive(false);
        synthesisMain.ClearSlotItem();
    }
    public void TextS()
    {
        text_Count.text = itemCount.ToString();// 아이템의 갯수 보이기
    }

    public void Stu_Com()// 합성창의 재료 4개의 버튼마다 나타낼 아이템 종류 구별해내는 함수
    {
        InputStu.Play();
        if (item.itemType == Item.ItemType.Color)
        {
            inrNumver(3);
        }
        else if (item.itemType == Item.ItemType.Head)
        {
            inrNumver(0);
        }
        else if ( item.itemType == Item.ItemType.Tail)
        {
            inrNumver(2);
        }
        else if (item.itemType == Item.ItemType.Eye)
        {
            inrNumver(1);
        }
        
    }

    public void inrNumver(int a)
    {
        switch (a)
        {
            case 0:
                creSlot[0].item = item;
                creSlot[0].TextSlot();
                break;
            case 1:
                creSlot[1].item = item;
                creSlot[1].TextSlot();
                break;
            case 2:
                creSlot[2].item = item;
                creSlot[2].TextSlot();
                break;
            case 3:
                creSlot[3].item = item;
                creSlot[3].TextSlot();
                break;
            default:
                break;
        }
    }

    public void MMM()
    {
        synthesisMain.slot_bool = 0;
    }
}
