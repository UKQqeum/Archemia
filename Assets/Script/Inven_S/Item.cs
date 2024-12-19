using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    public string itemName;// 아이템의 이름
    public ItemType itemType;// 아이템의 유형
    public Sprite itemImage;// 아이템의 이미지
    public GameObject itemPrefab;// 아이템의 프리팹
    [TextArea]
    public string itemStatusText;// 아이템의 기능 설명 = 꼬리에 쓰이는 것 등?

    public Sprite detailed_image;// 아이템의 눈, 꼬리, 뿔, 컬러같은 상세 설명 이미지
    public string itemNumber;// 아이템 식별 번호?
    public bool item_ = false;// 아이템 사용 여부

    public float Love;// 캐릭터의 애정 수치를 보여주기 위한 변수
    public float Hunger;// 캐릭터의 공복 수치를 보여주기 위한 변수
    public float Stature;// 캐릭터의 성장 수치를 보여주기 위한 변수

    public int Start;// 파견지의 획득 아이템 시작을 나타내 줄 변수
    public int End;// 파견지의 획득 아이템의 끝을 나타내 줄 변수
    public int Count;// 파견지의 획득 아이템 개수를 나타내 줄 변수

    public string Head;// 캐릭터 재료 나타내기 위함
    public string Eye;
    public string Tail;

    public enum ItemType
    {
        Used,// 소모품 애들 밥
        Ingredient,// 재료형 애들 합성
        ETC,// 기타
        Eye,// 눈
        Head,// 머리카락, 뿔?
        Tail,/// 꼬리
        Color,// 색
        Complete// 완성품
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    item_ = false;
    //}
}
