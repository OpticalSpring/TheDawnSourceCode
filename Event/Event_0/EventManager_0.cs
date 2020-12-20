using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EventManager_0 : EventManager
{
    public GameObject[] checkPoint;
    void Start()
    {
        SoundManager.instance.SoundPlay(0, 0);
        nextReady = true;
        LoadPlayerPos();
    }

    void LoadPlayerPos()
    {
        int num = 0;
        PlayerPrefs.SetInt("CheckPoint", 0);

        Player.instance.transform.position = checkPoint[num].transform.position;

        switch (num)
        {
            case 0:
                eventNumber = 0;
                PlayerCameraSystem.instance.rotateValue = new Vector3(0, 0, 0);
                break;

        }
    }

    Coroutine nowCorutine;
    bool recall;
    bool ultimate;
    bool stopfield;
    // Update is called once per frame
    void Update()
    {
        if (recall == true)
        {
            Player.instance.timeRecallDelayTimeNow = Player.instance.timeRecallDelayTime;
        }
        if (stopfield == true)
        {
            Player.instance.timeStopFieldDelayTimeNow = Player.instance.timeStopFieldDelayTime;
        }
        if (ultimate == true)
        {
            Player.instance.ultimateGage = 0;
        }
        switch (eventNumber)
        {
            case 4:
                if (SpawnManager.instance.enemyCount == 0)
                {
                    nextReady = true;
                }
                break;
            case 7:

                if (Player.instance.HP_Point == 100)
                {
                    nextReady = true;
                }
                break;
            case 9:

                if (Player.instance.HP_Point < 100)
                {
                    nextReady = true;
                }
                if (Player.instance.timeStopFieldDelayTimeNow > 0)
                {
                    eventNumber = 10;
                    nextReady = true;
                }
                break;
            case 15:
                if (Player.instance.ultimateGage == 0)
                {
                    nextReady = true;
                }
                break;
            default:
                break;
        }
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


    IEnumerator Event_0()
    {
        PlayerUISystem.instance.SetObjectiveText(0, "");
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        yield return new WaitForSeconds(0);
        recall = true;
        ultimate = true;
        stopfield = true;
        nextReady = true;
    }

    IEnumerator Event_1()
    {
        
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(0);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(100), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(1);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(101), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(2);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(102), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(3);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(103), 6);
        yield return new WaitForSeconds(7);
        nextReady = true;

    }

    IEnumerator Event_2()
    {

        PlayerUISystem.instance.SetTutoUI(CSVManager.instance.LoadText(400));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(401));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        SpawnManager.instance.SpawnEnemy(1, 1, Vector3.zero);
        yield return new WaitForSeconds(0);
        nextReady = true;
    }

    IEnumerator Event_3()
    {
        SoundManager.instance.VoicePlay(4);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(104), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(5);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(105), 6);
    }
    IEnumerator Event_4()
    {
        PlayerUISystem.instance.EndTutoUI();
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(402));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        SoundManager.instance.VoicePlay(6);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(106), 5);
        yield return new WaitForSeconds(6); 
        nextReady = true;

    }
    IEnumerator Event_5()
    {
        
        SoundManager.instance.VoicePlay(7);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(107), 6);
        yield return new WaitForSeconds(6);
        SoundManager.instance.VoicePlay(8);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(108), 5);
        yield return new WaitForSeconds(5);
        nextReady = true;
    }
    IEnumerator Event_6()
    {
        PlayerUISystem.instance.SetTutoUI(CSVManager.instance.LoadText(403));
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(405));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.SetObjectiveIcon(1);
        Player.instance.Hit(99);
        recall = false;
        Player.instance.timeRecallDelayTimeNow = 0;
        yield return new WaitForSeconds(4);

    }
    IEnumerator Event_7()
    {
        PlayerUISystem.instance.EndTutoUI();
        SoundManager.instance.VoicePlay(9);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(109), 4);
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(10);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(110), 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.VoicePlay(11);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(111), 4);
        yield return new WaitForSeconds(5);
        SoundManager.instance.VoicePlay(12);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(112), 6);
        yield return new WaitForSeconds(6);
        SoundManager.instance.VoicePlay(13);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(113), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(14);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(114), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(15);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(115), 5);
        yield return new WaitForSeconds(6);
        SoundManager.instance.VoicePlay(16);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(116), 1);
        SpawnManager.instance.SpawnEnemy(2, 1, Vector3.zero);
        PlayerUISystem.instance.SetTutoUI(CSVManager.instance.LoadText(404));
        stopfield = false;
        Player.instance.timeStopFieldDelayTimeNow = 0;
        nextReady = true;
    }
    IEnumerator Event_8()
    {
        yield return new WaitForSeconds(4);
    }
    IEnumerator Event_9()
    {
        eventNumber = 8;
        Player.instance.HP_Point = Player.instance.HP_PointMax;
        yield return new WaitForSeconds(4);
        SoundManager.instance.VoicePlay(17);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(117), 3);
        yield return new WaitForSeconds(4);
    }
    IEnumerator Event_10()
    {
        PlayerUISystem.instance.EndTutoUI();
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(406));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        SpawnManager.instance.DestroyAllEnemy();
        SoundManager.instance.VoicePlay(18);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(118), 3);
        yield return new WaitForSeconds(3);
        nextReady = true;
    }
    IEnumerator Event_11()
    {
        SoundManager.instance.VoicePlay(19);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(119), 4);
        yield return new WaitForSeconds(4);
        
        nextReady = true;
    }
    IEnumerator Event_12()
    {
        yield return new WaitForSeconds(0);
        nextReady = true;
    }
    IEnumerator Event_13()
    {
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(408));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        PlayerUISystem.instance.SetObjectiveIcon(2);
        SoundManager.instance.VoicePlay(20);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(120), 2);
        yield return new WaitForSeconds(3);
        SoundManager.instance.VoicePlay(21);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(121), 6);
        yield return new WaitForSeconds(6);
        PlayerUISystem.instance.SetTutoUI(CSVManager.instance.LoadText(407));
        ultimate = false;
        Player.instance.ultimateGage = Player.instance.ultimateGageMax;
        for (int i = 0; i < 5; i++)
        {
            SpawnManager.instance.SpawnEnemy(1, 1, new Vector3((i - 2) * 2, 0, -3));

        }
        for (int i = 0; i < 5; i++)
        {
            SpawnManager.instance.SpawnEnemy(1, 1, new Vector3((i - 2) * 2, 0, 3));
        }
        yield return new WaitForSeconds(0);
        nextReady = true;
    }
    IEnumerator Event_14()
    {
        yield return new WaitForSeconds(4);
    }
    IEnumerator Event_15()
    {
        PlayerUISystem.instance.EndTutoUI();
        PlayerUISystem.instance.SetObjectiveText(0, CSVManager.instance.LoadText(409));
        PlayerUISystem.instance.SetObjectiveText(1, "");
        PlayerUISystem.instance.SetObjectiveText(2, "");
        yield return new WaitForSeconds(10);
        SoundManager.instance.VoicePlay(22);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(122), 5);
        yield return new WaitForSeconds(6);
        SoundManager.instance.VoicePlay(23);
        PlayerUISystem.instance.SetDialogText(CSVManager.instance.LoadText(123), 2);
        yield return new WaitForSeconds(4);
        GameManager.instance.GoNextScene();
    }
}
