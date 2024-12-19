using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class BookSlot : MonoBehaviour// ������ �ϼ��� �ռ����� �ֱ� ���ؼ���
{
    [SerializeField]
    public Item item;

    [SerializeField]
    private ItemList list;

    [SerializeField]
    public Image CreImage;// �ϼ��� �̹���

    [SerializeField]
    private Image BImage;// ����� ��� �̹���

    [SerializeField]
    private GameObject ItemPanel;// ��ü �ǳ�

    [SerializeField]
    private Image Head;
    [SerializeField]
    private Image Eye;
    [SerializeField]
    private Image Tail;

    [SerializeField]
    private SaveAndLoad saveAL;


    [SerializeField]
    private Book book;

    public AudioSource bookUse;// ĳ���� �ռ��� �ʿ��� ��� �����ִ� �˸��Ҹ�
    // Start is called before the first frame update

    void Start()
    {
        ItemPanel.SetActive(false);
        BImage.color = Color.gray;// ��濡 ȸ�� �־��ֱ�
        CreImage.color = Color.black;// ĳ���� ���������� ĥ���ֱ�
        //AddBook();
        //saveAL.LoadData();
    }
    void Update()
    {
        if (book.panel)
        {
            AddBook();
        }
    }

    //public void SetColor(float _alpha)// �̹��� ���� ���İ�, ���� ���� ���� �Լ�
    //{
    //    Color color = CreImage.color;
    //    color.a = _alpha;
    //    CreImage.color = color;
    //}
    public void AddBook()// ������ ȹ��
    {
        //item = _item;

        //CreImage.sprite = item.itemImage;

        //SetColor(1);// �������� ���Դٸ� 1�� �ٲ㼭 ���̵��� ���ֱ�
        //gameObject.SetActive(true);
        
        if (item.item_ == true)
        {
            BImage.color = new Color(255 / 255, 255 / 255, 255 / 255);
            //BImage.color = Color.white;// ĳ���� ��������� �� �־��ֱ�
            CreImage.color = Color.white;
            //Debug.Log("�� �־��ֱ�!");
        }

    }
    public void ABook(string itemn)
    {
        if (itemn == item.itemName)
            item.item_ = true;
    }

    public void BookUse()// ĳ���� �ʿ� ��ᰡ ���̵���
    {
        bookUse.Play();
        ItemPanel.SetActive(true);
        for (int i = 0; i < list.item.Length; i++)
        {
            if (item.Head == list.item[i].itemNumber)
            {
                Head.sprite = list.item[i].itemImage;
            }
            else if (item.Eye == list.item[i].itemNumber)
            {
                Eye.sprite = list.item[i].itemImage;
            }
            else if(item.Tail == list.item[i].itemNumber)
            {
                Tail.sprite = list.item[i].itemImage;
            }
        }
    }
}
