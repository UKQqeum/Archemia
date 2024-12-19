using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdvenSlot : MonoBehaviour
{
    [SerializeField]
    private AdventureManager adventureManager;

    public Item item;// ȹ���� ������
    public Image itemImage;

    public TextMeshProUGUI WhatAdven;// ��� �İ����� �� �� Ȯ���ϴ� ����
    public GameObject AllAdven;// ȸ���� �Ѱ� UI
    public GameObject GoAdven;// �� �İ����� ���� �� ���ΰ� ���� UI

    public GameObject AdvenGrayPanel;// �İ� �� �İ� ��� UI

    public TextMeshProUGUI AdvenTime;// ���� â���� �İ� �ð��� �󸶳� ���Ҵ��� ǥ�����ֱ�
    public GameObject ClearPanel;// �İ� �Ϸ� �� ��� ���� ��ư UI

    public GameObject DarkImage;// �İ����� �� �ٸ� �İ��� ���� �̹���

    // Start is called before the first frame update
    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        //Debug.Log("�İ��� �̸�: " +  item.itemName);
        //Debug.Log("���� �κ�: " + item.Start);
        //Debug.Log("�� �κ�: " + item.End);
        //Debug.Log("��� �İ� ��������: " + item.Count);
    }
    public void WhatAdvenGo()// �İ߰��⸦ �����ϴ� ��ư
    {
        adventureManager.AdvenHBgm.Play();
        AllAdven.SetActive(true);
        GoAdven.SetActive(true);// �� �İ����� ���� �� ���ΰ� ���� UI
        WhatAdven.text = item.itemStatusText.ToString();
        //Debug.Log("�İ��� �̸�: " + item.itemName);
        //Debug.Log("���� �κ�: " + item.Start);
        //Debug.Log("�� �κ�: " + item.End);
        //Debug.Log("��� �İ� ��������: " + item.Count);
        adventureManager.AdvenStart = item.Start;
        adventureManager.AdvenEnd = item.End;
        adventureManager.SlotsCount = item.Count;// �İ��� �������� ������ ī���͸� �޾ƿ;� ��
    }
    public void AdvenTimer()// �� �ð��̳� �� �� ���� �ǳ�
    {
        adventureManager.AdvenHBgm.Play();
        GoAdven.SetActive(false);// �İ����� �������� ���� �ݾƾ� ��
        adventureManager.TimerPanel();// �ð� ���ϴ� �ǳ� ����
    }
}
