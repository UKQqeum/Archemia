using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodSlot : MonoBehaviour
{
    public Item item;

    [SerializeField]
    private CafeManager cafeManager;

    public int itemCount;// ȹ���� �������� ����
    public Image itemImage;// �������� �̹���

    public TextMeshProUGUI text_Count;// ȹ���� �������� ����

    public GameObject ItemStatePanel;// �������� �� ���� �ǳ�
    public TextMeshProUGUI ItemStateText;// ������ ���� �ؽ�Ʈ
    public Image ItemStateImage;// ���� ������ ����â�� �̹���

    // Start is called before the first frame update
    void Start()
    {
        ItemStatePanel.SetActive(false);
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

    public void PickItemData()// �������� ������ �� ���� ���� �˾�â
    {
        cafeManager.ButtonBgm.Play();
        ItemStatePanel.SetActive(true);
        ItemStateImage.sprite = itemImage.sprite;
        ItemStateText.text = item.itemStatusText;
    }
}
