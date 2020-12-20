using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_2 : EventManager
{
    public GameObject[] eventObject;
    public GameObject[] gate;
    public GameObject[] spotPoint;
    public GameObject[] checkPoint;
    public GameObject[] cineObj;
    public GameObject boss;
    public GameObject[] door;
    public GameObject[] doorEff;
    public bool[] evtBool = new bool[13];
    void Start()
    {

        LoadPlayerPos();
        SoundManager.instance.SoundPlay(0, 3);
        SoundManager.instance.SoundPlay(0, 4);
        nextReady = true;
    }

    void LoadPlayerPos()
    {
        int num = PlayerPrefs.GetInt("CheckPoint");

        Player.instance.transform.position = checkPoint[num].transform.position;

        switch (num)
        {
            case 0:
                eventNumber = 0;
                eventNumber = 7;
                 break;
            case 1:
                eventNumber = 10;
                break;
            case 2:
                eventNumber = 12;
                break;
            case 3:
                eventNumber = 15;
                 break;
        }
    }
    Coroutine nowCorutine;
    // Update is called once per frame
    void Update()
    {
        if (nextReady == false)
        {
            return;
        }
        string cName = "Event_" + eventNumber.ToString();
        if (nowCorutine != null)
        {

            StopCoroutine(nowCorutine);
        }
        nowCorutine = StartCoroutine(cName);
        eventNumber++;
        nextReady = false;

    }
    public void EventTrigger(string evtName)
    {
        if (nowCorutine != null)
        {

            StopCoroutine(nowCorutine);
        }
        nowCorutine = StartCoroutine(evtName);
    }

    IEnumerator Event_0()
    {
        SaveState(0);
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_1()
    {
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_2()
    {
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_3()
    {
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_4()
    {
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_5()
    {
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_6()
    {
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_7()
    {
        PlayerUISystem.instance.SetObjectiveText(0, "");
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(0);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(300), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(1);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(301), 4);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(2);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(302), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(3);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(303), 3);
        yield return new WaitForSeconds(3);
        PlayerUISystem.instance.SetTitleUI(2, CSVManager.instance.LoadText(424));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(424));
        SetWave("W1", true);
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_8()
    {
        
        SoundManager.instance.VoicePlay(4);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(304), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(5);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(305), 4);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(6);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(306), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(7);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(307), 2);
        yield return new WaitForSeconds(2);

        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(422));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(422));
        PlayerUISystem.instance.StartMarkerUI(eventObject[0].transform.position + new Vector3(0, 1, 0));
        eventObject[0].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[0].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_9()
    {
        PlayerUISystem.instance.EndMarkerUI();
        yield return new WaitForSeconds(0);
        cineObj[1].SetActive(true);
        yield return new WaitForSeconds(0);
        cineObj[1].GetComponent<CinematicManager>().StartCinematic(4, true);
        door[0].SetActive(false);
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Cinematic;
        SoundManager.instance.SoundPlay(6, 2);
        yield return new WaitForSeconds(6.5f);
        doorEff[0].SetActive(true);
        doorEff[1].SetActive(true);
        yield return new WaitForSeconds(5.5f);
        cineObj[1].GetComponent<CinematicManager>().EndCinematic();
        door[0].SetActive(true);
        PlayerCameraSystem.instance.rotateValue = new Vector3(0, 0, 0);
        Player.instance.characterModel.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.instance.transform.position = checkPoint[1].transform.position;
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Hipfire;
        CheckPointManager.instance.InitSave();
        
        SoundManager.instance.VoicePlay(8);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(308), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(9);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(309), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(10);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(310), 3);
        yield return new WaitForSeconds(3);
        nextReady = true;
    }

    IEnumerator Event_10()
    {
        SaveState(1);
        PlayerUISystem.instance.SetTitleUI(2, CSVManager.instance.LoadText(420));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(420));
        PlayerUISystem.instance.StartMarkerUI(spotPoint[0].transform.position + new Vector3(0, 1, 0));

        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_11()
    {
        //SoundManager.instance.SoundStop(0, 3);
        //SoundManager.instance.SoundForceStop(0, 4);
        PlayerUISystem.instance.EndMarkerUI();
        CheckPointManager.instance.InitSave();
        yield return new WaitForSeconds(0);
        cineObj[0].SetActive(true);
        yield return new WaitForSeconds(0);
        cineObj[0].GetComponent<CinematicManager>().StartCinematic(3);
        SoundManager.instance.SoundPlay(6, 3);
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Cinematic;
        Player.instance.ultimateGage = 0;
        yield return new WaitForSeconds(14.5f); //19f
        ScreenBlur_script.instance.SetBlur(15, 24, 1, 3);  
        yield return new WaitForSeconds(1f);
        cineObj[0].GetComponent<CinematicManager>().EndCinematic();
        PlayerCameraSystem.instance.rotateValue = new Vector3(0, 0, 0);
        Player.instance.characterModel.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.instance.transform.position = checkPoint[2].transform.position;
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Hipfire;
        CheckPointManager.instance.InitSave();
        MekaBoss.instance.bossAnimator.SetInteger("AniState",0);
        nextReady = true;

    }

    IEnumerator Event_12()
    {
        SaveState(2);
        boss.GetComponent<MekaBoss>().bossAnimator.SetInteger("AniState", 0);
        PlayerCameraSystem.instance.SetBossBattle();
        PlayerUISystem.instance.SetTitleUI(2, CSVManager.instance.LoadText(421));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(421));
        gate[0].SetActive(true);
        SoundManager.instance.SoundStop(0, 3);
        SoundManager.instance.SoundForceStop(0, 4);
        yield return new WaitForSeconds(2);
        SoundManager.instance.SoundPlay(0, 5);
        yield return new WaitForSecondsRealtime(3.5f);
        PlayerUISystem.instance.bossUI.SetActive(true);
        boss.GetComponent<MekaBossAI>().enabled = true;
        SoundManager.instance.SoundPlay(0, 6);
        yield return new WaitForSecondsRealtime(2f);
        boss.GetComponent<MekaBoss>().bossFSM = BossStatus.MBossFSM.Idle;
        PlayerUISystem.instance.bossUI.GetComponent<BossUISystem>().aniState = 1;
    }

    IEnumerator Event_13()
    {
        GameManager.instance.FadeOutIn();
        yield return new WaitForSeconds(1);
        PlayerUISystem.instance.EndMarkerUI();
        yield return new WaitForSeconds(0);
        cineObj[2].SetActive(true);
        yield return new WaitForSeconds(0);
        cineObj[2].GetComponent<CinematicManager>().StartCinematic(5, true);
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Cinematic;
        
        yield return new WaitForSeconds(10);
        cineObj[2].GetComponent<CinematicManager>().EndCinematic();
        SoundManager.instance.SoundStop(0, 6);
        gate[1].SetActive(true);
        gate[2].SetActive(true);
        gate[3].SetActive(false);
        PlayerCameraSystem.instance.rotateValue = new Vector3(0, 0, 0);
        Player.instance.characterModel.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.instance.transform.position = checkPoint[3].transform.position;
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Hipfire;
        CheckPointManager.instance.InitSave();
        SoundManager.instance.VoicePlay(11);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(311), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(12);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(312), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(13);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(313), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(14);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(314), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(15);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(315), 3);
        yield return new WaitForSeconds(3);
        gate[4].SetActive(false);
        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(423));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(423));
        PlayerUISystem.instance.StartMarkerUI(eventObject[1].transform.position + new Vector3(0, 1, 0));
        eventObject[1].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[1].GetComponent<HackingInteraction>().Ready();
        
    }

    IEnumerator Event_14()
    {
        SoundManager.instance.VoicePlay(16);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(316), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(17);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(317), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator Event_15()
    {
        PlayerUISystem.instance.EndMarkerUI();
        yield return new WaitForSeconds(0);
        cineObj[3].SetActive(true);
        yield return new WaitForSeconds(0);
        cineObj[3].GetComponent<CinematicManager>().StartCinematic(6);
        SoundManager.instance.SoundPlay(0, 11);
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Cinematic;
        eventObject[2].SetActive(true);
        yield return new WaitForSeconds(30);
        GameManager.instance.GoTitleScene();
    }

    void W1()
    {
        SpawnManager.instance.SpawnEnemy(0, 2, new Vector3(64, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(63, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(62, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(61, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(60, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(65, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(66, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(67, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(68, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(69, 4.5f, -80));
        SpawnManager.instance.SpawnEnemy(1, 2, new Vector3(64, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(63, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(62, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(61, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(60, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(65, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(66, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(67, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(68, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(69, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(64, 4.5f, -77));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(63, 4.5f, -77));
        
        SpawnManager.instance.status.enemySpawnCount = 25;
        SpawnManager.instance.status.enemyMaxCount = 20;
        SpawnManager.instance.status.unitCountMax[0] = 8;
        SpawnManager.instance.status.unitCountMax[1] = 8;
        SpawnManager.instance.status.unitCountMax[2] = 6;
        SpawnManager.instance.status.unitCountMax[3] = 6;
        SpawnManager.instance.status.unitRatio[0] = 30;
        SpawnManager.instance.status.unitRatio[1] = 35;
        SpawnManager.instance.status.unitRatio[2] = 15;
        SpawnManager.instance.status.unitRatio[3] = 20;
    }

    IEnumerator BossP1()//0
    {
        yield return new WaitForSeconds(1);
        SoundManager.instance.VoicePlay(18);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(318), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(19);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(319), 5);
        yield return new WaitForSeconds(6);
        SoundManager.instance.VoicePlay(20);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(320), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossP21()//1
    {
        yield return new WaitForSeconds(1);
        SoundManager.instance.VoicePlay(21);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(321), 3);
        yield return new WaitForSeconds(3);
       
    }

    IEnumerator BossP22()//2
    {
        
        SoundManager.instance.VoicePlay(23);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(323), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossP3()//3
    {
        yield return new WaitForSeconds(11);
        SoundManager.instance.VoicePlay(24);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(324), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(25);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(325), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossP4()//4
    {
        yield return new WaitForSeconds(1);
        SoundManager.instance.VoicePlay(26);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(326), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossP5()//5
    {
        yield return new WaitForSeconds(1);
        SoundManager.instance.VoicePlay(27);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(327), 3);
        PlayerUISystem.instance.SetTitleUI(2, CSVManager.instance.LoadText(425));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(425));
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(29);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(329), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossP6()//6
    {
        yield return new WaitForSeconds(1);
        SoundManager.instance.VoicePlay(30);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(330), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossP7()//7
    {
        yield return new WaitForSeconds(1);
       
    }

    IEnumerator BossP8()//8
    {
        yield return new WaitForSeconds(1);
        SoundManager.instance.VoicePlay(32);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(332), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossH1()//9
    {
        SoundManager.instance.VoicePlay(33);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(333), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(34);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(334), 5);
        yield return new WaitForSeconds(6);
        SoundManager.instance.VoicePlay(35);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(335), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(36);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(336), 5);
        yield return new WaitForSeconds(5);
    }

    IEnumerator BossH2()//10
    {
        SoundManager.instance.VoicePlay(37);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(337), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(38);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(338), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(39);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(339), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(40);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(340), 3);
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossH3()//11
    {
        SoundManager.instance.VoicePlay(41);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(341), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(42);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(342), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(43);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(343), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(44);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(344), 3);
        yield return new WaitForSeconds(3);
    }
    IEnumerator BossH4()//12
    {
        SoundManager.instance.VoicePlay(45);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(345), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(46);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(346), 3);
        yield return new WaitForSeconds(3);
    }
}
