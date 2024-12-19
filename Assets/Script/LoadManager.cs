using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;

public class LoadManager : MonoBehaviour
{
    private SaveAndLoad SaveandLoad;

    public static LoadManager instance;// 싱글톤으로 만들어야 씬이 전환되어도 파괴되지 않는다

    public Image LoadImage;// 로딩될 이미지
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;

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
                    operation.allowSceneActivation = true;
            }
        }

        SaveandLoad = FindObjectOfType<SaveAndLoad>();// 메인 화면의 세이브 앤 로드
        SaveandLoad.LoadData();
        //AdventureManager LoadAdven = SaveandLoad.LoadAdvenData();
        //SaveandLoad.LoadAdvenData();
        gameObject.SetActive(false);// 타이틀 스크립트의 오브젝트 비활성화
    }
}
