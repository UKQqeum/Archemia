using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EatSlot : MonoBehaviour
{
    [SerializeField]
    private EatSlotManager manager;

    [SerializeField]
    private EatManager EatM;

    public Item item;

    public int itemCount;// 획득한 아이템의 갯수
    public Image itemImage;// 아이템의 이미지

    public TextMeshProUGUI text_Count;// 획득한 아이템의 개수

    public GameObject FoodEatButton;// 아이템 먹이기 버튼
    //public TextMeshProUGUI ItemStateText;// 아이템 설명 텍스트
    //public Image ItemStateImage;// 현재 아이템 설명창의 이미지

    // Start is called before the first frame update
    void Start()
    {
        FoodEatButton.SetActive(false);
    }
    public void SetColor(float _alpha)// 이미지 색의 알파값, 투명도 조절 관련 함수
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddPickFood(Item _item, int _count)// 아이템 획득
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        text_Count.text = _count.ToString();

        SetColor(1);// 아이템이 들어왔다면 1로 바꿔서 보이도록 해주기
        this.gameObject.SetActive(true);
    }
    public void SetFoodCount(int _count)
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
        //number = " ";
        SetColor(0);
        gameObject.SetActive(false);
        text_Count.text = " ";
        manager.ClearSlotItem();
    }
    public void FoodEat()// 먹이를 눌렀을 때 먹이기 버튼이 보이도록
    {
        EatM.EatBgm.Play();
        FoodEatButton.SetActive(true);
        EatM.item = item;
        //EatM.CharEat(item);
    }
}
