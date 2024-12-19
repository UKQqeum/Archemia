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

    public static LoadManager instance;// �̱������� ������ ���� ��ȯ�Ǿ �ı����� �ʴ´�

    public Image LoadImage;// �ε��� �̹���
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

        SaveandLoad = FindObjectOfType<SaveAndLoad>();// ���� ȭ���� ���̺� �� �ε�
        SaveandLoad.LoadData();
        //AdventureManager LoadAdven = SaveandLoad.LoadAdvenData();
        //SaveandLoad.LoadAdvenData();
        gameObject.SetActive(false);// Ÿ��Ʋ ��ũ��Ʈ�� ������Ʈ ��Ȱ��ȭ
    }
}
