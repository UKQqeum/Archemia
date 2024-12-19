using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainToCafeManager : MonoBehaviour
{
    [SerializeField]
    private SaveAndLoad SaveandLoad;

    [SerializeField]
    private GameManager Game;

    public static MainToCafeManager instance;// �̱������� ������ ���� ��ȯ�Ǿ �ı����� �ʴ´�

    public Image LoadImage;// �ε��� �̹���

    public GameObject DimeObject;// ���� ������Ʈ�� ������ �ʾƾ� ��

    public GameObject FalseObject;// ĳ���� ����â �ٽ� �ݾ��ֱ� ����

    public AudioSource MainAudio;// ���� ���� �Ҹ�
    public AudioSource CafeAudio;// �̴� ���� �Ҹ�

    public bool PPP;

    public GameObject LodeP;// �ε� �ǳ�
    public float T;
    public float RT;
    // Start is called before the first frame update
    void Awake()
    {
        MainAudio.Play();
        CafeAudio.Stop();
        //Debug.Log("�ǳ�?");
    }
    void Update()
    {
        if (PPP)
        {
            T = Time.deltaTime;
            RT += T;
            LoadImage.fillAmount = RT;
            if (RT >= 1)
            {
                LodeP.SetActive(false);
                SceneManager.LoadSceneAsync("CafeGame", LoadSceneMode.Additive);// �ǳ��� �ı����� �ʰ� �ε�
                RT = 0;
                PPP = false;
            }
        }
    }
    public void GoCafe()
    {
        MainAudio.Stop();
        CafeAudio.Play();
        FalseObject.SetActive(false);
        Game.Panel_Exit();
        Game.TestButtons.SetActive(false);
        LodeP.SetActive(true);
        PPP = true;
        //StartCoroutine(CafeCoroutine());
    }
    public void CCC()
    {
        CafeAudio.Stop();
        MainAudio.Play();
        Game.Test3();
        SceneManager.UnloadSceneAsync("CafeGame");// �ǳ� ���ֱ�
        Time.timeScale = 1;// �̷��� ����� �� �̵� �� ĳ���Ͱ� �ȸ��߰� ī�信�� ī��Ʈ�ٿ��� ������ ����
        //StartCoroutine(ReturnMain());
    }
    IEnumerator CafeCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("CafeGame");
        operation.allowSceneActivation = false;
        
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                LoadImage.fillAmount = Mathf.Lerp(operation.progress, 1f, timer);
                if (LoadImage.fillAmount >= operation.progress)
                    timer = 0f;
            }
            else
            {
                LoadImage.fillAmount = Mathf.Lerp(LoadImage.fillAmount, 1f, timer);
                if (LoadImage.fillAmount >= 0.99f)
                {
                    //operation.allowSceneActivation = true;
                    SceneManager.LoadSceneAsync("CafeGame", LoadSceneMode.Additive);
                    break;
                }
            }
        }

        //SaveandLoad = FindObjectOfType<SaveAndLoad>();// ���� ȭ���� ���̺� �� �ε�
        //SaveandLoad.LoadData();
        DimeObject.SetActive(false);
        //gameObject.SetActive(false);// ��ũ��Ʈ�� ������Ʈ ��Ȱ��ȭ
    }

    IEnumerator ReturnMain()
    {
        
        Time.timeScale = 1;// �̷��� ����� �� �̵� �� ĳ���Ͱ� �ȸ��߰� ī�信�� ī��Ʈ�ٿ��� ������ ����
        gameObject.SetActive(true);// ��ũ��Ʈ�� ������Ʈ Ȱ��ȭ
        DimeObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        //AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        operation.allowSceneActivation = false;
        //if (instance == this)
        //{
        //    instance = null;
        //    Destroy(gameObject);
        //}
        Game.Go_Save();
        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                LoadImage.fillAmount = Mathf.Lerp(operation.progress, 1f, timer);
                if (LoadImage.fillAmount >= operation.progress)
                    timer = 0f;
            }
            else
            {
                LoadImage.fillAmount = Mathf.Lerp(LoadImage.fillAmount, 1f, timer);
                if (LoadImage.fillAmount >= 0.99f)
                {
                    //operation.allowSceneActivation = true;
                    SceneManager.UnloadSceneAsync("CafeGame");
                    break;
                }
            }
        }
        
    }
}
