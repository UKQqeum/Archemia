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

    public static MainToCafeManager instance;// 싱글톤으로 만들어야 씬이 전환되어도 파괴되지 않는다

    public Image LoadImage;// 로딩될 이미지

    public GameObject DimeObject;// 밑의 오브젝트가 보이지 않아야 함

    public GameObject FalseObject;// 캐릭터 상태창 다시 닫아주기 위해

    public AudioSource MainAudio;// 메인 게임 소리
    public AudioSource CafeAudio;// 미니 게임 소리

    public bool PPP;

    public GameObject LodeP;// 로드 판넬
    public float T;
    public float RT;
    // Start is called before the first frame update
    void Awake()
    {
        MainAudio.Play();
        CafeAudio.Stop();
        //Debug.Log("되나?");
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
                SceneManager.LoadSceneAsync("CafeGame", LoadSceneMode.Additive);// 판넬을 파괴하지 않고 로드
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
        SceneManager.UnloadSceneAsync("CafeGame");// 판넬 없애기
        Time.timeScale = 1;// 이렇게 해줘야 씬 이동 후 캐릭터가 안멈추고 카페에서 카운트다운이 멈추지 않음
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

        //SaveandLoad = FindObjectOfType<SaveAndLoad>();// 메인 화면의 세이브 앤 로드
        //SaveandLoad.LoadData();
        DimeObject.SetActive(false);
        //gameObject.SetActive(false);// 스크립트의 오브젝트 비활성화
    }

    IEnumerator ReturnMain()
    {
        
        Time.timeScale = 1;// 이렇게 해줘야 씬 이동 후 캐릭터가 안멈추고 카페에서 카운트다운이 멈추지 않음
        gameObject.SetActive(true);// 스크립트의 오브젝트 활성화
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
