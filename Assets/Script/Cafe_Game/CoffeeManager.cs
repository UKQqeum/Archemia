using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;

public class CoffeeManager : MonoBehaviour
{
    [SerializeField]
    private CafeManager cafeManager;

    public Item[] Coffees_;// Ŀ�� ����� �� ����Ʈ
    public Item[] CoffeeMatter;// Ŀ�� ��� ����Ʈ Coffee_Matter
    public Item Co1;
    public Item Co2;
    public Item Co3;

    public int RandomNumber1;// ù ��° ������ �������� ���� ����
    public int RandomNumber2;
    public int RandomNumber3;
    public int RandomCoffee;// �������� ���� Ŀ��

    public Image[] MainCoffee;// �������� ���� Ŀ���� �̹��� ����Ʈ
    public TextMeshProUGUI[] CoffeeText;// Ŀ�� �ĺ� ��ȣ�� ���� �ؽ�Ʈ ����Ʈ

    public TextMeshProUGUI CoffeeTestTest;// Ŀ�� 
    public string Test;// ���� ����� ������ �ĺ� ��ȣ

    public int CoffeesScore = 0;// ������ ���� ��Ȳ
    public TextMeshProUGUI CoffeeScore;// ������ ���� �ؽ�Ʈ

    public Button[] buttons;// Ŀ�� ��� Ȱ��, ��Ȱ��ȭ�� ���� ��ư ����Ʈ. ���� ���� ���� �ϱ� ������

    public Image CoffeesMake;// ���� �÷��̾ ����� Ŀ�� �̹���
    public Image CoffeesMake2;// ���� �÷��̾ ����� Ŀ�� �̹���2 ����Ƽ�� ����

    public Image Red;// ������ ���� ǥ�� �̹���
    public Color color;// ������ ���� ǥ�ø� ����
    float st = 0;// �������� ����������
    float st2 = 0;// �������� ����������

    public Sprite[] images;// �̹��� ����Ʈ ��Ḧ ����

    public AudioSource CoffeeCle;// Ŀ�� ���� �Ҹ�

    public AudioSource CupBgm;// �� �Ҹ�
    public AudioSource BaseBgm;// ���̽� �Ҹ�
    public AudioSource SyrupBgm;// �÷� �Ҹ�
    public AudioSource CoffeeBgm;// Ŀ�� ���� �Ҹ�

    // Start is called before the first frame update
    void Start()
    {
        RandomCoffees();
        RandomCoffee2();
        RandomCoffee3();
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;// �� ���� ��� ��ư ��Ȱ��ȭ
            Red.gameObject.SetActive(true);
        }
        //color.a = 1f;
        //color2.a = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cafeManager.TestTimer <= 0)
        {
            bool cup = Test.Contains("0");// �� ������ 0�� ����ִ��� ������ Ȯ��
            if (cup)// ���� ������� �� Test == "0"
            {
                Red.gameObject.SetActive(false);
                buttons[0].interactable = false;// �� ��Ȱ��ȭ
                for (int i = 1; i < 5; i++)
                {
                    buttons[i].interactable = true;// ��� Ȱ��ȭ
                }
            }
            else
            {
                Red.gameObject.SetActive(true);
                CupRed();
            }
        }
    }
    public void RandomCoffees()// Ŀ�ǰ� �������� ������ �Լ�
    {
        RandomNumber1 = Random.Range(0, Coffees_.Length);
        Co1 = Coffees_[RandomNumber1];
        MainCoffee[0].sprite = Coffees_[RandomNumber1].itemImage;
        CoffeeText[0].text =Co1.itemNumber.ToString();
    }
    public void RandomCoffee2()
    {
        RandomNumber2 = Random.Range(0, Coffees_.Length);
        Co2 = Coffees_[RandomNumber2];
        MainCoffee[1].sprite = Coffees_[RandomNumber2].itemImage;
        CoffeeText[1].text =Co2.itemNumber.ToString();// "Ŀ�� �ĺ� ��ȣ: " + 
    }
    public void RandomCoffee3()
    {
        RandomNumber3 = Random.Range(0, Coffees_.Length);
        Co3 = Coffees_[RandomNumber3];
        MainCoffee[2].sprite = Coffees_[RandomNumber3].itemImage;
        CoffeeText[2].text = Co3.itemNumber.ToString();// "Ŀ�� �ĺ� ��ȣ: " + 
    }
    public void Submit()// ���� ���Ḧ �����ϴ� ��ư �Լ�
    {
        SyrupBgm.Stop();
        CoffeeCle.Play();
        // ���Ḧ ���������� �ź��� �ٽ� �����ؾ� ��
        buttons[0].interactable = true;// �� Ȱ��ȭ
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;// �� ���� ��� ��ư ��Ȱ��ȭ
        }

        if ((Test == Co1.itemNumber &&  Test == Co2.itemNumber) || (Test == Co1.itemNumber &&
            Test == Co3.itemNumber) || (Test == Co2.itemNumber && Test == Co3.itemNumber))// Ŀ�ǰ� ������ ��
        {
            if (Co1.itemNumber == Co2.itemNumber)// ������ ��� ���� �� �� �� �ð��� ���� ������ ������� ��
            {
                if (cafeManager.timer1 <= cafeManager.timer2)// ù��° ������ ���� �ð��� �� ���� ��
                {// ù ��° ������ ���� ���������
                    CompleteCoffee();
                    RandomCoffees();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
                    cafeManager.timer1 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
                }
                else
                {
                    CompleteCoffee();
                    RandomCoffee2();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
                    cafeManager.timer2 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
                }
            }
            else if (Co1.itemNumber == Co3.itemNumber)
            {
                if (cafeManager.timer1 <= cafeManager.timer3)// ù��° ������ ���� �ð��� �� ���� ��
                {// ù ��° ������ ���� ���������
                    CompleteCoffee();
                    RandomCoffees();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
                    cafeManager.timer1 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
                }
                else
                {
                    CompleteCoffee();
                    RandomCoffee3();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
                    cafeManager.timer3 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
                }
            }
            else if (Co2.itemNumber == Co3.itemNumber)
            {
                if (cafeManager.timer2 <= cafeManager.timer3)// ù��° ������ ���� �ð��� �� ���� ��
                {// ù ��° ������ ���� ���������
                    CompleteCoffee();
                    RandomCoffee2();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
                    cafeManager.timer2 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
                }
                else
                {
                    CompleteCoffee();
                    RandomCoffee3();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
                    cafeManager.timer3 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
                }
            }
        }
        if (Test == Co1.itemNumber)// ���� ������ ��ȣ�� ������ ��ȣ�� ���ٸ�
        {
            CompleteCoffee();
            RandomCoffees();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
            cafeManager.timer1 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
        }
        if (Test == Co2.itemNumber)
        {
            CompleteCoffee();
            RandomCoffee2();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
            cafeManager.timer2 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
        }
        if (Test == Co3.itemNumber)
        {
            CompleteCoffee();
            RandomCoffee3();// ���߾����� Ŀ�� �ٽ� �������� ��Ÿ����
            cafeManager.timer3 = 1;// Ÿ�̸� �ð� �ٽ� ���߱�
        }
        //else if (Test == Co1.itemNumber || Test == Co2.itemNumber)// Ŀ�Ǹ� Ʋ���� ������� ��
        //{
        //    FailCoffee();
        //}
        Test = "";// ���� ����Ʈ ����
        CoffeesMake.sprite = images[0];// �̹��� ����ֱ�
        CoffeesMake2.sprite = images[0];// �̹��� ����ֱ�
        ButtonFalse();
        CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + Test.ToString();
    }
    public void CompleteCoffee()// Ŀ�Ǹ���⸦ �������� ��
    {
        CoffeesScore += 50;// ���߾����� ���� �÷��ֱ�
        CoffeeScore.text = CoffeesScore.ToString();// �ø� ���� �ݿ����ֱ�
        Test = "";// ���� ����Ʈ ����
        //Debug.Log(Test + "����Ʈ �� �������?");
        CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + Test.ToString();
    }
    //public void FailCoffee()// Ŀ�Ǹ� Ʋ���� ������� ��
    //{
    //    CoffeesScore -= 25;// ���� ���
    //    CoffeeScore.text = CoffeesScore.ToString();
    //    CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + Test.ToString();// ����� Ŀ�ǰ� ��������� Ȯ��
    //}

    public void MakeClear()// ����� Ŀ�� ����ϴ� �Լ�
    {
        Test = "";// ���� ����Ʈ ����
        CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + Test.ToString();

        // Ŀ�Ǹ� ��������� ����Ʈ�� ��� ���� �ź��� �ٽ� �����ؾ� ��
        buttons[0].interactable = true;// �� Ȱ��ȭ
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;// �� ���� ��� ��ư ��Ȱ��ȭ
        }
    }
    public void CupRed()// ���� ���������� ���� ǥ���ϱ�
    {
        float tt = Time.deltaTime;
        
        if (st < 0.9f)
        {
            st += tt;
            color.a = st;
            Red.color = color;
            //Debug.Log(st + "���� ����");
            st2 = 0.9f;
        }
        else if(st >= 0.9f && st2 <= 0.9f)
        {
            st2 -= tt;
            color.a = st2;
            Red.color = color;
            if (st2 < 0.1f)
                st = 0;
        }
    }
    public void ButtonFalse()
    {
        for (int i = 5; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }
    public void ButtonTrue()
    {
        for (int i = 5; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
