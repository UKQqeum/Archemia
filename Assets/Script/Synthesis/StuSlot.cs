using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StuSlot : MonoBehaviour// �ռ��� �ʿ��� ��Ḧ ���� �κ��丮 ĭ
{
    [SerializeField]
    private GameObject inven;

    [SerializeField]
    private SynthesisMain synthesisMain;

    [SerializeField]
    private ItemList list;

    public Item item;

    public int itemCount;// ȹ���� �������� ����
    public Image itemImage;// �������� �̹���

    public TextMeshProUGUI text_Count;// ��� �������� ����

    public Image[] stu_image;// �ռ� ����� �̹���

    public CreSlot[] creSlot;// �ռ� ���ĭ

    public GameObject creslots;// �ռ� ���ĭ�� ������

    public bool item_ = false;

    public AudioSource InputStu;// ��� �ִ� �Ҹ�

    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.Find("Inventory");
        //StuffSlot = GameObject.Find("Stuff_Content");
        creSlot = creslots.GetComponentsInChildren<CreSlot>();
    }
    public void SetColor(float _alpha)// �̹��� ���� ���İ�, ���� ���� ���� �Լ�
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
        //if (_alpha == 1)
        //    item_ = true;
    }

    public void AddStu(Item _item, int _count)// ������ ȹ��
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        text_Count.text = itemCount.ToString();

        SetColor(1);// �������� ���Դٸ� 1�� �ٲ㼭 ���̵��� ���ֱ�
        gameObject.SetActive(true);
    }
    public void SetSlotCount(int _count)// ������ ���� ����
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    public void ClearSlot()// ���� �ʱ�ȭ
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
        text_Count.text = itemCount.ToString();// �������� ���� ���̱�
    }

    public void Stu_Com()// �ռ�â�� ��� 4���� ��ư���� ��Ÿ�� ������ ���� �����س��� �Լ�
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
