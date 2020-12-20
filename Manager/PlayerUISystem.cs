using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUISystem : MonoBehaviour
{
    public static PlayerUISystem instance;
    private void Awake()
    {
        instance = this;
    }
    public Player player;
    public bool normalLock;
    public GameObject[] crossHair;
    public GameObject[] aimIcon;
    public double shotTime;
    public double hitTime;
    public double headTime;
    public double killTime;
    public GameObject ammoObject;
    public Text tAmmo;
    public GameObject iAmmo;
    public Image reloadImage;
    public Image[] iDodge;
    public GameObject HPGroup;
    public Image HPBar;
    public Image HPBarHit;
    public GameObject HPEff;
    public Text HPMax;
    public Text HPNow;
    public Image timeStopIcon;
    public Image timeStopBG;
    public Image timeStopEff;
    public Image timeRecallIcon;
    public Image timeRecallBG;
    public Image timeRecallEff;
    public Image ultimateIcon;
    public Image ultimateBG;
    public GameObject ultimateEff;
    public Image ultimateGage;
    public GameObject ultimateGageEff;


    public Text dialogText;
    public double textTime;
    public Animator[] spaceAnimator;

    public GameObject assult;
    public GameObject[] assultText;
    public bool waveState;
    public GameObject marker;
    public GameObject markerPoint;
    public Text markerText;
    public Vector3 markerTargetPos;
    public bool markerState;
    public GameObject[] titleObj;
    public Text[] titleSubText;
    public GameObject[] objectiveText;
    public bool objectiveOpen;
    public GameObject objectiveObj;

    public Vector4 ammoColor;

    public GameObject saveIcon;
    public GameObject letterbox;

    public GameObject tutoObj;
    public Text tutoText;
    public GameObject SpotGroup;

    public GameObject popupTimeRecall;
    public GameObject bossUI;
    

    //Start is called before the first frame update
    void Start()
    {
        ammoColor = iAmmo.transform.GetChild(0).gameObject.GetComponent<Image>().color;
    }

    //Update is called once per frame
    void Update()
    {
        
        SetUI();
        CrossHairSet();
        AssultTextPosUpdate();
        MarkerUI();
        CheckDelayTime();
        ChangeUIAlpha();
    }

    void CheckDelayTime()
    {
        if (shotTime > 0)
        {
            shotTime -= Time.deltaTime;
            if (crossHair[0].activeSelf == true)
                crossHair[0].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Shot", true);
            if (crossHair[1].activeSelf == true)
                crossHair[1].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Shot", true);
        }
        else
        {
            if (crossHair[0].activeSelf == true)
                crossHair[0].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Shot", false);
            if (crossHair[1].activeSelf == true)
                crossHair[1].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Shot", false);
        }
        if (hitTime > 0)
        {
            hitTime -= Time.deltaTime;
        }
        if (headTime > 0)
        {
            headTime -= Time.deltaTime;
        }
        if (killTime > 0)
        {
            killTime -= Time.deltaTime;
        }
        if (textTime > 0)
        {
            textTime -= Time.deltaTime;
        }
        else
        {
            dialogText.text = "";
        }
    }

    public void SetDialogText(string iText, double iTime)
    {
        textTime = iTime;
        dialogText.text = iText;
    }

    float markerScale = 1;
    float markerScaleNow = 1;
    Vector3[] mainGroupPos = new Vector3[2];
    public Vector3[] statedHPPos = new Vector3[2];
    public Vector3[] statedAMMOPos = new Vector3[2];
    void CrossHairSet()
    {
        mainGroupPos[0] = statedHPPos[0];
        mainGroupPos[1] = statedAMMOPos[0];
        if (player.PlayerMode == PlayerStatus.EPlayerMode.TacticsMode)
        {
            if (player.PlayerFSM == PlayerStatus.EPlayerFSM.Hipfire)
            {
                markerScale = 1;
                crossHair[0].SetActive(true);
                crossHair[1].SetActive(false);
                CrossHairLock();
                CrossHairSet(0);
            }
            else if (player.PlayerFSM == PlayerStatus.EPlayerFSM.Shoulderfire)
            {
                markerScale = 1.5f;
                crossHair[0].SetActive(false);
                crossHair[1].SetActive(true);
                CrossHairSet(1);
                mainGroupPos[0] = statedHPPos[1];
                mainGroupPos[1] = statedAMMOPos[1];
            }
            else
            {
                markerScale = 1;
                CrossHairFalse();
            }
        }
        else
        {
            markerScale = 1;
            CrossHairFalse();
        }
        markerScaleNow = Mathf.Lerp(markerScaleNow, markerScale, Time.deltaTime * 10);
        HPGroup.transform.localScale = Vector3.one * markerScaleNow;
        HPGroup.transform.localPosition = Vector3.Lerp(HPGroup.transform.localPosition, mainGroupPos[0], Time.deltaTime * 10);
        ammoObject.transform.localScale = Vector3.one * markerScaleNow;
        ammoObject.transform.localPosition = Vector3.Lerp(ammoObject.transform.localPosition, mainGroupPos[1], Time.deltaTime * 10);
    }

    void CrossHairLock()
    {
        if (normalLock == true)
        {
            crossHair[0].transform.localScale = Vector3.Lerp(crossHair[0].transform.localScale, Vector3.one, Time.deltaTime * 10);
        }
        else
        {
            crossHair[0].transform.localScale = Vector3.Lerp(crossHair[0].transform.localScale, Vector3.one * 0.8f, Time.deltaTime * 10);
        }
    }

    void CrossHairFalse()
    {
        crossHair[0].SetActive(false);
        crossHair[1].SetActive(false);
    }

    void CrossHairSet(int i)
    {
        if (headTime <= 0)
        {
            crossHair[i].transform.GetChild(2).gameObject.SetActive(false);
            if (hitTime <= 0)
            {
                crossHair[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                crossHair[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            crossHair[i].transform.GetChild(2).gameObject.SetActive(true);
        }
        if (killTime <= 0)
        {
            crossHair[i].transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            crossHair[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void ActFlashSpace(int num)
    {
        //StartCoroutine(FlashSpace(num));
    }

    IEnumerator FlashSpace(int num)
    {
        yield return new WaitForSecondsRealtime(0.05f);
        spaceAnimator[num].SetInteger("Flash", 1);
        yield return new WaitForSecondsRealtime(0.1f);
        spaceAnimator[num].SetInteger("Flash", 0);
    }
    float hpbarFill;
    float hpbarFillNow;
    void SetUI()
    {
        //tFSM.text = "현재 상태 : " + player.PlayerFSM;
        hpbarFill = (float)player.HP_Point / player.HP_PointMax;
        hpbarFillNow = Mathf.Lerp(hpbarFillNow, hpbarFill, Time.deltaTime);
        HPBar.fillAmount = hpbarFill;
        HPBarHit.fillAmount = hpbarFillNow;
        //HPNow.text = player.HP_Point + "";
        //HPMax.text = player.HP_PointMax + "";
        if(hpbarFill <= 0.5f)
        {
            HPEff.SetActive(true);
            HPEff.GetComponent<Image>().fillAmount = hpbarFill;
        }
        else
        {
            HPEff.SetActive(false);
        }

        tAmmo.text = "" + player.ammo;
        reloadImage.fillAmount = (float)player.reloadDelayTimeNow / (float)player.reloadDelayTime;
        for (int i = 0; i < player.dodgeStack; i++)
        {
            iDodge[i].fillAmount = 1;
            // iDodge[i + 5].fillAmount = 1;
        }
        for (int i = player.dodgeStack + 1; i < 5; i++)
        {
            iDodge[i].fillAmount = 0;
        }
        //for (int i = player.dodgeStack; i < 5; i++)
        //{
        //    iDodge[i + 5].fillAmount = 0;
        //}
        if (player.dodgeStack < 5)
        {
            //iDodge[player.dodgeStack].color = new Vector4(150f / 255, 150f / 255, 150f / 255, 1);
            iDodge[player.dodgeStack].fillAmount = (float)((player.dodgeDelayTime - player.dodgeDelayTimeNow) / player.dodgeDelayTime);
        }
        for (int i = 0; i < 5; i++)
        {
            iDodge[i].GetComponent<Image>().color = ammoColor;
        }
        if (player.dodgeStack != 5)
        {
            iDodge[player.dodgeStack].GetComponent<Image>().color = new Vector4(150f / 255, 150f / 255, 150f / 255, 1);
        }



        if (player.timeRecallDelayTimeNow > 0)
        {
            timeRecallBG.fillAmount = (float)((player.timeRecallDelayTime - player.timeRecallDelayTimeNow) / player.timeRecallDelayTime);
            timeRecallIcon.color = new Vector4(1, 1, 1, 1);
            //timeRecallFrame.color = new Vector4(183 / 255f, 183 / 255f, 183 / 255f, 1);
            timeRecallBG.color = new Vector4(150f / 255, 150f / 255, 150f / 255, 150f / 255);
            timeRecallEff.enabled = false;
            //timeRecallIcon.gameObject.GetComponent<Animator>().SetInteger("AniState", 0);
        }
        else
        {
            //timeRecallIcon.gameObject.GetComponent<Animator>().SetInteger("AniState", 1);
            timeRecallBG.fillAmount = 1;
            timeRecallIcon.color = new Vector4(1, 1, 1, 120f / 255);
            //timeRecallFrame.color = new Vector4(1,1,1, 1);
            timeRecallBG.color = new Vector4(148f / 255, 0f / 255, 255f / 255, 40f / 255);
            timeRecallEff.enabled = true;
        }

        if (player.timeStopFieldDelayTimeNow > 0)
        {
            timeStopBG.fillAmount = (float)((player.timeStopFieldDelayTime - player.timeStopFieldDelayTimeNow) / player.timeStopFieldDelayTime);
            timeStopIcon.color = new Vector4(1, 1, 1, 1);
            //timeStopFrame.color = new Vector4(183 / 255f, 183 / 255f, 183 / 255f, 1);
            timeStopBG.color = new Vector4(150f / 255, 150f / 255, 150f / 255, 150f / 255);
            timeStopEff.enabled = false;
            //timeStopIcon.gameObject.GetComponent<Animator>().SetInteger("AniState", 0);
        }
        else
        {
            //timeStopIcon.gameObject.GetComponent<Animator>().SetInteger("AniState", 1);
            timeStopBG.fillAmount = 1;
            timeStopIcon.color = new Vector4(1, 1, 1, 120f / 255);
            //timeStopFrame.color = new Vector4(1,1,1, 1);
            timeStopBG.color = new Vector4(148f / 255, 0f / 255, 255f / 255, 40f / 255);
            timeStopEff.enabled = true;
        }

        if (player.ultimateGage < player.ultimateGageMax)
        {
            ultimateGage.color = new Vector4(212f / 255, 212f / 255, 212f / 255, 1);
            ultimateGage.fillAmount = ((float)player.ultimateGage / player.ultimateGageMax);
            ultimateBG.gameObject.SetActive(false);
            ultimateEff.GetComponent<Animator>().SetInteger("AniState", 0);
            ultimateGageEff.gameObject.SetActive(false);
            //ultimateIcon.color = new Vector4(183 / 255f, 183 / 255f, 183 / 255f, 1);
        }
        else
        {
            ultimateGage.color = new Vector4(130f / 255, 0, 195f / 255, 200f / 255);
            ultimateGage.fillAmount = 1;
            ultimateBG.gameObject.SetActive(true);
            ultimateEff.GetComponent<Animator>().SetInteger("AniState", 1);
            ultimateGageEff.gameObject.SetActive(true);
            //ultimateIcon.color = new Vector4(1,1,1, 1);
        }

        if (player.timeStopFieldState == true)
        {
            aimIcon[0].SetActive(true);
        }
        else
        {
            aimIcon[0].SetActive(false);
        }

        if (objectiveOpen == true)
        {
            objectiveObj.GetComponent<Animator>().SetBool("Open", true);
        }
        else
        {
            objectiveObj.GetComponent<Animator>().SetBool("Open", false);
        }
    }

    public void SetTab()
    {
        if (objectiveOpen == true)
        {
            objectiveOpen = false;
        }
        else
        {
            objectiveOpen = true;
        }
    }



    public void SetShot()
    {
        shotTime = 0.1f;
    }

    public void SetHit()
    {
        hitTime = 0.3f;
        SoundManager.instance.RandomPlay(5, 1, 3, 0.5f);
        SoundManager.instance.RandomPlay(5, 4, 5, 0.5f);
    }

    public void SetHead()
    {
        headTime = 0.3f;
        SoundManager.instance.SoundPlay(5, 8);
    }

    public void SetKill()
    {
        killTime = 0.3f;
        SoundManager.instance.SoundPlay(5, 9);
    }

    public void StartWaveUI()
    {
        if (waveState == true)
        {
            return;
        }
        waveState = true;
        assult.SetActive(true);
        assult.GetComponent<Animator>().SetInteger("AssultState", 1);
        for (int i = 0; i < assultText.Length; i++)
        {
            assultText[i].transform.localPosition = new Vector3((i + 2) * 300, 0, 0);

        }
    }

    void AssultTextPosUpdate()
    {
        if (waveState == false)
        {
            return;
        }
        assult.GetComponent<Animator>().SetInteger("AssultState", 1);
        for (int i = 0; i < assultText.Length; i++)
        {
            assultText[i].transform.localPosition += new Vector3(-Time.deltaTime * 50, 0, 0);
            if (assultText[i].transform.localPosition.x < -600)
            {
                assultText[i].transform.localPosition = new Vector3(600, 0, 0);
            }
        }
    }

    public void EndWaveUI()
    {
        if (waveState == false)
        {
            return;
        }
        StartCoroutine(EndWave());
    }

    IEnumerator EndWave()
    {
        waveState = false;
        assult.GetComponent<Animator>().SetInteger("AssultState", 0);
        yield return new WaitForSecondsRealtime(1f);
        assult.SetActive(false);
    }

    public void StartMarkerUI(Vector3 pos)
    {
        markerState = true;
        markerTargetPos = pos;
    }

    public void EndMarkerUI()
    {
        markerState = false;
    }

    void MarkerUI()
    {
        if (markerState == true)
        {
            marker.SetActive(true);
            //거리를 표기한다.
            Vector3 newMarkerPos = Camera.main.WorldToScreenPoint(markerTargetPos);
            float dis = Vector3.Distance(Player.instance.transform.position, markerTargetPos);
            markerText.text = (int)dis + " M";
            //목표가 카메라 안에 있을 경우
            if (newMarkerPos.z > 0)
            {
                markerText.gameObject.transform.localPosition = new Vector3(0, -51.4f, 0);
                markerPoint.SetActive(false);
                if (newMarkerPos.x < 100)
                {
                    newMarkerPos.x = 100;
                }
                else if (newMarkerPos.x > Screen.width - 100)
                {
                    newMarkerPos.x = Screen.width - 100;
                }

                if (newMarkerPos.y < 100)
                {
                    newMarkerPos.y = 100;
                }
                else if (newMarkerPos.y > Screen.height - 100)
                {
                    newMarkerPos.y = Screen.height - 100;
                }
            }//목표가 카메라 범위 밖에 있을 경우
            else
            {

                if (newMarkerPos.x < 100)
                {
                    newMarkerPos.x = 100;
                }
                else if (newMarkerPos.x > Screen.width - 100)
                {
                    newMarkerPos.x = Screen.width - 100;
                }
                else
                {
                    if (newMarkerPos.y < Screen.height / 2)
                    {
                        newMarkerPos.y = 100;
                    }
                    else
                    {
                        newMarkerPos.y = Screen.height - 100;
                    }
                }

                if (newMarkerPos.y < 100)
                {
                    newMarkerPos.y = 100;
                }
                else if (newMarkerPos.y > Screen.height - 100)
                {
                    newMarkerPos.y = Screen.height - 100;
                }
                newMarkerPos.x = Screen.width - newMarkerPos.x;
                newMarkerPos.y = Screen.height - newMarkerPos.y;

                //화살표 방향 지정
                markerPoint.SetActive(true);
                float dy = markerPoint.transform.position.y - Screen.height / 2;
                float dx = markerPoint.transform.position.x - Screen.width / 2;

                float rotateDegree = Mathf.Atan2(-dx, dy) * Mathf.Rad2Deg;

                markerPoint.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
                if (newMarkerPos.y < Screen.height / 2)
                {
                    markerText.gameObject.transform.localPosition = new Vector3(0, 51.4f, 0);
                }
                else
                {
                    markerText.gameObject.transform.localPosition = new Vector3(0, -51.4f, 0);
                }
            }
            marker.transform.position = newMarkerPos;
        }
        else
        {
            marker.SetActive(false);
        }
    }
    float uiAlphaValue;
    float uiAlphaValueNow;
    void ChangeUIAlpha()
    {
        if(Player.instance.PlayerMode == PlayerStatus.EPlayerMode.NonCombatMode)
        {
            uiAlphaValue = 0;
        }
        else
        {
            float cVlaue = Mathf.Abs(PlayerCameraSystem.instance.rotateValue.x);
            if (cVlaue < 20)
            {
                uiAlphaValue = 1;
            }
            else if (cVlaue < 50)
            {
                uiAlphaValue = 1 - ((cVlaue - 20) / 30f);
            }
            else
            {
                uiAlphaValue = 0;
            }
        }
        uiAlphaValueNow = Mathf.MoveTowards(uiAlphaValueNow, uiAlphaValue, Time.fixedDeltaTime * 2);

        for (int i = 0; i < HPGroup.transform.childCount; i++)
        {
            Vector4 aCol;
            if (HPGroup.transform.GetChild(i).gameObject.GetComponent<Image>())
            {
                aCol = HPGroup.transform.GetChild(i).gameObject.GetComponent<Image>().color;
                aCol.w = uiAlphaValueNow;
                HPGroup.transform.GetChild(i).gameObject.GetComponent<Image>().color = aCol;
            }
            if (HPGroup.transform.GetChild(i).gameObject.GetComponent<Text>())
            {
                aCol = HPGroup.transform.GetChild(i).gameObject.GetComponent<Text>().color;
                aCol.w = uiAlphaValueNow;
                HPGroup.transform.GetChild(i).gameObject.GetComponent<Text>().color = aCol;
            }
        }
        for (int i = 0; i < ammoObject.transform.childCount; i++)
        {
            Vector4 aCol;
            if (ammoObject.transform.GetChild(i).gameObject.GetComponent<Image>())
            {
                aCol = ammoObject.transform.GetChild(i).gameObject.GetComponent<Image>().color;
                aCol.w = uiAlphaValueNow;
                ammoObject.transform.GetChild(i).gameObject.GetComponent<Image>().color = aCol;
            }
            if (ammoObject.transform.GetChild(i).gameObject.GetComponent<Text>())
            {
                aCol = ammoObject.transform.GetChild(i).gameObject.GetComponent<Text>().color;
                aCol.w = uiAlphaValueNow;
                ammoObject.transform.GetChild(i).gameObject.GetComponent<Text>().color = aCol;
            }
        }
    }

    public void SetTitleUI(int num, string iText)
    {

        titleSubText[num].text = iText;
        StartCoroutine(DelayTitleOff(num));
    }


    IEnumerator DelayTitleOff(int num)
    {
        titleObj[num].SetActive(true);
        yield return new WaitForSeconds(1f);
        SoundManager.instance.SoundPlay(8, 3);
        yield return new WaitForSeconds(3f);
        titleObj[num].SetActive(false);
    }



    public void SetObjectiveText(int num, string iText)
    {
        if (iText == "")
        {
            objectiveText[num].SetActive(false);
        }
        else
        {
            objectiveObj.SetActive(false);
            objectiveObj.SetActive(true);
            objectiveText[num].SetActive(true);
            objectiveText[num].transform.GetChild(0).gameObject.GetComponent<Text>().text = iText;
        }
    }

    public void SetObjectiveIcon(int num)
    {
        for (int i = 0; i < 3; i++)
        {
            objectiveText[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        objectiveText[num].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    public void ActSaveIcon()
    {
        saveIcon.SetActive(true);
        StartCoroutine(EndSaveIcon());
    }

    IEnumerator EndSaveIcon()
    {
        yield return new WaitForSecondsRealtime(3);
        saveIcon.SetActive(false);
    }

    public void SetLetterBox()
    {
        letterbox.GetComponent<Animator>().SetInteger("AniState", 1);
    }
    public void EndLetterBox()
    {
        letterbox.GetComponent<Animator>().SetInteger("AniState", 0);
        
    }

    public void SetTutoUI(string iText)
    {
        tutoObj.SetActive(true);
        tutoText.text = iText;
    }
    public void EndTutoUI()
    {
        tutoObj.SetActive(false);
    }

    int spotCount;
    public void SetSpotUI(Vector3 pos)
    {
        spotCount++;
        if(spotCount >= SpotGroup.transform.childCount)
        {
            spotCount = 0;
        }
        SpotGroup.transform.GetChild(spotCount).gameObject.SetActive(true);
        SpotGroup.transform.GetChild(spotCount).gameObject.GetComponent<SpotAgent>().markerTargetPos = pos;
        SpotGroup.transform.GetChild(spotCount).gameObject.GetComponent<SpotAgent>().aliveTime = 2;
    }
}
