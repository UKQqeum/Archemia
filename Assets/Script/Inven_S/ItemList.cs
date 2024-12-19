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
    private Inventory theInventory;// �κ��丮 ��ũ��Ʈ

    [SerializeField]
    public Item[] item;// ������ ���

    [SerializeField]
    private Item[] CreItem;// �����۵��� �ռ� ����� ���

    [SerializeField]
    private Book book;// ���� ��ũ��Ʈ

    [SerializeField]
    private AdventureManager adventureManager;// ��庥�� ��ũ��Ʈ

    [SerializeField]
    private EatSlotManager atSlotManager;

    //[SerializeField]
    //public Adventure adven;// ��庥�� ��ũ��Ʈ

    public GameObject Item_Pick_Panel;// ������ ȹ�� �� ��Ÿ���� �ǳ�

    public TextMeshProUGUI Item_Pick_Cnt;// ������ ȹ�� ����

    [SerializeField]
    private GameObject All_Slot;// �κ��丮ó�� �θ� ��ü�� �־�� �� ��?

    [SerializeField]
    private UIClick uic;

    private Slot slot;// �κ��丮 ����

    private PickSlot[] Slots;// ȹ��â�� ������ ȹ�� ����

    [SerializeField]
    private SynthesisMain stumain;

    [SerializeField]
    private GameManager gamemanager;

    public bool Pick_ = false;// �Ⱦ�â�� ����� ���� ������ Ȱ��ȭ��

    public AudioSource ItemPickUp;// �İ� �Ϸ� �� ������ ��� �Ҹ�

    // Start is called before the first frame update
    void Start()
    {
        Slots = All_Slot.GetComponentsInChildren<PickSlot>();

        Item_Pick_Panel.SetActive(false);

        for (int i = 0; i < Slots.Length; i++)// ������ ������ŭ �ݺ�.SetSlotColor(0);
        {
            Slots[i].SetColor(0);
            //Debug.Log("���� ���� ��Ȱ��ȭ");
            Slots[i].gameObject.SetActive(false);// ���� ���� �������� ���� �Ⱥ��̰� ���ֱ�
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
                if (Slots[i].item != null)// �������� ����ִٸ�
                {
                    Slots[i].item = null;
                }
            }
            uic.panel = false;
        }
    }
    public void Forest()
    {
        theInventory.AII(item[0], 1);// �İ� �Ϸ� �� ��ư ������ ����. �κ��� �������� ������

        //adven.Adven_clear.SetActive(false);// �İ� �Ϸ� �� ���� ��ư �ٽ� ������
    }
    public void Random_Forest()// �������� �������� ��� �Լ�
    {
        Time.timeScale = 0;// �Ͻ����� �����ֱ�
        Pick(item[0], 1000);// ������ ȹ�� â�� ������ �� ���� ������� ���� �־��ֱ�
        stumain.GameGold += 1000;// �İ� �� ��� ȹ��
        stumain.TextGold.text = stumain.GameGold.ToString();// ȹ���� ��� �����ֱ�
        stumain.Syngold.text = stumain.GameGold.ToString();
        //item[0].Count += 1000;

        for (int i = 1; i < item.Length; i++)
        {
            gamemanager.BoolPanelFalse();// Ȩ��ư, �ڷΰ��� ��ư ���ֱ�
            //int Pick_Cnt_1 = Random.Range(1, 5);
            Item_Pick_Panel.SetActive(true);
            theInventory.AII(item[i], 9);
            Pick(item[i], 9);// ������ ȹ�� â�� ������ �� ���� ������� ���� �־��ֱ�
            stumain.StuAdd(item[i], 9);
            Pick_ = true;
            ItemPickUp.Play();
        }
    }
    public void Random_FoodPick()// ���� �׽�Ʈ ��ư
    {
        Time.timeScale = 0;// �Ͻ����� �����ֱ�
        gamemanager.BoolPanelFalse();// Ȩ��ư, �ڷΰ��� ��ư ���ֱ�
        for (int i = 0; i < theInventory.Foods.Length; i++)
        {
            Item_Pick_Panel.SetActive(true);
            theInventory.AII(theInventory.Foods[i], 5);
            Pick(theInventory.Foods[i], 5);// ������ ȹ�� â�� ������ �� ���� ������� ���� �־��ֱ�
            atSlotManager.FoodPick(theInventory.Foods[i], 5);
            Pick_ = true;
            ItemPickUp.Play();
        }
    }
    public void RandomAdven(int _gold, int _start, int _end, int _count)
    {// ���ʴ�� ���� ���, ����� ���� ����, ������ ����, ��Ḧ �� �� �������� ����

        Time.timeScale = 0;// �Ͻ����� �����ֱ�
        Pick(item[0], _gold);// ������ ȹ�� â�� ��� ȹ�� �־��ֱ�
        stumain.GameGold += _gold;// �İ� �� ��� ȹ��
        stumain.TextGold.text = stumain.GameGold.ToString();// ȹ���� ��� �����ֱ�
        stumain.Syngold.text = stumain.GameGold.ToString();
        //item[0].Count += _gold;// ��� ������ �����տ� ��� ���� �־��ֱ�

        gamemanager.BoolPanelFalse();// Ȩ��ư, �ڷΰ��� ��ư ���ֱ�
        for (int i = _start; i < _end; i++)// ��Ḧ ���� ��ŭ�� �ݺ����ֱ�
        {
            int Pick_Cnt_1 = Random.Range(0, _count + 1);
            theInventory.AII(item[i], _count + Pick_Cnt_1);
            Pick(item[i], _count + Pick_Cnt_1);// ������ ȹ�� â�� ������ �� ���� ������� ���� �־��ֱ�
            stumain.StuAdd(item[i], _count + Pick_Cnt_1);
        }
        Item_Pick_Panel.SetActive(true);
        Pick_ = true;
        ItemPickUp.Play();
        adventureManager.EndAdven();
        
    }
    public void AddBookCre(string number)
    {
        //Debug.Log(number + "�ĺ���ȣ");
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
        for (int i = 0; i < Slots.Length; i++)// ������ ������ŭ �ݺ�
        {
            if (Slots[i].item == null)// ĭ�� �������� ���� ��
            {
                Slots[i].AddPickItem(_item, count);// �������� �־� ��
                Slots[i].gameObject.SetActive(true);// �������� ������� �ش� ���� ���̰� ���ֱ�
                return;
            }
        }
    }

    public void PickFalse()// ������ �ʱ�ȭ �� �� �ֵ���
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
