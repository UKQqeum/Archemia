using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class CreSlot : MonoBehaviour// �ռ��� ������ 4���� ���ĭ
{
    [SerializeField]
    public Item item;

    [SerializeField]
    private SynthesisMain main;

    [SerializeField]
    public Image image;

    [SerializeField]
    public Image colorI;

    public GameObject BGItemImage;// �ռ� �������� ������ �� ���

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
    public void AddCreItem(Item _item, int _count = 1)// �ռ� ���ĭ�� �� ��
    {
        item = _item;
        TextSlot();
    }
    public void TextSlot()// �̹����� �ְ� + �ؽ�Ʈ�� ������ �Լ�
    {
        b1 = true;
        BGItemImage.SetActive(true);
        //image.gameObject.SetActive(true);
        image.sprite = item.itemImage;
    }
    public void TSlot()// �̹����� ���� +�ؽ�Ʈ �Լ��� �ٽ� ������ �Լ�
    {
        item = null;
        image.sprite = null;
        BGItemImage.SetActive(false);
        main.aaab = true;
        //image.gameObject.SetActive(false);
    }
    public void AB()// �ռ� ��ư ��� �ִϸ��̼�
    {
        a1 += aa;
        colorI.fillAmount = a1;
        if (a1 >= 1)
        {
            a1 = 0;
        }
    }
}
