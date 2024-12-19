using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Collections.AllocatorManager;

public class UIFalse : MonoBehaviour, IPointerClickHandler
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject click = eventData.pointerCurrentRaycast.gameObject;
        if (click.name == "Item_Data_Panel")
        {
            this.gameObject.SetActive(false);// 자기 자신을 없애기?
        }
    }
}
