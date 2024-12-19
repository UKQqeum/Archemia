using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour, IPointerClickHandler
{
    public GameObject TOto;// Ʃ�丮�� �ǳ�
    [TextArea]
    public string[] strings;// Ʃ�丮��� �ϳ��� �� ����

    [SerializeField]
    public TextMeshProUGUI Tuto;// Ʃ�丮���� ����

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
        if (click.name == "Tutorial")// Ʃ�丮�� �ǳ��� �ǵ帰�ٸ�
        {
            if (toto < 6)// 2������ �İ�â, 3������ �ռ�â, 4���� �ռ�â
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
