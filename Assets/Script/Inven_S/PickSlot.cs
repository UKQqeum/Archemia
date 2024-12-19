using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickSlot : MonoBehaviour// 파견 완료 시 아이템을 얻었을 때 쓰이는 스크립트
{
    public Item item;

    public int itemCount;// 획득한 아이템의 갯수
    public Image itemImage;// 아이템의 이미지

    public TextMeshProUGUI text_Count;// 픽업 아이템의 개수

    public GameObject ItemStatePanel;// 아이템의 상세 설명 판넬
    public TextMeshProUGUI ItemStateText;// 아이템 설명 텍스트
    public Image ItemStateImage;// 현재 아이템 설명창의 이미지

    public AudioSource Bgm;

    public void SetColor(float _alpha)// 이미지 색의 알파값, 투명도 조절 관련 함수
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddPickItem(Item _item, int _count)// 아이템 획득
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        text_Count.text = _count.ToString();

        SetColor(1);// 아이템이 들어왔다면 1로 바꿔서 보이도록 해주기
    }

    public void PickItemData()// 아이템을 눌렀을 때 나올 설명 팝업창
    {
        Bgm.Play();
        ItemStatePanel.SetActive(true);
        ItemStateImage.sprite = itemImage.sprite;
        //ItemStateText.text = item.itemStatusText;
        if (item.item_ == true)
            ItemStateText.text = item.itemStatusText;
        else
            ItemStateText.text = "아직까지 무슨 \r\n용도인지 모르겠다.";
    }
    public void ItemFalse()// 아이템을 비우고 아무것도 없도록 해주기
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        this.gameObject.SetActive(false);
        SetColor(0);// 아이템이 들어왔다면 1로 바꿔서 보이도록 해주기
    }
}
