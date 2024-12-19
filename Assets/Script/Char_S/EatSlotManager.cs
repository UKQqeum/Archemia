using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EatSlotManager : MonoBehaviour
{
    [SerializeField]
    private GameObject All_Slot;// ��� ���� ���Ե��� �����ϴ� ��. content

    private EatSlot[] eatSlots;

    // Start is called before the first frame update
    void Start()
    {
        eatSlots = All_Slot.GetComponentsInChildren<EatSlot>();

        for (int i = 0; i < eatSlots.Length; i++)// ������ ������ŭ �ݺ�.SetSlotColor(0);
        {
            eatSlots[i].SetColor(0);
            eatSlots[i].gameObject.SetActive(false);// ���� ���� �������� ���� �Ⱥ��̰� ���ֱ�
        }
    }

    public void FoodPick(Item _item, int count)
    {
        for (int i = 0; i < eatSlots.Length; i++)
        {
            if (eatSlots[i].item != null)// ĭ�� �������� ���� ��
            {
                if (eatSlots[i].item.itemName == _item.itemName)// ���� �ȿ� ���� �������� �������
                {
                    //Debug.Log(i + "��° ���Կ� ���� �������� �������");
                    eatSlots[i].SetFoodCount(count);// ���Ծ��� ������ ���� ����
                    return;
                }
            }
            else
            {
                eatSlots[i].SetColor(1);
                eatSlots[i].AddPickFood(_item, count);
                eatSlots[i].gameObject.SetActive(true);// ���� ���� �������� ���� �Ⱥ��̰� ���ֱ�
                return;
            }
        }
    }
    public void ClearSlotItem()// �������� ���� ���� �� ���� �������� ������ ������
    {
        for (int i = 0; i < eatSlots.Length - 1; i++)
        {
            //Debug.Log(i + " ��?");
            if (eatSlots[i].item == null)// i��° ������ ����ְ�
            {
                if (eatSlots[i + 1].item != null)// i�� ���� ���Կ� �������� ���� ��
                {// ������ �� �����̰� �޽����� ������ ������ �޽����� �ս������� �ű��
                    eatSlots[i].item = eatSlots[i + 1].item;
                    eatSlots[i].itemImage.sprite = eatSlots[i + 1].itemImage.sprite;
                    eatSlots[i].itemCount = eatSlots[i + 1].itemCount;
                    eatSlots[i].item.itemNumber = eatSlots[i + 1].item.itemNumber;
                    eatSlots[i].gameObject.SetActive(true);
                    eatSlots[i].SetColor(1);
                    eatSlots[i].SetFoodCount(0);
                    eatSlots[i + 1].item = null;
                    eatSlots[i + 1].itemImage.sprite = null;
                    eatSlots[i + 1].itemCount = 0;
                    eatSlots[i + 1].gameObject.SetActive(false);
                    eatSlots[i + 1].SetColor(0);
                }
            }
        }
    }
}
