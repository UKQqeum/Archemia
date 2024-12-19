using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class ItemList : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;// 인벤토리 스크립트

    [SerializeField]
    public Item[] item;// 아이템 목록

    [SerializeField]
    private Item[] CreItem;// 아이템들의 합성 결과물 목록

    [SerializeField]
    private Book book;// 도감 스크립트

    [SerializeField]
    private AdventureManager adventureManager;// 어드벤쳐 스크립트

    [SerializeField]
    private EatSlotManager atSlotManager;

    //[SerializeField]
    //public Adventure adven;// 어드벤쳐 스크립트

    public GameObject Item_Pick_Panel;// 아이템 획득 시 나타나는 판넬

    public TextMeshProUGUI Item_Pick_Cnt;// 아이템 획득 숫자

    [SerializeField]
    private GameObject All_Slot;// 인벤토리처럼 부모 객체가 있어야 할 듯?

    [SerializeField]
    private UIClick uic;

    private Slot slot;// 인벤토리 슬롯

    private PickSlot[] Slots;// 획득창의 아이템 획득 슬롯

    [SerializeField]
    private SynthesisMain stumain;

    [SerializeField]
    private GameManager gamemanager;

    public bool Pick_ = false;// 픽업창이 띄워져 있을 때에만 활성화됨

    public AudioSource ItemPickUp;// 파견 완료 후 아이템 얻는 소리

    // Start is called before the first frame update
    void Start()
    {
        Slots = All_Slot.GetComponentsInChildren<PickSlot>();

        Item_Pick_Panel.SetActive(false);

        for (int i = 0; i < Slots.Length; i++)// 슬롯의 개수만큼 반복.SetSlotColor(0);
        {
            Slots[i].SetColor(0);
            //Debug.Log("슬롯 전부 비활성화");
            Slots[i].gameObject.SetActive(false);// 슬롯 전부 시작하자 마자 안보이게 해주기
        }
        for (int i = 0; i < item.Length; i++)
        {
            item[i].item_ = false;
        }

        item[0].item_ = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (uic.panel == true)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].item != null)// 아이템이 들어있다면
                {
                    Slots[i].item = null;
                }
            }
            uic.panel = false;
        }
    }
    public void Forest()
    {
        theInventory.AII(item[0], 1);// 파견 완료 후 버튼 눌러서 수령. 인벤에 아이템이 들어가도록

        //adven.Adven_clear.SetActive(false);// 파견 완료 후 수령 버튼 다시 가리기
    }
    public void Random_Forest()// 랜덤으로 아이템을 얻는 함수
    {
        Time.timeScale = 0;// 일시정지 시켜주기
        Pick(item[0], 1000);// 아이템 획득 창에 아이템 몇 개를 얻었는지 개수 넣어주기
        stumain.GameGold += 1000;// 파견 후 골드 획득
        stumain.TextGold.text = stumain.GameGold.ToString();// 획득한 골드 더해주기
        stumain.Syngold.text = stumain.GameGold.ToString();
        //item[0].Count += 1000;

        for (int i = 1; i < item.Length; i++)
        {
            gamemanager.BoolPanelFalse();// 홈버튼, 뒤로가기 버튼 꺼주기
            //int Pick_Cnt_1 = Random.Range(1, 5);
            Item_Pick_Panel.SetActive(true);
            theInventory.AII(item[i], 9);
            Pick(item[i], 9);// 아이템 획득 창에 아이템 몇 개를 얻었는지 개수 넣어주기
            stumain.StuAdd(item[i], 9);
            Pick_ = true;
            ItemPickUp.Play();
        }
    }
    public void Random_FoodPick()// 음식 테스트 버튼
    {
        Time.timeScale = 0;// 일시정지 시켜주기
        gamemanager.BoolPanelFalse();// 홈버튼, 뒤로가기 버튼 꺼주기
        for (int i = 0; i < theInventory.Foods.Length; i++)
        {
            Item_Pick_Panel.SetActive(true);
            theInventory.AII(theInventory.Foods[i], 5);
            Pick(theInventory.Foods[i], 5);// 아이템 획득 창에 아이템 몇 개를 얻었는지 개수 넣어주기
            atSlotManager.FoodPick(theInventory.Foods[i], 5);
            Pick_ = true;
            ItemPickUp.Play();
        }
    }
    public void RandomAdven(int _gold, int _start, int _end, int _count)
    {// 차례대로 얻을 골드, 재료의 시작 범위, 마지막 범위, 재료를 몇 개 얻을지의 변수

        Time.timeScale = 0;// 일시정지 시켜주기
        Pick(item[0], _gold);// 아이템 획득 창에 골드 획득 넣어주기
        stumain.GameGold += _gold;// 파견 후 골드 획득
        stumain.TextGold.text = stumain.GameGold.ToString();// 획득한 골드 더해주기
        stumain.Syngold.text = stumain.GameGold.ToString();
        //item[0].Count += _gold;// 골드 아이템 프리팹에 골드 개수 넣어주기

        gamemanager.BoolPanelFalse();// 홈버튼, 뒤로가기 버튼 꺼주기
        for (int i = _start; i < _end; i++)// 재료를 얻을 만큼만 반복해주기
        {
            int Pick_Cnt_1 = Random.Range(0, _count + 1);
            theInventory.AII(item[i], _count + Pick_Cnt_1);
            Pick(item[i], _count + Pick_Cnt_1);// 아이템 획득 창에 아이템 몇 개를 얻었는지 개수 넣어주기
            stumain.StuAdd(item[i], _count + Pick_Cnt_1);
        }
        Item_Pick_Panel.SetActive(true);
        Pick_ = true;
        ItemPickUp.Play();
        adventureManager.EndAdven();
        
    }
    public void AddBookCre(string number)
    {
        //Debug.Log(number + "식별번호");
        for (int i = 0; i < CreItem.Length; i++)
        {
            if (number == CreItem[i].itemNumber)
            {
                book.CreBook(CreItem[i]);
            }
        }
    }
    public void Pick(Item _item, int count)
    {
        for (int i = 0; i < Slots.Length; i++)// 슬롯의 개수만큼 반복
        {
            if (Slots[i].item == null)// 칸에 아이템이 없을 때
            {
                Slots[i].AddPickItem(_item, count);// 아이템을 넣어 줌
                Slots[i].gameObject.SetActive(true);// 아이템을 얻었으면 해당 슬롯 보이게 해주기
                return;
            }
        }
    }

    public void PickFalse()// 슬롯을 초기화 할 수 있도록
    {
        for (int i = 0;i < Slots.Length; i++)
        {
            if (Slots[i].item != null)
            {
                Slots[i].ItemFalse();
            }
        }
    }
}
