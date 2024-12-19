using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EatSlot : MonoBehaviour
{
    [SerializeField]
    private EatSlotManager manager;

    [SerializeField]
    private EatManager EatM;

    public Item item;

    public int itemCount;// ȹ���� �������� ����
    public Image itemImage;// �������� �̹���

    public TextMeshProUGUI text_Count;// ȹ���� �������� ����

    public GameObject FoodEatButton;// ������ ���̱� ��ư
    //public TextMeshProUGUI ItemStateText;// ������ ���� �ؽ�Ʈ
    //public Image ItemStateImage;// ���� ������ ����â�� �̹���

    // Start is called before the first frame update
    void Start()
    {
        FoodEatButton.SetActive(false);
    }
    public void SetColor(float _alpha)// �̹��� ���� ���İ�, ���� ���� ���� �Լ�
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddPickFood(Item _item, int _count)// ������ ȹ��
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        text_Count.text = _count.ToString();

        SetColor(1);// �������� ���Դٸ� 1�� �ٲ㼭 ���̵��� ���ֱ�
        this.gameObject.SetActive(true);
    }
    public void SetFoodCount(int _count)
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
        //number = " ";
        SetColor(0);
        gameObject.SetActive(false);
        text_Count.text = " ";
        manager.ClearSlotItem();
    }
    public void FoodEat()// ���̸� ������ �� ���̱� ��ư�� ���̵���
    {
        EatM.EatBgm.Play();
        FoodEatButton.SetActive(true);
        EatM.item = item;
        //EatM.CharEat(item);
    }
}
