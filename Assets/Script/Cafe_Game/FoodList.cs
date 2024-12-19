using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodList : MonoBehaviour
{
    [SerializeField]
    private CafeManager Cafe;

    private FoodSlot[] FSlots;// ����â�� ����

    [SerializeField]
    private MainToCafeManager mainToCafe;

    [SerializeField]
    private Inventory inven;

    [SerializeField]
    private EatSlotManager eatManager;

    public Item[] foodItems;// ���� ����Ʈ

    public GameObject FoodContent;// ����â ������ �θ� ��ü

    public List<int> foodCnt = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        FSlots = FoodContent.GetComponentsInChildren<FoodSlot>();
        for (int i = 0; i < FSlots.Length; i++)
        {
            FSlots[i].gameObject.SetActive(false);// ��� ������ ������ �ʵ��� ���ֱ�
        }
        mainToCafe = GameObject.Find("GameObject (1)").GetComponent<MainToCafeManager>();
        //mainToCafe = GameObject.Find("").transform.Find("").gameObject.SetActive(true);
        //inven = FindObjectOfType<Inventory>();// �κ��丮 �־��ֱ�
        inven = GameObject.Find("GameObject (1)").transform.Find("Inventory").GetComponentInChildren<Inventory>();
        eatManager = GameObject.Find("GameObject (1)").transform.Find("EatSlotManager").GetComponentInChildren<EatSlotManager>();
        RandomFoodPick();
    }

    public void RandomFoodPick()// ������ ���� �� �������� ���� ȹ���ϱ�
    {
        //inven = FindObjectOfType<Inventory>();// �κ��丮 �־��ֱ�
        //inven = GameObject.Find("Inventory").GetComponent<Inventory>();
        foodCnt.Clear();
        for (int i = 0; i < 3; i++)// 3 ������ ���̸� �� ��
        {
            int Pick_Food = Random.Range(0, foodItems.Length);// ��ü ���̿��� �� ������ ����
            
            if (i == 1)
            {
                while (Pick_Food == foodCnt[0])// ���� ȹ���� ���ϰ� �̹��� ȹ���� ������ ��ĥ ��
                {
                    Pick_Food = Random.Range(0, foodItems.Length);// �ٽ� ���ڸ� ���
                }
                //foodCnt.Add(Pick_Food);
            }
            if (i == 2)
            {
                while (Pick_Food == foodCnt[0] || Pick_Food == foodCnt[1])// ���� ȹ���� ���ϰ� �̹��� ȹ���� ������ ��ĥ ��
                {
                    Pick_Food = Random.Range(0, foodItems.Length);// �ٽ� ���ڸ� ���
                }
                //foodCnt.Add(Pick_Food);
            }
            foodCnt.Add(Pick_Food);// ����Ʈ�� �̹� �� ���� ��ȣ �߰����ֱ�
            int Food_Number = Random.Range(1, 4);// 1���� 3 �� �� ���� �� ������
            FSlots[i].AddPickFood(foodItems[Pick_Food], Food_Number);// ����â�� ���� �����ֱ�
            inven.AII(foodItems[Pick_Food], Food_Number);// �κ��丮�� ���� �־��ֱ�
            eatManager.FoodPick(foodItems[Pick_Food], Food_Number);
        }
    }

    public void TestTest()
    {
        mainToCafe = FindObjectOfType<MainToCafeManager>();// ������ ��ũ��Ʈ ������Ʈ ã�Ƽ� �־��ֱ�
        mainToCafe.CCC();
    }
}
