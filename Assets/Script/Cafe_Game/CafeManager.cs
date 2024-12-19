using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.IO;

[Serializable]
public class SaveCafe
{
    public bool Recipe;// ������ �ǳ� ���ʷ� ��� ������ �� ������
}
public class CafeManager : MonoBehaviour
{
    [SerializeField]
    private CoffeeManager coffeeManager;

    [SerializeField]
    private FoodList foodList;

    [SerializeField]
    private SaveCafe saveCafe;

    [SerializeField]
    private CharStateManager charStateManager;

    public float TestTimer = 3;// ���� ���� �� 3�ʸ� ���� ���� ����
    public TextMeshProUGUI TestTimerTextPro;
    public GameObject TestTimePanel;

    public float timer1 = 1;// �մ� ��� �ð��� ���̴� ����
    //public float ttmer1 = 0;

    public float timer2 = 1;

    public float timer3 = 1;
    //public float ttmer2 = 1;

    public Slider[] TimerSlider;// �մ� ��� �ð�

    public GameObject RecipePanel;// ������ �ǳ�
    public GameObject RecipePane1;// ������ �ǳ�1
    public GameObject RecipePanel2;// ������ �ǳ�2

    public GameObject RecipeR;
    public GameObject RecipeL;
    public GameObject RecipeX;

    public float GameTime = 60;// ���� �ð� 1��
    public float gameTime = 0;
    public TextMeshProUGUI Gametime;

    public GameObject CafeExit;// �̴ϰ��� ���� �ǳ�
    public bool GameEnd = false;// ���� ���Ḧ ��Ÿ�� ����

    public GameObject CafeM;

    public TextMeshProUGUI LastScore;

    private string filePath;// ����
    public bool Recipe;// ������ �ǳ� ���ʷ� ��� ������ �� ������

    public AudioSource MiniGameEnd;// �̴ϰ��� ����
    public AudioSource ButtonBgm;
    // Start is called before the first frame update

    void Start()
    {
        Time.timeScale = 1;
        TestTimePanel.SetActive(true);
        CafeExit.SetActive(false);// ���� ���� â �̸� ������

        filePath = Application.persistentDataPath + "CafeData.json";// ����
        //if (!Directory.Exists(filePath))// �ش� ��ΰ� �������� �ʴ´ٸ�
        //{
        //    Directory.CreateDirectory(filePath);// ���� ����(��� ������ֱ�)
        //
        //}
        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
        {
            NameLoadData();
        }
        else
        {
            SaveSave();
        }
        if (Recipe == false)// �����Ǹ� �� ���� �ִٸ�
        {
            OpenRecipe();// ������ �ǳ��� ���� �ð� ���߱�
            Recipe = true;
        }
        charStateManager = GameObject.Find("GameObject (1)").transform.Find("CharStateManager").GetComponentInChildren<CharStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {// ESC��ư�� �����ų� �ڷΰ��� ��ư�� ������ ��
            OpenRecipe();// ������ �ǳ� �ٽ� �����ֱ�
        }
        gameTime = Time.deltaTime;
        if (TestTimer <= 3)// ���� ���� �� 3�� ����
        {
            TestTimerTextPro.text = TestTimer.ToString("N0");
            TestTimer -= gameTime;
        }
        if (TestTimer <= 0)// ��¥ ���� ����
        {
            TestTimePanel.SetActive(false);// ���� ī��Ʈ�ٿ� �ǳ� ���ֱ�
            Gametime.text = GameTime.ToString("N0");// �ð� �ݿ��ϱ�
            GameTime -= gameTime;// ������ �ð��� �帣���� �����
            timer1 -= gameTime / 7;// 7�ʸ��� �ֹ��� ��������
            TimerSlider[0].value = timer1;
            if (timer1 <= 0)
            {
                coffeeManager.RandomCoffees();
                timer1 = 1;
            }
        }

        if (GameTime < 45)// 2��° �ֹ� Ȱ��ȭ�ϱ�
        {
            TimerSlider[1].gameObject.SetActive(true);
            //coffeeManager.RandomCoff2 = true;
            timer2 -= gameTime / 7;
            TimerSlider[1].value = timer2;
            if (timer2 <= 0)
            {
                coffeeManager.RandomCoffee2();
                timer2 = 1;
            }
        }
        if (GameTime < 30)// 3��° �ֹ� Ȱ��ȭ�ϱ�
        {
            TimerSlider[2].gameObject.SetActive(true);
            timer3 -= gameTime / 7;
            TimerSlider[2].value = timer3;
            if (timer3 <= 0)
            {
                coffeeManager.RandomCoffee3();
                timer3 = 1;
            }
        }

        if (GameTime < 0)// ���� �ð��� ���� �� ���� â�� ����� ��
        {
            //MiniGameEnd.Play();
            GameEnd = true;
            //foodList.RandomFoodPick();
            CafeExit.SetActive(true);// �ð��� �� ������ �̴� ���� ���� �ǳ� ����

            LastScore.text = "�����ϼ̽��ϴ�!\r\n���� ����: " +
                coffeeManager.CoffeesScore.ToString() + "\r\n";

            Time.timeScale = 0;
        }
        if (GameTime <= 20)
        {
            //Gametime.color = 
            Gametime.text = "<color=#FF9900>" + GameTime.ToString("0") + "</color>";// �ð� �ݿ��ϱ�
            //GameTime -= gameTime;
        }
        if (GameTime <= 10)
        {
            Gametime.text = "<color=#FF6666>" + GameTime.ToString("N0") + "</color>";// �ð� �ݿ��ϱ�
            //GameTime -= gameTime;
        }
    }

    public void OpenRecipe()// ������ �ǳ��� ���� �ð� ���߱�
    {
        ButtonBgm.Play();
        Time.timeScale = 0;
        RecipePanel.SetActive(true);
        RecipePane1.SetActive(true);
        RecipeR.SetActive(true);
        RecipePanel2.SetActive(false);
        RecipeL.SetActive(false);
        RecipeX.SetActive(false);
    }
    public void RecipeRi()// ������ �ǳ� ������ ��ư
    {
        ButtonBgm.Play();
        RecipePane1.SetActive(false);
        RecipeR.SetActive(false);
        RecipePanel2.SetActive(true);
        RecipeL.SetActive(true);
        RecipeX.SetActive(true);
    }
    public void ReciprLe()// ������ �ǳ� ���� ��ư
    {
        ButtonBgm.Play();
        RecipePane1.SetActive(true);
        RecipeR.SetActive(true);
        RecipePanel2.SetActive(false);
        RecipeL.SetActive(false);
        RecipeX.SetActive(false);
    }
    public void RecipeFalse()// ������ �ǳ��� �ݰ� �ð� �帣�� �ϱ�
    {
        ButtonBgm.Play();
        Time.timeScale = 1;
        RecipePanel.SetActive(false);
        RecipePanel2.SetActive(false);
        RecipeL.SetActive(false);
        RecipeX.SetActive(false);
        
    }
    public void CafeToMain()// �̴� ���ӿ��� ������ �ٽ� ���� ȭ������ ���ư��� �Լ�
    {
        charStateManager.MiniGameBool = false;
        SaveSave();
        Destroy(CafeM);// �ڱ� �ڽ� ����
        gameObject.SetActive(false);
        CafeM.SetActive(false);
        foodList.TestTest();
        //Time.timeScale = 1;
        //SceneManager.LoadScene("Main");// ���� ȭ������ ���ư���
    }
    public void SaveSave()
    {
        saveCafe.Recipe = Recipe;
        //Debug.Log(saveCafe.Recipe + "ī�� ���� ����");
        NameSaveData(saveCafe);
    }
    public void NameSaveData(SaveCafe data)
    {// ������ ���� �Լ�
        // ������ ��ü�� ���̽� ���ڿ��� ��ȯ
        string json = JsonUtility.ToJson(data, true);// Ʈ��� ������ �ɼ�(�������� ���� ��)

        // ���Ͽ� ���̽� ���ڿ� ����
        File.WriteAllText(filePath, json);

        //Debug.Log("ī�� ���̽� ������ �����" + filePath);

    }
    public SaveCafe NameLoadData()
    {
        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
        {
            string json = File.ReadAllText(filePath);// ���Ͽ��� ���̽� ���ڿ� �б�

            SaveCafe data = JsonUtility.FromJson<SaveCafe>(json);// ���̽� ���ڿ��� ��ȯ

            //Debug.Log("���̽� �����Ͱ� �ε�� " + filePath);

            Recipe = data.Recipe;

            return data;
        }
        else
        {
            //Debug.Log("����� ���̽� ������ ����.");
            return null;
        }
    }
}
