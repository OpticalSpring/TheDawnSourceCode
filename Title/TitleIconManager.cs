using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleIconManager : MonoBehaviour
{
    public GameObject nlight;
    // Start is called before the first frame update
    void Start()
    {
       
        //ani = oButton.GetComponent<Button>().animator;
    }

    // Update is called once per frame
    void Update()
    {
    }

   


    public void SetLightAni(int i)
    {
        GameObject.Find("Chapter_UI").GetComponent<Animator>().SetInteger("AniState", i);
        SetLight(); 
    }

    public void SetLight()
    {
        nlight.SetActive(true);
    }

    public void OutLight()
    {
        nlight.SetActive(false);
    }
}
