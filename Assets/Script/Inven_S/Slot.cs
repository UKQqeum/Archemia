using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Slot : MonoBehaviour
{
    //private Vector3 originPos;
    public Item item;// 획득한 아이템
    public int itemCount;// 획득한 아이템의 갯수
    public Image itemImage;// 아이템의 이미지
    public string number;// 아이템 식별 번호
    public bool itemBool;// 아이템의 사용 유무

    // 필요한 컴포넌트들
    [SerializeField]
    private TextMeshProUGUI text_Count;// 아이템의 갯수를 표시해줄 텍스트
    
    public GameObject ItemStatePanel;// 아이템의 상세 설명 판넬
    public TextMeshProUGUI ItemStateText;// 아이템 설명 텍스트
    public Image ItemStateImage;// 현재 아이템 설명창의 이미지

    public Inventory inventory;// 인벤토리 스크립트

    // Start is called before the first frame update
    void Start()
    {
        ItemStatePanel.SetActive(false);// 인벤토리를 처음 열면 상세 설명이 보이지 않도록
        ItemStateText.gameObject.SetActive(false);
        ItemStateImage.gameObject.SetActive(false);
    }
    public void SetColor(float _alpha)// 이미지 색의 알파값, 투명도 조절 관련 함수
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)// 아이템 획득
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        number = item.itemNumber;
        itemBool = item.item_;

        if (item.item_ == true)
            ItemStateText.text = item.itemStatusText;
        else
            ItemStateText.text = "???";

        text_Count.text = itemCount.ToString();// 아이템의 갯수 보이기
        //text_Count.text = "";// 글자가 보이지 않도록

        SetColor(1);// 아이템이 들어왔다면 1로 바꿔서 보이도록 해주기
        gameObject.SetActive(true);
    }

    public void SetSlotCount(int _count)// 아이템 개수 조정
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)// 아이템의 개수가 없을 때
        {
            ClearSlot();
        }
    }
    public void ClearSlot()// 슬롯 초기화
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        number = " ";
        SetColor(0);
        gameObject.SetActive(false);
        text_Count.text = " ";
        inventory.ClearSlotItem();
    }
    public void TextS()
    {
        text_Count.text = itemCount.ToString();// 아이템의 갯수 보이기
    }
    public void Use()
    {
        inventory.ItemSound.Play();
        ItemStatePanel.SetActive(true);// 아이템을 눌렀으니 상세 설명이 보이도록
        inventory.ItemStatePanel.SetActive(true);
        ItemStateText.gameObject.SetActive(true);
        ItemStateImage.gameObject.SetActive(true);
        ItemStateImage.sprite = item.detailed_image;
        if (item.item_ == true)
            ItemStateText.text = item.itemStatusText;
        else
            ItemStateText.text = "아직까지 무슨 \r\n용도인지 모르겠다.";
        //Debug.Log(item.itemName + "다른 함수에서도?");
        //database.UseItem(item);// 데이터베이스의 유즈 아이템 함수 가져오기
        //SetSlotCount(-1);
    }
    //public void PointerDown()// 마우스를 계속 홀드하고 있을 때
    //{
    //    itemClick = true;
    //    Debug.Log("포인터 다운");
    //    itemTime += Time.deltaTime;
    //}
    //public void PointerUp()// 마우스 홀드 취소
    //{
    //    itemClick = false;
    //    Debug.Log("포인터 업");
    //    itemTime = 0;
    //    GrayItemImage.fillAmount = 0f;
    //    Debug.Log(itemTime + "시간 초기화");
    //}
}
