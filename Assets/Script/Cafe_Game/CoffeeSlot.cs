using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeSlot : MonoBehaviour
{
    [SerializeField]
    private CoffeeManager coffeeManager;

    private Item item;// ������
    private Image itemImage;// ������ �̹���
    private string itemNumber;// ������ �ĺ� ��ȣ

    public float time;
    float TT = 0;
    bool T;

    void Update()
    {
        time = Time.deltaTime;
        if (T)
        {
            TT += time;
            coffeeManager.CoffeesMake2.fillAmount = TT;// �̹��� �ؿ������� õõ�� �����ֱ�
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
    public void CupUse()// ��
    {
        coffeeManager.CupBgm.Play();
        coffeeManager.Test += itemNumber;
        coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
        coffeeManager.CoffeesMake.sprite = item.itemImage;
        coffeeManager.CoffeesMake2.sprite = item.itemImage;
        coffeeManager.ButtonFalse();// �� ���� �� �÷� ��Ȱ��ȭ
        //coffeeManager.buttons[6].interactable = false;
        //coffeeManager.buttons[7].interactable = false;
        //coffeeManager.buttons[8].interactable = false;
    }
    public void IteUse()// ���̽�
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
            coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
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
        //        coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //    else if (coffeeManager.Test.Contains("05"))
        //    {
        //        coffeeManager.Test = "0";
        //        coffeeManager.Test += itemNumber;
        //        coffeeManager.Test += "5";
        //        coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //    else if (coffeeManager.Test.Contains("06"))
        //    {
        //        coffeeManager.Test = "0";
        //        coffeeManager.Test += itemNumber;
        //        coffeeManager.Test += "6";
        //        coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //    else if (coffeeManager.Test.Contains("07"))
        //    {
        //        coffeeManager.Test = "0";
        //        coffeeManager.Test += itemNumber;
        //        coffeeManager.Test += "7";
        //        coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
        //        Com();
        //    }
        //}
        
        coffeeManager.ButtonTrue();// ���̽� ���� �� �÷� Ȱ��ȭ
    }
    public void ButtomItemUse()// �÷�
    {
        coffeeManager.BaseBgm.Stop();
        TT = 0;
        coffeeManager.SyrupBgm.Play();
        //coffeeManager.ButtonFalse();
        if (coffeeManager.Test.Contains("01"))// �ʷϻ� ���̽�
        {
            coffeeManager.CoffeesMake.sprite = coffeeManager.images[2];
            coffeeManager.Test = "01";
            coffeeManager.Test += itemNumber;
            //coffeeManager.CoffeesMake2.sprite = item.itemImage;
            coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
            //coffeeManager.CoffeesMake.sprite = item.detailed_image; 
            Com();
        }
        if (coffeeManager.Test.Contains("02"))// ���� ���̽�
        {
            coffeeManager.CoffeesMake.sprite = coffeeManager.images[3];
            coffeeManager.Test = "02";
            coffeeManager.Test += itemNumber;
            //coffeeManager.CoffeesMake2.sprite = item.itemImage;
            coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
            //coffeeManager.CoffeesMake.sprite = item.itemImage;
            Com();
        }
        //if (coffeeManager.Test == "0")// �÷��� ���� �������� ��
        //{
        //    MMM();// �÷� ���� �ִ� �Լ�
        //}
        //else
        //{
        //    Com();// �÷� ���� �ִ� �Լ�
        //}
    }
    public void Com()// ���� �ϼ� ���� �ִ� �Լ�
    {
        for (int i = 0; i < coffeeManager.Coffees_.Length; i++)
        {
            if (coffeeManager.Test == coffeeManager.Coffees_[i].itemNumber)// �Ϸù�ȣ�� ��ġ�� ��
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
    //    //    MMM();// �÷� ���� �ִ� �Լ�
    //    //}
    //    //if (coffeeManager.Test == "05")
    //    //{
    //    //    MMM();// �÷� ���� �ִ� �Լ�
    //    //}
    //    //if (coffeeManager.Test == "06")
    //    //{
    //    //    MMM();// �÷� ���� �ִ� �Լ�
    //    //}
    //    //if (coffeeManager.Test == "07")
    //    //{
    //    //    MMM();// �÷� ���� �ִ� �Լ�
    //    //}
    //}
    //public void MMM()// �÷� ���� �ִ� �Լ�
    //{
    //    coffeeManager.Test = "0";
    //    coffeeManager.Test += itemNumber;
    //    coffeeManager.CoffeeTestTest.text = "���� �ĺ� ��ȣ: " + coffeeManager.Test.ToString();
    //    int a = item.Count;
    //    coffeeManager.CoffeesMake.sprite = coffeeManager.images[a];
    //}
}
