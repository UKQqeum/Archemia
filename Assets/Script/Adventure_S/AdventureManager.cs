using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

[System.Serializable]
public class AdvenData
{
    public float AdventureTimer;// 파견을 가있는 시간
    public int AdvenGold;// 파견을 통해 얻는 골드
    public bool AdventureBool;// 파견을 가 있는지

    public int AdvenStart;// 파견을 가서 얻을 재료의 범위 시작부분
    public int AdvenEnd;// 파견을 가서 얻을 재료의 범위 마지막 부분
    public int AdvenItem;// 파견을 가서 재료를 얼만큼 얻을 것인지

    public int SlotsCount;// 슬롯의 어느 부분을 회색으로 만들어야 하는지
}
public class AdventureManager : MonoBehaviour
{

    public AdvenData advenData;
    [SerializeField]
    private GameObject AdvenContent;// 파견지의 부모 슬롯들

    [SerializeField]
    private ItemList itemList;

    [SerializeField]
    private GameManager gameManager;

    private AdvenSlot[] Slots;// 파견지 슬롯

    public Item[] AdvenList;// 파견지 아이템을 넣을 변수

    public GameObject GrayPanel;// 파견지 이름 확인, 시간 확인 총괄 판넬
    public GameObject AdvenPanel;// 파견 판넬
    public GameObject HourPanel;// 얼마나 파견나갈지 묻는 UI

    public float AdventureTimer;// 파견을 얼마나 갈 것인지
    public float RearTimer;// 실제로 흐르는 시간
    
    public int AdvenGold;// 파견을 통해 얻을 골드
    public bool AdventureBool;// 파견을 갔다면 확인해서 업데이트 함수로 시간을 빼주어야 하기에

    public int AdvenStart;// 파견을 가서 얻을 재료의 범위 시작부분
    public int AdvenEnd;// 파견을 가서 얻을 재료의 범위 마지막 부분
    public int AdvenItem;// 파견을 가서 재료를 얼만큼 얻을 것인지

    public int SlotsCount;// 슬롯의 어느 부분을 회색으로 만들어야 하는지

    public TextMeshProUGUI TestAdven;

    public GameObject Advenalarm;// 파견 종료 후 완료 알림

    private string filePath;// 저장

    public float LastTimer;// 게임 종료 후 경과한 시간을 받아내기

    public AudioSource Advenend;// 파견 종료 소리
    public AudioSource AdvenHBgm;// 시간 소리

    // Start is called before the first frame update
    void Start()
    {
        Slots = AdvenContent.GetComponentsInChildren<AdvenSlot>();

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].AddItem(AdvenList[i]);// 파견지 슬롯에 파견 지역 넣어주기
            //Debug.Log(AdvenList[i].name + "파견 잘 들어가있나?");
        }

        filePath = Application.persistentDataPath + "AdvenData.json";// 저장

        //if (!Directory.Exists(filePath))// 해당 경로가 존재하지 않는다면
        //{
        //    Directory.CreateDirectory(filePath);// 폴더 생성(경로 만들어주기)
        //
        //}

        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            LoadAdvenData();// 파일경로를 지정해 준 다음에 로드하기
        }
        else
        {
            SaveSave();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AdventureBool)// AdventureBool가 켜져있을 때
        {
            RearTimer = Time.deltaTime;
            AdventureTimer -= RearTimer;
            Slots[SlotsCount].AdvenTime.text = AdventureTimer.ToString("F0");
            TestAdven.text = AdventureTimer.ToString("F0");
            //AdvenTime.text = AdventureTimer.ToString("F0");
            if (AdventureTimer <= 0)
            {
                Advenend.Play();
                //AdvenGrayPanel.SetActive(false);// 파견창 잠금 해재하기
                Slots[SlotsCount].ClearPanel.SetActive(true);// 파견 완료 후 재료 수령 버튼 활성화
                //ClearPanel.SetActive(true);// 파견 완료 후 재료 수령 버튼 활성화
                Slots[SlotsCount].AdvenGrayPanel.SetActive(false);
                Slots[SlotsCount].DarkImage.SetActive(false);
                AdventureBool = false;
                Advenalarm.SetActive(true);
            }
        }
    }
    public void GoAdven()// 메뉴의 파견 버튼
    {
        AdvenPanel.SetActive(true);
        gameManager.OpenPanel.Play();
        gameManager.Open();
    }
    public void TimerPanel()
    {
        HourPanel.SetActive(true);// 시간 판넬 보이도록
    }
    public void FiveSeconds()// 5초동안 파견가기
    {
        AdvenHBgm.Play();
        AdventureTimer = 5;
        AdvenGold = 15;
        AdvenItem = 1;
        AdvenEnd -= 2;// 파견 시간이 길어졌으니 재료 범위 늘려주기
        AdvenTimerF();
    }
    public void TenSeconds()// 10초동안 파견가기
    {
        AdvenHBgm.Play();
        AdventureTimer = 10;
        AdvenGold = 20;
        AdvenItem = 2;
        AdvenEnd -= 2;// 파견 시간이 길어졌으니 재료 범위 늘려주기
        AdvenTimerF();
    }
    public void ThirtySeconds()// 30초동안 파견가기
    {
        AdvenHBgm.Play();
        AdventureTimer = 30;
        AdvenGold = 50;
        AdvenItem = 2;
        AdvenEnd -= 1;// 파견 시간이 길어졌으니 재료 범위 늘려주기
        AdvenTimerF();
    }
    public void SixtySeconds()// 60초동안 파견가기
    {
        AdvenHBgm.Play();
        AdventureTimer = 60;
        AdvenGold = 100;
        AdvenItem = 3;
        AdvenTimerF();
    }
    public void AdvenTimerF()
    {
        GrayPanel.SetActive(false);// 시간이 선택되었으니 꺼주기
        HourPanel.SetActive(false);// 시간이 선택되었으니 꺼주기
        Slots[SlotsCount].AdvenGrayPanel.SetActive(true);
        AdvenFalse();
        //Debug.Log(SlotsCount + "숫자가..?");
        AdventureBool = true;
        Time.timeScale = 1;
    }
    public void AdventureClear()// 아이템 수령 버튼
    {// 차례대로 얻을 골드, 재료의 시작 범위, 마지막 범위, 재료를 몇 개 얻을지의 변수
        itemList.RandomAdven(AdvenGold, AdvenStart, AdvenEnd, AdvenItem);
        Advenalarm.SetActive(false);
        AdvenTrue();
    }
    public void EndAdven()// 파견 재료를 수령한 후 회색과 수령 버튼 없애주기
    {
        Slots[SlotsCount].AdvenGrayPanel.SetActive(false);
        Slots[SlotsCount].ClearPanel.SetActive(false);// 파견 완료 후 재료 수령 버튼 활성화
    }
    public void AdvenFalse()// 파견중일 때 다른 파견지 막기
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i != SlotsCount)
            {
                Slots[i].DarkImage.SetActive(true);// 파견을 가있는 장소말고는 막아버리기
                //Debug.Log(i + "번째 슬롯 막아버리기");
            }
        }
    }
    public void AdvenTrue()// 파견지 다시 풀어주기
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i != SlotsCount)
            {
                Slots[i].DarkImage.SetActive(false);
            }
        }
    }
    public void AdvenObjectFalse()
    {
        GrayPanel.SetActive(false);
        HourPanel.SetActive(false);
    }

    public void SaveSave()// 필요한 변수들 저장해주기
    {
        advenData.AdventureTimer = AdventureTimer;// 남은 파견 시간 넣어주기
        advenData.AdvenGold = AdvenGold;// 파견으로 얻을 골드 다시 넣어주기
        advenData.AdventureBool = AdventureBool;// 파견을 가있다는걸 다시 넣어주기
        
        advenData.AdvenStart = AdvenStart;// 얻을 아이템 시작 부분 넣어주기
        advenData.AdvenEnd = AdvenEnd;// 얻을 아이템 끝부분 넣어주기
        advenData.AdvenItem = AdvenItem;// 얻을 아이템 개수 넣어주기

        advenData.SlotsCount = SlotsCount;// 어디로 파견을 갔는지 저장

        AdvenSaveData(advenData);
        //Debug.Log(advenData.AdventureTimer + "파견 시간 저장");
        //Debug.Log(advenData.AdvenGold + "파견 골드 저장");
        //Debug.Log(advenData.AdventureBool + "파견 유무 저장");

        //Debug.Log(advenData.AdvenStart + "얻을 아이템 시작 부분 저장");
        //Debug.Log(advenData.AdvenEnd + "얻을 아이템 끝부분 저장");
        //Debug.Log(advenData.AdvenItem + "얻을 아이템 개수 저장");

        //Debug.Log(advenData.SlotsCount + "어디로 파견 저장");
    }
    public void AdvenSaveData(AdvenData data)
    {// 데이터 저장 함수
        // 데이터 객체를 제이슨 문자열로 반환
        string json = JsonUtility.ToJson(data, true);// 트루는 포맷팅 옵션(가독성을 위한 것)

        // 파일에 제이슨 문자열 쓰기
        File.WriteAllText(filePath, json);

        //Debug.Log("제이슨 파일이 저장됨" + filePath);

    }
    public AdvenData LoadAdvenData()// 데이터 불러오기
    {
        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            string json = File.ReadAllText(filePath);// 파일에서 제이슨 문자열 읽기

            AdvenData data = JsonUtility.FromJson<AdvenData>(json);// 제이슨 문자열을 반환

            //Debug.Log("제이슨 데이터가 로드됨 " + filePath);

            AdventureTimer = data.AdventureTimer;
            AdvenGold = data.AdvenGold;
            AdventureBool = data.AdventureBool;

            AdvenStart = data.AdvenStart;
            AdvenEnd = data.AdvenEnd;
            AdvenItem = data.AdvenItem;

            SlotsCount = data.SlotsCount;

            AdventureTimer -= LastTimer;
            //Debug.Log(AdventureTimer + "제대로 된 시간이?");

            //Debug.Log(AdventureTimer + "로드 파견 시간 저장");
            //Debug.Log(AdvenGold + "로드 파견 골드 저장");
            //Debug.Log(AdventureBool + "로드 파견 유무 저장");

            //Debug.Log(AdvenStart + "얻을 아이템 시작 부분 저장");
            //Debug.Log(AdvenEnd + "얻을 아이템 끝부분 저장");
            //Debug.Log(AdvenItem + "얻을 아이템 개수 저장");

            //Debug.Log(SlotsCount + "어디로 파견 저장");
            if (AdventureBool)
            {
                AdvenFalse();// 파견중일 때 다른 파견지 막기
                Slots[SlotsCount].AdvenGrayPanel.SetActive(true);
                //Debug.Log("파견막기?");
            }

            return data;
        }
        else
        {
            //Debug.Log("저장된 제이슨 파일이 없다.");
            return null;
        }
    }
    public void LastTime(TimeSpan timeAway)
    {
        LastTimer = (float)timeAway.TotalSeconds;
        //Debug.Log(LastTimer + "숫자를 받아와서 더블을 플롯으로 바꿨을 때");
        //Debug.Log(AdventureTimer + "시간 확인?");
        //AdventureTimer -= (float)timeAway.TotalSeconds;
        //Debug.Log(AdventureTimer + "시간 확인?2");
    }
}
