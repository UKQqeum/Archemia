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

    public GameObject Manu_Panel;// �޴� �ǳ�
    public GameObject Blur_Panel;// �� ó���� �ǳ�
    //public GameObject Inven_Panel;// �κ� �ǳ�
    public GameObject ESC_Panel;// ESC �ǳ�
    //public GameObject Adventure_Panel;// �İ� �ý��� �ǳ�
    public GameObject Book_Panel;// ���� �ý��� �ǳ�
    public GameObject Char_State_Panel;// ĳ���� ����â �ǳ�
    public GameObject Char_Eat_Panel;// ĳ���� ���� �ǳ�

    public GameObject Stu_Panel;// �ռ� �ǳ�

    public GameObject Panel_Exit_Object;// Ȩ ��ư, �ڷΰ��� ��ư��

    public bool panel_bool = false;// �ǳ��� ���� �ִ��� �����ִ���

    public GameObject[] char_;// ������ ĳ���͵� ����

    public GameObject Guide;// ���̵� �ǳ�

    public AudioSource OpenPanel;// �ǳ� ���� �Ҹ�
    public AudioSource ClosePanel;// �ǳ� �ݴ� �Ҹ�

    public int TestOpen = 0;
    public int TestClose = 0;
    public GameObject TestButtons;// ���� �׽�Ʈ�� ����

    // Start is called before the first frame update
    void Start()
    {
        Manu_Panel.SetActive(true);// �޴� ��ư Ű��
        Blur_Panel.SetActive(false);// �� �ǳ� ����
        inven.InvenPanel.SetActive(false);
        ESC_Panel.SetActive(false);// ESC �ǳ� ����
        adventureManager.AdvenPanel.SetActive(false);
        Book_Panel.SetActive(false);// ���� �ǳ� ����
        Stu_Panel.SetActive (false);// �ռ�â ����
        Panel_Exit_Object.SetActive(false);//��ư ����
        Char_Eat_Panel.SetActive(false);// ����â ����
        //TestButtons.SetActive(false);
        string lastPlayTime = PlayerPrefs.GetString("LastPlayTime", DateTime.Now.ToString());
        DateTime lastDateTime = DateTime.Parse(lastPlayTime);

        TimeSpan timeAway = DateTime.Now - lastDateTime;

        //Debug.Log("����� �ð�(��): " + timeAway.TotalSeconds);
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
        if (characterStateManager.MiniGameBool == false)// �̴� ������ ���� ���� ��
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {// ESC��ư�� �����ų� �ڷΰ��� ��ư�� ������ ��
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
        Blur_Panel.SetActive(true);// �� �ǳ� Ű��
        Manu_Panel.SetActive(false);// �޴� �ǳ� ����
        Panel_Exit_Object.SetActive(true);// Ȩ��ư Ȱ��ȭ
        panel_bool = true;
    }
    public void Panel_Exit()
    {
        ClosePanel.Play();
        panel_bool  = false;
        Manu_Panel.SetActive(true);// �޴� �ǳ� Ű��
        Blur_Panel.SetActive(false);// �� �ǳ� ����
        inven.InvenPanel.SetActive(false);
        //Inven_Panel.SetActive(false);// �κ� �ǳ� ����
        ESC_Panel.SetActive(false);// ESC �ǳ� ����
        adventureManager.AdvenPanel.SetActive(false);
        //Adventure_Panel.SetActive(false);// �İ� �ǳ� ����
        Book_Panel.SetActive(false);// ���� �ǳ� ����
        Stu_Panel.SetActive(false);// �ռ�â ����
        Guide.SetActive(false);
        Char_State_Panel.SetActive(false);
        Char_Eat_Panel.SetActive(false);// ����â ����
        adventureManager.AdvenObjectFalse();
        inven.CreFalse();// �ռ�ĭ�� ������ ���ֱ�
        BoolPanelFalse();
        book.panel = false;
        book.itempanel.SetActive(false);

        if (Synthesismain.slot_bool == 1)
            Synthesismain.slot_bool = 2;
    }
    public void BoolPanelFalse()
    {
        Panel_Exit_Object.SetActive(false);// Ȩ��ư ��Ȱ��ȭ
    }
    public void BoolPanelTrue()
    {
        Panel_Exit_Object.SetActive(true);// Ȩ��ư ��Ȱ��ȭ
    }
    public void Game_ESC_Exit()// �������� ���ư��� ��ư
    {
        panel_bool = true;
        ESC_Panel.SetActive(false);// ESC �ǳ� ����
    }
    public void Game_Title()// Ÿ��Ʋ�� ���ư���
    {
        SceneManager.LoadScene("Title");
    }
    public void Game_Exit()// ���� ������ �� ����ǵ���
    {
        SaveandLoad.SaveData();
        adventureManager.SaveSave();// �İ��� �ʿ��� ������ �����Ű�� �Լ� ����
        Synthesismain.SaveSave();// ���� ȭ�� ĳ���� �����ϱ�
        nickNameManager.SaveSave();// �г��� �����ϱ�
        Application.Quit();
    }
    public void Go_Save()// �����ϱ�
    {
        SaveandLoad.SaveData();
        adventureManager.SaveSave();// �İ��� �ʿ��� ������ �����Ű�� �Լ� ����
        Synthesismain.SaveSave();// ���� ȭ�� ĳ���� �����ϱ�
        nickNameManager.SaveSave();// �г��� �����ϱ�
        //Debug.Log("���̺� ����!");
    }

    public void MainToCafe()// ���� ȭ�鿡�� ī�� �̴ϰ������� �̵�
    {
        SceneManager.LoadScene("CafeGame");
    }

    public void OnApplicationQuit()// ���������� ������ �ð��� �����ϴ� �Լ�
    {
        PlayerPrefs.SetString("LastPlayTime", DateTime.Now.ToString());
    }
    public void Test1()
    {
        TestOpen++;
        //Debug.Log(TestOpen + "������ Ŭ�� Ƚ��");
        if (TestOpen == 3 && TestClose == 7)
        {
            TestButtons.SetActive(true);
        }
    }
    public void Test2()
    {
        TestClose++;
        //Debug.Log(TestClose + "�ռ�â Ŭ�� Ƚ��");
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
