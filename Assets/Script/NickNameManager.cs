using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;

[System.Serializable]
public class NameSaveData
{
    public string Playername;// ������ ������ �г���
    public bool InputName;// �г����� �Է��ߴ��� Ȯ���ϱ�
}
public class NickNameManager : MonoBehaviour
{
    [SerializeField]
    private NameSaveData nameSave;

    public TextMeshProUGUI PlayerTextName;// �÷��̾��� ����â �г���
    public TMP_InputField NickName;
    public string Playername;// ������ ������ �г���

    public GameObject NickPanel;

    public bool InputName = false;// �г����� �Է��ߴ��� Ȯ���ϱ�
    private string filePath;// ����

    public AudioSource NickBgm;// �г��� Ȯ�� ��ư ���
    // Start is called before the first frame update
    void Start()
    {
        NickName.characterLimit = 7;
        
        filePath = Application.persistentDataPath + "NameData.json";// ����
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
        //Debug.Log(InputName + " �г��� ����");

        if (InputName == false)// �г����� �Է��� ���� ���ٸ�
        {
            Time.timeScale = 0;// �г����� ���ٸ� �ϴ� �ð� �����ϱ�
            NickPanel.SetActive(true);
        }
        else// �г����� �Է��ߴٸ�
        {
            NickPanel.SetActive(false);
        }

    }

    //void Update()
    //{
    //    if (InputName == false)// �г����� �Է��� ���� ���ٸ�
    //    {
    //        Time.timeScale = 0;// �г����� ���ٸ� �ϴ� �ð� �����ϱ�
    //        NickPanel.SetActive(true);
    //    }
    //    else// �г����� �Է��ߴٸ�
    //    {
    //        NickPanel.SetActive(false);
    //    }
    //}

    private void OnInput(string name)
    {
        string newname = Regex.Replace(name, @"[^0-9a-zA-A��;�R]", "");
        if (NickName.text != name)
        {
            NickName.text = newname;
            NickName.caretPosition = NickName.text.Length;
        }
    }
    public void Inputname()
    {
        //Debug.Log(NickName.text + "������?2");
        Playername = NickName.text;// �г��� �־��ֱ�
    }
    public void NickNameSave()// �г��� �Է� Ȯ�� �Լ�
    {
        NickBgm.Play();
        InputName = true;
        PlayerTextName.text = Playername.ToString();
        NickPanel.SetActive(false);
        //Debug.Log(Playername + " �г��� Ȯ���غ���");
        Time.timeScale = 1;// �г����� �Է������� �ð� ���� Ǯ���ֱ�
    }
    public void NameSaveData(NameSaveData data)
    {// ������ ���� �Լ�
        // ������ ��ü�� ���̽� ���ڿ��� ��ȯ
        string json = JsonUtility.ToJson(data, true);// Ʈ��� ������ �ɼ�(�������� ���� ��)

        // ���Ͽ� ���̽� ���ڿ� ����
        File.WriteAllText(filePath, json);

        //Debug.Log("ĳ������ ���̽� ������ �����" + filePath);

    }
    public void SaveSave()
    {
        nameSave.InputName = InputName;
        nameSave.Playername = Playername;
        NameSaveData(nameSave);
        //Debug.Log(nameSave.InputName + " �г��� ���� ����");
        //Debug.Log(nameSave.Playername + " ����� �г���");
    }
    public NameSaveData NameLoadData()
    {
        if (File.Exists(filePath))// ������ �����ϴ��� Ȯ��
        {
            string json = File.ReadAllText(filePath);// ���Ͽ��� ���̽� ���ڿ� �б�

            NameSaveData data = JsonUtility.FromJson<NameSaveData>(json);// ���̽� ���ڿ��� ��ȯ

            //Debug.Log("���̽� �����Ͱ� �ε�� " + filePath);

            InputName = data.InputName;
            Playername = data.Playername;
            PlayerTextName.text = Playername.ToString();

            //Debug.Log(InputName + " �г��� ���� �ε�");
            //Debug.Log(Playername + " ����� �г��� �ε�");

            return data;
        }
        else
        {
            Debug.Log("����� ���̽� ������ ����.");
            return null;
        }
    }
}
