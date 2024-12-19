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
    int Anime;// ĳ���Ͱ� ���� �̵��� ������
    private RectTransform rectTransform;

    public bool CharBool;

    public bool smileBool;// ��ġ �� Ȱ��ȭ
    public float smailTimer = 1;

    public AudioSource charTouch;// ĳ���� ������ �� �Ҹ�������

    public GameObject CharNameState;// ������ ĳ������ �̸��� ���¸� Ȯ���� �� �ֵ���
    public TextMeshProUGUI CharName;// ĳ���� �̸�
    public Image CharImage;// ĳ���� �Ϸ���Ʈ

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
        if (smileBool)// ��ġ �� ���� ��
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
            if (manager.panel_bool == false)// �ǳ��� ���� ���� ����  && CharBool == true
            {// ĳ���Ͱ� ȭ�鿡 ���� �� �����̵���
                this.gameObject.SetActive(CharBool);
                AnimeRight();
                timer += Time.deltaTime;
                //Debug.Log(timer + "�ð� ���");
            }
            else
            {
                this.gameObject.SetActive(CharBool);
                rigid.velocity = new Vector3(0, rigid.velocity.y);
                anim.SetBool("isRun", false);// �̵��� �������� �ִϸ��̼� ���ֱ�
            }
        }
        
    }
    public void charAnime()
    {
        anim.SetBool("isRun", true);// �ִϸ��̼� �̸� Ȱ��ȭ���ֱ�
        int Anime = Random.Range(1, 2);
        AnimeRight();
    }
    public void AnimeRight()
    {
        if (Anime > 0)// ���������� �̵�
        {
            if (timer < 1)
            {
                anim.SetBool("isRun", true);// �ִϸ��̼� �̸� Ȱ��ȭ���ֱ�
                //this.rectTransform = GetComponent<RectTransform>();
                this.rigid.velocity = new Vector3((float) Anime * 10, 0, 0);
                //rigid.velocity = new Vector3(Anime, rigid.velocity.y, 1);// 2�ʵ��� ���������� 2��ŭ �̵��ϱ�
                this.transform.localScale = new Vector3(-1, 1, 1);
                //spriteRenderer.flipX = true;
            }
            else if (timer > 1 && timer < 2.5)
            {
                anim.SetBool("isRun", false);// �̵��� �������� �ִϸ��̼� ���ֱ�
                this.rigid.velocity = new Vector3((float)Anime * 10, 0, 0);
                //this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 1);// 2�ʵ��� ���������� 2��ŭ �̵��ϱ�
            }
            else if (timer > 2.5)
            {
                timer = 0;
                Ran();
            }
        }
        else if (Anime == 0)
        {
            anim.SetBool("isRun", false);// �̵��� �������� �ִϸ��̼� ���ֱ�
            if (timer > 2)
            {
                timer = 0;
                Ran();
            }
        }
        else// �������� �̵�
        {
            if (timer < 1)
            {
                anim.SetBool("isRun", true);// �ִϸ��̼� �̸� Ȱ��ȭ���ֱ�
                this.rigid.velocity = new Vector3((float)Anime * 10, 0, 0);
                //this.GetComponent<RectTransform>().anchoredPosition = new Vector3(Anime, 0, 0);
                //rigid.velocity = new Vector3(Anime, rigid.velocity.y, 1);// 2�ʵ��� ���������� 2��ŭ �̵��ϱ�
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (timer > 1 && timer < 2.5)
            {
                anim.SetBool("isRun", false);// �̵��� �������� �ִϸ��̼� ���ֱ�
                this.rigid.velocity = new Vector3((float)Anime * 10, 0, 0);
                //this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 1);// 2�ʵ��� ���������� 2��ŭ �̵��ϱ�
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
        anim.SetBool("isRun", false);// �̵��� �������� �ִϸ��̼� ���ֱ�
        //rigid.velocity = new Vector2(0, rigid.velocity.y);
        Invoke("AnimeRight", 3f);// 5�� �� ĳ���� �ִ� �Լ��� �����Ŵ
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "��R")
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            rigid.velocity = new Vector3(0, rigid.velocity.y, 1);
            anim.SetBool("isRun", false);// �̵��� �������� �ִϸ��̼� ���ֱ�
            Anime = Random.Range(-3, 0);
        }
        if (coll.gameObject.name == "��L")
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            rigid.velocity = new Vector3(0, rigid.velocity.y, 1);
            anim.SetBool("isRun", false);// �̵��� �������� �ִϸ��̼� ���ֱ�
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
