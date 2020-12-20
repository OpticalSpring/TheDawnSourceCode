using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVContinueText : MonoBehaviour
{
    Text continueText;

    private void Awake()
    {
        continueText = GetComponent<Text>();
    }

    void Start()
    {
        if (!GetComponent<Text>())
        {
            Debug.Log("Start(): " + gameObject.name);
            return;
        }
        switch (PlayerPrefs.GetInt("SceneNum"))
        {
            case 0:
                continueText.text = CSVManager.instance.LoadText(30);
                break;
            case 3:
                continueText.text = CSVManager.instance.LoadText(31);
                break;
            case 4:
                continueText.text = CSVManager.instance.LoadText(32) + PlayerPrefs.GetInt("CheckPoint") + "]";
                break;
            case 5:
                continueText.text = CSVManager.instance.LoadText(33) + PlayerPrefs.GetInt("CheckPoint") + "]";
                break;
            default:
                continueText.text = CSVManager.instance.LoadText(34);
                break;
        }
    }

    void OnEnable()
    {
        if (!GetComponent<Text>())
        {
            Debug.Log("OnEnable(): " + gameObject.name);
            return;
        }
        switch (PlayerPrefs.GetInt("SceneNum"))
        {
            case 0:
                continueText.text = CSVManager.instance.LoadText(30);
                break;
            case 3:
                continueText.text = CSVManager.instance.LoadText(31);
                break;
            case 4:
                continueText.text = CSVManager.instance.LoadText(32) + PlayerPrefs.GetInt("CheckPoint") + "]";
                break;
            case 5:
                continueText.text = CSVManager.instance.LoadText(33) + PlayerPrefs.GetInt("CheckPoint") + "]";
                break;
            default:
                continueText.text = CSVManager.instance.LoadText(34);
                break;
        }
    }
}
