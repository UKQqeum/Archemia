using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class BookSlot : MonoBehaviour// 도감에 완성된 합성물을 넣기 위해서임
{
    [SerializeField]
    public Item item;

    [SerializeField]
    private ItemList list;

    [SerializeField]
    public Image CreImage;// 완성본 이미지

    [SerializeField]
    private Image BImage;// 보라색 배경 이미지

    [SerializeField]
    private GameObject ItemPanel;// 전체 판넬

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

    public AudioSource bookUse;// 캐릭터 합성에 필요한 재료 보여주는 알림소리
    // Start is called before the first frame update

    void Start()
    {
        ItemPanel.SetActive(false);
        BImage.color = Color.gray;// 배경에 회색 넣어주기
        CreImage.color = Color.black;// 캐릭터 검은색으로 칠해주기
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

    //public void SetColor(float _alpha)// 이미지 색의 알파값, 투명도 조절 관련 함수
    //{
    //    Color color = CreImage.color;
    //    color.a = _alpha;
    //    CreImage.color = color;
    //}
    public void AddBook()// 아이템 획득
    {
        //item = _item;

        //CreImage.sprite = item.itemImage;

        //SetColor(1);// 아이템이 들어왔다면 1로 바꿔서 보이도록 해주기
        //gameObject.SetActive(true);
        
        if (item.item_ == true)
        {
            BImage.color = new Color(255 / 255, 255 / 255, 255 / 255);
            //BImage.color = Color.white;// 캐릭터 만들었으니 색 넣어주기
            CreImage.color = Color.white;
            //Debug.Log("색 넣어주기!");
        }

    }
    public void ABook(string itemn)
    {
        if (itemn == item.itemName)
            item.item_ = true;
    }

    public void BookUse()// 캐릭터 필요 재료가 보이도록
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
