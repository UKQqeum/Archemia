using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodList : MonoBehaviour
{
    [SerializeField]
    private CafeManager Cafe;

    private FoodSlot[] FSlots;// 보상창의 슬롯

    [SerializeField]
    private MainToCafeManager mainToCafe;

    [SerializeField]
    private Inventory inven;

    [SerializeField]
    private EatSlotManager eatManager;

    public Item[] foodItems;// 음식 리스트

    public GameObject FoodContent;// 보상창 슬롯의 부모 객체

    public List<int> foodCnt = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        FSlots = FoodContent.GetComponentsInChildren<FoodSlot>();
        for (int i = 0; i < FSlots.Length; i++)
        {
            FSlots[i].gameObject.SetActive(false);// 모든 슬롯을 보이지 않도록 해주기
        }
        mainToCafe = GameObject.Find("GameObject (1)").GetComponent<MainToCafeManager>();
        //mainToCafe = GameObject.Find("").transform.Find("").gameObject.SetActive(true);
        //inven = FindObjectOfType<Inventory>();// 인벤토리 넣어주기
        inven = GameObject.Find("GameObject (1)").transform.Find("Inventory").GetComponentInChildren<Inventory>();
        eatManager = GameObject.Find("GameObject (1)").transform.Find("EatSlotManager").GetComponentInChildren<EatSlotManager>();
        RandomFoodPick();
    }

    public void RandomFoodPick()// 게임이 끝난 후 랜덤으로 먹이 획득하기
    {
        //inven = FindObjectOfType<Inventory>();// 인벤토리 넣어주기
        //inven = GameObject.Find("Inventory").GetComponent<Inventory>();
        foodCnt.Clear();
        for (int i = 0; i < 3; i++)// 3 종류의 먹이만 줄 것
        {
            int Pick_Food = Random.Range(0, foodItems.Length);// 전체 먹이에서 한 가지를 고르기
            
            if (i == 1)
            {
                while (Pick_Food == foodCnt[0])// 전에 획득한 과일과 이번에 획득할 과일이 겹칠 때
                {
                    Pick_Food = Random.Range(0, foodItems.Length);// 다시 숫자를 얻기
                }
                //foodCnt.Add(Pick_Food);
            }
            if (i == 2)
            {
                while (Pick_Food == foodCnt[0] || Pick_Food == foodCnt[1])// 전에 획득한 과일과 이번에 획득할 과일이 겹칠 때
                {
                    Pick_Food = Random.Range(0, foodItems.Length);// 다시 숫자를 얻기
                }
                //foodCnt.Add(Pick_Food);
            }
            foodCnt.Add(Pick_Food);// 리스트에 이미 들어간 과일 번호 추가해주기
            int Food_Number = Random.Range(1, 4);// 1에서 3 중 몇 개를 줄 것인지
            FSlots[i].AddPickFood(foodItems[Pick_Food], Food_Number);// 보상창에 과일 보여주기
            inven.AII(foodItems[Pick_Food], Food_Number);// 인벤토리에 과일 넣어주기
            eatManager.FoodPick(foodItems[Pick_Food], Food_Number);
        }
    }

    public void TestTest()
    {
        mainToCafe = FindObjectOfType<MainToCafeManager>();// 게임의 스크립트 오브젝트 찾아서 넣어주기
        mainToCafe.CCC();
    }
}
