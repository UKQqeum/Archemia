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

    public AudioSource EatBgm;// 캐릭터 먹는 소리

    public void CharEat()// 먹이기 버튼
    {
        EatBgm.Play();
        State.HungerS.value += item.Hunger;// 음식을 먹었으니 공복도 수치 올려주기
        inven.AII(item, -1);// 인벤토리에서 먹은 과일 빼주기
        slotManager.FoodPick(item, -1);// 먹이창에서 먹은 과일 빼주기
        State.ItemValue(item.Hunger);
        
    }
}
