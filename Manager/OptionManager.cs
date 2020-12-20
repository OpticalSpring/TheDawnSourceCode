using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;
    public GameObject hideObject;
    public GameObject optionObject;
    public GameObject optionAni;
    public GameObject mainAni;
    public bool optionState;
    private void Awake()
    {
        instance = this;
        
            if (PlayerPrefs.GetInt("SaveState") == 0)
            {
                PlayerPrefs.SetInt("GamePlay_1", 60);
                PlayerPrefs.SetInt("GamePlay_2", 30);
                PlayerPrefs.SetInt("Sound_1", 100);
                PlayerPrefs.SetInt("Sound_2", 100);
                PlayerPrefs.SetInt("Sound_3", 100);
                PlayerPrefs.SetInt("SaveState", 1);
            }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(optionState == true)
            {
                if(optionAni == null)
                {
                    OffOptionFast();
                }
                else
                {

                    OffOption();
                }
            }
        }
    }

    public void SetOption()
    {
        SoundManager.instance.SoundPlay(8, 1);
        StartCoroutine(SetAni());
    }

    public void SetOptionFast()
    {
        SoundManager.instance.SoundPlay(8, 1);
        hideObject.SetActive(false);
        optionObject.SetActive(true);
        optionState = true;
    }
    public void OffOption()
    {
        SoundManager.instance.SoundPlay(8, 1);
        StartCoroutine(OffAni());
    }
    public void OffOptionFast()
    {
        SoundManager.instance.SoundPlay(8, 1);
        hideObject.SetActive(true);
        optionObject.SetActive(false);
        optionState = false;
    }

    IEnumerator SetAni()
    {
       mainAni.SetActive(true);
        hideObject.SetActive(false);
        mainAni.GetComponent<Animator>().SetInteger("AniState", 1);
        yield return new WaitForSecondsRealtime(1);
        optionState = true;
        optionObject.SetActive(true);
        mainAni.SetActive(false);
    }

    IEnumerator OffAni()
    {
        optionObject.SetActive(false);
        optionState = false;
        optionAni.SetActive(true);
        optionAni.GetComponent<Animator>().SetInteger("AniState", 2);
        yield return new WaitForSecondsRealtime(1);
        hideObject.SetActive(true);
        optionAni.SetActive(false);
    }

    
}
