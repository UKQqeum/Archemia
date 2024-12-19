using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class Book : MonoBehaviour
{
    [SerializeField]
    private Item[] items;

    [SerializeField]
    private SynthesisMain synthesisMain;

    [SerializeField]
    private SaveAndLoad saveAL;

    public BookSlot[] GetBookSlots() { return bookSlots; }
    public void LoadToBook(int _arryNum, string _itemName, bool _item)// ���̺��
    {// ������� ������ ��ġ, ������ �̸�
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
            {
                if (_item)
                {
                    bookSlots[_arryNum].ABook(_itemName);
                }
                
            }
        }
    }
    [SerializeField]
    private GameObject BookContent;// ���� ������ �θ�, ������

    private BookSlot[] bookSlots;// ���� ����

    public GameObject BookPanel;// ���� �ǳ�

    [SerializeField]
    private GameManager gameManager;

    public bool panel;

    public GameObject itempanel;

    // Start is called before the first frame update
    void Start()
    {
        bookSlots = BookContent.GetComponentsInChildren<BookSlot>();
        BookPanel.SetActive(false);

        for (int i = 0; i < bookSlots.Length; i++)
        {
            if (i < items.Length)
            {
                bookSlots[i].item = items[i];
                bookSlots[i].CreImage.sprite = items[i].itemImage;
            }
            else
            {
                bookSlots[i].gameObject.SetActive(false);
            }
            //bookSlots[i].SetColor(0);
            //bookSlots[i].gameObject.SetActive(false);
        }
    }

    public void OpenBook()
    {
        BookPanel.SetActive(true);
        gameManager.OpenPanel.Play();
        gameManager.Open();
        panel = true;
        //saveAL.LoadData();
    }

    public void CreBook(Item bookItem)
    {
        for (int i = 0; i < bookSlots.Length; i++)
        {
            if (bookSlots[i].item == bookItem)//
            {
                bookSlots[i].AddBook();
                synthesisMain.Char(bookItem.itemName);// �ߺ� ������̶� �־��ֱ�
                //bookSlots[i].gameObject.SetActive(true);// ���� ���Կ� ����� ���� �ֱ�
                //synthesisMain.Char(bookItem.itemName);
                break;
            }
            //if (bookSlots[i].item != null)// ���� ������ ������� �ʴٸ�
            //{
            //    if (bookSlots[i].item.itemName == bookItem.itemName)// �ߺ��� ������̶��
            //    {
            //        synthesisMain.Char(bookItem.itemName);// �ߺ� ������̶� �־��ֱ�
            //        break;
            //    }
            //}
            //else// ������ �ƹ��͵� ������� �ʴٸ�
            //{
            //    bookSlots[i].AddBook(bookItem);
            //    bookSlots[i].gameObject.SetActive(true);// ���� ���Կ� ����� ���� �ֱ�
            //    synthesisMain.Char(bookItem.itemName);
            //    break;
            //}
        }
        //saveAL.LoadData();
    }
}
