using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVTest : MonoBehaviour
{
    Text uiText;
    public int ID;

    void Start()
    {
        uiText = GetComponent<Text>();
        uiText.text = CSVManager.instance.LoadText(ID);
    }

    void OnEnable()
    {
        uiText.text = CSVManager.instance.LoadText(ID);
    }


}
