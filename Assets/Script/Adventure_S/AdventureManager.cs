using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

[System.Serializable]
public class AdvenData
{
    public float AdventureTimer;// �İ��� ���ִ� �ð�
    public int AdvenGold;// �İ��� ���� ��� ���
    public bool AdventureBool;// �İ��� �� �ִ���

    public int AdvenStart;// �İ��� ���� ���� ����� ���� ���ۺκ�
    public int AdvenEnd;// �İ��� ���� ���� ����� ���� ������ �κ�
    public int AdvenItem;// �İ��� ���� ��Ḧ ��ŭ ���� ������

    public int SlotsCount;// ������ ��� �κ��� ȸ������ ������ �ϴ���
}
public class AdventureManager : MonoBehaviour
{

    public AdvenData advenData;
    [SerializeField]
    private GameObject AdvenContent;// �İ����� �θ� ���Ե�

    [SerializeField]
    private ItemList itemList;

    [SerializeField]
    private GameManager gameManager;

    private AdvenSlot[] Slots;// �İ��� ����

    public Item[] AdvenList;// �İ��� �������� ���� ����

    public GameObject GrayPanel;// �İ��� �̸� Ȯ��, �ð� Ȯ�� �Ѱ� �ǳ�
    public GameObject AdvenPanel;// �İ� �ǳ�
    public GameObject HourPanel;// �󸶳� �İ߳����� ���� UI

    public float AdventureTimer;// �İ��� �󸶳� �� ������
    public float RearTimer;// ������ �帣�� �ð�
    
    public int AdvenGold;// �İ��� ���� ���� ���
    public bool AdventureBool;// �İ��� ���ٸ� Ȯ���ؼ� ������Ʈ �Լ��� �ð��� ���־�� �ϱ⿡

    public int AdvenStart;// �İ��� ���� ���� ����� ���� ���ۺκ�
    public int AdvenEnd;// �İ��� ���� ���� ����� ���� ������ �κ�
    public int AdvenItem;// �İ��� ���� ��Ḧ ��ŭ ���� ������

    public int SlotsCount;// ������ ��� �κ��� ȸ������ ������ �ϴ���

    public TextMeshProUGUI TestAdven;

    public GameObject Advenalarm;// �İ� ���� �� �Ϸ� �˸�

    private string filePath;// ����

    public float LastTimer;// ���� ���� �� ����� �ð��� �޾Ƴ���

    public AudioSource Advenend;// �İ� ���� �Ҹ�
    public AudioSource AdvenHBgm;// �ð� �Ҹ�

    // Start is called before the first frame update
    void Start()
    {
        Slots = AdvenContent.GetComponentsInChildren<AdvenSlot>();

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].AddItem(AdvenList[i]);// �İ��� ���Կ� �İ� ���� �־��ֱ�
            //Debug.Log(AdvenList[i].name + "�İ� �� ���ֳ�?");
        }

        filePath = Application.persistentDataPath + "AdvenData.json";// ����

        //if (!Directory.Exists(filePath))// �ش� ��ΰ� �������� �ʴ´ٸ�
        //{
        //    Directory.CreateDirectory(filePath);// ���� ����(��� ������ֱ�)
        //
        //}

        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
        {
            LoadAdvenData();// ���ϰ�θ� ������ �� ������ �ε��ϱ�
        }
        else
        {
            SaveSave();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AdventureBool)// AdventureBool�� �������� ��
        {
            RearTimer = Time.deltaTime;
            AdventureTimer -= RearTimer;
            Slots[SlotsCount].AdvenTime.text = AdventureTimer.ToString("F0");
            TestAdven.text = AdventureTimer.ToString("F0");
            //AdvenTime.text = AdventureTimer.ToString("F0");
            if (AdventureTimer <= 0)
            {
                Advenend.Play();
                //AdvenGrayPanel.SetActive(false);// �İ�â ��� �����ϱ�
                Slots[SlotsCount].ClearPanel.SetActive(true);// �İ� �Ϸ� �� ��� ���� ��ư Ȱ��ȭ
                //ClearPanel.SetActive(true);// �İ� �Ϸ� �� ��� ���� ��ư Ȱ��ȭ
                Slots[SlotsCount].AdvenGrayPanel.SetActive(false);
                Slots[SlotsCount].DarkImage.SetActive(false);
                AdventureBool = false;
                Advenalarm.SetActive(true);
            }
        }
    }
    public void GoAdven()// �޴��� �İ� ��ư
    {
        AdvenPanel.SetActive(true);
        gameManager.OpenPanel.Play();
        gameManager.Open();
    }
    public void TimerPanel()
    {
        HourPanel.SetActive(true);// �ð� �ǳ� ���̵���
    }
    public void FiveSeconds()// 5�ʵ��� �İ߰���
    {
        AdvenHBgm.Play();
        AdventureTimer = 5;
        AdvenGold = 15;
        AdvenItem = 1;
        AdvenEnd -= 2;// �İ� �ð��� ��������� ��� ���� �÷��ֱ�
        AdvenTimerF();
    }
    public void TenSeconds()// 10�ʵ��� �İ߰���
    {
        AdvenHBgm.Play();
        AdventureTimer = 10;
        AdvenGold = 20;
        AdvenItem = 2;
        AdvenEnd -= 2;// �İ� �ð��� ��������� ��� ���� �÷��ֱ�
        AdvenTimerF();
    }
    public void ThirtySeconds()// 30�ʵ��� �İ߰���
    {
        AdvenHBgm.Play();
        AdventureTimer = 30;
        AdvenGold = 50;
        AdvenItem = 2;
        AdvenEnd -= 1;// �İ� �ð��� ��������� ��� ���� �÷��ֱ�
        AdvenTimerF();
    }
    public void SixtySeconds()// 60�ʵ��� �İ߰���
    {
        AdvenHBgm.Play();
        AdventureTimer = 60;
        AdvenGold = 100;
        AdvenItem = 3;
        AdvenTimerF();
    }
    public void AdvenTimerF()
    {
        GrayPanel.SetActive(false);// �ð��� ���õǾ����� ���ֱ�
        HourPanel.SetActive(false);// �ð��� ���õǾ����� ���ֱ�
        Slots[SlotsCount].AdvenGrayPanel.SetActive(true);
        AdvenFalse();
        //Debug.Log(SlotsCount + "���ڰ�..?");
        AdventureBool = true;
        Time.timeScale = 1;
    }
    public void AdventureClear()// ������ ���� ��ư
    {// ���ʴ�� ���� ���, ����� ���� ����, ������ ����, ��Ḧ �� �� �������� ����
        itemList.RandomAdven(AdvenGold, AdvenStart, AdvenEnd, AdvenItem);
        Advenalarm.SetActive(false);
        AdvenTrue();
    }
    public void EndAdven()// �İ� ��Ḧ ������ �� ȸ���� ���� ��ư �����ֱ�
    {
        Slots[SlotsCount].AdvenGrayPanel.SetActive(false);
        Slots[SlotsCount].ClearPanel.SetActive(false);// �İ� �Ϸ� �� ��� ���� ��ư Ȱ��ȭ
    }
    public void AdvenFalse()// �İ����� �� �ٸ� �İ��� ����
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i != SlotsCount)
            {
                Slots[i].DarkImage.SetActive(true);// �İ��� ���ִ� ��Ҹ���� ���ƹ�����
                //Debug.Log(i + "��° ���� ���ƹ�����");
            }
        }
    }
    public void AdvenTrue()// �İ��� �ٽ� Ǯ���ֱ�
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i != SlotsCount)
            {
                Slots[i].DarkImage.SetActive(false);
            }
        }
    }
    public void AdvenObjectFalse()
    {
        GrayPanel.SetActive(false);
        HourPanel.SetActive(false);
    }

    public void SaveSave()// �ʿ��� ������ �������ֱ�
    {
        advenData.AdventureTimer = AdventureTimer;// ���� �İ� �ð� �־��ֱ�
        advenData.AdvenGold = AdvenGold;// �İ����� ���� ��� �ٽ� �־��ֱ�
        advenData.AdventureBool = AdventureBool;// �İ��� ���ִٴ°� �ٽ� �־��ֱ�
        
        advenData.AdvenStart = AdvenStart;// ���� ������ ���� �κ� �־��ֱ�
        advenData.AdvenEnd = AdvenEnd;// ���� ������ ���κ� �־��ֱ�
        advenData.AdvenItem = AdvenItem;// ���� ������ ���� �־��ֱ�

        advenData.SlotsCount = SlotsCount;// ���� �İ��� ������ ����

        AdvenSaveData(advenData);
        //Debug.Log(advenData.AdventureTimer + "�İ� �ð� ����");
        //Debug.Log(advenData.AdvenGold + "�İ� ��� ����");
        //Debug.Log(advenData.AdventureBool + "�İ� ���� ����");

        //Debug.Log(advenData.AdvenStart + "���� ������ ���� �κ� ����");
        //Debug.Log(advenData.AdvenEnd + "���� ������ ���κ� ����");
        //Debug.Log(advenData.AdvenItem + "���� ������ ���� ����");

        //Debug.Log(advenData.SlotsCount + "���� �İ� ����");
    }
    public void AdvenSaveData(AdvenData data)
    {// ������ ���� �Լ�
        // ������ ��ü�� ���̽� ���ڿ��� ��ȯ
        string json = JsonUtility.ToJson(data, true);// Ʈ��� ������ �ɼ�(�������� ���� ��)

        // ���Ͽ� ���̽� ���ڿ� ����
        File.WriteAllText(filePath, json);

        //Debug.Log("���̽� ������ �����" + filePath);

    }
    public AdvenData LoadAdvenData()// ������ �ҷ�����
    {
        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
        {
            string json = File.ReadAllText(filePath);// ���Ͽ��� ���̽� ���ڿ� �б�

            AdvenData data = JsonUtility.FromJson<AdvenData>(json);// ���̽� ���ڿ��� ��ȯ

            //Debug.Log("���̽� �����Ͱ� �ε�� " + filePath);

            AdventureTimer = data.AdventureTimer;
            AdvenGold = data.AdvenGold;
            AdventureBool = data.AdventureBool;

            AdvenStart = data.AdvenStart;
            AdvenEnd = data.AdvenEnd;
            AdvenItem = data.AdvenItem;

            SlotsCount = data.SlotsCount;

            AdventureTimer -= LastTimer;
            //Debug.Log(AdventureTimer + "����� �� �ð���?");

            //Debug.Log(AdventureTimer + "�ε� �İ� �ð� ����");
            //Debug.Log(AdvenGold + "�ε� �İ� ��� ����");
            //Debug.Log(AdventureBool + "�ε� �İ� ���� ����");

            //Debug.Log(AdvenStart + "���� ������ ���� �κ� ����");
            //Debug.Log(AdvenEnd + "���� ������ ���κ� ����");
            //Debug.Log(AdvenItem + "���� ������ ���� ����");

            //Debug.Log(SlotsCount + "���� �İ� ����");
            if (AdventureBool)
            {
                AdvenFalse();// �İ����� �� �ٸ� �İ��� ����
                Slots[SlotsCount].AdvenGrayPanel.SetActive(true);
                //Debug.Log("�İ߸���?");
            }

            return data;
        }
        else
        {
            //Debug.Log("����� ���̽� ������ ����.");
            return null;
        }
    }
    public void LastTime(TimeSpan timeAway)
    {
        LastTimer = (float)timeAway.TotalSeconds;
        //Debug.Log(LastTimer + "���ڸ� �޾ƿͼ� ������ �÷����� �ٲ��� ��");
        //Debug.Log(AdventureTimer + "�ð� Ȯ��?");
        //AdventureTimer -= (float)timeAway.TotalSeconds;
        //Debug.Log(AdventureTimer + "�ð� Ȯ��?2");
    }
}
