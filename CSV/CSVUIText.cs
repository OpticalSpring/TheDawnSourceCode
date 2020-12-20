using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVUIText : MonoBehaviour
{
    public int ID;

    void Start()
    {
        GetComponent<Text>().text = CSVManager.instance.LoadText(ID);
    }

    void OnEnable()
    {
        GetComponent<Text>().text = CSVManager.instance.LoadText(ID);
    }
}
