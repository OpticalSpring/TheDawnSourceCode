using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int sceneNum;
    public GameObject playUI;
    public GameObject escUI;
    public GameObject deadUI;
    public GameObject respawnUI;
    public GameObject endUI;
    public GameObject consoleUI;
    public InputField consoleText;
    public Text cpCount;
    public GameObject[] bSound;
    bool noui;
    private void Awake()
    {
        instance = this;
    }

    public bool timeStopState;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        PlayerPrefs.SetInt("SceneNum", sceneNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStopState == true)
        {

            if (Input.GetKeyDown(KeyCode.F12))
            {
                if (consoleUI.activeSelf == true)
                {
                    consoleUI.SetActive(false);
                }
                else
                {
                    consoleUI.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SoundManager.instance.SoundPlay(8, 1);
                switch (consoleText.text)
                {
                    case "NoDeath":
                        PlayerUISystem.instance.player.HP_Point = 100000000;
                        PlayerUISystem.instance.player.HP_PointMax = 100000000;
                        consoleText.text = "AccessComplete";
                        break;
                    case "NoDelay":
                        PlayerUISystem.instance.player.timeRecallDelayTime = 0;
                        PlayerUISystem.instance.player.timeStopFieldDelayTime = 0;
                        PlayerUISystem.instance.player.dodgeDelayTime = 0;
                        PlayerUISystem.instance.player.ultimateGageMax = 0;
                        consoleText.text = "AccessComplete";
                        break;
                    case "CheckPoint-0":
                        PlayerPrefs.SetInt("CheckPoint", 0);
                        consoleText.text = "AccessComplete";
                        break;
                    case "CheckPoint-1":
                        PlayerPrefs.SetInt("CheckPoint", 1);
                        consoleText.text = "AccessComplete";
                        break;
                    case "CheckPoint-2":
                        PlayerPrefs.SetInt("CheckPoint", 2);
                        consoleText.text = "AccessComplete";
                        break;
                    case "CheckPoint-3":
                        PlayerPrefs.SetInt("CheckPoint", 3);
                        consoleText.text = "AccessComplete";
                        break;
                    case "CheckPoint-4":
                        PlayerPrefs.SetInt("CheckPoint", 4);
                        consoleText.text = "AccessComplete";
                        break;
                    case "CheckPoint-5":
                        PlayerPrefs.SetInt("CheckPoint", 5);
                        consoleText.text = "AccessComplete";
                        break;
                    case "NoUI":
                        noui = true;
                        consoleText.text = "AccessComplete";
                        break;
                    case "OnFPS":
                        CheckFPS.instance.check = true;
                        consoleText.text = "AccessComplete";
                        break;
                    case "Cine-0":
                        CinematicCam.instance.Set(0);
                        break;
                    case "Cine-1":
                        CinematicCam.instance.Set(1);
                        break;
                    case "Cine-2":
                        CinematicCam.instance.Set(2);
                        break;
                    case "Cine-3":
                        CinematicCam.instance.Set(3);
                        break;
                    case "PowerUp":
                        Player.instance.hipFireDamage = 5000;
                        Player.instance.shoulderFireDamage = 5000;
                        consoleText.text = "AccessComplete";
                        break;
                    default:
                        consoleText.text = "ErrorCode";
                        break;
                }
            }

        }

        if(noui == true)
        {
            playUI.SetActive(false);
        }
    }

    public void SetEnd()
    {
        playUI.SetActive(false);
        endUI.SetActive(true);
        timeStopState = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cpCount.text = PlayerPrefs.GetInt("CPCount").ToString("D2");
    }

    public void SetESC()
    {
        SoundManager.instance.SoundPlay(8, 2);
        playUI.SetActive(false);
        escUI.SetActive(true);
        timeStopState = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OutESC()
    {
        SoundManager.instance.SoundPlay(8, 1);
        consoleUI.SetActive(false);
        playUI.SetActive(true);
        escUI.SetActive(false);
        timeStopState = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetDead()
    {
        playUI.SetActive(false);
        deadUI.SetActive(true);
        respawnUI.SetActive(false);
        timeStopState = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    

    public void Resume()
    {
        OutESC();
    }

    public void RestartScene()
    {
        SoundManager.instance.SoundPlay(8, 1);
        StartScene(sceneNum);
        PlayerPrefs.SetInt("CheckPoint", 0);
        PlayerPrefs.SetInt("CPCount", 0);
    }


    public void RestartCheckPoint()
    {
        SoundManager.instance.SoundPlay(8, 1);
        StartScene(sceneNum);
        PlayerPrefs.SetInt("CPCount", PlayerPrefs.GetInt("CPCount") + 1);
    }

    public void GoTitleScene()
    {
        SoundManager.instance.SoundPlay(8, 1);
        StartScene(0);
    }

    public void GoNextScene()
    {
        StartScene(sceneNum + 1);
        PlayerPrefs.SetInt("CheckPoint", 0);
    }

    bool done;
    public Animator fadeAni;
    public void StartScene(int i)
    {

        if (done == true)
        {
            return;
        }
        done = true;
        StartCoroutine(DelayStartScene(i));
    }

    IEnumerator DelayStartScene(int i)
    {
        fadeAni.transform.gameObject.SetActive(true);
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadSceneAsync(i);
    }

    public void FadeOutIn()
    {
        StartCoroutine(FadeOutInState());
    }

    IEnumerator FadeOutInState()
    {
        fadeAni.transform.gameObject.SetActive(true);
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSecondsRealtime(1);
        fadeAni.SetBool("Fade", false);
    }
}
