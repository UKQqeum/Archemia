using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharStateManager : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject CharStatePanel;// 캐릭터의 상태창 자체
    public Image CharImage;// 상태창에 보일 캐릭터 이미지

    public GameObject CharNameState;// 누르면 캐릭터의 이름과 상태를 확인할 수 있도록

    [SerializeField]
    private MainToCafeManager mainToCafeManager;

    [SerializeField]
    private SynthesisMain SyMain;

    [SerializeField]
    private ItemList itemlist;

    [SerializeField]
    public Slider LoveS;// 애정 슬라이더

    [SerializeField]
    public Slider HungerS;// 배고픔 슬라이더

    [SerializeField]
    public Slider StatureS;// 성장 슬라이더

    public GameObject EatPanel;// 캐릭터의 먹이를 주는 창

    public GameObject EatButton;// 먹이기 버튼

    public int CharNumber;

    public Button CharExit;// 캐릭터 내보내기 버튼
    public Image BBB;// 캐릭터 스테이터스 버튼 뒤의 이미지. 처음 써보기 위해 붉은색으로 강조 표시된 이미지
    public Color color;

    public TextMeshProUGUI FoodT;// 푸드 텍스트

    public AudioSource CharBgm;// 소리 브금
    
    public Color a;

    public bool MiniGameBool;// 미니 게임을 들어갔는지 확인하기

    public TextMeshProUGUI CHname;
    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        CharStatePanel.SetActive(false);
        //CharNameState.SetActive(false);
        EatPanel.SetActive(false);
        EatButton.SetActive(false);

        LoveS.interactable = false;// 플레이어가 슬라이더 못 움직이도록 하기
        HungerS.interactable = false;
        StatureS.interactable = false;
        color.a = 0f;
        //CharImage.sprite = Ccc.sprite;
    }

    public void StateChar()// 캐릭터 상태창 열기
    {
        CharBgm.Play();
        CharStatePanel.SetActive(true);
        gameManager.Open();
        BBB.color = color;
        //State(itemlist.item[0].Start);
        if (LoveS.value != 1 || HungerS.value != 1 || StatureS.value != 1)// 모든 게이지가 차있지 않을 때
        {
            CharExit.interactable = false;
        }
        else
        {
            CharExit.interactable = true;
        }
    }
    public void CharHung()// 캐릭터 먹이 주는 창 열기
    {
        CharBgm.Play();
        EatPanel.SetActive(true);
        EatButton.SetActive(false);
        gameManager.Open();
    }
    public void State(int a)// 캐릭터의 현재 상태를 나타내기 위함
    {
        CharNumber = a;
        //Debug.Log(CharNumber);
        CharImage.sprite = SyMain.CharCom[CharNumber].itemImage;
        //LoveS.value = SyMain.CharCom[CharNumber].Love;
        //HungerS.value = SyMain.CharCom[CharNumber].Hunger;
        //StatureS.value = SyMain.CharCom[CharNumber].Stature;
        CHname.text = SyMain.CharCom[CharNumber].itemName;
        if (LoveS.value != 1 || HungerS.value != 1 || StatureS.value != 1)// 모든 게이지가 차있지 않을 때
        {
            CharExit.interactable = false;
        }
        else
        {
            CharExit.interactable = true;
        }

        //for (int i = 0; i < SyMain.charNumbers.Count; i++)
        //{
        //    if (a == SyMain.charNumbers[i])
        //    {
        //        LoveS.value = SyMain.LoveV[i];
        //        HungerS.value = SyMain.HungerV[i];
        //        StatureS.value = SyMain.StatureV[i];
        //    }
        //}
        //SyMain.Val(a);
    }
    public void ItemValue(float Hvalue)
    {
        SyMain.CharCom[CharNumber].Hunger += Hvalue;
        FoodT.gameObject.SetActive(true);
        FoodT.text = "+" + Hvalue * 100 + "%";
        Invoke("FoodTF", 1f);
        if (Hvalue != 0)
        {
            float HHH = Hvalue * 0.2f;
            //Debug.Log(HHH + "성장도를 위해");
            SyMain.CharCom[CharNumber].Stature += HHH;// 성장도 더해주기
            StatureS.value = SyMain.CharCom[CharNumber].Stature;
            SyMain.EAt(HHH);
        }
    }
    public void GoCa()
    {
        MiniGameBool = true;
        CharBgm.Play();
        SyMain.CharCom[CharNumber].Love += 0.2f;
        LoveS.value = SyMain.CharCom[CharNumber].Love;
        SyMain.CharCom[CharNumber].Stature += 0.1f;// 성장도 더해주기
        StatureS.value = SyMain.CharCom[CharNumber].Stature;
        mainToCafeManager.GoCafe();
        SyMain.Go();
    }
    public void CharAExit()// 다 키운 후 캐릭터 내보내기
    {
        CharStatePanel.SetActive(false);// 캐릭터 상태창 닫아버리기
        SyMain.CharCom[CharNumber].Love = 0;
        SyMain.CharCom[CharNumber].Hunger = 0;
        SyMain.CharCom[CharNumber].Stature = 0;// 캐릭터 프리팹의 변수 전부 초기화시켜주기
        //Debug.Log(this.gameObject.name + "오브젝트 이름 확인");
        
        SyMain.CharCom[CharNumber].item_ = false;// 캐릭터 상태
        SyMain.CharStateRight();
    }
    public void FoodTF()
    {
        FoodT.gameObject.SetActive(false);
    }
}
