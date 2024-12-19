using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdvenSlot : MonoBehaviour
{
    [SerializeField]
    private AdventureManager adventureManager;

    public Item item;// 획득한 아이템
    public Image itemImage;

    public TextMeshProUGUI WhatAdven;// 어느 파견지를 갈 지 확인하는 글자
    public GameObject AllAdven;// 회색의 총괄 UI
    public GameObject GoAdven;// 이 파견지로 정말 갈 것인가 묻는 UI

    public GameObject AdvenGrayPanel;// 파견 중 파견 잠금 UI

    public TextMeshProUGUI AdvenTime;// 게임 창에서 파견 시간이 얼마나 남았는지 표시해주기
    public GameObject ClearPanel;// 파견 완료 후 재료 수령 버튼 UI

    public GameObject DarkImage;// 파견중일 때 다른 파견지 막는 이미지

    // Start is called before the first frame update
    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        //Debug.Log("파견지 이름: " +  item.itemName);
        //Debug.Log("시작 부분: " + item.Start);
        //Debug.Log("끝 부분: " + item.End);
        //Debug.Log("어느 파견 슬롯인지: " + item.Count);
    }
    public void WhatAdvenGo()// 파견가기를 선택하는 버튼
    {
        adventureManager.AdvenHBgm.Play();
        AllAdven.SetActive(true);
        GoAdven.SetActive(true);// 이 파견지로 정말 갈 것인가 묻는 UI
        WhatAdven.text = item.itemStatusText.ToString();
        //Debug.Log("파견지 이름: " + item.itemName);
        //Debug.Log("시작 부분: " + item.Start);
        //Debug.Log("끝 부분: " + item.End);
        //Debug.Log("어느 파견 슬롯인지: " + item.Count);
        adventureManager.AdvenStart = item.Start;
        adventureManager.AdvenEnd = item.End;
        adventureManager.SlotsCount = item.Count;// 파견지 프리팹의 아이템 카운터를 받아와야 함
    }
    public void AdvenTimer()// 몇 시간이나 갈 지 묻는 판넬
    {
        adventureManager.AdvenHBgm.Play();
        GoAdven.SetActive(false);// 파견지를 정했으니 이제 닫아야 함
        adventureManager.TimerPanel();// 시간 정하는 판넬 띄우기
    }
}
