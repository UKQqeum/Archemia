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
    public string Playername;// º¯¼ö·Î ÀúÀåÇÒ ´Ğ³×ÀÓ
    public bool InputName;// ´Ğ³×ÀÓÀ» ÀÔ·ÂÇß´ÂÁö È®ÀÎÇÏ±â
}
public class NickNameManager : MonoBehaviour
{
    [SerializeField]
    private NameSaveData nameSave;

    public TextMeshProUGUI PlayerTextName;// ÇÃ·¹ÀÌ¾îÀÇ »óÅÂÃ¢ ´Ğ³×ÀÓ
    public TMP_InputField NickName;
    public string Playername;// º¯¼ö·Î ÀúÀåÇÒ ´Ğ³×ÀÓ

    public GameObject NickPanel;

    public bool InputName = false;// ´Ğ³×ÀÓÀ» ÀÔ·ÂÇß´ÂÁö È®ÀÎÇÏ±â
    private string filePath;// ÀúÀå

    public AudioSource NickBgm;// ´Ğ³×ÀÓ È®ÀÎ ¹öÆ° ºê±İ
    // Start is called before the first frame update
    void Start()
    {
        NickName.characterLimit = 7;
        
        filePath = Application.persistentDataPath + "NameData.json";// ÀúÀå
        //if (!Directory.Exists(filePath))// ÇØ´ç °æ·Î°¡ Á¸ÀçÇÏÁö ¾Ê´Â´Ù¸é
        //{
        //    Directory.CreateDirectory(filePath);// Æú´õ »ı¼º(°æ·Î ¸¸µé¾îÁÖ±â)
        //
        //}
        if (File.Exists(filePath))// ÆÄÀÏÀÌ Á¸ÀçÇÏ´ÂÁö È®ÀÎ
        {
            NameLoadData();
        }
        else
        {
            SaveSave();
        }
        //Debug.Log(InputName + " ´Ğ³×ÀÓ À¯¹«");

        if (InputName == false)// ´Ğ³×ÀÓÀ» ÀÔ·ÂÇÑ ÀûÀÌ ¾ø´Ù¸é
        {
            Time.timeScale = 0;// ´Ğ³×ÀÓÀÌ ¾ø´Ù¸é ÀÏ´Ü ½Ã°£ Á¤ÁöÇÏ±â
            NickPanel.SetActive(true);
        }
        else// ´Ğ³×ÀÓÀ» ÀÔ·ÂÇß´Ù¸é
        {
            NickPanel.SetActive(false);
        }

    }

    //void Update()
    //{
    //    if (InputName == false)// ´Ğ³×ÀÓÀ» ÀÔ·ÂÇÑ ÀûÀÌ ¾ø´Ù¸é
    //    {
    //        Time.timeScale = 0;// ´Ğ³×ÀÓÀÌ ¾ø´Ù¸é ÀÏ´Ü ½Ã°£ Á¤ÁöÇÏ±â
    //        NickPanel.SetActive(true);
    //    }
    //    else// ´Ğ³×ÀÓÀ» ÀÔ·ÂÇß´Ù¸é
    //    {
    //        NickPanel.SetActive(false);
    //    }
    //}

    private void OnInput(string name)
    {
        string newname = Regex.Replace(name, @"[^0-9a-zA-A°¡;ÆR]", "");
        if (NickName.text != name)
        {
            NickName.text = newname;
            NickName.caretPosition = NickName.text.Length;
        }
    }
    public void Inputname()
    {
        //Debug.Log(NickName.text + "³ª¿À³ª?2");
        Playername = NickName.text;// ´Ğ³×ÀÓ ³Ö¾îÁÖ±â
    }
    public void NickNameSave()// ´Ğ³×ÀÓ ÀÔ·Â È®ÀÎ ÇÔ¼ö
    {
        NickBgm.Play();
        InputName = true;
        PlayerTextName.text = Playername.ToString();
        NickPanel.SetActive(false);
        //Debug.Log(Playername + " ´Ğ³×ÀÓ È®ÀÎÇØº¸±â");
        Time.timeScale = 1;// ´Ğ³×ÀÓÀ» ÀÔ·ÂÇßÀ¸´Ï ½Ã°£ Á¤Áö Ç®¾îÁÖ±â
    }
    public void NameSaveData(NameSaveData data)
    {// µ¥ÀÌÅÍ ÀúÀå ÇÔ¼ö
        // µ¥ÀÌÅÍ °´Ã¼¸¦ Á¦ÀÌ½¼ ¹®ÀÚ¿­·Î ¹İÈ¯
        string json = JsonUtility.ToJson(data, true);// Æ®·ç´Â Æ÷¸ËÆÃ ¿É¼Ç(°¡µ¶¼ºÀ» À§ÇÑ °Í)

        // ÆÄÀÏ¿¡ Á¦ÀÌ½¼ ¹®ÀÚ¿­ ¾²±â
        File.WriteAllText(filePath, json);

        //Debug.Log("Ä³¸¯ÅÍÀÇ Á¦ÀÌ½¼ ÆÄÀÏÀÌ ÀúÀåµÊ" + filePath);

    }
    public void SaveSave()
    {
        nameSave.InputName = InputName;
        nameSave.Playername = Playername;
        NameSaveData(nameSave);
        //Debug.Log(nameSave.InputName + " ´Ğ³×ÀÓ À¯¹« ÀúÀå");
        //Debug.Log(nameSave.Playername + " ÀúÀåµÈ ´Ğ³×ÀÓ");
    }
    public NameSaveData NameLoadData()
    {
        if (File.Exists(filePath))// ÆÄÀÏÀÌ Á¸ÀçÇÏ´ÂÁö È®ÀÎ
        {
            string json = File.ReadAllText(filePath);// ÆÄÀÏ¿¡¼­ Á¦ÀÌ½¼ ¹®ÀÚ¿­ ÀĞ±â

            NameSaveData data = JsonUtility.FromJson<NameSaveData>(json);// Á¦ÀÌ½¼ ¹®ÀÚ¿­À» ¹İÈ¯

            //Debug.Log("Á¦ÀÌ½¼ µ¥ÀÌÅÍ°¡ ·ÎµåµÊ " + filePath);

            InputName = data.InputName;
            Playername = data.Playername;
            PlayerTextName.text = Playername.ToString();

            //Debug.Log(InputName + " ´Ğ³×ÀÓ À¯¹« ·Îµå");
            //Debug.Log(Playername + " ÀúÀåµÈ ´Ğ³×ÀÓ ·Îµå");

            return data;
        }
        else
        {
            Debug.Log("ÀúÀåµÈ Á¦ÀÌ½¼ ÆÄÀÏÀÌ ¾ø´Ù.");
            return null;
        }
    }
}
