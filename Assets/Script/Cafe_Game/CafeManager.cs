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
    public bool Recipe;// 레시피 판넬 최초로 열어볼 것인지 말 것인지
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

    public float TestTimer = 3;// 게임 시작 전 3초를 세기 위한 변수
    public TextMeshProUGUI TestTimerTextPro;
    public GameObject TestTimePanel;

    public float timer1 = 1;// 손님 대기 시간에 쓰이는 변수
    //public float ttmer1 = 0;

    public float timer2 = 1;

    public float timer3 = 1;
    //public float ttmer2 = 1;

    public Slider[] TimerSlider;// 손님 대기 시간

    public GameObject RecipePanel;// 레시피 판넬
    public GameObject RecipePane1;// 레시피 판넬1
    public GameObject RecipePanel2;// 레시피 판넬2

    public GameObject RecipeR;
    public GameObject RecipeL;
    public GameObject RecipeX;

    public float GameTime = 60;// 게임 시간 1분
    public float gameTime = 0;
    public TextMeshProUGUI Gametime;

    public GameObject CafeExit;// 미니게임 종료 판넬
    public bool GameEnd = false;// 게임 종료를 나타낼 변수

    public GameObject CafeM;

    public TextMeshProUGUI LastScore;

    private string filePath;// 저장
    public bool Recipe;// 레시피 판넬 최초로 열어볼 것인지 말 것인지

    public AudioSource MiniGameEnd;// 미니게임 종료
    public AudioSource ButtonBgm;
    // Start is called before the first frame update

    void Start()
    {
        Time.timeScale = 1;
        TestTimePanel.SetActive(true);
        CafeExit.SetActive(false);// 게임 보상 창 미리 꺼놓기

        filePath = Application.persistentDataPath + "CafeData.json";// 저장
        //if (!Directory.Exists(filePath))// 해당 경로가 존재하지 않는다면
        //{
        //    Directory.CreateDirectory(filePath);// 폴더 생성(경로 만들어주기)
        //
        //}
        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            NameLoadData();
        }
        else
        {
            SaveSave();
        }
        if (Recipe == false)// 레시피를 켠 적이 있다면
        {
            OpenRecipe();// 레시피 판넬을 열고 시간 멈추기
            Recipe = true;
        }
        charStateManager = GameObject.Find("GameObject (1)").transform.Find("CharStateManager").GetComponentInChildren<CharStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {// ESC버튼을 누르거나 뒤로가기 버튼을 눌렀을 때
            OpenRecipe();// 레시피 판넬 다시 보여주기
        }
        gameTime = Time.deltaTime;
        if (TestTimer <= 3)// 게임 시작 전 3초 세기
        {
            TestTimerTextPro.text = TestTimer.ToString("N0");
            TestTimer -= gameTime;
        }
        if (TestTimer <= 0)// 진짜 게임 시작
        {
            TestTimePanel.SetActive(false);// 게임 카운트다운 판넬 없애기
            Gametime.text = GameTime.ToString("N0");// 시간 반영하기
            GameTime -= gameTime;// 실제로 시간이 흐르도록 만들기
            timer1 -= gameTime / 7;// 7초만에 주문이 끝나버림
            TimerSlider[0].value = timer1;
            if (timer1 <= 0)
            {
                coffeeManager.RandomCoffees();
                timer1 = 1;
            }
        }

        if (GameTime < 45)// 2번째 주문 활성화하기
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
        if (GameTime < 30)// 3번째 주문 활성화하기
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

        if (GameTime < 0)// 게임 시간이 끝난 후 보상 창을 띄워야 함
        {
            //MiniGameEnd.Play();
            GameEnd = true;
            //foodList.RandomFoodPick();
            CafeExit.SetActive(true);// 시간이 다 됐으니 미니 게임 종료 판넬 띄우기

            LastScore.text = "수고하셨습니다!\r\n최종 점수: " +
                coffeeManager.CoffeesScore.ToString() + "\r\n";

            Time.timeScale = 0;
        }
        if (GameTime <= 20)
        {
            //Gametime.color = 
            Gametime.text = "<color=#FF9900>" + GameTime.ToString("0") + "</color>";// 시간 반영하기
            //GameTime -= gameTime;
        }
        if (GameTime <= 10)
        {
            Gametime.text = "<color=#FF6666>" + GameTime.ToString("N0") + "</color>";// 시간 반영하기
            //GameTime -= gameTime;
        }
    }

    public void OpenRecipe()// 레시피 판넬을 열고 시간 멈추기
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
    public void RecipeRi()// 레시피 판넬 오른쪽 버튼
    {
        ButtonBgm.Play();
        RecipePane1.SetActive(false);
        RecipeR.SetActive(false);
        RecipePanel2.SetActive(true);
        RecipeL.SetActive(true);
        RecipeX.SetActive(true);
    }
    public void ReciprLe()// 레시피 판넬 왼쪽 버튼
    {
        ButtonBgm.Play();
        RecipePane1.SetActive(true);
        RecipeR.SetActive(true);
        RecipePanel2.SetActive(false);
        RecipeL.SetActive(false);
        RecipeX.SetActive(false);
    }
    public void RecipeFalse()// 레시피 판넬을 닫고 시간 흐르게 하기
    {
        ButtonBgm.Play();
        Time.timeScale = 1;
        RecipePanel.SetActive(false);
        RecipePanel2.SetActive(false);
        RecipeL.SetActive(false);
        RecipeX.SetActive(false);
        
    }
    public void CafeToMain()// 미니 게임에서 나가고 다시 메인 화면으로 돌아가는 함수
    {
        charStateManager.MiniGameBool = false;
        SaveSave();
        Destroy(CafeM);// 자기 자신 삭제
        gameObject.SetActive(false);
        CafeM.SetActive(false);
        foodList.TestTest();
        //Time.timeScale = 1;
        //SceneManager.LoadScene("Main");// 메인 화면으로 돌아가기
    }
    public void SaveSave()
    {
        saveCafe.Recipe = Recipe;
        //Debug.Log(saveCafe.Recipe + "카페 변수 저장");
        NameSaveData(saveCafe);
    }
    public void NameSaveData(SaveCafe data)
    {// 데이터 저장 함수
        // 데이터 객체를 제이슨 문자열로 반환
        string json = JsonUtility.ToJson(data, true);// 트루는 포맷팅 옵션(가독성을 위한 것)

        // 파일에 제이슨 문자열 쓰기
        File.WriteAllText(filePath, json);

        //Debug.Log("카페 제이슨 파일이 저장됨" + filePath);

    }
    public SaveCafe NameLoadData()
    {
        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            string json = File.ReadAllText(filePath);// 파일에서 제이슨 문자열 읽기

            SaveCafe data = JsonUtility.FromJson<SaveCafe>(json);// 제이슨 문자열을 반환

            //Debug.Log("제이슨 데이터가 로드됨 " + filePath);

            Recipe = data.Recipe;

            return data;
        }
        else
        {
            //Debug.Log("저장된 제이슨 파일이 없다.");
            return null;
        }
    }
}
