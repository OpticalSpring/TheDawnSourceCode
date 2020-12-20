using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    public Animator fadeAni;
    public bool done;

    public GameObject main;
    public GameObject exit;
    public GameObject game;
    public GameObject chapter;

    public int stackUI;
    public Text continueText;

    public GameObject gameAni;
    public GameObject chapterAni;
    public bool optionState;

    public Animator camAni;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.SoundPlay(0, 7);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        fadeAni.SetBool("Fade", false);
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        switch (PlayerPrefs.GetInt("SceneNum"))
        {
            case 0:
                continueText.text = "[데이터 없음]";
                break;
            case 3:
                continueText.text = "[튜토리얼]";
                break;
            case 4:
                continueText.text = "[챕터 1 - 파트 " + PlayerPrefs.GetInt("CheckPoint") + "]";
                break;
            case 5:
                continueText.text = "[챕터 2 - 파트 " + PlayerPrefs.GetInt("CheckPoint") + "]";
                break;
            default:
                continueText.text = "[에러]";
                break;
        }
                
    }

    // Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionState == true)
            {
                return;
            }
            switch (stackUI)
            {
                case 1:
                    OffGamePlay();
                    break;
                case 2:
                    OffChapterUI();
                    break;
            }
        }
    }
    public void StartScene(int i)
    {

        if(done == true)
        {
            return;
        }
        done = true;
        StartCoroutine(DelayStartScene(i));
        SoundManager.instance.SoundPlay(8, 0);
    }

    public void Continue()
    {

        if (done == true)
        {
            return;
        }
        done = true;
        StartCoroutine(DelayContinueScene());
        SoundManager.instance.SoundPlay(8, 0);
    }
    public void CreditScene(int i)
    {

        if (done == true)
        {
            return;
        }
        done = true;
        StartCoroutine(DelayCreditScene(i));
        SoundManager.instance.SoundPlay(8, 0);
    }

    IEnumerator DelayStartScene(int i)
    {
        camAni.SetInteger("AniState", 2);
        yield return new WaitForSeconds(1);
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        if(i != 1)
        {

            PlayerPrefs.SetInt("CheckPoint", 0);
        }
        PlayerPrefs.SetInt("CPCount", 0);
        
        
        SceneManager.LoadSceneAsync(i);
    }

    IEnumerator DelayContinueScene()
    {
        camAni.SetInteger("AniState", 3);
        yield return new WaitForSeconds(1);
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        
        
        int s;
        if(PlayerPrefs.GetInt("SceneNum") == 0)
        {
            s = 2;
        }
        else
        {
            s = PlayerPrefs.GetInt("SceneNum");
        }
        Debug.Log(PlayerPrefs.GetInt("CheckPoint"));
        SceneManager.LoadSceneAsync(s);
    }
    IEnumerator DelayCreditScene(int i)
    {
       
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        if (i != 1)
        {

            PlayerPrefs.SetInt("CheckPoint", 0);
        }
        PlayerPrefs.SetInt("CPCount", 0);


        SceneManager.LoadSceneAsync(i);
    }
    public void OnExit()
    {
        SoundManager.instance.SoundPlay(8, 1);
        exit.SetActive(true);
    }

    public void OffExit()
    {
        SoundManager.instance.SoundPlay(8, 1);
        exit.SetActive(false);
    }

    public void ExitGame()
    {
        if (done == true)
        {
            return;
        }
        done = true;
        SoundManager.instance.SoundPlay(8, 1);
        StartCoroutine(DelayExitGame());
    }

    IEnumerator DelayExitGame()
    {
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    public void OnGamePlayUI()
    {
        camAni.SetInteger("AniState", 1);
        SoundManager.instance.SoundPlay(8, 1);
        main.SetActive(false);
        game.SetActive(true);
        stackUI = 1;
    }

    public void OnChapterUI()
    {

        SoundManager.instance.SoundPlay(8, 1);

        stackUI = 2;
        StartCoroutine(SetAni());
    }

    public void OffGamePlay()
    {
        camAni.SetInteger("AniState", 0);
        SoundManager.instance.SoundPlay(8, 1);
        main.SetActive(true);
        game.SetActive(false);
        stackUI = 0;
    }

    public void OffChapterUI()
    {
        GameObject.Find("Chapter_UI").GetComponent<Animator>().SetInteger("AniState", -1);
        SoundManager.instance.SoundPlay(8, 1);

        stackUI = 1;
        StartCoroutine(OffAni());
    }




   
   

    IEnumerator SetAni()
    {
        optionState = true;
        game.SetActive(false);
        gameAni.SetActive(true);
        gameAni.GetComponent<Animator>().SetInteger("AniState", 1);
        yield return new WaitForSecondsRealtime(0.95f);
        gameAni.SetActive(false);
        chapter.SetActive(true);
        optionState = false;
    }

    IEnumerator OffAni()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        optionState = true;
        chapter.SetActive(false);
        chapterAni.SetActive(true);
        chapterAni.GetComponent<Animator>().SetInteger("AniState", 1);
        yield return new WaitForSecondsRealtime(0.95f);
        chapterAni.SetActive(false);
        game.SetActive(true);
        optionState = false;
    }
}
