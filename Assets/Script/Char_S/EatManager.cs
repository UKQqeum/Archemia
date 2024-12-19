using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EatManager : MonoBehaviour
{
    [SerializeField]
    private EatSlotManager slotManager;

    [SerializeField]
    private Inventory inven;

    [SerializeField]
    private CharStateManager State;

    public Item item;

    public AudioSource EatBgm;// ĳ���� �Դ� �Ҹ�

    public void CharEat()// ���̱� ��ư
    {
        EatBgm.Play();
        State.HungerS.value += item.Hunger;// ������ �Ծ����� ������ ��ġ �÷��ֱ�
        inven.AII(item, -1);// �κ��丮���� ���� ���� ���ֱ�
        slotManager.FoodPick(item, -1);// ����â���� ���� ���� ���ֱ�
        State.ItemValue(item.Hunger);
        
    }
}
