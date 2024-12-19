using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Collections.AllocatorManager;

public class UIClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]// 아이템을 얻은 후 배경을 눌러 팝업 창을 끄는 스크립트
    private ItemList list;

    [SerializeField]
    private SynthesisMain symain;

    public bool panel = false;// 판넬이 꺼지면 픽업 창도 리셋시켜줘야 함
    public bool Exit_Bool = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject click = eventData.pointerCurrentRaycast.gameObject;
        if (click.name == "Panel")
        {
            if (list.Pick_)// 아이템 획득 창이 띄워져 있을 때
            {
                Exit_Bool = true;
                list.Item_Pick_Panel.SetActive(false);// 빈 곳을 터치해 아이템 획득 팝업 창 닫아버리기
                panel = true;
                list.PickFalse();
            }
        }
        if (click)
        {
            if (symain.Cre_)// 합성의 재료 창이 열려있다면
            {
                //symain.Stuff_Panel.SetActive(false);// 재료 창 닫아버리기
                symain.ItemSee();
                //symain.Cre_ = false;// 다시 바꿔줘야 함

            }
        }
        if (symain.Com_)
        {
            symain.Com_Panel.SetActive(false);
            Exit_Bool = true;
            symain.Com_ = false;
        }
        Time.timeScale = 1;// 일시정지 풀어주기
    }
}
