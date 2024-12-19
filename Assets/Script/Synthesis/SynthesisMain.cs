using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Assertions.Must;
//using UnityEngine.UIElements; // 얜 뭔데 생겨있는거임;

[Serializable]
public class SaveSynData
{
    public bool Char_Cre;// 만들어낸 캐릭터가 하나 이상 있을 때
    public int Char_int;// 현재 캐릭터의 번호 나타내기!
    public List<string> CharNames;// 현재까지 만들어 낸 캐릭터 저장하기
    public List<int> charNumbers;// 캐릭터 번호로 왼쪽 오른쪽 넘기기
    public int Char_bool;// 화면에 캐릭터가 이미 생성되어 있는지 확인할 함수

    public int GameGold;// 게임 재화

    public List<float> LoveV = new List<float>();// 캐릭터의 애정도
    public List<float> HungerV = new List<float>();// 캐릭터의 공복도
    public List<float> StatureV = new List<float>();// 캐릭터의 성장도
    public List<bool> _Char = new List<bool>();// 캐릭터를 만들었는지 안 만들었는지

    public float RearTime;// 시간이 지나면 공복과 애정을 깎아야 하기 때문에
}
public class SynthesisMain : MonoBehaviour
{
    [SerializeField]
    private Item[] items;

    [SerializeField]
    private SaveSynData saveSynData;
    //public StuSlot[] GetStuSlots() { return stus; }
    //public Item[] GetChar() { return items; }

    [SerializeField]
    public GameObject Creation_Panel;// 합성의 전체적인 창

    [SerializeField]
    public GameObject Cre_Panel;// 합성 메인 창

    [SerializeField]
    public GameObject Stuff_Panel;// 합성의 재료 창

    [SerializeField]
    public Inventory inven;

    [SerializeField]
    private GameManager game;// 게임 매니저

    [SerializeField]
    private CreSlot[] creSlots;

    [SerializeField]
    private ItemList itemList;

    public int minusItem = 0;

    private StuSlot[] stus;

    public bool Cre_ = false;

    public GameObject InPanel;// 인벤 판넬
    public GameObject All_S;// 재료칸들의 컨텐츠

    [SerializeField]
    public Item[] CharCom;// 캐릭터들의 리스트

    [SerializeField]
    private GameObject[] CharList;// 캐릭터들의 일러스트 리스트

    [SerializeField]
    private CharStateManager CharManager;

    //public GameObject AllAnime;

    public GameObject Com_Panel;// 합성물 완성 알림 판넬
    public Image CharCreImage;// 합성에 성공한 캐릭터 이미지
    public bool Com_ = false;// 합성 완성 창이 떴는지 확인

    public int slot_bool = 0;

    public GameObject Shortage_Panel;// 재료가 부족할 때 띄울 판넬

    public Image gggg;
    public bool imageSet;// 이미지 켤 지 말지
    public float settimer = 1;
    public TextMeshProUGUI Shtext;// 재료가 부족함 텍스트

    public AudioSource ComCom;// 합성 완료 소리
    public AudioSource ComFalse;// 합성 실패 소리

    public int Char_bool;// 화면에 캐릭터가 이미 생성되어 있는지 확인할 함수
    public int Char_int;// 현재 캐릭터의 번호 나타내기!
    public bool Char_Cre;// 만들어낸 캐릭터가 하나 이상 있을 때

    public GameObject AnimeCard;

    //public GameObject CharStateNAme;// 캐릭터 전환하자 마자 이름 바꿔주기 위해
    private string filePath;// 저장

    public string CharNum;// 캐릭터 번호를 확인하기 위해
    public List<string> CharNames;// 현재까지 만들어 낸 캐릭터 저장하기
    public bool cnt = false;// 캐릭터 중복 여부를 확인하기 위해
    public List<int> charNumbers;// 캐릭터 번호로 왼쪽 오른쪽 넘기기

    public TextMeshProUGUI Syngold;// 합성창 골드 보유량

    public GameObject PPPPanel;// 캐릭터 스테이트 창 판넬

    public List<float> LoveV;// 캐릭터의 애정도
    public List<float> HungerV;// 캐릭터의 공복도
    public List<float> StatureV;// 캐릭터의 성장도
    public List<bool> _Char;// 캐릭터를 만들었는지 안 만들었는지

    public int GameGold;// 게임 재화
    public TextMeshProUGUI TextGold;

    public float RearTime;// 시간이 지나면 공복과 애정을 깎아야 하기 때문에
    public float Timer;
    public float LastTimer;// 게임 종료 후 경과한 시간을 받아내기

    public string ttttt;

    public bool aaab;// 합성하고 버튼 애니메이션 꺼 줄 변수

    public Image ima;// 재료 3가지가 모두 들어가면 활성화 될 이미지
    public float a;

    public AudioSource Ready;// 재료 3개 모두 넣음, 합성 준비 완료 소리
    public AudioSource sold_out;// 재료 부족 알림음
    public AudioSource Char_out;// 캐릭터 해금 안됨 알림음
    public AudioSource CharBgm;// 합성 성공 알림음
    public AudioSource Imputbgm;// 합성 아이콘 건드리는 소리

    public AudioSource CharEndBgm;// 캐릭터 내보내기 브금

    // Start is called before the first frame update
    void Start()
    {
        //itemList.item[0].Love = Char_int;// 프리팹으로 상태 저장해보기
        //Debug.Log(Char_int + "캐릭터 번호 확인");
        Shortage_Panel.SetActive(false);
        stus = All_S.GetComponentsInChildren<StuSlot>();
        //Anime = AllAnime.GetComponentsInChildren<CharAnime>();

        Creation_Panel.SetActive(false);
        Cre_Panel.SetActive(false);
        Com_Panel.SetActive(false);
        imageSet = false;
        //Stuff_Panel.SetActive(false);
        for (int i = 0; i < stus.Length; i++)
        {
            stus[i].SetColor(0);
            stus[i].gameObject.SetActive(false);// 이미지 보이도록 흰 색으로 해놔서 필요함ㅠ
        }

        filePath = Application.persistentDataPath + "SynData.json";// 저장
        //if (!Directory.Exists(filePath))// 해당 경로가 존재하지 않는다면
        //{
        //    Directory.CreateDirectory(filePath);// 폴더 생성(경로 만들어주기)
        //
        //}

        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            SynLoadData();
        }
        else
        {
            SaveSave();
        }
        TextGold.text = GameGold.ToString();
        Syngold.text = GameGold.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        Timer = Time.deltaTime;
        if (aaab)
        {
            creSlots[0].b1 = false;
            creSlots[1].b1 = false;
            creSlots[2].b1 = false;

            creSlots[0].a1 = 0;
            creSlots[1].a1 = 0;
            creSlots[2].a1 = 0;

            creSlots[0].colorI.fillAmount = 0;
            creSlots[1].colorI.fillAmount = 0;
            creSlots[2].colorI.fillAmount = 0;

            aaab = false;
            ima.gameObject.SetActive(false);
            ima.fillAmount = 0;
        }
        if (creSlots[0].b1 && creSlots[1].b1 && creSlots[2].b1)
        {
            ima.gameObject.SetActive(true);
            Imaa();
        }
        //if (Char_Cre)
        //{
        //    for (int i = 0; i < charNumbers.Count; i++)
        //    {
        //        if (HungerV[i] != 0 && LoveV[i] != 0)
        //        {
        //            RearTime += Timer;
        //            if (RearTime > 0)
        //            {
        //                float a = RearTime / 60;
        //                if (a > 1)
        //                {
        //                    Debug.Log(0.2f * a + "얼마나 빼야 할까?" + a + "나누는 것도");
        //                    HungerV[i] -= 0.2f * a;
        //                    LoveV[i] -= 0.1f * a;
        //                    RearTime = 0;
        //                }
        //            }
        //        }
        //
        //    }
        //}
        
        
        if (slot_bool == 2)
        {
            stus[0].MMM();// 뭐지 이게 무슨 역할이였음
        }
        if (imageSet)
        {
            settimer -= Time.deltaTime;
            SetColor(settimer);
            if (settimer <= 0f)
            {
                SetColor(0);
                imageSet = false;
                settimer = 1;
                Shortage_Panel.SetActive(false);
            }
        }
    }
    public void ItemSee()
    {
        for (int i = 0; i < stus.Length;i++)
        {
            if (stus[i].item != null)
            {
                stus[i].SetColor(1);
                stus[i].gameObject.SetActive(true);
            }
        }
    }
    public void OpenCre()// 합성창 여는 함수
    {
        Syngold.text = GameGold.ToString();
        InPanel.SetActive(false);// 인벤 판넬 꺼주기
        game.OpenPanel.Play();
        game.Open();// 게임 매니저의 오픈 함수
        Creation_Panel.SetActive(true);
        Cre_Panel.SetActive(true);
        Stuff_Panel.SetActive(true);
        slot_bool = 1;
    }
    public void Clo()// 합성창의 재료창 닫는 함수
    {
        Stuff_Panel.SetActive(false);
    }

    public void Stuff()
    {
        //LeanTween.move(Cre_Panel, new Vector3(720f, 550f, 0f), 2f);
        Stuff_Panel.SetActive(true);
        //LeanTween.move(Stuff_Panel, new Vector3(1800f, 550f, 0f), 1f);
        Cre_ = true;// 합성 재료 창이 열렸는지 유무
    }
    public void CreH()// 합성대의 첫 버튼 / 뿔
    {
        //stu_text.gameObject.SetActive(true);
        //stu_image.sprite = null;
        Imputbgm.Play();
        Stuff();// 재료 창 열기
        for (int i = 0;i < stus.Length;i++)
        {
            if (stus[i].item != null)
            {
                //Debug.Log(stus[i].item.itemName);
                if (stus[i].item.itemType == Item.ItemType.Head)// 첫번째 칸에 해당하는 놈만 들어가도록
                {
                    stus[i].SetColor(1);
                    stus[i].gameObject.SetActive(true);
                }
                else
                {
                    stus[i].SetColor(0);
                    stus[i].gameObject.SetActive(false);
                }
            }
            
        }
    }

    public void CreE()// 합성대의 두번째 버튼 / 눈
    {
        Imputbgm.Play();
        Stuff();
        for (int i = 0; i < stus.Length; i++)
        {
            if (stus[i].item != null)
            {
                //Debug.Log(stus[i].item.itemName);
                if (stus[i].item.itemType == Item.ItemType.Eye)
                {
                    stus[i].SetColor(1);
                    stus[i].gameObject.SetActive(true);
                }
                else
                {
                    stus[i].SetColor(0);
                    stus[i].gameObject.SetActive(false);
                }
            }

        }
    }
    public void CreT()// 합성대의 세번째 버튼 / 꼬리
    {
        Imputbgm.Play();
        Stuff();// 재료 창 열기
        for (int i = 0; i < stus.Length; i++)
        {
            if (stus[i].item != null)
            {
                //Debug.Log(stus[i].item.itemName);
                if (stus[i].item.itemType == Item.ItemType.Tail)// 첫번째 칸에 해당하는 놈만 들어가도록
                {
                    stus[i].SetColor(1);
                    stus[i].gameObject.SetActive(true);
                }
                else
                {
                    stus[i].SetColor(0);
                    stus[i].gameObject.SetActive(false);
                }
            }

        }
    }

    //public void CreC()// 합성대의 네번째 버튼 / 컬러
    //{
    //    Stuff();// 재료 창 열기
    //    for (int i = 0; i < stus.Length; i++)
    //    {
    //        if (stus[i].item != null)
    //        {
    //            Debug.Log(stus[i].item.itemName);
    //            if (stus[i].item.itemType == Item.ItemType.Color)// 첫번째 칸에 해당하는 놈만 들어가도록
    //            {
    //                stus[i].SetColor(1);
    //                stus[i].gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                stus[i].SetColor(0);
    //                stus[i].gameObject.SetActive(false);
    //            }
    //        }
    //
    //    }
    //}
    public void StuAdd(Item _item, int count)// 아이템을 합성 재료칸에 넣어주기
    {
        for (int i = 0; i < stus.Length; i++)// 슬롯의 개수만큼 반복
        {
            if (stus[i].item != null)// 칸에 아이템이 있을 때
            {
                if (stus[i].item.itemName == _item.itemName)// 슬롯 안에 같은 아이템이 들어있음
                {
                    //Debug.Log(i + "번째 슬롯에 같은 아이템이 들어있음");
                    stus[i].SetSlotCount(count);// 슬롯안의 아이템 갯수 증가
                    return;
                }
            }
            else// 칸에 아이템이 없을 때
            {
                stus[i].AddStu(_item, count);// 아이템을 넣어 줌
                stus[i].gameObject.SetActive(true);// 아이템을 얻었으면 해당 슬롯 보이게 해주기
                stus[i].SetColor(1);
                return;
            }
        }
    }
    public void Minus(int _i, int _cnt)
    {
        stus[_i].SetSlotCount(_cnt);
        Com_Panel.SetActive(true);
        Com_ = true;
        //stus[_i].ClearSlot();
    }
    public void ClearSlotItem()// 아이템을 전부 썼을 때 뒤의 아이템이 앞으로 오도록
    {
        for (int i = 0; i < stus.Length - 1; i++)
        {
            if (stus[i].item == null)// i번째 슬롯이 비어있고
            {
                if (stus[i + 1].item != null)// i의 다음 슬롯에 아이템이 있을 때
                {// 본인이 빈 슬롯이고 뒷슬롯이 차있을 때에는 뒷슬롯을 앞슬롯으로 옮기기
                    stus[i].item = stus[i + 1].item;
                    stus[i].itemImage.sprite = stus[i + 1].itemImage.sprite;
                    stus[i].itemCount = stus[i + 1].itemCount;
                    stus[i].gameObject.SetActive(true);
                    stus[i].SetColor(1);
                    stus[i].TextS();
                    stus[i + 1].item = null;
                    stus[i + 1].itemImage.sprite = null;
                    stus[i + 1].itemCount = 0;
                    stus[i + 1].gameObject.SetActive(false);
                    stus[i + 1].SetColor(0);
                }
            }
        }
    }
    public void Char(string name)// 합성 성공 후 캐릭터 메인 화면에 나타내기, 도감에서 아이템 받아오기
    {
        for (int i = 0; i < CharCom.Length; i++)// 결과물들을 하나씩 검사하기
        {
            if (CharCom[i].itemName == name)// 합성 결과물 이름이 같은 것이 있다면
            {
                if (Char_bool == 0)
                {// 최초 1회에 만들어지는 캐릭터가 화면에 보이도록
                    //itemList.item[0].Hunger = 1;// 이걸로 캐릭 만든 것을 변수로 저장하자
                    Char_Cre = true;// 캐릭터가 생성됨!
                    Char_bool = 1;
                    CharList[i].gameObject.SetActive(true);// 일치하는 것 나타내기/
                    Char_int = CharCom[i].Count;// 확인하는 변수에 캐릭터의 순서 넣어주기/
                    Val(Char_int);
                    CharManager.State(Char_int);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기
                    //CharCreImage.sprite = CharCom[i].itemImage;// 합성 성공에 캐릭터 사진 넣어주기?
                    //itemList.item[0].Start = Char_int;// 프리팹으로 상태 저장해보기
                    //Debug.Log("캐릭터 이름: " + CharCom[i].itemName);
                    //Debug.Log(Char_int + "순서");
                }
                //else// 그 후에는 일단 트루만..
                //{
                //    charNumbers.Add(CharCom[i].Count);// 캐릭터 번호 받기
                //}
                CharBgm.Play();
                charNumbers.Add(CharCom[i].Count);// 캐릭터 번호 받기
                CharCom[i].item_ = true;// 화면에 나타낼 캐릭터를 위해 켜주기
                CharCreImage.sprite = CharCom[i].itemImage;// 합성 성공에 캐릭터 사진 넣어주기?
                aaab = true;
                //Debug.Log("캐릭터 이름: " + CharCom[i].itemName);
                //Debug.Log(Char_int + "순서");
            }
            //for (int j = 0; j < CharList.Length; j++)
            //{
            //    if (CharCom[i].itemName == name)// 합성 결과물 이름이 같은 것이 있다면
            //    {
            //        if (Char_bool == 0)
            //        {// 최초 1회에 만들어지는 캐릭터가 화면에 보이도록
            //            Char_bool = 1;
            //            AniLi[i] = Instantiate(CharList[i], AnimeCard.transform);
            //            AniLi[i].transform.position = new Vector3(950, 430, 0);
            //            AniLi[i].SetActive(true);
            //            Char_int = i;
            //            CharManager.State(i);
            //            break;
            //            //GameObject abc = Instantiate(CharList[i], AnimeCard.transform);
            //            //abc.transform.position = new Vector3(950, 430, 0);
            //            //abc.SetActive(true);
            //
            //            //CharList[i].gameObject.SetActive(true);// 일치하는 것 나타내기/
            //            //Char_int = i;// 확인하는 변수에 캐릭터의 순서 넣어주기/
            //            //CharCom[i].item_ = true;// 화면에 나타낼 캐릭터를 위해 켜주기
            //            //CharManager.State(i);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기/
            //        }
            //        else// 그 후에는 일단 트루만..
            //        {
            //            AniLi[i] = Instantiate(CharList[i], AnimeCard.transform);
            //            AniLi[i].transform.position = new Vector3(950, 430, 0);
            //            CharCom[i].item_ = true;// 화면에 나타낼 캐릭터를 위해 켜주기
            //        }
            //    }
            //}

        }
    }

    public void CharStateRight()
    {
        //for (int i = 0;i < CharCom.Length;i++)
        //{
        //    if (Char_int < i)// 현재 번호가 전체 결과물보다 적을 때
        //    {// 오른쪽으로 옮기면 다음 번호로 넘어가도록
        //        if (CharCom[i].item_ == true)// 결과물을 만든 적이 있다면
        //        {
        //            CharList[Char_int].gameObject.SetActive(false);// 현재 메인 캐릭터 지워주기
        //            CharList[i].gameObject.SetActive(true);// 다음 메인 캐릭터 보여주기
        //            Char_int = i;// 다시 현재 번호 고쳐주기
        //            CharManager.State(i);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기
        //
        //            //itemList.item[0].Start = Char_int;// 프리팹으로 상태 저장해보기
        //            CharStateNAme.SetActive(false);// 캐릭터 이름 가려주기
        //            Debug.Log("캐릭터 이름: " + CharCom[i].itemName);
        //            Debug.Log(i + "순서");
        //            break;
        //        }
        //    }
        //}
        for (int i = 0; i < charNumbers.Count; i++)
        {
            //Debug.Log(Char_int + "확인");
            //Debug.Log(i + "?");
            //Debug.Log(charNumbers[i] + "더블확인");
            if (Char_int == charNumbers[i])// 현재 번호가 같을 때
            {
                if (i < charNumbers.Count - 1)
                {
                    CharList[Char_int].gameObject.SetActive(false);// 현재 메인 캐릭터 지워주기
                    CharList[charNumbers[i + 1]].gameObject.SetActive(true);// 다음 메인 캐릭터 보여주기
                    Char_int = charNumbers[i + 1];// 다시 현재 번호 고쳐주기
                    Val(Char_int);
                    CharManager.State(Char_int);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기

                    //itemList.item[0].Start = Char_int;// 프리팹으로 상태 저장해보기
                    //Debug.Log("캐릭터 이름: " + CharCom[Char_int].itemName);
                    break;
                }
            }
        }
    }
    public void CharStateLeft()
    {
        //for (int i = CharCom.Length - 1; i > -1; i--)// 끝에서부터 조사
        //{
        //    if (Char_int > i)// 현재 번호가 전체 결과물보다 적을 때
        //    {// 왼쪽으로 옮기면 이전 번호로 넘어가도록
        //        if (CharCom[i].item_ == true)// 결과물을 만든 적이 있다면
        //        {

        for (int i = charNumbers.Count - 1; i > -1; i--)
        {
            //Debug.Log(Char_int + "확인");
            //Debug.Log(i + "?");
            //Debug.Log(charNumbers[i - 1] + "더블확인");
            if (Char_int == charNumbers[i])// 현재 번호가 같을 때
            {
                //Debug.Log(Char_int + "확인2");
                //Debug.Log(i + "?2");
                //Debug.Log(charNumbers[i - 1] + "더블확인2");
                if (i > 0)
                {
                    CharList[Char_int].gameObject.SetActive(false);// 현재 메인 캐릭터 지워주기
                    CharList[charNumbers[i - 1]].gameObject.SetActive(true);// 다음 메인 캐릭터 보여주기
                    Char_int = charNumbers[i - 1];// 다시 현재 번호 고쳐주기
                    Val(Char_int);
                    CharManager.State(Char_int);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기

                    //itemList.item[0].Start = Char_int;// 프리팹으로 상태 저장해보기
                    //Debug.Log("캐릭터 이름: " + CharCom[Char_int].itemName);
                    break;
                }
            }
        }
    }
    public void CCC()// 합성하기 버튼 함수
    {
        if (GameGold >= 50)// 골드가 충분할 때만 합성하기
        {
            GameGold -= 50;// 합성할 때 골드 소모하기
            TextGold.text = GameGold.ToString();// 골드 소모 후 텍스트 수정해주기
            Syngold.text = GameGold.ToString();
            //itemList.item[0].Count -= 50;// 골드 프리팹에서 골드 소모시켜주기
            cnt = false;
            ttttt = "";
            CharNum = "";
            minusItem = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (creSlots[i].item != null)// 아이템이 있을 때
                {
                    CharNum += creSlots[i].item.itemNumber;
                    minusItem++;
                    ttttt += creSlots[i].item.itemNumber;
                    //Debug.Log(ttttt + "제발 미안");
                    if (minusItem == 3)
                    {
                        //Debug.Log(ttttt + "제발 ! 미안");
                        for (int j = 0; j < CharCom.Length; j++)
                        {
                            if (ttttt == CharCom[j].itemNumber)
                            {
                                
                                if (CharNames.Count == 0)// 캐릭터를 한 번도 만든 적이 없을 때
                                {
                                    //CharManager.BBB.gameObject.SetActive(true);
                                    CharNames.Add(CharNum);
                                    LoveV.Add(0);
                                    HungerV.Add(0);
                                    StatureV.Add(0);
                                    _Char.Add(true);
                                    ComCom.Play();
                                    inven.GetComponent<Inventory>().Stu_Inven();
                                    CharManager.CharNameState.SetActive(true);// 캐릭터 상세 창 열어놓기
                                    break;
                                }
                                else
                                {
                                    for (int i2 = 0; i2 < CharNames.Count; i2++)
                                    {
                                        //Debug.Log(CharNames[i] + "리스트 확인 한 번만");
                                        if (CharNames[i2] == CharNum)// 리스트와 이번에 만들 캐릭터가 겹칠 때
                                        {
                                            cnt = true;
                                            //Debug.Log(cnt + "횟수 기록");
                                            break;
                                        }
                                    }
                                    if (cnt == false)
                                    {
                                        //Debug.Log("중복없음!");
                                        CharNames.Add(CharNum);
                                        LoveV.Add(0);
                                        HungerV.Add(0);
                                        StatureV.Add(0);
                                        _Char.Add(true);
                                        ComCom.Play();
                                        inven.GetComponent<Inventory>().Stu_Inven();
                                        break;
                                    }
                                    else
                                    {
                                        CCCFalse();
                                        Char_out.Play();
                                        Shtext.text = "이미 만들어진 캐릭터입니다.";
                                        //Debug.Log("중복된 캐릭터!");
                                        break;
                                    }
                                }
                                
                            }
                            else
                            {
                                if (j == CharCom.Length - 1)
                                {
                                    CCCFalse();
                                    Char_out.Play();
                                    Shtext.text = "현재 만들지 못하는 캐릭터입니다.";
                                }
                            }
                        }
                        //Debug.Log(CharNum + "여기서 테스트 한 번");
                        
                        // 인벤 슬롯의 횟수를 차감시키도록 도와줄 함수
                        //creSlots[i].item = null;
                        //creSlots[i].item.itemImage = null;
                    }
                }
                else// 합성 재료에 넣은 재료가 부족할 때
                {
                    CCCFalse();
                    sold_out.Play();
                    break;
                }
            }
        }
        else
        {
            CCCFalse();
            sold_out.Play();
            Shtext.text = "골드가 부족합니다.";
        }
    }
    public void TestT()// 내보내기 위한 테스트 버튼
    {
        CharCom[Char_int].Love = 1;
        CharCom[Char_int].Hunger = 1;
        CharCom[Char_int].Stature = 1;
        CharManager.State(Char_int);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기
        for (int i = 0; i < charNumbers.Count; i++)
        {
            if (charNumbers[i] == Char_int)// 수가 겹쳤을 때
            {
                LoveV[i] += 1f;
                HungerV[i] += 1f;
                StatureV[i] += 1f;
            }
        }
        Val(Char_int);
    }
    public void CharExit()// 캐릭터 내보내기 버튼
    {
        CharEndBgm.Play();

        CharCom[Char_int].Love = 0;
        CharCom[Char_int].Hunger = 0;
        CharCom[Char_int].Stature = 0;// 캐릭터 프리팹의 변수 전부 초기화시켜주기

        //CharCom[Char_int].item_ = false;// 캐릭터 상태
        CharList[Char_int].SetActive(false);// 캐릭터 메인 화면에서 제거해주기
        CharNames.Remove(CharCom[Char_int].itemNumber);// 만들었던 리스트에서 제거해주기
        CharManager.CharNameState.SetActive(false);// 캐릭터가 없으니 상세 창 닫아놓기

        for (int i = 0; i < charNumbers.Count; i++)
        {
            //Debug.Log(LoveV[i] + "혹시?");
            if (i == Char_int)
            {
                LoveV[i] = 2f;
                HungerV[i] = 2f;
                StatureV[i] = 2f;
                //Debug.Log(i + "이 순서가 맞나");
                //Debug.Log(LoveV[i] + "??????");
                _Char[i] = false;
                LoveV.Remove(2f);
                HungerV.Remove(2f);
                StatureV.Remove(2f);
                _Char.Remove(false);
                //Debug.Log(LoveV + " 애정 리스트 확인");
                //CharManager.CharNameState.SetActive(false);// 캐릭터가 없으니 상세 창 닫아놓기
                //Debug.Log("어디가 실행?");
                break;
            }
            //if (LoveV[i] == 1 || HungerV[i] == 1 || StatureV[i] == 1)
            //{
            //    
            //    
            //}
        }
        charNumbers.Remove(CharCom[Char_int].Count);
        PPPPanel.SetActive(false);// 캐릭터 스테이트 창 판넬 꺼주기
        game.Panel_Exit();

        if (Char_Cre)
        {
            if (charNumbers.Count == 0)
            {
                Char_int = 0;
                Char_Cre = false;
                Char_bool = 0;
                CharManager.CharNameState.SetActive(false);// 캐릭터가 없으니 상세 창 닫아놓기
                LoveV.Clear();
                HungerV.Clear();
                StatureV.Clear();
                _Char.Clear();
            }
            else
            {
                for (int i = 0; i < charNumbers.Count; i++)// 내보낸 후 첫 캐릭터 보여주기
                {
                    if (_Char[i] == true)
                    {
                        Char_int = charNumbers[i];
                        CharList[Char_int].gameObject.SetActive(true);// 다음 메인 캐릭터 보여주기
                        Val(Char_int);
                        //Debug.Log("이쪽이 실행?");
                        CharManager.State(Char_int);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기
                        CharManager.CharNameState.SetActive(true);// 캐릭터가 없으니 상세 창 닫아놓기
                                                                  //CharStateNAme.SetActive(false);// 캐릭터 이름 가려주기
                        break;
                    }
                    //else
                    //{
                    //    if (i == charNumbers.Count - 1)
                    //    {
                    //        Debug.Log("이쪽이 실행되나?");
                    //        
                    //    }
                    //}
                }
            }
            
        }

        //for (int i = 0; i < CharNames.Count; i++)// 내보낸 후 첫 캐릭터 보여주기
        //{
        //    if (_Char[i] == true)// 결과물을 만든 적이 있다면
        //    {
        //        CharList[i].gameObject.SetActive(true);// 다음 메인 캐릭터 보여주기
        //        //Char_int = CharCom[i].Count;// 다시 현재 번호 고쳐주기
        //        //CharManager.State(Char_int);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기
        //
        //        //itemList.item[0].Start = Char_int;// 프리팹으로 상태 저장해보기
        //        //CharStateNAme.SetActive(false);// 캐릭터 이름 가려주기
        //        Debug.Log("캐릭터 이름: " + CharCom[i].itemName);
        //        Debug.Log(i + "순서");
        //        break;
        //    }
        //    else
        //    {
        //        Char_Cre = false;
        //        Char_bool = 0;
        //    }
        //}
    }

    public void SetSh()
    {
        Shortage_Panel.SetActive(false);// 재료가 부족하니 띄우기
    }
    public void CCCFalse()
    {
        ComFalse.Play();
        imageSet = true;
        Shtext.text = "재료가 부족합니다";
        Shortage_Panel.SetActive(true);// 재료가 부족하니 띄우기
        Time.timeScale = 1;
        Invoke("SetSh", 1f);// 1초 후 재료부족 알림 꺼주기
    }
    public void SaveSave()
    {
        //Debug.Log(CharNames[0] + "호옥시..?");
        saveSynData.Char_Cre = Char_Cre;
        saveSynData.Char_int = Char_int;
        saveSynData.CharNames = CharNames;
        saveSynData.charNumbers = charNumbers;
        saveSynData.Char_bool = Char_bool;
        saveSynData.GameGold = GameGold;

        saveSynData.LoveV = LoveV;
        saveSynData.HungerV = HungerV;
        saveSynData.StatureV = StatureV;
        saveSynData._Char = _Char;

        saveSynData.RearTime = RearTime;

        SynSaveData(saveSynData);
        //Debug.Log(saveSynData.Char_Cre + "캐릭터 유무 저장");
        //Debug.Log(saveSynData.Char_int + "캐릭터 번호 저장");
    }
    public void SynSaveData(SaveSynData data)
    {// 데이터 저장 함수
        // 데이터 객체를 제이슨 문자열로 반환
        string json = JsonUtility.ToJson(data, true);// 트루는 포맷팅 옵션(가독성을 위한 것)

        // 파일에 제이슨 문자열 쓰기
        File.WriteAllText(filePath, json);

        //Debug.Log("캐릭터의 제이슨 파일이 저장됨" + filePath);

    }
    public SaveSynData SynLoadData()
    {
        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            string json = File.ReadAllText(filePath);// 파일에서 제이슨 문자열 읽기

            SaveSynData data = JsonUtility.FromJson<SaveSynData>(json);// 제이슨 문자열을 반환

            //Debug.Log("제이슨 데이터가 로드됨 " + filePath);

            Char_int = data.Char_int;// 현재 캐릭터의 번호 나타내기!
            Char_Cre = data.Char_Cre;// 만들어낸 캐릭터가 하나 이상 있을 때
            CharNames = data.CharNames;// 현재까지 만들어 낸 캐릭터 저장하기 리스트
            charNumbers = data.charNumbers;// 캐릭터 번호로 왼쪽 오른쪽 넘기기 리스트
            CharManager.State(Char_int);// 캐릭터의 상태를 나타내기 위해 캐릭터의 번호 넘겨주기
            Char_bool = data.Char_bool;// 화면에 캐릭터가 이미 생성되어 있는지 확인할 함수
            GameGold = data.GameGold;

            LoveV = data.LoveV;
            HungerV = data.HungerV;
            StatureV = data.StatureV;
            _Char = data._Char;

            RearTime = data.RearTime;
            RearTime += LastTimer;
            //Debug.Log(Char_int + " 캐릭터 번호 로드");
            //Debug.Log(Char_Cre + " 캐릭터 유무 로드");

            TextGold.text = GameGold.ToString();
            Syngold.text = GameGold.ToString();

            //if (Char_Cre)
            //{
            //    for (int i = 0; i < CharNames.Count; i++)
            //    {
            //        if (CharNames[0] == CharCom[i].itemNumber)
            //        {
            //            CharList[i].SetActive(true);
            //        }
            //    }
            //}

            //for (int i = 0; i < CharCom.Length; i++)// 완성품 캐릭터 검사하기
            //{
            //    for (int j = 0; j < CharNames.Count; j++)// 진짜 만들어 낸 캐릭터 검사하기
            //    {
            //        if (CharCom[i].itemNumber == CharNames[j])// 목록과 일치하는 것이 있다면
            //        {// 저장되어있던 애정도 배고픔 성장도 모두 넣어주기
            //            CharCom[i].Love = data.LoveV[j];
            //            CharCom[i].Hunger = data.HungerV[j];
            //            CharCom[i].Stature = data.StatureV[j];
            //            CharCom[i].item_ = data._Char[j];
            //        }
            //    }
            //}

            if (Char_Cre)// 만약 캐릭터가 생성되어 있다면
            {
                CharList[Char_int].gameObject.SetActive(true);// 일치하는 것 나타내기/
                Val(Char_int);
                CharManager.State(Char_int);
                CharManager.CharNameState.SetActive(true);// 캐릭터 창 열어놓기

            }
            return data;
        }
        else
        {
            //Debug.Log("저장된 제이슨 파일이 없다.");
            return null;
        }
    }
    public void SetColor(float _alpha)// 이미지 색의 알파값, 투명도 조절 관련 함수
    {
        Color color = gggg.color;
        Color textColor = Shtext.color;
        color.a = _alpha;
        textColor.a = _alpha;
        gggg.color = color;
        //Shtext.color = color;
    }
    public void EAt(float a)
    {
        for (int i = 0; i < charNumbers.Count; i++)
        {
            if (charNumbers[i] == Char_int)// 수가 겹쳤을 때
            {
                if (HungerV[i] <= 0.9f)
                    HungerV[i] += a;
            }
        }
    }
    public void Go()
    {
        for (int i = 0; i < charNumbers.Count; i++)
        {
            if (charNumbers[i] == Char_int)// 수가 겹쳤을 때
            {
                if (StatureV[i] <= 0.9f && LoveV[i] <= 0.9f)
                {
                    LoveV[i] += 0.2f;
                    StatureV[i] += 0.1f;
                }
            }
        }
    }
    public void LastTime(TimeSpan timeAway)
    {
        LastTimer = (float)timeAway.TotalSeconds;
    }
    public void Val(int a)
    {
        for (int i = 0; i < charNumbers.Count; i++)
        {
            if (a == charNumbers[i])
            {
                //Debug.Log(a + "상태 확인이 필요..");
                CharManager.LoveS.value = LoveV[i];
                //Debug.Log(LoveV[i] + "수치 확인이 필요함!");
                CharManager.HungerS.value = HungerV[i];
                CharManager.StatureS.value = StatureV[i];
            }
        }
            
    }

    public void Imaa()// 재료 3가지가 모두 들어갔을 때 중앙의 캐릭터 애니메이션을 해줄것임
    {
        if (a == 0)
            Ready.Play();
        a += Timer / 2;
        ima.fillAmount = a;
        if (a >= 1)
        {
            a = 0;
            
        }
    }
}
