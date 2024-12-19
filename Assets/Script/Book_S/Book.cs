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
    public void LoadToBook(int _arryNum, string _itemName, bool _item)// 세이브용
    {// 순서대로 슬롯의 위치, 아이템 이름
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
    private GameObject BookContent;// 도감 슬롯의 부모, 컨텐츠

    private BookSlot[] bookSlots;// 도감 슬롯

    public GameObject BookPanel;// 도감 판넬

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
                synthesisMain.Char(bookItem.itemName);// 중복 결과물이라도 넣어주기
                //bookSlots[i].gameObject.SetActive(true);// 도감 슬롯에 결과물 사진 넣기
                //synthesisMain.Char(bookItem.itemName);
                break;
            }
            //if (bookSlots[i].item != null)// 도감 슬롯이 비어있지 않다면
            //{
            //    if (bookSlots[i].item.itemName == bookItem.itemName)// 중복된 결과물이라면
            //    {
            //        synthesisMain.Char(bookItem.itemName);// 중복 결과물이라도 넣어주기
            //        break;
            //    }
            //}
            //else// 도감에 아무것도 들어있지 않다면
            //{
            //    bookSlots[i].AddBook(bookItem);
            //    bookSlots[i].gameObject.SetActive(true);// 도감 슬롯에 결과물 사진 넣기
            //    synthesisMain.Char(bookItem.itemName);
            //    break;
            //}
        }
        //saveAL.LoadData();
    }
}
