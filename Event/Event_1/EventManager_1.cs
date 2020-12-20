using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_1 : EventManager
{
    public GameObject[] eventObject;
    public GameObject[] gate;
    public GameObject[] spotPoint;
    public GameObject[] checkPoint;
    public GameObject[] door;
    public GameObject[] hack;
    public GameObject[] cineObj;
    // Start is called before the first frame update
    void Start()
    {

        LoadPlayerPos();
        SoundManager.instance.SoundPlay(0, 1);
        SoundManager.instance.SoundPlay(0, 2);
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
                PlayerCameraSystem.instance.rotateValue = new Vector3(0, 90, 0);
                break;
            case 1:
                eventNumber = 8;
                PlayerCameraSystem.instance.rotateValue = new Vector3(0, 90, 0);
                break;
            case 2:
                eventNumber = 17;
                PlayerCameraSystem.instance.rotateValue = new Vector3(0, 270, 0);
                break;
            case 3:
                eventNumber = 31;
                PlayerCameraSystem.instance.rotateValue = new Vector3(0, 90, 0);
                break;
            case 4:
                eventNumber = 45;
                PlayerCameraSystem.instance.rotateValue = new Vector3(0, 180, 0);
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

    IEnumerator OpenDoor()
    {
        double oTime = 0;
        while (oTime < 2)
        {
            oTime += Time.deltaTime;
            door[0].transform.localPosition = Vector3.Lerp(door[0].transform.localPosition, new Vector3(-1.5f, 0, 0.3f), Time.deltaTime);
            door[1].transform.localPosition = Vector3.Lerp(door[1].transform.localPosition, new Vector3(1.5f, 0, 0.3f), Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator CloseDoor()
    {
        double oTime = 0;
        while (oTime < 2)
        {
            oTime += Time.deltaTime;
            door[0].transform.localPosition = Vector3.Lerp(door[0].transform.localPosition, new Vector3(-0.6f, 0, 0), Time.deltaTime);
            door[1].transform.localPosition = Vector3.Lerp(door[1].transform.localPosition, new Vector3(0.6f, 0, 0), Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

    
    IEnumerator Event_0()
    {
        PlayerUISystem.instance.SetObjectiveText(0, "");
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(0);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(200), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(1);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(201), 3);
        yield return new WaitForSeconds(4);
        //SoundManager.instance.VoicePlay(2);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>주변에 뭔가 보여? 운 좋으면 안 들키고 지나갈 수도..", 5);
        //yield return new WaitForSeconds(5);
        PlayerUISystem.instance.StartMarkerUI(spotPoint[0].transform.position);
        nextReady = true;
    }

    IEnumerator Event_1()
    {
        yield return new WaitForSeconds(1);

    }
    IEnumerator Event_2()
    {
        PlayerUISystem.instance.EndMarkerUI();
        //SoundManager.instance.VoicePlay(3);
        //PlayerUISystem.instance.SetDialogText("<color=#008000ff>[맥스] </color>이런, 들켰네요. 총 쏘는 법은 알고 있죠?", 3);
        //yield return new WaitForSeconds(4);
        SetWave("W1", false);
        SoundManager.instance.VoicePlay(2);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(202), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(3);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(203), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(4);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(204), 5);
        yield return new WaitForSeconds(5);
    }
    IEnumerator Event_3()
    {
        SoundManager.instance.VoicePlay(5);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(205), 4);
        yield return new WaitForSeconds(5);
        //SoundManager.instance.VoicePlay(8);
        //PlayerUISystem.instance.SetDialogText("<color=#008000ff>[맥스] </color>어···.  지금 루트를 검색 중인데···. ", 3);
        //yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(6);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(206), 7);
        yield return new WaitForSeconds(8);
        //SoundManager.instance.VoicePlay(10);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>그게 뭔데?", 1);
        //yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(7);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(207), 3);
        yield return new WaitForSeconds(4);
        //SoundManager.instance.VoicePlay(12);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>젠장, 일단 CCTV를 전부 제거해보자.", 4);
        //yield return new WaitForSeconds(4);
        nextReady = true;
    }
    IEnumerator Event_4()
    {
        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(410));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(410));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.StartMarkerUI(eventObject[0].transform.position + new Vector3(0, 1, 0));
        eventObject[0].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[0].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(1);
    }
    IEnumerator Event_5()
    {
        SoundManager.instance.VoicePlay(8);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(208), 1);
        yield return new WaitForSeconds(2);
    }
    IEnumerator Event_6()
    {
        PlayerUISystem.instance.EndMarkerUI();
        //SoundManager.instance.VoicePlay(14);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>음침하네, 이런 곳에 CCTV를 숨겨놓다니.", 4);
        //yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(9);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(209), 3);
        yield return new WaitForSeconds(3);
        nextReady = true;
    }
    IEnumerator Event_7()
    {
        SetWave("W2", true);
        yield return new WaitForSeconds(5);
    }
    IEnumerator Event_8()
    {
        SaveState(1);
        SoundManager.instance.VoicePlay(10);
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(411));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(210), 2);
        yield return new WaitForSeconds(3);
        //SoundManager.instance.VoicePlay(17);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>뭔갈 찾아낼 때마다 이렇게 몰려오는 건 아니겠지?", 2);
        //yield return new WaitForSeconds(3);
        //SoundManager.instance.VoicePlay(18);
        //PlayerUISystem.instance.SetDialogText("<color=#008000ff>[맥스] </color>음.. 긍정적으로 생각해요.", 2);
        //yield return new WaitForSeconds(3);
        //SoundManager.instance.VoicePlay(19);
        //PlayerUISystem.instance.SetDialogText("<color=#008000ff>[맥스] </color>축하의 의미로 다들 반겨주러 오는 거라고. 어때요?", 3);
        //yield return new WaitForSeconds(4);
        //SoundManager.instance.VoicePlay(20);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>끔찍해.", 1);
        //yield return new WaitForSeconds(2);
        gate[0].SetActive(false);
        PlayerUISystem.instance.StartMarkerUI(spotPoint[1].transform.position);
    }
    IEnumerator Event_9()
    {
        PlayerUISystem.instance.EndMarkerUI();
        gate[0].SetActive(true);
        CheckPointManager.instance.InitSave();
        SetWave("W3", false);
        SoundManager.instance.VoicePlay(11);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(211), 2);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(12);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(212), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(13);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(213), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(14);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(214), 4);
        yield return new WaitForSeconds(3);
    }
    IEnumerator Event_10()
    {
        yield return new WaitForSeconds(0);
        nextReady = true;
    }

    IEnumerator Event_11()
    {
        SoundManager.instance.VoicePlay(15);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(215), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(16);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(216), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(17);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(217), 2);
        yield return new WaitForSeconds(3);

        PlayerUISystem.instance.StartMarkerUI(spotPoint[2].transform.position + new Vector3(0, 1, 0));
        yield return new WaitForSeconds(1);

    }

    IEnumerator Event_12()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SetWave("W4", false);
        SoundManager.instance.VoicePlay(18);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(218), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(19);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(219), 1);
        yield return new WaitForSeconds(2);
        //SoundManager.instance.VoicePlay(30);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>왜 그래? 맥스.", 1);
        //yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(20);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(220), 2);
        yield return new WaitForSeconds(2);
    }
    IEnumerator Event_13()
    {
        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(412));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(412));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.StartMarkerUI(eventObject[1].transform.position + new Vector3(0, 1, 0));
        eventObject[1].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[1].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(2);
    }


    IEnumerator Event_14()
    {
        //SoundManager.instance.VoicePlay(32);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>휴, 드디어···. ", 1);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Event_15()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SoundManager.instance.VoicePlay(21);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(221), 3);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(22);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(222), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(23);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(223), 3);
        nextReady = true;
        yield return new WaitForSeconds(3);
    }

    IEnumerator Event_16()
    {
        SetWave("W5", true);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Event_17()
    {
        SaveState(2);
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(413));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        SoundManager.instance.VoicePlay(24);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(224), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(25);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(225), 1);
        yield return new WaitForSeconds(2);
        gate[1].SetActive(false);
        PlayerUISystem.instance.StartMarkerUI(spotPoint[3].transform.position + new Vector3(0, 1, 0));
    }

    IEnumerator Event_18()
    {
        gate[1].SetActive(true);
        PlayerUISystem.instance.EndMarkerUI();
        CheckPointManager.instance.InitSave();
        SoundManager.instance.VoicePlay(26);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(226), 3);
        yield return new WaitForSeconds(4);
        //SoundManager.instance.VoicePlay(39);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>예전에 번화가였던 걸로 기억하는데.", 2);
        //yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(27);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(227), 3);
        yield return new WaitForSeconds(4);
        //SoundManager.instance.VoicePlay(41);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>얼른 세상이 예전처럼 돌아왔으면 좋겠어.", 3);
        //yield return new WaitForSeconds(4);
        //SoundManager.instance.VoicePlay(42);
        //PlayerUISystem.instance.SetDialogText("<color=#008000ff>[맥스] </color>꼭 그렇게 될 거예요, 마야.", 1);
        //yield return new WaitForSeconds(2);
        PlayerUISystem.instance.StartMarkerUI(spotPoint[4].transform.position + new Vector3(0, 1, 0));
        nextReady = true;
    }
    IEnumerator Event_19()
    {
        yield return new WaitForSeconds(0);
        nextReady = true;
    }
    IEnumerator Event_20()
    {
        yield return new WaitForSeconds(0);
        nextReady = true;
    }
    IEnumerator Event_21()
    {
        yield return new WaitForSeconds(0);
        nextReady = true;
    }
    IEnumerator Event_22()
    {
        yield return new WaitForSeconds(0);
        nextReady = true;
    }
    IEnumerator Event_23()
    {
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_24()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SoundManager.instance.VoicePlay(28);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(228), 1);
        yield return new WaitForSeconds(2);
        //SoundManager.instance.VoicePlay(46);
        //PlayerUISystem.instance.SetDialogText("<color=#800080ff>[마야] </color>빌어먹을. 또?", 1);
        //yield return new WaitForSeconds(2);
        //SoundManager.instance.VoicePlay(47);
        //PlayerUISystem.instance.SetDialogText("<color=#008000ff>[맥스] </color>그래도 저번처럼 떼거지로 몰려오진 않는군요.", 2);
        nextReady = true;
    }

    IEnumerator Event_25()
    {
        SetWave("W7", false);
        yield return new WaitForSeconds(3);
    }

    IEnumerator Event_26()
    {
        SoundManager.instance.VoicePlay(29);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(229), 2);
        yield return new WaitForSeconds(3);
        nextReady = true;
    }

    IEnumerator Event_27()
    {
        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(414));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(414));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.StartMarkerUI(eventObject[2].transform.position + new Vector3(0, 1, 0));
        eventObject[2].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[2].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(3);
    }

    IEnumerator Event_28()
    {
        SoundManager.instance.VoicePlay(30);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(230), 2);
        yield return new WaitForSeconds(3);
    }

    IEnumerator Event_29()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SetWave("W8", true);
        yield return new WaitForSeconds(3);
    }

    IEnumerator Event_30()
    {

        yield return new WaitForSeconds(0);

        nextReady = true;
    }

    IEnumerator Event_31()
    {
        gate[2].SetActive(false);
        SaveState(3);
        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(415));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(415));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.SetObjectiveIcon(1);
        PlayerUISystem.instance.StartMarkerUI(spotPoint[5].transform.position + new Vector3(0, 1, 0));
        yield return new WaitForSeconds(3);
    }

    IEnumerator Event_32()
    {
        gate[2].SetActive(true);
        CheckPointManager.instance.InitSave();
        PlayerUISystem.instance.EndMarkerUI();
        SoundManager.instance.VoicePlay(31);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(231), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(32);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(232), 1);
        yield return new WaitForSeconds(2);
        //SoundManager.instance.VoicePlay(52);
        //PlayerUISystem.instance.SetDialogText("<color=#008000ff>[맥스] </color>데이터 정보에 따르면 여기가 마지막 구역이에요.", 2);
        //yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(33);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(233), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(34);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(234), 3);
        yield return new WaitForSeconds(3);
        SetWave("W9", false);
    }

    IEnumerator Event_33()
    {

        SoundManager.instance.VoicePlay(35);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(235), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(36);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(236), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(37);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(237), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(38);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(238), 3);
        yield return new WaitForSeconds(2);
        nextReady = true;
    }

    IEnumerator Event_34()
    {
        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(416));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(416));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.StartMarkerUI(eventObject[3].transform.position + new Vector3(0, 1, 0));
        eventObject[3].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[3].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(2);
    }

    IEnumerator Event_35()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SoundManager.instance.VoicePlay(39);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(239), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(40);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(240), 2);
        yield return new WaitForSeconds(2);
        SetWave("L1", true);
    }

    IEnumerator Event_36()
    {
        SoundManager.instance.SoundPlay(1, 11);
        SoundManager.instance.VoicePlay(41);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(241), 1);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(42);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(242), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(43);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(243), 2);
        yield return new WaitForSeconds(2);
        PlayerUISystem.instance.StartMarkerUI(eventObject[3].transform.position + new Vector3(0, 1, 0));
        eventObject[3].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[3].GetComponent<HackingInteraction>().Ready();
        nextReady = true;
    }

    IEnumerator Event_37()
    {
        yield return new WaitForSeconds(0);
    }

    IEnumerator Event_38()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SoundManager.instance.VoicePlay(44);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(244), 2);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Event_39()
    {
        SoundManager.instance.SoundPlay(1, 11);
        SoundManager.instance.VoicePlay(45);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(245), 2);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(46);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(246), 2);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(47);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(247), 2);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(48);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(248), 2);
        yield return new WaitForSeconds(2);
        nextReady = true;
    }

    IEnumerator Event_40()
    {
        PlayerUISystem.instance.StartMarkerUI(eventObject[3].transform.position + new Vector3(0, 1, 0));
        eventObject[3].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[3].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_41()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SoundManager.instance.VoicePlay(49);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(249), 1);
        yield return new WaitForSeconds(2);
    }
    IEnumerator Event_42()
    {
        SoundManager.instance.SoundPlay(1, 11);
        SoundManager.instance.VoicePlay(50);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(250), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(51);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(251), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(52);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(252), 1);
        yield return new WaitForSeconds(2);
        nextReady = true;
    }
    IEnumerator Event_43()
    {
        PlayerUISystem.instance.StartMarkerUI(eventObject[3].transform.position + new Vector3(0, 1, 0));
        eventObject[3].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[3].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_44()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SoundManager.instance.VoicePlay(53);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(253), 1);
        yield return new WaitForSeconds(2);
        SpawnManager.instance.status.enemySpawnCount = 0;
        SpawnManager.instance.ForceStopWave();
    }
    IEnumerator Event_45()
    {
        SaveState(4);
        StartCoroutine(OpenDoor());
        PlayerUISystem.instance.SetTitleUI(0, CSVManager.instance.LoadText(417));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(417));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.SetObjectiveIcon(2);
        PlayerUISystem.instance.StartMarkerUI(eventObject[4].transform.position + new Vector3(0, 1, 0));
        eventObject[4].GetComponent<HackingInteraction>().doneInteraction = false;
        eventObject[4].GetComponent<HackingInteraction>().Ready();
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_46()
    {
        PlayerUISystem.instance.EndMarkerUI();
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(418));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        hack[0].GetComponent<MeshRenderer>().material.SetFloat("_Hack", 1);
        hack[1].SetActive(true);
        PlayerUISystem.instance.EndMarkerUI();
        cineObj[0].SetActive(true);
        eventObject[5].SetActive(true);
        eventObject[5].transform.GetChild(0).gameObject.GetComponent<AnimationSystem>().SetUpperState(1);
        eventObject[5].transform.GetChild(1).gameObject.GetComponent<AnimationSystem>().SetUpperState(2);
        eventObject[5].transform.GetChild(3).gameObject.GetComponent<AnimationSystem>().SetUpperState(1);
        yield return new WaitForSeconds(0);
        cineObj[0].GetComponent<CinematicManager>().StartCinematic(1);
        yield return new WaitForSeconds(4);
        StartCoroutine(CloseDoor());
        SoundManager.instance.SoundPlay(6, 0);
        yield return new WaitForSeconds(5);
        SoundManager.instance.SoundPlay(6, 1);
        yield return new WaitForSeconds(1);
        cineObj[0].GetComponent<CinematicManager>().EndCinematic();
        Player.instance.transform.position = checkPoint[4].transform.position;
        Player.instance.transform.eulerAngles = new Vector3(0, 270, 0);
        PlayerCameraSystem.instance.rotateValue = new Vector3(0, 0, 0);
        CheckPointManager.instance.InitSave();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < eventObject[5].transform.childCount; i++)
        {
            if(eventObject[5].transform.GetChild(i).gameObject.GetComponent<EnemyType1AI>())
                eventObject[5].transform.GetChild(i).gameObject.GetComponent<EnemyType1AI>().enabled = true;
            if (eventObject[5].transform.GetChild(i).gameObject.GetComponent<EnemyType2AI>())
                eventObject[5].transform.GetChild(i).gameObject.GetComponent<EnemyType2AI>().enabled = true;
        }
        eventObject[5].transform.GetChild(0).gameObject.GetComponent<AnimationSystem>().SetUpperState(0);
        eventObject[5].transform.GetChild(1).gameObject.GetComponent<AnimationSystem>().SetUpperState(0);
        eventObject[5].transform.GetChild(3).gameObject.GetComponent<AnimationSystem>().SetUpperState(0);
        nextReady = true;
    }
    IEnumerator Event_47()
    {
        SetWave("W11", true);
        SoundManager.instance.VoicePlay(54);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(254), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(55);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(255), 2);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(56);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(256), 1);
        yield return new WaitForSeconds(2);
        nextReady = true;
    }
    IEnumerator Event_48()
    {
        SoundManager.instance.VoicePlay(57);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(257), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(58);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(258), 3);
        yield return new WaitForSeconds(3);
        nextReady = true;
    }
    IEnumerator Event_49()
    {
        PlayerUISystem.instance.SetTitleUI(1, CSVManager.instance.LoadText(419));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(419));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.SetObjectiveIcon(2);
        PlayerUISystem.instance.StartMarkerUI(spotPoint[6].transform.position + new Vector3(0, 1, 0));
        gate[2].SetActive(false);
        gate[3].SetActive(false);
        gate[4].SetActive(false);
        yield return new WaitForSeconds(1);
        nextReady = true;
    }
    IEnumerator Event_50()
    {

        yield return new WaitForSeconds(1);
    }
    IEnumerator Event_51()
    {
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(30, 0, 16));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(30, 0, 13));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(30, 0, 10));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(32, 0, 13));
        SpawnManager.instance.SpawnEnemy(2, 2, new Vector3(70, 0, 10));
        SpawnManager.instance.SpawnEnemy(3, 2, new Vector3(70, 0, 10));
        PlayerUISystem.instance.StartMarkerUI(spotPoint[7].transform.position + new Vector3(0, 1, 0));
        SoundManager.instance.VoicePlay(59);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(259), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(60);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(260), 1);
        yield return new WaitForSeconds(2);
    }
    IEnumerator Event_52()
    {
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(59, 0, 36));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(58, 0, 36));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(57, 0, 36));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(56, 0, 36));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(57, 0, 38));
        PlayerUISystem.instance.StartMarkerUI(spotPoint[8].transform.position + new Vector3(0, 1, 0));
        SoundManager.instance.VoicePlay(61);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(261), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(62);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(262), 2);
        yield return new WaitForSeconds(2);
    }
    IEnumerator Event_53()
    {
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(58, 0, 62));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(57, 0, 64));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(56, 0, 62));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(55, 0, 64));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(54, 0, 62));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(53, 0, 64));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(58, 0, 72));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(57, 0, 74));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(56, 0, 72));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(55, 0, 74));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(54, 0, 72));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(53, 0, 74));
        PlayerUISystem.instance.StartMarkerUI(spotPoint[9].transform.position + new Vector3(0, 1, 0));
        SoundManager.instance.VoicePlay(63);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(263), 3);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(64);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(264), 2);
        yield return new WaitForSeconds(3);
    }
    IEnumerator Event_54()
    {
        PlayerUISystem.instance.EndMarkerUI();
        SpawnManager.instance.status.enemySpawnCount = 0;
        cineObj[1].SetActive(true);
        yield return new WaitForSeconds(0);
        cineObj[1].GetComponent<CinematicManager>().StartCinematic(2);
        yield return new WaitForSeconds(3);
        SpawnManager.instance.ForceStopWave();
        GameManager.instance.GoNextScene();
        //GameManager.instance.StartScene(6);
    }



    void W1()
    {

        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(46, 0, 93));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(48, 0, 93));
        SpawnManager.instance.status.enemySpawnCount = 0;
        SpawnManager.instance.status.enemyMaxCount = 6;
        SpawnManager.instance.status.unitCountMax[0] = 3;
        SpawnManager.instance.status.unitCountMax[1] = 6;
        SpawnManager.instance.status.unitCountMax[2] = 0;
        SpawnManager.instance.status.unitCountMax[3] = 0;
        SpawnManager.instance.status.unitRatio[0] = 0;
        SpawnManager.instance.status.unitRatio[1] = 0;
        SpawnManager.instance.status.unitRatio[2] = 0;
        SpawnManager.instance.status.unitRatio[3] = 0;
    }
    void W2()
    {
        SpawnManager.instance.status.enemySpawnCount = 6;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 2;
        SpawnManager.instance.status.unitCountMax[1] = 4;
        SpawnManager.instance.status.unitCountMax[2] = 0;
        SpawnManager.instance.status.unitCountMax[3] = 0;
        SpawnManager.instance.status.unitRatio[0] = 40;
        SpawnManager.instance.status.unitRatio[1] = 60;
        SpawnManager.instance.status.unitRatio[2] = 0;
        SpawnManager.instance.status.unitRatio[3] = 0;
    }

    void W3()
    {
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(54, 0, 77));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(53, 0, 75));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(55, 0, 75));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(63, 0, 64));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(63, 0, 61));
        SpawnManager.instance.status.enemySpawnCount = 0;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 5;
        SpawnManager.instance.status.unitCountMax[1] = 10;
        SpawnManager.instance.status.unitCountMax[2] = 2;
        SpawnManager.instance.status.unitCountMax[3] = 3;
        SpawnManager.instance.status.unitRatio[0] = 23;
        SpawnManager.instance.status.unitRatio[1] = 35;
        SpawnManager.instance.status.unitRatio[2] = 25;
        SpawnManager.instance.status.unitRatio[3] = 17;
    }

    void W4()
    {
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(94, 0, 72));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(96, 0, 72));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(98, 0, 72));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(101, 5, 39));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(101, 5, 37));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(103, 5, 38));

        SpawnManager.instance.status.enemySpawnCount = 0;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 3;
        SpawnManager.instance.status.unitCountMax[1] = 3;
        SpawnManager.instance.status.unitCountMax[2] = 0;
        SpawnManager.instance.status.unitCountMax[3] = 2;
        SpawnManager.instance.status.unitRatio[0] = 23;
        SpawnManager.instance.status.unitRatio[1] = 35;
        SpawnManager.instance.status.unitRatio[2] = 25;
        SpawnManager.instance.status.unitRatio[3] = 17;
    }

    void W5()
    {
        SpawnManager.instance.status.enemySpawnCount = 12;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 3;
        SpawnManager.instance.status.unitCountMax[1] = 2;
        SpawnManager.instance.status.unitCountMax[2] = 2;
        SpawnManager.instance.status.unitCountMax[3] = 3;
        SpawnManager.instance.status.unitRatio[0] = 35;
        SpawnManager.instance.status.unitRatio[1] = 18;
        SpawnManager.instance.status.unitRatio[2] = 22;
        SpawnManager.instance.status.unitRatio[3] = 25;
    }

    void W6()
    {
        SpawnManager.instance.status.enemySpawnCount = 13;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 1;
        SpawnManager.instance.status.unitCountMax[1] = 3;
        SpawnManager.instance.status.unitCountMax[2] = 3;
        SpawnManager.instance.status.unitCountMax[3] = 3;
        SpawnManager.instance.status.unitRatio[0] = 15;
        SpawnManager.instance.status.unitRatio[1] = 25;
        SpawnManager.instance.status.unitRatio[2] = 30;
        SpawnManager.instance.status.unitRatio[3] = 30;
    }

    void W7()
    {
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(22, 0, 14));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(22, 0, 12));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(22, 0, 10));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(19, 0, 14));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(19, 0, 10));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(17, 0, 12));
        SpawnManager.instance.status.enemySpawnCount = 0;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 0;
        SpawnManager.instance.status.unitCountMax[1] = 3;
        SpawnManager.instance.status.unitCountMax[2] = 2;
        SpawnManager.instance.status.unitCountMax[3] = 1;
        SpawnManager.instance.status.unitRatio[0] = 15;
        SpawnManager.instance.status.unitRatio[1] = 25;
        SpawnManager.instance.status.unitRatio[2] = 30;
        SpawnManager.instance.status.unitRatio[3] = 30;
    }

    void W8()
    {
        SpawnManager.instance.status.enemySpawnCount = 25;
        SpawnManager.instance.status.enemyMaxCount = 11;
        SpawnManager.instance.status.unitCountMax[0] = 4;
        SpawnManager.instance.status.unitCountMax[1] = 4;
        SpawnManager.instance.status.unitCountMax[2] = 8;
        SpawnManager.instance.status.unitCountMax[3] = 4;
        SpawnManager.instance.status.unitRatio[0] = 25;
        SpawnManager.instance.status.unitRatio[1] = 20;
        SpawnManager.instance.status.unitRatio[2] = 30;
        SpawnManager.instance.status.unitRatio[3] = 25;
    }

    void W9()
    {
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(30, -5, 55));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(32, -5, 55));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(34, -5, 55));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(30, -5, 57));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(34, -5, 57));
        SpawnManager.instance.SpawnEnemy(3, 1, new Vector3(31.5f, -5, 60));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(11, -5, 55));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(13, -5, 55));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(11, -5, 57));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(13, -5, 57));
        SpawnManager.instance.SpawnEnemy(2, 1, new Vector3(12, -5, 60));
        SpawnManager.instance.status.enemySpawnCount = 0;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 5;
        SpawnManager.instance.status.unitCountMax[1] = 4;
        SpawnManager.instance.status.unitCountMax[2] = 1;
        SpawnManager.instance.status.unitCountMax[3] = 1;
        SpawnManager.instance.status.unitRatio[0] = 15;
        SpawnManager.instance.status.unitRatio[1] = 25;
        SpawnManager.instance.status.unitRatio[2] = 30;
        SpawnManager.instance.status.unitRatio[3] = 30;
    }

    void W10()
    {

        SpawnManager.instance.status.enemySpawnCount = 0;
        SpawnManager.instance.status.enemyMaxCount = 10;
        SpawnManager.instance.status.unitCountMax[0] = 4;
        SpawnManager.instance.status.unitCountMax[1] = 4;
        SpawnManager.instance.status.unitCountMax[2] = 2;
        SpawnManager.instance.status.unitCountMax[3] = 2;
        SpawnManager.instance.status.unitRatio[0] = 33;
        SpawnManager.instance.status.unitRatio[1] = 33;
        SpawnManager.instance.status.unitRatio[2] = 19;
        SpawnManager.instance.status.unitRatio[3] = 15;
    }

    void W11()
    {
        SpawnManager.instance.status.enemySpawnCount = 10000;
        SpawnManager.instance.status.enemyMaxCount = 40;
        SpawnManager.instance.status.unitCountMax[0] = 18;
        SpawnManager.instance.status.unitCountMax[1] = 18;
        SpawnManager.instance.status.unitCountMax[2] = 16;
        SpawnManager.instance.status.unitCountMax[3] = 16;
        SpawnManager.instance.status.unitRatio[0] = 30;
        SpawnManager.instance.status.unitRatio[1] = 35;
        SpawnManager.instance.status.unitRatio[2] = 15;
        SpawnManager.instance.status.unitRatio[3] = 20;
    }

    void L1()
    {
        SpawnManager.instance.status.enemySpawnCount = 10000;
        SpawnManager.instance.status.enemyMaxCount = 12;
        SpawnManager.instance.status.unitCountMax[0] = 5;
        SpawnManager.instance.status.unitCountMax[1] = 6;
        SpawnManager.instance.status.unitCountMax[2] = 4;
        SpawnManager.instance.status.unitCountMax[3] = 4;
        SpawnManager.instance.status.unitRatio[0] = 30;
        SpawnManager.instance.status.unitRatio[1] = 35;
        SpawnManager.instance.status.unitRatio[2] = 15;
        SpawnManager.instance.status.unitRatio[3] = 20;
    }
}
