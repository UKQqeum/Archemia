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

    public void LoadToInven(int _arryNum, string _itemName, int _itemNum, bool _itemBool)// �ε��
    {// ������� ������ ��ġ, ������ �̸�, �������� ����, ������ ��� ����
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
            else if (i < 5)// ���� ����Ʈ�� ������ ����Ʈ�� ũ�Ⱑ �ٸ��� ������
            {
                if (Foods[i].itemName == _itemName)
                {
                    Slots[_arryNum].AddItem(Foods[i], _itemNum);
                }
            }
        }
    }

    public static bool InvenBool = false;// �κ� Ȱ��ȭ Ȯ��, �̰� Ʈ��� ���콺 ������ ���ƾ���

    // �ʿ��� ������Ʈ
    [SerializeField]
    public GameObject InvenPanel;// �κ��丮 ��ü �ǳ�
    [SerializeField]
    private GameObject All_Slot;// ��� �κ� ���Ե��� �����ϴ� ��. content

    [SerializeField]
    private GameManager game;// ���� �Ŵ���

    [SerializeField]
    private CreSlot[] sslot;

    [SerializeField]
    private GameObject stuSlot;// �ռ�â�� ��� ������ ������

    [SerializeField]
    private SynthesisMain SyMain;

    [SerializeField]
    private ItemList itemList;

    [SerializeField]
    private EatSlotManager EatManager;

    private StuSlot[] stu;// �ռ�â�� ��� ����

    private Slot[] Slots;// �κ��丮 ����
    private Item item;

    //public ItemClick itemclick;
    public string clickText = "";

    public GameObject ItemStatePanel;// �������� �� ���� �ǳ�

    public AudioSource ItemSound;// ������ ������ �� �� �Ҹ�

    void Start()
    {
        Slots = All_Slot.GetComponentsInChildren<Slot>();
        stu = stuSlot.GetComponentsInChildren<StuSlot>();

        ItemStatePanel.SetActive(false);

        for (int i = 0; i < Slots.Length; i++)// ������ ������ŭ �ݺ�.SetSlotColor(0);
        {
            Slots[i].SetColor(0);
            //Debug.Log("���� ���� ��Ȱ��ȭ");
            Slots[i].gameObject.SetActive(false);// ���� ���� �������� ���� �Ⱥ��̰� ���ֱ�
        }
    }
    public void OpenInven()
    {
        game.OpenPanel.Play();
        InvenPanel.SetActive(true);// �κ��丮 â ����
        game.Open();// ���� �Ŵ����� ���� �Լ�
        //Vector3 uiV = new Vector3(960, 540, 0);
        //InvenPanel.GetComponent<RectTransform>().anchoredPosition = uiV;// �κ�â ����
        //LeanTween.move(InvenPanel, uiV, 0.5f);// 0.5�ʸ��� �κ� �ǳ��� ���� �ø���
    }// �κ� â ����

    public void AII(Item _item, int count)// �İ� �� �������� �޾Ƽ� �κ��丮�� �ֱ�
    {
        for (int i = 0; i < Slots.Length; i++)// ������ ������ŭ �ݺ�
        {
            if (Slots[i].item != null)// ĭ�� �������� ���� ��
            {
                if (Slots[i].item.itemName == _item.itemName)// ���� �ȿ� ���� �������� �������
                {
                    Slots[i].SetSlotCount(count);// ���Ծ��� ������ ���� ����
                    return;
                }
            }
            else// ĭ�� �������� ���� ��
            {
                Slots[i].SetColor(1);
                Slots[i].AddItem(_item, count);// �������� �־� ��
                Slots[i].gameObject.SetActive(true);// �������� ������� �ش� ���� ���̰� ���ֱ�
                return;
            }
        }
    }

    public void Stu_Inven()// �ռ��� ���̰� ���� �κ����� ������ ������ų �Լ�
    {
        game.BoolPanelFalse();// Ȩ��ư, �ڷΰ��� ��ư ���ֱ�
        for (int i = 2; i >= 0; i--)// �ռ� ��Ḹŭ �˻�
        {
            for (int j = 0; j < Slots.Length; j++)// �κ� ���Ը�ŭ �˻�
            {
                if (sslot[i].item.itemName == Slots[j].item.itemName)
                {
                    clickText += Slots[j].item.itemNumber;
                    //Debug.Log(clickText + "�ϼ�ǰ ��ȣ �˻�");
                    Slots[j].item.item_ = true;
                    Slots[j].SetSlotCount(-1);// �κ��丮�� ��� ���� ����
                    sslot[i].TSlot();// �ռ�â�� ���ĭ�� �������� ���� +�� �ٽ� ������ �ڵ�
                    SyMain.Minus(j, -1);// �ռ�â�� ��ῡ �� ��� ������ ���� ����
                    break;
                    //stu[j].SetSlotCount(-1); �� ��û�� ���� �� �ڵ尡 ���� �ʾƼ� ���� �ڵ�� ������
                }
            }
        }
        itemList.AddBookCre(clickText);
        clickText = "";// �Ѱ��� ���� �ʱ�ȭ �����ֱ�
    }

    public void SaveInvenToSyn()// �κ��丮�� ����� ��ᰡ �ռ�â���� ������
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].item != null)// ���� �ȿ� �������� ������� ��
            {
                if (Slots[i].item.itemType != Item.ItemType.Used)// �������� ������ �ƴ� ��
                {
                    SyMain.StuAdd(Slots[i].item, Slots[i].itemCount);
                }
                else// �������� ��� ������ ��
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
            sslot[i].TSlot();// �ռ�â�� ���ĭ�� �������� ���� +�� �ٽ� ������ �ڵ�
        }
    }
    //public void AddInvenItem(Item _item, int _count = 1)// �������� �κ��� ���� �Լ�
    //{
    //    if (Item.ItemType.Equipment != _item.itemType)// �������� ������� �ƴ� ��, �ߺ��� �˻��ϱ� ����
    //    {
    //        for (int i = 0; i < Slots.Length; i++)// ������ ������ŭ �ݺ�
    //        {
    //            if (Slots[i].item != null)
    //            {
    //                //Debug.Log(i + "��° ���Կ� �������� �������");
    //                if (Slots[i].item.itemName == _item.itemName)// ���� �ȿ� ���� �������� �������
    //                {
    //                    //Debug.Log(i + "��° ���Կ� ���� �������� �������");
    //                    Slots[i].SetSlotCount(_count);// ���Ծ��� ������ ���� ����
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //
    //    for (int i = 0; i < Slots.Length; i++)// ������ ������ŭ �ݺ�
    //    {
    //        if (Slots[i].item == null)// ���Կ� ���ڸ��� ���� ��
    //        {
    //            Slots[i].AddItem(_item, _count);// �������� �־� ��
    //            //Slots[i].gameObject.SetActive(true);// �������� ������� �ش� ���� ���̰� ���ֱ�
    //            return;
    //        }
    //    }
    //}
    public void ClearSlotItem()// �������� ���� ���� �� ���� �������� ������ ������
    {
        for (int i = 0; i < Slots.Length - 1; i++)
        {
            //Debug.Log(i + " ��?");
            if (Slots[i].item == null)// i��° ������ ����ְ�
            {
                if (Slots[i + 1].item != null)// i�� ���� ���Կ� �������� ���� ��
                {// ������ �� �����̰� �޽����� ������ ������ �޽����� �ս������� �ű��
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

                    //Debug.Log(Slots[i].item.itemName + "��� ���Կ� ��� ������");
                }
                else
                {// ���ΰ� �޽��� ��� ����� ������ ��� ���� �����
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
