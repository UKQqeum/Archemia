using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickSlot : MonoBehaviour// �İ� �Ϸ� �� �������� ����� �� ���̴� ��ũ��Ʈ
{
    public Item item;

    public int itemCount;// ȹ���� �������� ����
    public Image itemImage;// �������� �̹���

    public TextMeshProUGUI text_Count;// �Ⱦ� �������� ����

    public GameObject ItemStatePanel;// �������� �� ���� �ǳ�
    public TextMeshProUGUI ItemStateText;// ������ ���� �ؽ�Ʈ
    public Image ItemStateImage;// ���� ������ ����â�� �̹���

    public AudioSource Bgm;

    public void SetColor(float _alpha)// �̹��� ���� ���İ�, ���� ���� ���� �Լ�
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddPickItem(Item _item, int _count)// ������ ȹ��
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        text_Count.text = _count.ToString();

        SetColor(1);// �������� ���Դٸ� 1�� �ٲ㼭 ���̵��� ���ֱ�
    }

    public void PickItemData()// �������� ������ �� ���� ���� �˾�â
    {
        Bgm.Play();
        ItemStatePanel.SetActive(true);
        ItemStateImage.sprite = itemImage.sprite;
        //ItemStateText.text = item.itemStatusText;
        if (item.item_ == true)
            ItemStateText.text = item.itemStatusText;
        else
            ItemStateText.text = "�������� ���� \r\n�뵵���� �𸣰ڴ�.";
    }
    public void ItemFalse()// �������� ���� �ƹ��͵� ������ ���ֱ�
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        this.gameObject.SetActive(false);
        SetColor(0);// �������� ���Դٸ� 1�� �ٲ㼭 ���̵��� ���ֱ�
    }
}
