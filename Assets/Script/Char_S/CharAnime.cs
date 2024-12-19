//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class CharAnime : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public SynthesisMain SynthesisMain;

    [SerializeField]
    private GameManager manager;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    float timer = 0;
    int Anime;// 캐릭터가 어디로 이동할 것인지
    private RectTransform rectTransform;

    public bool CharBool;

    public bool smileBool;// 터치 시 활성화
    public float smailTimer = 1;

    public AudioSource charTouch;// 캐릭터 눌렀을 때 소리나도록

    public GameObject CharNameState;// 누르면 캐릭터의 이름과 상태를 확인할 수 있도록
    public TextMeshProUGUI CharName;// 캐릭터 이름
    public Image CharImage;// 캐릭터 일러스트

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //CharNameState.SetActive(false);
        //Spri.transform.localScale = this.transform.localScale;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Anime = Random.Range(-4, 5);
        rectTransform = GetComponent<RectTransform>();
        //AnimeRight();
        rigid.gravityScale = 0;
        CharBool = true;
    }
    public void Ran()
    {
        Anime = Random.Range(-4, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {//
        //CharNameState.transform.position = this.transform.position + new Vector3(0, -390);
        if (smileBool)// 터치 시 웃게 됨
        {
            if (smailTimer ==1)
            {
                charTouch.Play();
            }
            anim.SetBool("isSmile", true);
            rigid.velocity = new Vector3(0, rigid.velocity.y);
            smailTimer -= Time.deltaTime;
            if (smailTimer <= 0)
            {
                anim.SetBool("isSmile", false);
                smileBool = false;
                smailTimer = 1;
                timer = 0;
            }
        }
        else
        {
            if (manager.panel_bool == false)// 판넬이 꺼져 있을 때와  && CharBool == true
            {// 캐릭터가 화면에 있을 때 움직이도록
                this.gameObject.SetActive(CharBool);
                AnimeRight();
                timer += Time.deltaTime;
                //Debug.Log(timer + "시간 재기");
            }
            else
            {
                this.gameObject.SetActive(CharBool);
                rigid.velocity = new Vector3(0, rigid.velocity.y);
                anim.SetBool("isRun", false);// 이동이 끝났으니 애니메이션 꺼주기
            }
        }
        
    }
    public void charAnime()
    {
        anim.SetBool("isRun", true);// 애니메이션 미리 활성화해주기
        int Anime = Random.Range(1, 2);
        AnimeRight();
    }
    public void AnimeRight()
    {
        if (Anime > 0)// 오른쪽으로 이동
        {
            if (timer < 1)
            {
                anim.SetBool("isRun", true);// 애니메이션 미리 활성화해주기
                //this.rectTransform = GetComponent<RectTransform>();
                this.rigid.velocity = new Vector3((float) Anime * 10, 0, 0);
                //rigid.velocity = new Vector3(Anime, rigid.velocity.y, 1);// 2초동안 오른쪽으로 2만큼 이동하기
                this.transform.localScale = new Vector3(-1, 1, 1);
                //spriteRenderer.flipX = true;
            }
            else if (timer > 1 && timer < 2.5)
            {
                anim.SetBool("isRun", false);// 이동이 끝났으니 애니메이션 꺼주기
                this.rigid.velocity = new Vector3((float)Anime * 10, 0, 0);
                //this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 1);// 2초동안 오른쪽으로 2만큼 이동하기
            }
            else if (timer > 2.5)
            {
                timer = 0;
                Ran();
            }
        }
        else if (Anime == 0)
        {
            anim.SetBool("isRun", false);// 이동이 끝났으니 애니메이션 꺼주기
            if (timer > 2)
            {
                timer = 0;
                Ran();
            }
        }
        else// 왼쪽으로 이동
        {
            if (timer < 1)
            {
                anim.SetBool("isRun", true);// 애니메이션 미리 활성화해주기
                this.rigid.velocity = new Vector3((float)Anime * 10, 0, 0);
                //this.GetComponent<RectTransform>().anchoredPosition = new Vector3(Anime, 0, 0);
                //rigid.velocity = new Vector3(Anime, rigid.velocity.y, 1);// 2초동안 오른쪽으로 2만큼 이동하기
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (timer > 1 && timer < 2.5)
            {
                anim.SetBool("isRun", false);// 이동이 끝났으니 애니메이션 꺼주기
                this.rigid.velocity = new Vector3((float)Anime * 10, 0, 0);
                //this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 1);// 2초동안 오른쪽으로 2만큼 이동하기
            }
            else if (timer > 2.5)
            {
                timer = 0;
                Ran();
            }
        }
    }
    public void Delay()
    {
        anim.SetBool("isRun", false);// 이동이 끝났으니 애니메이션 꺼주기
        //rigid.velocity = new Vector2(0, rigid.velocity.y);
        Invoke("AnimeRight", 3f);// 5초 뒤 캐릭터 애니 함수를 실행시킴
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "벽R")
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            rigid.velocity = new Vector3(0, rigid.velocity.y, 1);
            anim.SetBool("isRun", false);// 이동이 끝났으니 애니메이션 꺼주기
            Anime = Random.Range(-3, 0);
        }
        if (coll.gameObject.name == "벽L")
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            rigid.velocity = new Vector3(0, rigid.velocity.y, 1);
            anim.SetBool("isRun", false);// 이동이 끝났으니 애니메이션 꺼주기
            Anime = Random.Range(1, 4);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject click = eventData.pointerCurrentRaycast.gameObject;
        smileBool = true;
        //CharNameState.SetActive(true);
        CharName.text = click.name;

        Invoke("GOFalse", 3f);
    }
    public void GOFalse()
    {
        //CharNameState.SetActive(false);
    }
}
