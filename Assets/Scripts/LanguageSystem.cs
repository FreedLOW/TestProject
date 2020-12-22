using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LanguageSystem : MonoBehaviour
{
    public GameObject panel;
    bool indexPanel = false;

    public Text[] textInformation;
    public Text[] textInfoT;

    public Dropdown setLanguage;
    //private int langIndex = 0;
    private string[] languageArray = { "ru_RU", "ua_UA", "eng_ENG" };  //массив доступных языков

    private string json;  //создаю переменную в которой будут храняться json файлы

    public static Lang lang = new Lang();  //создаю экземпляр класса

    private void Awake()
    {
        //реализую загрузку языка по умлочанию, либо тот на которм стоит система, либо тот который был сохранён:
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
                //var ruLang = setLanguage.options[0];  //создаю локальную переменную в которую передаю значения options из Dropdown
                //ruLang.text = "Русский";  //нахожу в options поле text и присваиваю ей значение
            }
            else if (Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("Language", "ua_UA");
                //var uaLang = setLanguage.options[1];  //создаю локальную переменную в которую передаю значения options из Dropdown
                //uaLang.text = "Українська";  //нахожу в options поле text и присваиваю ей значение
            }
            else if (Application.systemLanguage == SystemLanguage.English)
            {
                PlayerPrefs.SetString("Language", "eng_ENG");
                //var engLang = setLanguage.options[2];  //создаю локальную переменную в которую передаю значения options из Dropdown
                //engLang.text = "English";  //нахожу в options поле text и присваиваю ей значение
            }
        }
        else PlayerPrefs.SetString("Language", "ru_RU");

        LanguageLoad();
    }

    private void Update()
    {
        Pause();
    }

    void Pause()  //ставлю игру на паузу и показываю окно с кнопкой выхода:
    {
        if (Input.GetKeyDown(KeyCode.Escape) && indexPanel == false)
        {
            panel.SetActive(true);
            indexPanel = true;
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && indexPanel == true)
        {
            panel.SetActive(false);
            indexPanel = false;
            Time.timeScale = 1f;
        }
    }

    void LanguageLoad()
    {
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".json");  //получаю путь к папке с файлами json языков
        lang = JsonUtility.FromJson<Lang>(json);
        //Debug.Log(lang.information[2]);
        //print(lang.information[0]);

        for (int i = 0; i < textInformation.Length; i++)
        {
            textInformation[i].text = lang.information[i];  //присваиваю компонентам текст в json файлах
            textInfoT[i].text = lang.information[i];
        }
    }

    public void SwitchLanguage(int value)  //переключаю язык через Dropdown:
    {
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = languageArray[value];
        //Debug.Log(option.text);

        setLanguage.value = value;
        //Debug.Log(setLanguage.value);

        PlayerPrefs.SetString("Language", languageArray[value]);
        LanguageLoad();
    }

    public void ExitBtn()
    {
        Application.Quit();  //выхожу из игры
    }
}

public class Lang
{
    public string[] information = new string[3];
}