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

    public Item[] Coffees_;// 커피 목록이 들어갈 리스트
    public Item[] CoffeeMatter;// 커피 재료 리스트 Coffee_Matter
    public Item Co1;
    public Item Co2;
    public Item Co3;

    public int RandomNumber1;// 첫 번째 오더의 랜덤으로 나올 숫자
    public int RandomNumber2;
    public int RandomNumber3;
    public int RandomCoffee;// 랜덤으로 나올 커피

    public Image[] MainCoffee;// 랜덤으로 나올 커피의 이미지 리스트
    public TextMeshProUGUI[] CoffeeText;// 커피 식별 번호가 나올 텍스트 리스트

    public TextMeshProUGUI CoffeeTestTest;// 커피 
    public string Test;// 현재 만드는 음료의 식별 번호

    public int CoffeesScore = 0;// 게임의 점수 현황
    public TextMeshProUGUI CoffeeScore;// 게임의 점수 텍스트

    public Button[] buttons;// 커피 재료 활성, 비활성화를 위한 버튼 리스트. 컵을 먼저 골라야 하기 때문에

    public Image CoffeesMake;// 현재 플레이어가 만드는 커피 이미지
    public Image CoffeesMake2;// 현재 플레이어가 만드는 커피 이미지2 퀄리티를 위해

    public Image Red;// 붉은색 강조 표시 이미지
    public Color color;// 붉은색 강조 표시를 위해
    float st = 0;// 붉은색이 진해지도록
    float st2 = 0;// 붉은색이 옅어지도록

    public Sprite[] images;// 이미지 리스트 재료를 위해

    public AudioSource CoffeeCle;// 커피 제출 소리

    public AudioSource CupBgm;// 컵 소리
    public AudioSource BaseBgm;// 베이스 소리
    public AudioSource SyrupBgm;// 시럽 소리
    public AudioSource CoffeeBgm;// 커피 제출 소리

    // Start is called before the first frame update
    void Start()
    {
        RandomCoffees();
        RandomCoffee2();
        RandomCoffee3();
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;// 컵 제외 모든 버튼 비활성화
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
            bool cup = Test.Contains("0");// 이 변수에 0이 들어있는지 없는지 확인
            if (cup)// 컵을 사용했을 때 Test == "0"
            {
                Red.gameObject.SetActive(false);
                buttons[0].interactable = false;// 컵 비활성화
                for (int i = 1; i < 5; i++)
                {
                    buttons[i].interactable = true;// 재료 활성화
                }
            }
            else
            {
                Red.gameObject.SetActive(true);
                CupRed();
            }
        }
    }
    public void RandomCoffees()// 커피가 랜덤으로 나오는 함수
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
        CoffeeText[1].text =Co2.itemNumber.ToString();// "커피 식별 번호: " + 
    }
    public void RandomCoffee3()
    {
        RandomNumber3 = Random.Range(0, Coffees_.Length);
        Co3 = Coffees_[RandomNumber3];
        MainCoffee[2].sprite = Coffees_[RandomNumber3].itemImage;
        CoffeeText[2].text = Co3.itemNumber.ToString();// "커피 식별 번호: " + 
    }
    public void Submit()// 만든 음료를 제출하는 버튼 함수
    {
        SyrupBgm.Stop();
        CoffeeCle.Play();
        // 음료를 제출했으니 컵부터 다시 시작해야 함
        buttons[0].interactable = true;// 컵 활성화
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;// 컵 제외 모든 버튼 비활성화
        }

        if ((Test == Co1.itemNumber &&  Test == Co2.itemNumber) || (Test == Co1.itemNumber &&
            Test == Co3.itemNumber) || (Test == Co2.itemNumber && Test == Co3.itemNumber))// 커피가 정답일 때
        {
            if (Co1.itemNumber == Co2.itemNumber)// 변수가 모두 같을 때 좀 더 시간이 적은 오더가 사라져야 함
            {
                if (cafeManager.timer1 <= cafeManager.timer2)// 첫번째 오더가 제한 시간이 더 적을 때
                {// 첫 번째 오더가 먼저 사라지도록
                    CompleteCoffee();
                    RandomCoffees();// 맞추었으니 커피 다시 랜덤으로 나타내기
                    cafeManager.timer1 = 1;// 타이머 시간 다시 맞추기
                }
                else
                {
                    CompleteCoffee();
                    RandomCoffee2();// 맞추었으니 커피 다시 랜덤으로 나타내기
                    cafeManager.timer2 = 1;// 타이머 시간 다시 맞추기
                }
            }
            else if (Co1.itemNumber == Co3.itemNumber)
            {
                if (cafeManager.timer1 <= cafeManager.timer3)// 첫번째 오더가 제한 시간이 더 적을 때
                {// 첫 번째 오더가 먼저 사라지도록
                    CompleteCoffee();
                    RandomCoffees();// 맞추었으니 커피 다시 랜덤으로 나타내기
                    cafeManager.timer1 = 1;// 타이머 시간 다시 맞추기
                }
                else
                {
                    CompleteCoffee();
                    RandomCoffee3();// 맞추었으니 커피 다시 랜덤으로 나타내기
                    cafeManager.timer3 = 1;// 타이머 시간 다시 맞추기
                }
            }
            else if (Co2.itemNumber == Co3.itemNumber)
            {
                if (cafeManager.timer2 <= cafeManager.timer3)// 첫번째 오더가 제한 시간이 더 적을 때
                {// 첫 번째 오더가 먼저 사라지도록
                    CompleteCoffee();
                    RandomCoffee2();// 맞추었으니 커피 다시 랜덤으로 나타내기
                    cafeManager.timer2 = 1;// 타이머 시간 다시 맞추기
                }
                else
                {
                    CompleteCoffee();
                    RandomCoffee3();// 맞추었으니 커피 다시 랜덤으로 나타내기
                    cafeManager.timer3 = 1;// 타이머 시간 다시 맞추기
                }
            }
        }
        if (Test == Co1.itemNumber)// 만들어낸 음료의 번호와 문제의 번호가 같다면
        {
            CompleteCoffee();
            RandomCoffees();// 맞추었으니 커피 다시 랜덤으로 나타내기
            cafeManager.timer1 = 1;// 타이머 시간 다시 맞추기
        }
        if (Test == Co2.itemNumber)
        {
            CompleteCoffee();
            RandomCoffee2();// 맞추었으니 커피 다시 랜덤으로 나타내기
            cafeManager.timer2 = 1;// 타이머 시간 다시 맞추기
        }
        if (Test == Co3.itemNumber)
        {
            CompleteCoffee();
            RandomCoffee3();// 맞추었으니 커피 다시 랜덤으로 나타내기
            cafeManager.timer3 = 1;// 타이머 시간 다시 맞추기
        }
        //else if (Test == Co1.itemNumber || Test == Co2.itemNumber)// 커피를 틀리게 만들었을 때
        //{
        //    FailCoffee();
        //}
        Test = "";// 현재 리스트 비우기
        CoffeesMake.sprite = images[0];// 이미지 비워주기
        CoffeesMake2.sprite = images[0];// 이미지 비워주기
        ButtonFalse();
        CoffeeTestTest.text = "현재 식별 번호: " + Test.ToString();
    }
    public void CompleteCoffee()// 커피만들기를 성공했을 때
    {
        CoffeesScore += 50;// 맞추었으니 점수 올려주기
        CoffeeScore.text = CoffeesScore.ToString();// 올린 점수 반영해주기
        Test = "";// 현재 리스트 비우기
        //Debug.Log(Test + "리스트 잘 비워졌나?");
        CoffeeTestTest.text = "현재 식별 번호: " + Test.ToString();
    }
    //public void FailCoffee()// 커피를 틀리게 만들었을 때
    //{
    //    CoffeesScore -= 25;// 점수 깎기
    //    CoffeeScore.text = CoffeesScore.ToString();
    //    CoffeeTestTest.text = "현재 식별 번호: " + Test.ToString();// 만들던 커피가 사라졌는지 확인
    //}

    public void MakeClear()// 만들던 커피 폐기하는 함수
    {
        Test = "";// 현재 리스트 비우기
        CoffeeTestTest.text = "현재 식별 번호: " + Test.ToString();

        // 커피를 폐기했으니 리스트도 모두 비우고 컵부터 다시 시작해야 함
        buttons[0].interactable = true;// 컵 활성화
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;// 컵 제외 모든 버튼 비활성화
        }
    }
    public void CupRed()// 컵을 붉은색으로 강조 표시하기
    {
        float tt = Time.deltaTime;
        
        if (st < 0.9f)
        {
            st += tt;
            color.a = st;
            Red.color = color;
            //Debug.Log(st + "색의 진함");
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
