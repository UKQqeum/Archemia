using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour, IPointerClickHandler
{
    public GameObject TOto;// 튜토리얼 판넬
    [TextArea]
    public string[] strings;// 튜토리얼로 하나씩 쓸 말들

    [SerializeField]
    public TextMeshProUGUI Tuto;// 튜토리얼의 글자

    public int toto = 0;

    // Start is called before the first frame update
    void Start()
    {
        Tuto.text = strings[0].ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject click = eventData.pointerCurrentRaycast.gameObject;
        //Debug.Log(click.name);
        if (click.name == "Tutorial")// 튜토리얼 판넬을 건드린다면
        {
            if (toto < 6)// 2번에서 파견창, 3번에서 합성창, 4번은 합성창
            {
                toto++;
                Tuto.text = strings[toto].ToString();
            }
            else
            {
                TOto.SetActive(false);
                
            }
        }
    }
}
