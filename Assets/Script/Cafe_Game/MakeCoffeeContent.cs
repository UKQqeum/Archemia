using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeCoffeeContent : MonoBehaviour
{
    [SerializeField]
    private GameObject makeCoffeeContent;// Àç·áÀÇ ºÎ¸ð ½½·Ôµé

    [SerializeField]
    private CoffeeManager coffeeManager;

    private CoffeeSlot[] Slots;// ÄÅÀÇ ½½·Ô
    private Item item;

    // Start is called before the first frame update
    void Start()
    {
        Slots = makeCoffeeContent.GetComponentsInChildren<CoffeeSlot>();
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].AddItem(coffeeManager.CoffeeMatter[i + 1]);
        }
    }
}
