using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongHacking : MonoBehaviour
{
    public double hackingTime;
    public double hackingTimeNow;
    public float stopTime;
    public int stateNum;
    public GameObject longUI;
    public GameObject stopUI;
    public Image longGageBar;
    public Image stopGageBar;
    public Text longText;
    public Text stopText;
    public Image longIcon;
    public Image stopIcon;
    public int minute;
    public int second;
    public double iTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateNum)
        {
            case 0:
                longUI.SetActive(false);
                stopUI.SetActive(false);
                break;
            case 1:
                hackingTimeNow += Time.deltaTime;
                TimeRemaining();
                longIcon.gameObject.transform.localScale = Vector3.one * (float)(hackingTimeNow / hackingTime);
                longIcon.color = new Vector4(1, 1, 1, 1-(float)(hackingTimeNow / hackingTime)*0.9f);
                longText.text = minute.ToString("D2") + " : " + second.ToString("D2");
                longGageBar.fillAmount = (float)(hackingTimeNow / hackingTime);
                longUI.SetActive(true);
                stopUI.SetActive(false);
                if(hackingTimeNow >= hackingTime)
                {
                    stateNum = 4;
                    gameObject.GetComponent<DoneEvent>().ActEvent_2();
                    longUI.SetActive(false);
                    stopUI.SetActive(false);
                }
                break;
            case 2:
                stopIcon.gameObject.transform.localScale = Vector3.one * (float)(hackingTimeNow / hackingTime);
                stopIcon.color = new Vector4(1, 1, 1, 1 - (float)(hackingTimeNow / hackingTime) * 0.9f);
                stopGageBar.fillAmount = (float)(hackingTimeNow / hackingTime);
                stopText.text = minute.ToString("D2") + " : " + second.ToString("D2");
                longUI.SetActive(false);
                stopUI.SetActive(true);
                break;
            case 3:
                if(iTime > 0)
                {
                    iTime -= Time.deltaTime;
                }
                else
                {
                    stateNum = 2;
                }
                longUI.SetActive(false);
                stopUI.SetActive(false);
                break;
            case 4:
                
                break;
            default:
                break;
        }
    }

    void TimeRemaining()
    {
        int allTime = (int)(hackingTime - hackingTimeNow);
        minute=allTime / 60;
        second = allTime - minute * 60;
    }

    public void AutoStop()
    {
        StartCoroutine(DelayStop());
    }

    IEnumerator DelayStop()
    {
        yield return new WaitForSeconds(stopTime);
        if(stateNum == 1)
        {
            stateNum = 2;
            gameObject.GetComponent<DoneEvent>().ActEvent_2();
        }
    }
}
