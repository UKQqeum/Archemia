using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Slot : MonoBehaviour
{
    //private Vector3 originPos;
    public Item item;// ȹ���� ������
    public int itemCount;// ȹ���� �������� ����
    public Image itemImage;// �������� �̹���
    public string number;// ������ �ĺ� ��ȣ
    public bool itemBool;// �������� ��� ����

    // �ʿ��� ������Ʈ��
    [SerializeField]
    private TextMeshProUGUI text_Count;// �������� ������ ǥ������ �ؽ�Ʈ
    
    public GameObject ItemStatePanel;// �������� �� ���� �ǳ�
    public TextMeshProUGUI ItemStateText;// ������ ���� �ؽ�Ʈ
    public Image ItemStateImage;// ���� ������ ����â�� �̹���

    public Inventory inventory;// �κ��丮 ��ũ��Ʈ

    // Start is called before the first frame update
    void Start()
    {
        ItemStatePanel.SetActive(false);// �κ��丮�� ó�� ���� �� ������ ������ �ʵ���
        ItemStateText.gameObject.SetActive(false);
        ItemStateImage.gameObject.SetActive(false);
    }
    public void SetColor(float _alpha)// �̹��� ���� ���İ�, ���� ���� ���� �Լ�
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)// ������ ȹ��
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        number = item.itemNumber;
        itemBool = item.item_;

        if (item.item_ == true)
            ItemStateText.text = item.itemStatusText;
        else
            ItemStateText.text = "???";

        text_Count.text = itemCount.ToString();// �������� ���� ���̱�
        //text_Count.text = "";// ���ڰ� ������ �ʵ���

        SetColor(1);// �������� ���Դٸ� 1�� �ٲ㼭 ���̵��� ���ֱ�
        gameObject.SetActive(true);
    }

    public void SetSlotCount(int _count)// ������ ���� ����
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)// �������� ������ ���� ��
        {
            ClearSlot();
        }
    }
    public void ClearSlot()// ���� �ʱ�ȭ
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        number = " ";
        SetColor(0);
        gameObject.SetActive(false);
        text_Count.text = " ";
        inventory.ClearSlotItem();
    }
    public void TextS()
    {
        text_Count.text = itemCount.ToString();// �������� ���� ���̱�
    }
    public void Use()
    {
        inventory.ItemSound.Play();
        ItemStatePanel.SetActive(true);// �������� �������� �� ������ ���̵���
        inventory.ItemStatePanel.SetActive(true);
        ItemStateText.gameObject.SetActive(true);
        ItemStateImage.gameObject.SetActive(true);
        ItemStateImage.sprite = item.detailed_image;
        if (item.item_ == true)
            ItemStateText.text = item.itemStatusText;
        else
            ItemStateText.text = "�������� ���� \r\n�뵵���� �𸣰ڴ�.";
        //Debug.Log(item.itemName + "�ٸ� �Լ�������?");
        //database.UseItem(item);// �����ͺ��̽��� ���� ������ �Լ� ��������
        //SetSlotCount(-1);
    }
    //public void PointerDown()// ���콺�� ��� Ȧ���ϰ� ���� ��
    //{
    //    itemClick = true;
    //    Debug.Log("������ �ٿ�");
    //    itemTime += Time.deltaTime;
    //}
    //public void PointerUp()// ���콺 Ȧ�� ���
    //{
    //    itemClick = false;
    //    Debug.Log("������ ��");
    //    itemTime = 0;
    //    GrayItemImage.fillAmount = 0f;
    //    Debug.Log(itemTime + "�ð� �ʱ�ȭ");
    //}
}
