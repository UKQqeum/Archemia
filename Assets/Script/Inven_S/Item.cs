using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    public string itemName;// �������� �̸�
    public ItemType itemType;// �������� ����
    public Sprite itemImage;// �������� �̹���
    public GameObject itemPrefab;// �������� ������
    [TextArea]
    public string itemStatusText;// �������� ��� ���� = ������ ���̴� �� ��?

    public Sprite detailed_image;// �������� ��, ����, ��, �÷����� �� ���� �̹���
    public string itemNumber;// ������ �ĺ� ��ȣ?
    public bool item_ = false;// ������ ��� ����

    public float Love;// ĳ������ ���� ��ġ�� �����ֱ� ���� ����
    public float Hunger;// ĳ������ ���� ��ġ�� �����ֱ� ���� ����
    public float Stature;// ĳ������ ���� ��ġ�� �����ֱ� ���� ����

    public int Start;// �İ����� ȹ�� ������ ������ ��Ÿ�� �� ����
    public int End;// �İ����� ȹ�� �������� ���� ��Ÿ�� �� ����
    public int Count;// �İ����� ȹ�� ������ ������ ��Ÿ�� �� ����

    public string Head;// ĳ���� ��� ��Ÿ���� ����
    public string Eye;
    public string Tail;

    public enum ItemType
    {
        Used,// �Ҹ�ǰ �ֵ� ��
        Ingredient,// ����� �ֵ� �ռ�
        ETC,// ��Ÿ
        Eye,// ��
        Head,// �Ӹ�ī��, ��?
        Tail,/// ����
        Color,// ��
        Complete// �ϼ�ǰ
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    item_ = false;
    //}
}
