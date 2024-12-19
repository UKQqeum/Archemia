using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Collections.AllocatorManager;

public class UIClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]// �������� ���� �� ����� ���� �˾� â�� ���� ��ũ��Ʈ
    private ItemList list;

    [SerializeField]
    private SynthesisMain symain;

    public bool panel = false;// �ǳ��� ������ �Ⱦ� â�� ���½������ ��
    public bool Exit_Bool = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject click = eventData.pointerCurrentRaycast.gameObject;
        if (click.name == "Panel")
        {
            if (list.Pick_)// ������ ȹ�� â�� ����� ���� ��
            {
                Exit_Bool = true;
                list.Item_Pick_Panel.SetActive(false);// �� ���� ��ġ�� ������ ȹ�� �˾� â �ݾƹ�����
                panel = true;
                list.PickFalse();
            }
        }
        if (click)
        {
            if (symain.Cre_)// �ռ��� ��� â�� �����ִٸ�
            {
                //symain.Stuff_Panel.SetActive(false);// ��� â �ݾƹ�����
                symain.ItemSee();
                //symain.Cre_ = false;// �ٽ� �ٲ���� ��

            }
        }
        if (symain.Com_)
        {
            symain.Com_Panel.SetActive(false);
            Exit_Bool = true;
            symain.Com_ = false;
        }
        Time.timeScale = 1;// �Ͻ����� Ǯ���ֱ�
    }
}
