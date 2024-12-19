using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharStateManager : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject CharStatePanel;// ĳ������ ����â ��ü
    public Image CharImage;// ����â�� ���� ĳ���� �̹���

    public GameObject CharNameState;// ������ ĳ������ �̸��� ���¸� Ȯ���� �� �ֵ���

    [SerializeField]
    private MainToCafeManager mainToCafeManager;

    [SerializeField]
    private SynthesisMain SyMain;

    [SerializeField]
    private ItemList itemlist;

    [SerializeField]
    public Slider LoveS;// ���� �����̴�

    [SerializeField]
    public Slider HungerS;// ����� �����̴�

    [SerializeField]
    public Slider StatureS;// ���� �����̴�

    public GameObject EatPanel;// ĳ������ ���̸� �ִ� â

    public GameObject EatButton;// ���̱� ��ư

    public int CharNumber;

    public Button CharExit;// ĳ���� �������� ��ư
    public Image BBB;// ĳ���� �������ͽ� ��ư ���� �̹���. ó�� �Ẹ�� ���� ���������� ���� ǥ�õ� �̹���
    public Color color;

    public TextMeshProUGUI FoodT;// Ǫ�� �ؽ�Ʈ

    public AudioSource CharBgm;// �Ҹ� ���
    
    public Color a;

    public bool MiniGameBool;// �̴� ������ ������ Ȯ���ϱ�

    public TextMeshProUGUI CHname;
    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        CharStatePanel.SetActive(false);
        //CharNameState.SetActive(false);
        EatPanel.SetActive(false);
        EatButton.SetActive(false);

        LoveS.interactable = false;// �÷��̾ �����̴� �� �����̵��� �ϱ�
        HungerS.interactable = false;
        StatureS.interactable = false;
        color.a = 0f;
        //CharImage.sprite = Ccc.sprite;
    }

    public void StateChar()// ĳ���� ����â ����
    {
        CharBgm.Play();
        CharStatePanel.SetActive(true);
        gameManager.Open();
        BBB.color = color;
        //State(itemlist.item[0].Start);
        if (LoveS.value != 1 || HungerS.value != 1 || StatureS.value != 1)// ��� �������� ������ ���� ��
        {
            CharExit.interactable = false;
        }
        else
        {
            CharExit.interactable = true;
        }
    }
    public void CharHung()// ĳ���� ���� �ִ� â ����
    {
        CharBgm.Play();
        EatPanel.SetActive(true);
        EatButton.SetActive(false);
        gameManager.Open();
    }
    public void State(int a)// ĳ������ ���� ���¸� ��Ÿ���� ����
    {
        CharNumber = a;
        //Debug.Log(CharNumber);
        CharImage.sprite = SyMain.CharCom[CharNumber].itemImage;
        //LoveS.value = SyMain.CharCom[CharNumber].Love;
        //HungerS.value = SyMain.CharCom[CharNumber].Hunger;
        //StatureS.value = SyMain.CharCom[CharNumber].Stature;
        CHname.text = SyMain.CharCom[CharNumber].itemName;
        if (LoveS.value != 1 || HungerS.value != 1 || StatureS.value != 1)// ��� �������� ������ ���� ��
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
            //Debug.Log(HHH + "���嵵�� ����");
            SyMain.CharCom[CharNumber].Stature += HHH;// ���嵵 �����ֱ�
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
        SyMain.CharCom[CharNumber].Stature += 0.1f;// ���嵵 �����ֱ�
        StatureS.value = SyMain.CharCom[CharNumber].Stature;
        mainToCafeManager.GoCafe();
        SyMain.Go();
    }
    public void CharAExit()// �� Ű�� �� ĳ���� ��������
    {
        CharStatePanel.SetActive(false);// ĳ���� ����â �ݾƹ�����
        SyMain.CharCom[CharNumber].Love = 0;
        SyMain.CharCom[CharNumber].Hunger = 0;
        SyMain.CharCom[CharNumber].Stature = 0;// ĳ���� �������� ���� ���� �ʱ�ȭ�����ֱ�
        //Debug.Log(this.gameObject.name + "������Ʈ �̸� Ȯ��");
        
        SyMain.CharCom[CharNumber].item_ = false;// ĳ���� ����
        SyMain.CharStateRight();
    }
    public void FoodTF()
    {
        FoodT.gameObject.SetActive(false);
    }
}
