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
    public string Playername;// 변수로 저장할 닉네임
    public bool InputName;// 닉네임을 입력했는지 확인하기
}
public class NickNameManager : MonoBehaviour
{
    [SerializeField]
    private NameSaveData nameSave;

    public TextMeshProUGUI PlayerTextName;// 플레이어의 상태창 닉네임
    public TMP_InputField NickName;
    public string Playername;// 변수로 저장할 닉네임

    public GameObject NickPanel;

    public bool InputName = false;// 닉네임을 입력했는지 확인하기
    private string filePath;// 저장

    public AudioSource NickBgm;// 닉네임 확인 버튼 브금
    // Start is called before the first frame update
    void Start()
    {
        NickName.characterLimit = 7;
        
        filePath = Application.persistentDataPath + "NameData.json";// 저장
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
        //Debug.Log(InputName + " 닉네임 유무");

        if (InputName == false)// 닉네임을 입력한 적이 없다면
        {
            Time.timeScale = 0;// 닉네임이 없다면 일단 시간 정지하기
            NickPanel.SetActive(true);
        }
        else// 닉네임을 입력했다면
        {
            NickPanel.SetActive(false);
        }

    }

    //void Update()
    //{
    //    if (InputName == false)// 닉네임을 입력한 적이 없다면
    //    {
    //        Time.timeScale = 0;// 닉네임이 없다면 일단 시간 정지하기
    //        NickPanel.SetActive(true);
    //    }
    //    else// 닉네임을 입력했다면
    //    {
    //        NickPanel.SetActive(false);
    //    }
    //}

    private void OnInput(string name)
    {
        string newname = Regex.Replace(name, @"[^0-9a-zA-A가;힣]", "");
        if (NickName.text != name)
        {
            NickName.text = newname;
            NickName.caretPosition = NickName.text.Length;
        }
    }
    public void Inputname()
    {
        //Debug.Log(NickName.text + "나오나?2");
        Playername = NickName.text;// 닉네임 넣어주기
    }
    public void NickNameSave()// 닉네임 입력 확인 함수
    {
        NickBgm.Play();
        InputName = true;
        PlayerTextName.text = Playername.ToString();
        NickPanel.SetActive(false);
        //Debug.Log(Playername + " 닉네임 확인해보기");
        Time.timeScale = 1;// 닉네임을 입력했으니 시간 정지 풀어주기
    }
    public void NameSaveData(NameSaveData data)
    {// 데이터 저장 함수
        // 데이터 객체를 제이슨 문자열로 반환
        string json = JsonUtility.ToJson(data, true);// 트루는 포맷팅 옵션(가독성을 위한 것)

        // 파일에 제이슨 문자열 쓰기
        File.WriteAllText(filePath, json);

        //Debug.Log("캐릭터의 제이슨 파일이 저장됨" + filePath);

    }
    public void SaveSave()
    {
        nameSave.InputName = InputName;
        nameSave.Playername = Playername;
        NameSaveData(nameSave);
        //Debug.Log(nameSave.InputName + " 닉네임 유무 저장");
        //Debug.Log(nameSave.Playername + " 저장된 닉네임");
    }
    public NameSaveData NameLoadData()
    {
        if (File.Exists(filePath))// 파일이 존재하는지 확인
        {
            string json = File.ReadAllText(filePath);// 파일에서 제이슨 문자열 읽기

            NameSaveData data = JsonUtility.FromJson<NameSaveData>(json);// 제이슨 문자열을 반환

            //Debug.Log("제이슨 데이터가 로드됨 " + filePath);

            InputName = data.InputName;
            Playername = data.Playername;
            PlayerTextName.text = Playername.ToString();

            //Debug.Log(InputName + " 닉네임 유무 로드");
            //Debug.Log(Playername + " 저장된 닉네임 로드");

            return data;
        }
        else
        {
            Debug.Log("저장된 제이슨 파일이 없다.");
            return null;
        }
    }
}
