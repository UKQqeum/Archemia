using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Threading;
using Unity.VisualScripting;

[System.Serializable]// 직렬화를 해야 함
public class SaveData// 슬롯은 직렬화가 불가능
{
    public List<int> invenArrayNumber = new List<int>();// 인벤토리 슬롯
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public List<bool> invenItemBool = new List<bool>();

    //public List<int> stuArrayNumber = new List<int>();// 합성의 재료칸 슬롯
    //public List<string> stuItemName = new List<string>();
    //public List<int> stuItenNumber = new List<int>();

    public List<int> bookArrayNumber = new List<int>();// 합성 결과물 도감 슬롯
    public List<string> bookItemName = new List<string>();
    public List<bool> bookItemBool = new List<bool>();

    //public List<bool> charAnimeBool = new List<bool>();// 메인 화면에 있어야 할 캐릭터

}
public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;// 저장할 폴더 경로
    private string SAVE_FILENAME = "/SaveFile.txt";// 파일 이름

    [SerializeField]
    private Inventory inventory;// 저장할 인벤 슬롯을 가져오기 위해 필요
    //private SynthesisMain synthesisMain;// 저장할 합성 재료창 슬롯을 가져오기 위해 필요

    [SerializeField]
    private  Book book;//저장할 도감 슬롯을 가져오기 위해 필요

    public bool saveFile = false;// 저장의 유무 확인
    public GameObject savePanel;// 저장 완료 알림 판넬
    public Image saveImage;
    public TextMeshProUGUI saveText;
    public float saveTimer = 1;

    public bool loadFile = false;// 세이브 파일의 유무 확인
    public GameObject loadPanel;// 로드 실패 알림 판넬
    //public Image loadImage;
    public TextMeshProUGUI loadText;
    public float loadTimer = 1;

    public AudioSource SaveCom;// 저장 완료 소리

    [SerializeField]
    private AdventureManager advenManager;
    private string filePath;// 저장

    // Start is called before the first frame update
    void Start()
    {
        //SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";
        filePath = Application.persistentDataPath + "SaveSaveData.json";// 저장
        //filePath = Application.persistentDataPath + "PlayerData.json";

        //if (!Directory.Exists(SAVE_DATA_DIRECTORY))// 해당 경로가 존재하지 않는다면
        //{
        //    Directory.CreateDirectory(SAVE_DATA_DIRECTORY);// 폴더 생성(경로 만들어주기)
        //    
        //}
        savePanel.SetActive(false);

        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            LoadData();// 파일경로를 지정해 준 다음에 로드하기
        }
        else
        {
            SaveData();
        }
    }
    public void DestroySave()// 세이브 파일 삭제 함수
    {
        if (Directory.Exists(SAVE_DATA_DIRECTORY))// 세이브 파일이 존재한다면
        {
            Directory.Delete(SAVE_DATA_DIRECTORY, true);
            Debug.Log("삭제?");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (loadFile)
        {
            Time.timeScale = 1.0f;
            loadTimer -= Time.deltaTime;
            SetLoadColor(loadTimer);
            if (loadTimer <= 0f)
            {
                SetLoadColor(0);
                loadFile = false;
                loadTimer = 1;
                loadPanel.SetActive(false);
            }
        }
        if (saveFile)
        {
            saveTimer -= Time.deltaTime;
            SetSaveColor(saveTimer);
            if (saveTimer <= 0f)
            {
                SetSaveColor(0);
                saveFile = false;
                saveTimer = 1;
                savePanel.SetActive(false);
            }
        }
    }
    public void SaveData()// 저장할 데이터
    {
        //inventory = FindObjectOfType<Inventory>();
        //synthesisMain = FindObjectOfType<SynthesisMain>();
        //book = FindObjectOfType<Book>();

        saveData.invenArrayNumber.Clear();
        saveData.invenItemName.Clear();
        saveData.invenItemNumber.Clear();
        saveData.invenItemBool.Clear();

        Slot[] slots = inventory.GetSlots();// 인벤토리 슬롯 전부 가져오기
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
                saveData.invenItemBool.Add(slots[i].item.item_);
            }
        }

        BookSlot[] bookslots = book.GetBookSlots();// 도감의 슬롯 전부 가져오기

        saveData.bookArrayNumber.Clear();
        saveData.bookItemName.Clear();
        saveData.bookItemBool.Clear();
        for (int i = 0; i < bookslots.Length; i++)
        {
            if (bookslots[i].item != null)
            {
                //Debug.Log(bookslots[i].item.itemName + " 도감 저장 확인?");
                if (bookslots[i].item.item_)
                {
                    //Debug.Log(bookslots[i].item.itemName + " 트루가 있는지 확인?");
                    saveData.bookArrayNumber.Add(i);
                    saveData.bookItemName.Add(bookslots[i].item.itemName);
                    saveData.bookItemBool.Add(bookslots[i].item.item_);
                }
            }
            
        }
        // 최종 전체 저장
        string json = JsonUtility.ToJson(saveData);// 제이슨화
        File.WriteAllText(filePath, json);

        savePanel.SetActive(true);
        saveFile = true;
        SaveCom.Play();// 저장 완료 소리 내기
        //Debug.Log("저장 완료");
        //Debug.Log(json);
        //advenManager.SaveSave();
    }
    public void LoadData()// 저장된 데이터 불러오기 함수
    {
        if (File.Exists(filePath))
        {// 전체적으로 읽어오기
            string loadJson = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            //inventory = FindObjectOfType<Inventory>();
            //book = FindObjectOfType<Book>();
            for (int i = 0; i < saveData.invenItemName.Count; i++)// 인벤토리 저장된 것 로드하기
            {
                inventory.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i],
                    saveData.invenItemNumber[i], saveData.invenItemBool[i]);// 이거 저장은 사실 인벤만 하면 합성 재료도 되는건디
            }

            for (int i = 0; i < saveData.bookItemName.Count; i++)// 도감 저장된 것 로드하기
            {
                book.LoadToBook(saveData.bookArrayNumber[i], saveData.bookItemName[i],
                saveData.bookItemBool[i]);
            }

            //if (saveData.bookItemBool[0])
            //{
            //}
            inventory.SaveInvenToSyn();
            //Debug.Log("로드 완료");
        }
        else
        {
            //loadPanel.SetActive(true);
            loadFile = true;
            //Debug.Log("세이브 파일이 없습니다");
        }
    }
    public void SetLoadColor(float _alpha)// 이미지 색의 알파값, 투명도 조절 관련 함수
    {
        //Color color = loadImage.color;
        Color textColor = loadText.color;
        //color.a = _alpha;
        textColor.a = _alpha;
        //loadImage.color = color;
        //loadText.color = color;
    }
    public void SetSaveColor(float _alpha)
    {
        Color color = saveImage.color;
        Color textColor = saveText.color;
        color.a = _alpha;
        textColor.a = _alpha;
        saveImage.color = color;
        //saveText.color = color;
    }

    
}
