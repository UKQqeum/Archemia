using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Threading;
using Unity.VisualScripting;

[System.Serializable]// ����ȭ�� �ؾ� ��
public class SaveData// ������ ����ȭ�� �Ұ���
{
    public List<int> invenArrayNumber = new List<int>();// �κ��丮 ����
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public List<bool> invenItemBool = new List<bool>();

    //public List<int> stuArrayNumber = new List<int>();// �ռ��� ���ĭ ����
    //public List<string> stuItemName = new List<string>();
    //public List<int> stuItenNumber = new List<int>();

    public List<int> bookArrayNumber = new List<int>();// �ռ� ����� ���� ����
    public List<string> bookItemName = new List<string>();
    public List<bool> bookItemBool = new List<bool>();

    //public List<bool> charAnimeBool = new List<bool>();// ���� ȭ�鿡 �־�� �� ĳ����

}
public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;// ������ ���� ���
    private string SAVE_FILENAME = "/SaveFile.txt";// ���� �̸�

    [SerializeField]
    private Inventory inventory;// ������ �κ� ������ �������� ���� �ʿ�
    //private SynthesisMain synthesisMain;// ������ �ռ� ���â ������ �������� ���� �ʿ�

    [SerializeField]
    private  Book book;//������ ���� ������ �������� ���� �ʿ�

    public bool saveFile = false;// ������ ���� Ȯ��
    public GameObject savePanel;// ���� �Ϸ� �˸� �ǳ�
    public Image saveImage;
    public TextMeshProUGUI saveText;
    public float saveTimer = 1;

    public bool loadFile = false;// ���̺� ������ ���� Ȯ��
    public GameObject loadPanel;// �ε� ���� �˸� �ǳ�
    //public Image loadImage;
    public TextMeshProUGUI loadText;
    public float loadTimer = 1;

    public AudioSource SaveCom;// ���� �Ϸ� �Ҹ�

    [SerializeField]
    private AdventureManager advenManager;
    private string filePath;// ����

    // Start is called before the first frame update
    void Start()
    {
        //SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";
        filePath = Application.persistentDataPath + "SaveSaveData.json";// ����
        //filePath = Application.persistentDataPath + "PlayerData.json";

        //if (!Directory.Exists(SAVE_DATA_DIRECTORY))// �ش� ��ΰ� �������� �ʴ´ٸ�
        //{
        //    Directory.CreateDirectory(SAVE_DATA_DIRECTORY);// ���� ����(��� ������ֱ�)
        //    
        //}
        savePanel.SetActive(false);

        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
        {
            LoadData();// ���ϰ�θ� ������ �� ������ �ε��ϱ�
        }
        else
        {
            SaveData();
        }
    }
    public void DestroySave()// ���̺� ���� ���� �Լ�
    {
        if (Directory.Exists(SAVE_DATA_DIRECTORY))// ���̺� ������ �����Ѵٸ�
        {
            Directory.Delete(SAVE_DATA_DIRECTORY, true);
            Debug.Log("����?");
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
    public void SaveData()// ������ ������
    {
        //inventory = FindObjectOfType<Inventory>();
        //synthesisMain = FindObjectOfType<SynthesisMain>();
        //book = FindObjectOfType<Book>();

        saveData.invenArrayNumber.Clear();
        saveData.invenItemName.Clear();
        saveData.invenItemNumber.Clear();
        saveData.invenItemBool.Clear();

        Slot[] slots = inventory.GetSlots();// �κ��丮 ���� ���� ��������
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

        BookSlot[] bookslots = book.GetBookSlots();// ������ ���� ���� ��������

        saveData.bookArrayNumber.Clear();
        saveData.bookItemName.Clear();
        saveData.bookItemBool.Clear();
        for (int i = 0; i < bookslots.Length; i++)
        {
            if (bookslots[i].item != null)
            {
                //Debug.Log(bookslots[i].item.itemName + " ���� ���� Ȯ��?");
                if (bookslots[i].item.item_)
                {
                    //Debug.Log(bookslots[i].item.itemName + " Ʈ�簡 �ִ��� Ȯ��?");
                    saveData.bookArrayNumber.Add(i);
                    saveData.bookItemName.Add(bookslots[i].item.itemName);
                    saveData.bookItemBool.Add(bookslots[i].item.item_);
                }
            }
            
        }
        // ���� ��ü ����
        string json = JsonUtility.ToJson(saveData);// ���̽�ȭ
        File.WriteAllText(filePath, json);

        savePanel.SetActive(true);
        saveFile = true;
        SaveCom.Play();// ���� �Ϸ� �Ҹ� ����
        //Debug.Log("���� �Ϸ�");
        //Debug.Log(json);
        //advenManager.SaveSave();
    }
    public void LoadData()// ����� ������ �ҷ����� �Լ�
    {
        if (File.Exists(filePath))
        {// ��ü������ �о����
            string loadJson = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            //inventory = FindObjectOfType<Inventory>();
            //book = FindObjectOfType<Book>();
            for (int i = 0; i < saveData.invenItemName.Count; i++)// �κ��丮 ����� �� �ε��ϱ�
            {
                inventory.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i],
                    saveData.invenItemNumber[i], saveData.invenItemBool[i]);// �̰� ������ ��� �κ��� �ϸ� �ռ� ��ᵵ �Ǵ°ǵ�
            }

            for (int i = 0; i < saveData.bookItemName.Count; i++)// ���� ����� �� �ε��ϱ�
            {
                book.LoadToBook(saveData.bookArrayNumber[i], saveData.bookItemName[i],
                saveData.bookItemBool[i]);
            }

            //if (saveData.bookItemBool[0])
            //{
            //}
            inventory.SaveInvenToSyn();
            //Debug.Log("�ε� �Ϸ�");
        }
        else
        {
            //loadPanel.SetActive(true);
            loadFile = true;
            //Debug.Log("���̺� ������ �����ϴ�");
        }
    }
    public void SetLoadColor(float _alpha)// �̹��� ���� ���İ�, ���� ���� ���� �Լ�
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
