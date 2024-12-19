using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeSlot : MonoBehaviour
{
    [SerializeField]
    private CoffeeManager coffeeManager;

    private Item item;// 아이템
    private Image itemImage;// 아이템 이미지
    private string itemNumber;// 아이템 식별 번호

    public float time;
    float TT = 0;
    bool T;

    void Update()
    {
        time = Time.deltaTime;
        if (T)
        {
            TT += time;
            coffeeManager.CoffeesMake2.fillAmount = TT;// 이미지 밑에서부터 천천히 보여주기
            if (TT >= 1)
            {
                TT = 0;
                T = false;
            }
        }
        if (T == false)
        {
            TT = 0;
        }
    }

    // Start is called before the first frame update
    public void AddItem(Item _item)
    {
        item = _item;
        //itemImage.sprite = _item.itemImage;
        itemNumber = _item.itemNumber;
    }
    public void CupUse()// 컵
    {
        coffeeManager.CupBgm.Play();
        coffeeManager.Test += itemNumber;
        coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
        coffeeManager.CoffeesMake.sprite = item.itemImage;
        coffeeManager.CoffeesMake2.sprite = item.itemImage;
        coffeeManager.ButtonFalse();// 컵 선택 후 시럽 비활성화
        //coffeeManager.buttons[6].interactable = false;
        //coffeeManager.buttons[7].interactable = false;
        //coffeeManager.buttons[8].interactable = false;
    }
    public void IteUse()// 베이스
    {
        coffeeManager.BaseBgm.Play();
        //miniRiddle.CoffeesNumber.Add(itemNumber);
        if ((coffeeManager.Test == "0") || (coffeeManager.Test == "01") || (coffeeManager.Test == "02"))
        {
            coffeeManager.CoffeesMake.sprite = coffeeManager.images[1];
            coffeeManager.CoffeesMake2.sprite = item.itemImage;
            coffeeManager.Test = "0";
            coffeeManager.Test += itemNumber;
            T = true;
            coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
            if (TT >= 1)
            {
                coffeeManager.CoffeesMake.sprite = coffeeManager.CoffeesMake2.sprite;
            }
        }
        //else
        //{
        //    if (coffeeManager.Test.Contains("04"))
        //    {
        //        coffeeManager.Test = "0";
        //        coffeeManager.Test += itemNumber;
        //        coffeeManager.Test += "4";
        //        coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //    else if (coffeeManager.Test.Contains("05"))
        //    {
        //        coffeeManager.Test = "0";
        //        coffeeManager.Test += itemNumber;
        //        coffeeManager.Test += "5";
        //        coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //    else if (coffeeManager.Test.Contains("06"))
        //    {
        //        coffeeManager.Test = "0";
        //        coffeeManager.Test += itemNumber;
        //        coffeeManager.Test += "6";
        //        coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //    else if (coffeeManager.Test.Contains("07"))
        //    {
        //        coffeeManager.Test = "0";
        //        coffeeManager.Test += itemNumber;
        //        coffeeManager.Test += "7";
        //        coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //}
        
        coffeeManager.ButtonTrue();// 베이스 선택 후 시럽 활성화
    }
    public void ButtomItemUse()// 시럽
    {
        coffeeManager.BaseBgm.Stop();
        TT = 0;
        coffeeManager.SyrupBgm.Play();
        //coffeeManager.ButtonFalse();
        if (coffeeManager.Test.Contains("01"))// 초록색 베이스
        {
            coffeeManager.CoffeesMake.sprite = coffeeManager.images[2];
            coffeeManager.Test = "01";
            coffeeManager.Test += itemNumber;
            //coffeeManager.CoffeesMake2.sprite = item.itemImage;
            coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
            //coffeeManager.CoffeesMake.sprite = item.detailed_image; 
            Com();
        }
        if (coffeeManager.Test.Contains("02"))// 갈색 베이스
        {
            coffeeManager.CoffeesMake.sprite = coffeeManager.images[3];
            coffeeManager.Test = "02";
            coffeeManager.Test += itemNumber;
            //coffeeManager.CoffeesMake2.sprite = item.itemImage;
            coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
            //coffeeManager.CoffeesMake.sprite = item.itemImage;
            Com();
        }
        //if (coffeeManager.Test == "0")// 시럽을 먼저 선택했을 때
        //{
        //    MMM();// 시럽 먼저 넣는 함수
        //}
        //else
        //{
        //    Com();// 시럽 먼저 넣는 함수
        //}
    }
    public void Com()// 음료 완성 사진 넣는 함수
    {
        for (int i = 0; i < coffeeManager.Coffees_.Length; i++)
        {
            if (coffeeManager.Test == coffeeManager.Coffees_[i].itemNumber)// 일련번호가 일치할 때
            {
                T = true;
                coffeeManager.CoffeesMake2.sprite = coffeeManager.Coffees_[i].itemImage;
                
                if (TT >= 1)
                {
                    coffeeManager.CoffeesMake.sprite = coffeeManager.CoffeesMake2.sprite;
                }
                
            }
        }
    }
    //public void Ma()
    //{
    //    //if (coffeeManager.Test == "04")
    //    //{
    //    //    MMM();// 시럽 먼저 넣는 함수
    //    //}
    //    //if (coffeeManager.Test == "05")
    //    //{
    //    //    MMM();// 시럽 먼저 넣는 함수
    //    //}
    //    //if (coffeeManager.Test == "06")
    //    //{
    //    //    MMM();// 시럽 먼저 넣는 함수
    //    //}
    //    //if (coffeeManager.Test == "07")
    //    //{
    //    //    MMM();// 시럽 먼저 넣는 함수
    //    //}
    //}
    //public void MMM()// 시럽 먼저 넣는 함수
    //{
    //    coffeeManager.Test = "0";
    //    coffeeManager.Test += itemNumber;
    //    coffeeManager.CoffeeTestTest.text = "현재 식별 번호: " + coffeeManager.Test.ToString();
    //    int a = item.Count;
    //    coffeeManager.CoffeesMake.sprite = coffeeManager.images[a];
    //}
}
