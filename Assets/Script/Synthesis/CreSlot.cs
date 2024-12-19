using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class CreSlot : MonoBehaviour// 합성을 시작할 4개의 재료칸
{
    [SerializeField]
    public Item item;

    [SerializeField]
    private SynthesisMain main;

    [SerializeField]
    public Image image;

    [SerializeField]
    public Image colorI;

    public GameObject BGItemImage;// 합성 아이콘의 아이템 뒷 배경

    public float aa;
    public float a1 = 0;
    public bool b1;

    // Start is called before the first frame update
    void Update()
    {
        aa = Time.deltaTime;
        if (b1)
        {
            AB();
        }
    }
    public void AddCreItem(Item _item, int _count = 1)// 합성 재료칸에 들어갈 것
    {
        item = _item;
        TextSlot();
    }
    public void TextSlot()// 이미지를 넣고 + 텍스트를 지워줄 함수
    {
        b1 = true;
        BGItemImage.SetActive(true);
        //image.gameObject.SetActive(true);
        image.sprite = item.itemImage;
    }
    public void TSlot()// 이미지를 빼고 +텍스트 함수를 다시 보여줄 함수
    {
        item = null;
        image.sprite = null;
        BGItemImage.SetActive(false);
        main.aaab = true;
        //image.gameObject.SetActive(false);
    }
    public void AB()// 합성 버튼 재료 애니메이션
    {
        a1 += aa;
        colorI.fillAmount = a1;
        if (a1 >= 1)
        {
            a1 = 0;
        }
    }
}
