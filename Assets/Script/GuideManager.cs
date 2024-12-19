using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

   public GameObject GuidePanel;// 가이드 판넬

    public GameObject[] GuideMenu;// 가이드 총합

    //public GameObject AdvenGPanel;// 탐사 판넬
    //public GameObject InvenGPanel;// 인벤 판넬
    //public GameObject ConGPanel;// 합성 판넬
    //public GameObject BookGPanel;// 도감 판넬
    //public GameObject CharGPanel;// 캐릭터 판넬
    //public GameObject MiniGameGPanel;// 미니게임 판넬
    //public GameObject FoodGPanel;// 음식 판넬

    //public GameObject LeftB;// 왼쪽 페이지로
    //public GameObject RightB;// 오른쪽 페이지로

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuidePanel.SetActive(false);
        //AdvenGPanel.SetActive(false);
        //InvenGPanel.SetActive(false);
        //ConGPanel.SetActive(false);
        //BookGPanel.SetActive(false);
        //CharGPanel.SetActive(false);
        //MiniGameGPanel.SetActive(false);
        //FoodGPanel.SetActive(false);
        //LeftB.SetActive(false);
        //RightB.SetActive(false);
    }

    public void OpenG()// 가이드 열기, 첫 페이지부터 보여주기
    {
        gameManager.Open();
        GuideMenu[0].SetActive(true);
        //GuideMenu[1].SetActive(true);
        GuidePanel.SetActive(true);
        //AdvenGPanel.SetActive(true);
    }
    public void AdvenOpen()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuideMenu[0].SetActive(true);
    }
    public void InvenOpen()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuideMenu[1].SetActive(true);
    }
    public void ConOpen()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuideMenu[2].SetActive(true);
    }
    public void BookOpen()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuideMenu[3].SetActive(true);
    }
    public void CharOpen()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuideMenu[4].SetActive(true);
    }
    public void MiniOpen()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuideMenu[5].SetActive(true);
    }
    public void FoodOpen()
    {
        for (int i = 0; i < GuideMenu.Length; i++)
        {
            GuideMenu[i].SetActive(false);
        }
        GuideMenu[6].SetActive(true);
    }
    //public void Left()
    //{
    //    switch (Page)
    //    {
    //        case 2:
    //            Page = 1;
    //            AdvenPanel.SetActive(true);
    //            InvenPanel.SetActive(false);
    //            LeftB.SetActive(false);
    //            break;
    //        case 3:
    //            Page = 2;
    //            InvenPanel.SetActive(true);
    //            ConPanel.SetActive(false);
    //            break;
    //        case 4:
    //            Page = 3;
    //            ConPanel.SetActive(true);
    //            BookPanel.SetActive(false);
    //            RightB.SetActive(true);
    //            break;
    //        default:
    //            break;
    //    }
    //}
    //public void Right()
    //{
    //    switch (Page)
    //    {
    //        case 1:
    //            LeftB.SetActive(true);
    //            Page = 2;
    //            AdvenPanel.SetActive(false);
    //            InvenPanel.SetActive(true);
    //            break;
    //        case 2:
    //            Page = 3;
    //            InvenPanel.SetActive(false);
    //            ConPanel.SetActive(true);
    //            break;
    //        case 3:
    //            Page = 4;
    //            RightB.SetActive(false);
    //            ConPanel.SetActive(false);
    //            BookPanel.SetActive(true);
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
