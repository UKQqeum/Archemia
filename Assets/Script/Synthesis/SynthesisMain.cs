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
//using UnityEngine.UIElements; // �� ���� �����ִ°���;

[Serializable]
public class SaveSynData
{
    public bool Char_Cre;// ���� ĳ���Ͱ� �ϳ� �̻� ���� ��
    public int Char_int;// ���� ĳ������ ��ȣ ��Ÿ����!
    public List<string> CharNames;// ������� ����� �� ĳ���� �����ϱ�
    public List<int> charNumbers;// ĳ���� ��ȣ�� ���� ������ �ѱ��
    public int Char_bool;// ȭ�鿡 ĳ���Ͱ� �̹� �����Ǿ� �ִ��� Ȯ���� �Լ�

    public int GameGold;// ���� ��ȭ

    public List<float> LoveV = new List<float>();// ĳ������ ������
    public List<float> HungerV = new List<float>();// ĳ������ ������
    public List<float> StatureV = new List<float>();// ĳ������ ���嵵
    public List<bool> _Char = new List<bool>();// ĳ���͸� ��������� �� ���������

    public float RearTime;// �ð��� ������ ������ ������ ��ƾ� �ϱ� ������
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
    public GameObject Creation_Panel;// �ռ��� ��ü���� â

    [SerializeField]
    public GameObject Cre_Panel;// �ռ� ���� â

    [SerializeField]
    public GameObject Stuff_Panel;// �ռ��� ��� â

    [SerializeField]
    public Inventory inven;

    [SerializeField]
    private GameManager game;// ���� �Ŵ���

    [SerializeField]
    private CreSlot[] creSlots;

    [SerializeField]
    private ItemList itemList;

    public int minusItem = 0;

    private StuSlot[] stus;

    public bool Cre_ = false;

    public GameObject InPanel;// �κ� �ǳ�
    public GameObject All_S;// ���ĭ���� ������

    [SerializeField]
    public Item[] CharCom;// ĳ���͵��� ����Ʈ

    [SerializeField]
    private GameObject[] CharList;// ĳ���͵��� �Ϸ���Ʈ ����Ʈ

    [SerializeField]
    private CharStateManager CharManager;

    //public GameObject AllAnime;

    public GameObject Com_Panel;// �ռ��� �ϼ� �˸� �ǳ�
    public Image CharCreImage;// �ռ��� ������ ĳ���� �̹���
    public bool Com_ = false;// �ռ� �ϼ� â�� ������ Ȯ��

    public int slot_bool = 0;

    public GameObject Shortage_Panel;// ��ᰡ ������ �� ��� �ǳ�

    public Image gggg;
    public bool imageSet;// �̹��� �� �� ����
    public float settimer = 1;
    public TextMeshProUGUI Shtext;// ��ᰡ ������ �ؽ�Ʈ

    public AudioSource ComCom;// �ռ� �Ϸ� �Ҹ�
    public AudioSource ComFalse;// �ռ� ���� �Ҹ�

    public int Char_bool;// ȭ�鿡 ĳ���Ͱ� �̹� �����Ǿ� �ִ��� Ȯ���� �Լ�
    public int Char_int;// ���� ĳ������ ��ȣ ��Ÿ����!
    public bool Char_Cre;// ���� ĳ���Ͱ� �ϳ� �̻� ���� ��

    public GameObject AnimeCard;

    //public GameObject CharStateNAme;// ĳ���� ��ȯ���� ���� �̸� �ٲ��ֱ� ����
    private string filePath;// ����

    public string CharNum;// ĳ���� ��ȣ�� Ȯ���ϱ� ����
    public List<string> CharNames;// ������� ����� �� ĳ���� �����ϱ�
    public bool cnt = false;// ĳ���� �ߺ� ���θ� Ȯ���ϱ� ����
    public List<int> charNumbers;// ĳ���� ��ȣ�� ���� ������ �ѱ��

    public TextMeshProUGUI Syngold;// �ռ�â ��� ������

    public GameObject PPPPanel;// ĳ���� ������Ʈ â �ǳ�

    public List<float> LoveV;// ĳ������ ������
    public List<float> HungerV;// ĳ������ ������
    public List<float> StatureV;// ĳ������ ���嵵
    public List<bool> _Char;// ĳ���͸� ��������� �� ���������

    public int GameGold;// ���� ��ȭ
    public TextMeshProUGUI TextGold;

    public float RearTime;// �ð��� ������ ������ ������ ��ƾ� �ϱ� ������
    public float Timer;
    public float LastTimer;// ���� ���� �� ����� �ð��� �޾Ƴ���

    public string ttttt;

    public bool aaab;// �ռ��ϰ� ��ư �ִϸ��̼� �� �� ����

    public Image ima;// ��� 3������ ��� ���� Ȱ��ȭ �� �̹���
    public float a;

    public AudioSource Ready;// ��� 3�� ��� ����, �ռ� �غ� �Ϸ� �Ҹ�
    public AudioSource sold_out;// ��� ���� �˸���
    public AudioSource Char_out;// ĳ���� �ر� �ȵ� �˸���
    public AudioSource CharBgm;// �ռ� ���� �˸���
    public AudioSource Imputbgm;// �ռ� ������ �ǵ帮�� �Ҹ�

    public AudioSource CharEndBgm;// ĳ���� �������� ���

    // Start is called before the first frame update
    void Start()
    {
        //itemList.item[0].Love = Char_int;// ���������� ���� �����غ���
        //Debug.Log(Char_int + "ĳ���� ��ȣ Ȯ��");
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
            stus[i].gameObject.SetActive(false);// �̹��� ���̵��� �� ������ �س��� �ʿ��Ԥ�
        }

        filePath = Application.persistentDataPath + "SynData.json";// ����
        //if (!Directory.Exists(filePath))// �ش� ��ΰ� �������� �ʴ´ٸ�
        //{
        //    Directory.CreateDirectory(filePath);// ���� ����(��� ������ֱ�)
        //
        //}

        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
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
        //                    Debug.Log(0.2f * a + "�󸶳� ���� �ұ�?" + a + "������ �͵�");
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
            stus[0].MMM();// ���� �̰� ���� �����̿���
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
    public void OpenCre()// �ռ�â ���� �Լ�
    {
        Syngold.text = GameGold.ToString();
        InPanel.SetActive(false);// �κ� �ǳ� ���ֱ�
        game.OpenPanel.Play();
        game.Open();// ���� �Ŵ����� ���� �Լ�
        Creation_Panel.SetActive(true);
        Cre_Panel.SetActive(true);
        Stuff_Panel.SetActive(true);
        slot_bool = 1;
    }
    public void Clo()// �ռ�â�� ���â �ݴ� �Լ�
    {
        Stuff_Panel.SetActive(false);
    }

    public void Stuff()
    {
        //LeanTween.move(Cre_Panel, new Vector3(720f, 550f, 0f), 2f);
        Stuff_Panel.SetActive(true);
        //LeanTween.move(Stuff_Panel, new Vector3(1800f, 550f, 0f), 1f);
        Cre_ = true;// �ռ� ��� â�� ���ȴ��� ����
    }
    public void CreH()// �ռ����� ù ��ư / ��
    {
        //stu_text.gameObject.SetActive(true);
        //stu_image.sprite = null;
        Imputbgm.Play();
        Stuff();// ��� â ����
        for (int i = 0;i < stus.Length;i++)
        {
            if (stus[i].item != null)
            {
                //Debug.Log(stus[i].item.itemName);
                if (stus[i].item.itemType == Item.ItemType.Head)// ù��° ĭ�� �ش��ϴ� �� ������
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

    public void CreE()// �ռ����� �ι�° ��ư / ��
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
    public void CreT()// �ռ����� ����° ��ư / ����
    {
        Imputbgm.Play();
        Stuff();// ��� â ����
        for (int i = 0; i < stus.Length; i++)
        {
            if (stus[i].item != null)
            {
                //Debug.Log(stus[i].item.itemName);
                if (stus[i].item.itemType == Item.ItemType.Tail)// ù��° ĭ�� �ش��ϴ� �� ������
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

    //public void CreC()// �ռ����� �׹�° ��ư / �÷�
    //{
    //    Stuff();// ��� â ����
    //    for (int i = 0; i < stus.Length; i++)
    //    {
    //        if (stus[i].item != null)
    //        {
    //            Debug.Log(stus[i].item.itemName);
    //            if (stus[i].item.itemType == Item.ItemType.Color)// ù��° ĭ�� �ش��ϴ� �� ������
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
    public void StuAdd(Item _item, int count)// �������� �ռ� ���ĭ�� �־��ֱ�
    {
        for (int i = 0; i < stus.Length; i++)// ������ ������ŭ �ݺ�
        {
            if (stus[i].item != null)// ĭ�� �������� ���� ��
            {
                if (stus[i].item.itemName == _item.itemName)// ���� �ȿ� ���� �������� �������
                {
                    //Debug.Log(i + "��° ���Կ� ���� �������� �������");
                    stus[i].SetSlotCount(count);// ���Ծ��� ������ ���� ����
                    return;
                }
            }
            else// ĭ�� �������� ���� ��
            {
                stus[i].AddStu(_item, count);// �������� �־� ��
                stus[i].gameObject.SetActive(true);// �������� ������� �ش� ���� ���̰� ���ֱ�
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
    public void ClearSlotItem()// �������� ���� ���� �� ���� �������� ������ ������
    {
        for (int i = 0; i < stus.Length - 1; i++)
        {
            if (stus[i].item == null)// i��° ������ ����ְ�
            {
                if (stus[i + 1].item != null)// i�� ���� ���Կ� �������� ���� ��
                {// ������ �� �����̰� �޽����� ������ ������ �޽����� �ս������� �ű��
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
    public void Char(string name)// �ռ� ���� �� ĳ���� ���� ȭ�鿡 ��Ÿ����, �������� ������ �޾ƿ���
    {
        for (int i = 0; i < CharCom.Length; i++)// ��������� �ϳ��� �˻��ϱ�
        {
            if (CharCom[i].itemName == name)// �ռ� ����� �̸��� ���� ���� �ִٸ�
            {
                if (Char_bool == 0)
                {// ���� 1ȸ�� ��������� ĳ���Ͱ� ȭ�鿡 ���̵���
                    //itemList.item[0].Hunger = 1;// �̰ɷ� ĳ�� ���� ���� ������ ��������
                    Char_Cre = true;// ĳ���Ͱ� ������!
                    Char_bool = 1;
                    CharList[i].gameObject.SetActive(true);// ��ġ�ϴ� �� ��Ÿ����/
                    Char_int = CharCom[i].Count;// Ȯ���ϴ� ������ ĳ������ ���� �־��ֱ�/
                    Val(Char_int);
                    CharManager.State(Char_int);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�
                    //CharCreImage.sprite = CharCom[i].itemImage;// �ռ� ������ ĳ���� ���� �־��ֱ�?
                    //itemList.item[0].Start = Char_int;// ���������� ���� �����غ���
                    //Debug.Log("ĳ���� �̸�: " + CharCom[i].itemName);
                    //Debug.Log(Char_int + "����");
                }
                //else// �� �Ŀ��� �ϴ� Ʈ�縸..
                //{
                //    charNumbers.Add(CharCom[i].Count);// ĳ���� ��ȣ �ޱ�
                //}
                CharBgm.Play();
                charNumbers.Add(CharCom[i].Count);// ĳ���� ��ȣ �ޱ�
                CharCom[i].item_ = true;// ȭ�鿡 ��Ÿ�� ĳ���͸� ���� ���ֱ�
                CharCreImage.sprite = CharCom[i].itemImage;// �ռ� ������ ĳ���� ���� �־��ֱ�?
                aaab = true;
                //Debug.Log("ĳ���� �̸�: " + CharCom[i].itemName);
                //Debug.Log(Char_int + "����");
            }
            //for (int j = 0; j < CharList.Length; j++)
            //{
            //    if (CharCom[i].itemName == name)// �ռ� ����� �̸��� ���� ���� �ִٸ�
            //    {
            //        if (Char_bool == 0)
            //        {// ���� 1ȸ�� ��������� ĳ���Ͱ� ȭ�鿡 ���̵���
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
            //            //CharList[i].gameObject.SetActive(true);// ��ġ�ϴ� �� ��Ÿ����/
            //            //Char_int = i;// Ȯ���ϴ� ������ ĳ������ ���� �־��ֱ�/
            //            //CharCom[i].item_ = true;// ȭ�鿡 ��Ÿ�� ĳ���͸� ���� ���ֱ�
            //            //CharManager.State(i);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�/
            //        }
            //        else// �� �Ŀ��� �ϴ� Ʈ�縸..
            //        {
            //            AniLi[i] = Instantiate(CharList[i], AnimeCard.transform);
            //            AniLi[i].transform.position = new Vector3(950, 430, 0);
            //            CharCom[i].item_ = true;// ȭ�鿡 ��Ÿ�� ĳ���͸� ���� ���ֱ�
            //        }
            //    }
            //}

        }
    }

    public void CharStateRight()
    {
        //for (int i = 0;i < CharCom.Length;i++)
        //{
        //    if (Char_int < i)// ���� ��ȣ�� ��ü ��������� ���� ��
        //    {// ���������� �ű�� ���� ��ȣ�� �Ѿ����
        //        if (CharCom[i].item_ == true)// ������� ���� ���� �ִٸ�
        //        {
        //            CharList[Char_int].gameObject.SetActive(false);// ���� ���� ĳ���� �����ֱ�
        //            CharList[i].gameObject.SetActive(true);// ���� ���� ĳ���� �����ֱ�
        //            Char_int = i;// �ٽ� ���� ��ȣ �����ֱ�
        //            CharManager.State(i);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�
        //
        //            //itemList.item[0].Start = Char_int;// ���������� ���� �����غ���
        //            CharStateNAme.SetActive(false);// ĳ���� �̸� �����ֱ�
        //            Debug.Log("ĳ���� �̸�: " + CharCom[i].itemName);
        //            Debug.Log(i + "����");
        //            break;
        //        }
        //    }
        //}
        for (int i = 0; i < charNumbers.Count; i++)
        {
            //Debug.Log(Char_int + "Ȯ��");
            //Debug.Log(i + "?");
            //Debug.Log(charNumbers[i] + "����Ȯ��");
            if (Char_int == charNumbers[i])// ���� ��ȣ�� ���� ��
            {
                if (i < charNumbers.Count - 1)
                {
                    CharList[Char_int].gameObject.SetActive(false);// ���� ���� ĳ���� �����ֱ�
                    CharList[charNumbers[i + 1]].gameObject.SetActive(true);// ���� ���� ĳ���� �����ֱ�
                    Char_int = charNumbers[i + 1];// �ٽ� ���� ��ȣ �����ֱ�
                    Val(Char_int);
                    CharManager.State(Char_int);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�

                    //itemList.item[0].Start = Char_int;// ���������� ���� �����غ���
                    //Debug.Log("ĳ���� �̸�: " + CharCom[Char_int].itemName);
                    break;
                }
            }
        }
    }
    public void CharStateLeft()
    {
        //for (int i = CharCom.Length - 1; i > -1; i--)// ���������� ����
        //{
        //    if (Char_int > i)// ���� ��ȣ�� ��ü ��������� ���� ��
        //    {// �������� �ű�� ���� ��ȣ�� �Ѿ����
        //        if (CharCom[i].item_ == true)// ������� ���� ���� �ִٸ�
        //        {

        for (int i = charNumbers.Count - 1; i > -1; i--)
        {
            //Debug.Log(Char_int + "Ȯ��");
            //Debug.Log(i + "?");
            //Debug.Log(charNumbers[i - 1] + "����Ȯ��");
            if (Char_int == charNumbers[i])// ���� ��ȣ�� ���� ��
            {
                //Debug.Log(Char_int + "Ȯ��2");
                //Debug.Log(i + "?2");
                //Debug.Log(charNumbers[i - 1] + "����Ȯ��2");
                if (i > 0)
                {
                    CharList[Char_int].gameObject.SetActive(false);// ���� ���� ĳ���� �����ֱ�
                    CharList[charNumbers[i - 1]].gameObject.SetActive(true);// ���� ���� ĳ���� �����ֱ�
                    Char_int = charNumbers[i - 1];// �ٽ� ���� ��ȣ �����ֱ�
                    Val(Char_int);
                    CharManager.State(Char_int);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�

                    //itemList.item[0].Start = Char_int;// ���������� ���� �����غ���
                    //Debug.Log("ĳ���� �̸�: " + CharCom[Char_int].itemName);
                    break;
                }
            }
        }
    }
    public void CCC()// �ռ��ϱ� ��ư �Լ�
    {
        if (GameGold >= 50)// ��尡 ����� ���� �ռ��ϱ�
        {
            GameGold -= 50;// �ռ��� �� ��� �Ҹ��ϱ�
            TextGold.text = GameGold.ToString();// ��� �Ҹ� �� �ؽ�Ʈ �������ֱ�
            Syngold.text = GameGold.ToString();
            //itemList.item[0].Count -= 50;// ��� �����տ��� ��� �Ҹ�����ֱ�
            cnt = false;
            ttttt = "";
            CharNum = "";
            minusItem = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (creSlots[i].item != null)// �������� ���� ��
                {
                    CharNum += creSlots[i].item.itemNumber;
                    minusItem++;
                    ttttt += creSlots[i].item.itemNumber;
                    //Debug.Log(ttttt + "���� �̾�");
                    if (minusItem == 3)
                    {
                        //Debug.Log(ttttt + "���� ! �̾�");
                        for (int j = 0; j < CharCom.Length; j++)
                        {
                            if (ttttt == CharCom[j].itemNumber)
                            {
                                
                                if (CharNames.Count == 0)// ĳ���͸� �� ���� ���� ���� ���� ��
                                {
                                    //CharManager.BBB.gameObject.SetActive(true);
                                    CharNames.Add(CharNum);
                                    LoveV.Add(0);
                                    HungerV.Add(0);
                                    StatureV.Add(0);
                                    _Char.Add(true);
                                    ComCom.Play();
                                    inven.GetComponent<Inventory>().Stu_Inven();
                                    CharManager.CharNameState.SetActive(true);// ĳ���� �� â �������
                                    break;
                                }
                                else
                                {
                                    for (int i2 = 0; i2 < CharNames.Count; i2++)
                                    {
                                        //Debug.Log(CharNames[i] + "����Ʈ Ȯ�� �� ����");
                                        if (CharNames[i2] == CharNum)// ����Ʈ�� �̹��� ���� ĳ���Ͱ� ��ĥ ��
                                        {
                                            cnt = true;
                                            //Debug.Log(cnt + "Ƚ�� ���");
                                            break;
                                        }
                                    }
                                    if (cnt == false)
                                    {
                                        //Debug.Log("�ߺ�����!");
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
                                        Shtext.text = "�̹� ������� ĳ�����Դϴ�.";
                                        //Debug.Log("�ߺ��� ĳ����!");
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
                                    Shtext.text = "���� ������ ���ϴ� ĳ�����Դϴ�.";
                                }
                            }
                        }
                        //Debug.Log(CharNum + "���⼭ �׽�Ʈ �� ��");
                        
                        // �κ� ������ Ƚ���� ������Ű���� ������ �Լ�
                        //creSlots[i].item = null;
                        //creSlots[i].item.itemImage = null;
                    }
                }
                else// �ռ� ��ῡ ���� ��ᰡ ������ ��
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
            Shtext.text = "��尡 �����մϴ�.";
        }
    }
    public void TestT()// �������� ���� �׽�Ʈ ��ư
    {
        CharCom[Char_int].Love = 1;
        CharCom[Char_int].Hunger = 1;
        CharCom[Char_int].Stature = 1;
        CharManager.State(Char_int);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�
        for (int i = 0; i < charNumbers.Count; i++)
        {
            if (charNumbers[i] == Char_int)// ���� ������ ��
            {
                LoveV[i] += 1f;
                HungerV[i] += 1f;
                StatureV[i] += 1f;
            }
        }
        Val(Char_int);
    }
    public void CharExit()// ĳ���� �������� ��ư
    {
        CharEndBgm.Play();

        CharCom[Char_int].Love = 0;
        CharCom[Char_int].Hunger = 0;
        CharCom[Char_int].Stature = 0;// ĳ���� �������� ���� ���� �ʱ�ȭ�����ֱ�

        //CharCom[Char_int].item_ = false;// ĳ���� ����
        CharList[Char_int].SetActive(false);// ĳ���� ���� ȭ�鿡�� �������ֱ�
        CharNames.Remove(CharCom[Char_int].itemNumber);// ������� ����Ʈ���� �������ֱ�
        CharManager.CharNameState.SetActive(false);// ĳ���Ͱ� ������ �� â �ݾƳ���

        for (int i = 0; i < charNumbers.Count; i++)
        {
            //Debug.Log(LoveV[i] + "Ȥ��?");
            if (i == Char_int)
            {
                LoveV[i] = 2f;
                HungerV[i] = 2f;
                StatureV[i] = 2f;
                //Debug.Log(i + "�� ������ �³�");
                //Debug.Log(LoveV[i] + "??????");
                _Char[i] = false;
                LoveV.Remove(2f);
                HungerV.Remove(2f);
                StatureV.Remove(2f);
                _Char.Remove(false);
                //Debug.Log(LoveV + " ���� ����Ʈ Ȯ��");
                //CharManager.CharNameState.SetActive(false);// ĳ���Ͱ� ������ �� â �ݾƳ���
                //Debug.Log("��� ����?");
                break;
            }
            //if (LoveV[i] == 1 || HungerV[i] == 1 || StatureV[i] == 1)
            //{
            //    
            //    
            //}
        }
        charNumbers.Remove(CharCom[Char_int].Count);
        PPPPanel.SetActive(false);// ĳ���� ������Ʈ â �ǳ� ���ֱ�
        game.Panel_Exit();

        if (Char_Cre)
        {
            if (charNumbers.Count == 0)
            {
                Char_int = 0;
                Char_Cre = false;
                Char_bool = 0;
                CharManager.CharNameState.SetActive(false);// ĳ���Ͱ� ������ �� â �ݾƳ���
                LoveV.Clear();
                HungerV.Clear();
                StatureV.Clear();
                _Char.Clear();
            }
            else
            {
                for (int i = 0; i < charNumbers.Count; i++)// ������ �� ù ĳ���� �����ֱ�
                {
                    if (_Char[i] == true)
                    {
                        Char_int = charNumbers[i];
                        CharList[Char_int].gameObject.SetActive(true);// ���� ���� ĳ���� �����ֱ�
                        Val(Char_int);
                        //Debug.Log("������ ����?");
                        CharManager.State(Char_int);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�
                        CharManager.CharNameState.SetActive(true);// ĳ���Ͱ� ������ �� â �ݾƳ���
                                                                  //CharStateNAme.SetActive(false);// ĳ���� �̸� �����ֱ�
                        break;
                    }
                    //else
                    //{
                    //    if (i == charNumbers.Count - 1)
                    //    {
                    //        Debug.Log("������ ����ǳ�?");
                    //        
                    //    }
                    //}
                }
            }
            
        }

        //for (int i = 0; i < CharNames.Count; i++)// ������ �� ù ĳ���� �����ֱ�
        //{
        //    if (_Char[i] == true)// ������� ���� ���� �ִٸ�
        //    {
        //        CharList[i].gameObject.SetActive(true);// ���� ���� ĳ���� �����ֱ�
        //        //Char_int = CharCom[i].Count;// �ٽ� ���� ��ȣ �����ֱ�
        //        //CharManager.State(Char_int);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�
        //
        //        //itemList.item[0].Start = Char_int;// ���������� ���� �����غ���
        //        //CharStateNAme.SetActive(false);// ĳ���� �̸� �����ֱ�
        //        Debug.Log("ĳ���� �̸�: " + CharCom[i].itemName);
        //        Debug.Log(i + "����");
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
        Shortage_Panel.SetActive(false);// ��ᰡ �����ϴ� ����
    }
    public void CCCFalse()
    {
        ComFalse.Play();
        imageSet = true;
        Shtext.text = "��ᰡ �����մϴ�";
        Shortage_Panel.SetActive(true);// ��ᰡ �����ϴ� ����
        Time.timeScale = 1;
        Invoke("SetSh", 1f);// 1�� �� ������ �˸� ���ֱ�
    }
    public void SaveSave()
    {
        //Debug.Log(CharNames[0] + "ȣ����..?");
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
        //Debug.Log(saveSynData.Char_Cre + "ĳ���� ���� ����");
        //Debug.Log(saveSynData.Char_int + "ĳ���� ��ȣ ����");
    }
    public void SynSaveData(SaveSynData data)
    {// ������ ���� �Լ�
        // ������ ��ü�� ���̽� ���ڿ��� ��ȯ
        string json = JsonUtility.ToJson(data, true);// Ʈ��� ������ �ɼ�(�������� ���� ��)

        // ���Ͽ� ���̽� ���ڿ� ����
        File.WriteAllText(filePath, json);

        //Debug.Log("ĳ������ ���̽� ������ �����" + filePath);

    }
    public SaveSynData SynLoadData()
    {
        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
        {
            string json = File.ReadAllText(filePath);// ���Ͽ��� ���̽� ���ڿ� �б�

            SaveSynData data = JsonUtility.FromJson<SaveSynData>(json);// ���̽� ���ڿ��� ��ȯ

            //Debug.Log("���̽� �����Ͱ� �ε�� " + filePath);

            Char_int = data.Char_int;// ���� ĳ������ ��ȣ ��Ÿ����!
            Char_Cre = data.Char_Cre;// ���� ĳ���Ͱ� �ϳ� �̻� ���� ��
            CharNames = data.CharNames;// ������� ����� �� ĳ���� �����ϱ� ����Ʈ
            charNumbers = data.charNumbers;// ĳ���� ��ȣ�� ���� ������ �ѱ�� ����Ʈ
            CharManager.State(Char_int);// ĳ������ ���¸� ��Ÿ���� ���� ĳ������ ��ȣ �Ѱ��ֱ�
            Char_bool = data.Char_bool;// ȭ�鿡 ĳ���Ͱ� �̹� �����Ǿ� �ִ��� Ȯ���� �Լ�
            GameGold = data.GameGold;

            LoveV = data.LoveV;
            HungerV = data.HungerV;
            StatureV = data.StatureV;
            _Char = data._Char;

            RearTime = data.RearTime;
            RearTime += LastTimer;
            //Debug.Log(Char_int + " ĳ���� ��ȣ �ε�");
            //Debug.Log(Char_Cre + " ĳ���� ���� �ε�");

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

            //for (int i = 0; i < CharCom.Length; i++)// �ϼ�ǰ ĳ���� �˻��ϱ�
            //{
            //    for (int j = 0; j < CharNames.Count; j++)// ��¥ ����� �� ĳ���� �˻��ϱ�
            //    {
            //        if (CharCom[i].itemNumber == CharNames[j])// ��ϰ� ��ġ�ϴ� ���� �ִٸ�
            //        {// ����Ǿ��ִ� ������ ����� ���嵵 ��� �־��ֱ�
            //            CharCom[i].Love = data.LoveV[j];
            //            CharCom[i].Hunger = data.HungerV[j];
            //            CharCom[i].Stature = data.StatureV[j];
            //            CharCom[i].item_ = data._Char[j];
            //        }
            //    }
            //}

            if (Char_Cre)// ���� ĳ���Ͱ� �����Ǿ� �ִٸ�
            {
                CharList[Char_int].gameObject.SetActive(true);// ��ġ�ϴ� �� ��Ÿ����/
                Val(Char_int);
                CharManager.State(Char_int);
                CharManager.CharNameState.SetActive(true);// ĳ���� â �������

            }
            return data;
        }
        else
        {
            //Debug.Log("����� ���̽� ������ ����.");
            return null;
        }
    }
    public void SetColor(float _alpha)// �̹��� ���� ���İ�, ���� ���� ���� �Լ�
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
            if (charNumbers[i] == Char_int)// ���� ������ ��
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
            if (charNumbers[i] == Char_int)// ���� ������ ��
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
                //Debug.Log(a + "���� Ȯ���� �ʿ�..");
                CharManager.LoveS.value = LoveV[i];
                //Debug.Log(LoveV[i] + "��ġ Ȯ���� �ʿ���!");
                CharManager.HungerS.value = HungerV[i];
                CharManager.StatureS.value = StatureV[i];
            }
        }
            
    }

    public void Imaa()// ��� 3������ ��� ���� �� �߾��� ĳ���� �ִϸ��̼��� ���ٰ���
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
