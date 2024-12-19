using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public string startSceneName = "Main";
    public string loadSceneName = "Load";

    public GameObject LoadPanel;// �ε� ���� �ǳ�
    public Image LoadImage;
    public float timer = 0;
    public bool load = false;

    public GameObject Bottons;

    public AudioSource GameStarts;// ���� ���� �Ҹ�

    public AudioSource TitleBgm;// Ÿ��Ʋ ���

    // Start is called before the first frame update
    void Awake()
    {
        LoadPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (load)
        {
            LoadPanel.SetActive(true);
            timer += Time.deltaTime;
            
            if (timer >= 0f)
            {
                LoadImage.fillAmount = timer / 1;
                if (LoadImage.fillAmount == 1)
                {
                    load = false;
                    SceneManager.LoadScene("Main");
                }
                
            }
        }
    }
    public void Game_Start()
    {
        GameStarts.Play();
        load = true;
        Bottons.SetActive(false);
    }
    public void Game_Exit()
    {
        Application.Quit();// ���� ���� ��ư
    }
    public void Go_Load()// �ҷ�����
    {
        GameStarts.Play();
        SceneManager.LoadScene(loadSceneName);
    }
}
