using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour
{
    public static CSVManager instance;

    public List<Dictionary<string, object>> data_KOR;
    public List<Dictionary<string, object>> data_ENG;
    public List<Dictionary<string, object>> data_JPN;
    public List<Dictionary<string, object>> data_TUR;
    public List<Dictionary<string, object>> data_FR;
    private void Awake()
    {
        instance = this;
        
        data_KOR = CSVLoader.Read("KOR");
        data_ENG = CSVLoader.Read("ENG");
        data_JPN = CSVLoader.Read("JPN");
        data_TUR = CSVLoader.Read("TUR");
        data_FR = CSVLoader.Read("FR");
    }

    public string LoadText(int ID)
    {
        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                return (string)CSVManager.instance.data_ENG[ID]["Value"];
            case 1:
                return (string)CSVManager.instance.data_KOR[ID]["Value"];
            case 2:
                return (string)CSVManager.instance.data_JPN[ID]["Value"];
            case 3:
                return (string)CSVManager.instance.data_TUR[ID]["Value"];
            case 4:
                return (string)CSVManager.instance.data_FR[ID]["Value"];
            default:
                return (string)CSVManager.instance.data_ENG[ID]["Value"];
        }
       
    }
}
