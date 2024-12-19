using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using System;
//using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SaveAndLoad SaveandLoad;

    [SerializeField]
    public SynthesisMain Synthesismain;

    [SerializeField]
    public UIClick uiClick_1;

    [SerializeField]
    public UIClick uiClick_2;

    [SerializeField]
    private AdventureManager adventureManager;

    [SerializeField]
    private Inventory inven;

    [SerializeField]
    private ItemList itemList;

    [SerializeField]
    private NickNameManager nickNameManager;

    [SerializeField]
    private Book book;

    [SerializeField]
    private CharStateManager characterStateManager;

    public GameObject Manu_Panel;// 메뉴 판넬
    public GameObject Blur_Panel;// 블러 처리된 판넬
    //public GameObject Inven_Panel;// 인벤 판넬
    public GameObject ESC_Panel;// ESC 판넬
    //public GameObject Adventure_Panel;// 파견 시스템 판넬
    public GameObject Book_Panel;// 도감 시스템 판넬
    public GameObject Char_State_Panel;// 캐릭터 상태창 판넬
    public GameObject Char_Eat_Panel;// 캐릭터 음식 판넬

    public GameObject Stu_Panel;// 합성 판넬

    public GameObject Panel_Exit_Object;// 홈 버튼, 뒤로가기 버튼들

    public bool panel_bool = false;// 판넬이 켜져 있는지 꺼져있는지

    public GameObject[] char_;// 저장할 캐릭터들 상태

    public GameObject Guide;// 가이드 판넬

    public AudioSource OpenPanel;// 판넬 여는 소리
    public AudioSource ClosePanel;// 판넬 닫는 소리

    public int TestOpen = 0;
    public int TestClose = 0;
    public GameObject TestButtons;// 게임 테스트를 위함

    // Start is called before the first frame update
    void Start()
    {
        Manu_Panel.SetActive(true);// 메뉴 버튼 키기
        Blur_Panel.SetActive(false);// 블러 판넬 끄기
        inven.InvenPanel.SetActive(false);
        ESC_Panel.SetActive(false);// ESC 판넬 끄기
        adventureManager.AdvenPanel.SetActive(false);
        Book_Panel.SetActive(false);// 도감 판넬 끄기
        Stu_Panel.SetActive (false);// 합성창 끄기
        Panel_Exit_Object.SetActive(false);//버튼 끄기
        Char_Eat_Panel.SetActive(false);// 먹이창 끄기
        //TestButtons.SetActive(false);
        string lastPlayTime = PlayerPrefs.GetString("LastPlayTime", DateTime.Now.ToString());
        DateTime lastDateTime = DateTime.Parse(lastPlayTime);

        TimeSpan timeAway = DateTime.Now - lastDateTime;

        //Debug.Log("경과한 시간(초): " + timeAway.TotalSeconds);
        adventureManager.LastTime(timeAway);
        Synthesismain.LastTime(timeAway);
    }
    public void Awake()
    {
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterStateManager.MiniGameBool == false)// 미니 게임을 하지 않을 때
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {// ESC버튼을 누르거나 뒤로가기 버튼을 눌렀을 때
                if (panel_bool)
                {
                    Panel_Exit();
                }
                else
                    ESC_Panel.SetActive(true);
            }
        }
        
        if (uiClick_1.Exit_Bool)
        {
            uiClick_1.Exit_Bool = false;
            BoolPanelTrue();
            
        }
        if (uiClick_2.Exit_Bool)
        {
            uiClick_2.Exit_Bool = false;
            BoolPanelTrue();
        }
    }
    public void Open()
    {
        Blur_Panel.SetActive(true);// 블러 판넬 키기
        Manu_Panel.SetActive(false);// 메뉴 판넬 끄기
        Panel_Exit_Object.SetActive(true);// 홈버튼 활성화
        panel_bool = true;
    }
    public void Panel_Exit()
    {
        ClosePanel.Play();
        panel_bool  = false;
        Manu_Panel.SetActive(true);// 메뉴 판넬 키기
        Blur_Panel.SetActive(false);// 블러 판넬 끄기
        inven.InvenPanel.SetActive(false);
        //Inven_Panel.SetActive(false);// 인벤 판넬 끄기
        ESC_Panel.SetActive(false);// ESC 판넬 끄기
        adventureManager.AdvenPanel.SetActive(false);
        //Adventure_Panel.SetActive(false);// 파견 판넬 끄기
        Book_Panel.SetActive(false);// 도감 판넬 끄기
        Stu_Panel.SetActive(false);// 합성창 끄기
        Guide.SetActive(false);
        Char_State_Panel.SetActive(false);
        Char_Eat_Panel.SetActive(false);// 먹이창 끄기
        adventureManager.AdvenObjectFalse();
        inven.CreFalse();// 합성칸의 아이템 빼주기
        BoolPanelFalse();
        book.panel = false;
        book.itempanel.SetActive(false);

        if (Synthesismain.slot_bool == 1)
            Synthesismain.slot_bool = 2;
    }
    public void BoolPanelFalse()
    {
        Panel_Exit_Object.SetActive(false);// 홈버튼 비활성화
    }
    public void BoolPanelTrue()
    {
        Panel_Exit_Object.SetActive(true);// 홈버튼 비활성화
    }
    public void Game_ESC_Exit()// 게임으로 돌아가기 버튼
    {
        panel_bool = true;
        ESC_Panel.SetActive(false);// ESC 판넬 끄기
    }
    public void Game_Title()// 타이틀로 돌아가기
    {
        SceneManager.LoadScene("Title");
    }
    public void Game_Exit()// 게임 종료할 때 저장되도록
    {
        SaveandLoad.SaveData();
        adventureManager.SaveSave();// 파견의 필요한 변수들 저장시키는 함수 실행
        Synthesismain.SaveSave();// 메인 화면 캐릭터 저장하기
        nickNameManager.SaveSave();// 닉네임 저장하기
        Application.Quit();
    }
    public void Go_Save()// 저장하기
    {
        SaveandLoad.SaveData();
        adventureManager.SaveSave();// 파견의 필요한 변수들 저장시키는 함수 실행
        Synthesismain.SaveSave();// 메인 화면 캐릭터 저장하기
        nickNameManager.SaveSave();// 닉네임 저장하기
        //Debug.Log("세이브 시작!");
    }

    public void MainToCafe()// 메인 화면에서 카페 미니게임으로 이동
    {
        SceneManager.LoadScene("CafeGame");
    }

    public void OnApplicationQuit()// 마지막으로 접속한 시간을 저장하는 함수
    {
        PlayerPrefs.SetString("LastPlayTime", DateTime.Now.ToString());
    }
    public void Test1()
    {
        TestOpen++;
        //Debug.Log(TestOpen + "프로필 클릭 횟수");
        if (TestOpen == 3 && TestClose == 7)
        {
            TestButtons.SetActive(true);
        }
    }
    public void Test2()
    {
        TestClose++;
        //Debug.Log(TestClose + "합성창 클릭 횟수");
        if (TestOpen == 3 && TestClose == 7)
        {
            TestButtons.SetActive(true);
        }
    }
    public void Test3()
    {
        if (TestOpen == 3 && TestClose == 7)
        {
            TestButtons.SetActive(true);
        }
    }
}
