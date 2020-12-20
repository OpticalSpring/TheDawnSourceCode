using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MekaBossAI : MonoBehaviour
{
    MekaBoss boss;
    public float moveSpeed;
    public float downSpeed;
    public float backSpeed;
    public float theta;
    float dis;
    public GameObject missleTargetPos;
    public EventManager_2 eventManager;
    public GameObject fadeWhite;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<MekaBoss>();
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {
        float bossPercent = boss.HP_Point * 100 / boss.HP_PointMax;
        if (bossPercent < 80)
        {
            if (eventManager.evtBool[9] == false)
            {
                eventManager.evtBool[9] = true;
                eventManager.EventTrigger("BossH1");
            }
        }
        if (bossPercent < 50)
        {
            if (eventManager.evtBool[10] == false)
            {
                eventManager.evtBool[10] = true;
                eventManager.EventTrigger("BossH2");
            }
        }
        if (bossPercent < 30)
        {
            if (eventManager.evtBool[11] == false)
            {
                eventManager.evtBool[11] = true;
                eventManager.EventTrigger("BossH3");
            }
        }
        if (bossPercent < 10)
        {
            if (eventManager.evtBool[12] == false)
            {
                eventManager.evtBool[12] = true;
                eventManager.EventTrigger("BossH4");
            }
        }
        ProgressState();
    }
    void Rotate(GameObject obj, GameObject target)
    {
        float t = obj.GetComponent<TimeAgent>().speedFloat;
        obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, target.transform.rotation, Time.deltaTime * 10 * t);
    }
    void ProgressState()
    {
        switch (boss.bossFSM)
        {
            case MekaBoss.MBossFSM.Idle:
                boss.Turn(boss.model);
                break;
            case MekaBoss.MBossFSM.Pattern1:
                boss.Turn(boss.model);
                switch (boss.progressState)
                {
                    case 2:
                        Rotate(boss.HL, boss.model);
                        boss.HL.GetComponent<MBHand>().target = Player.instance.transform.position + 0.1f* (boss.transform.position - Player.instance.transform.position);

                        boss.HL.GetComponent<MBHand>().target.y = boss.MBHL.transform.position.y;
                        break;
                    case 4:
                        Rotate(boss.HL, boss.model);
                        boss.HL.GetComponent<MBHand>().target = boss.MBHL.transform.position;
                        break;

                    case 6:
                        Rotate(boss.HR, boss.model);
                        boss.HR.GetComponent<MBHand>().target = Player.instance.transform.position + 0.1f * (boss.transform.position - Player.instance.transform.position);
                        boss.HR.GetComponent<MBHand>().target.y = boss.MBHR.transform.position.y;
                        break;
                    case 8:
                        Rotate(boss.HR, boss.model);
                        boss.HR.GetComponent<MBHand>().target = boss.MBHR.transform.position;
                        break;
                }
                break;
            case MekaBoss.MBossFSM.Pattern2:
                boss.Turn(boss.model);
                switch (boss.progressState)
                {
                    case 0:
                        boss.HL.GetComponent<MBHand>().target = boss.MBHL.transform.position;
                        boss.HR.GetComponent<MBHand>().target = boss.MBHR.transform.position;
                        Rotate(boss.HL, boss.model);
                        Rotate(boss.HR, boss.model);
                        break;
                    case 2:
                        boss.Turn(boss.HL.transform.parent.gameObject);
                        break;
                    case 3:
                        Rotate(boss.HL, boss.model);
                        boss.HL.GetComponent<MBHand>().target = boss.HL.transform.position;

                        boss.HL.GetComponent<MBHand>().target.y = 13;
                        Vector3 nPos = Vector3.zero;
                        if(theta < 1 && boss.HL.GetComponent<TimeAgent>().set == false)
                        {
                            theta += 3 * Time.deltaTime;
                            nPos.x += dis * Mathf.Sin(theta);
                            nPos.z += dis * Mathf.Cos(theta);
                            nPos.y = boss.HL.transform.localPosition.y;
                            boss.HL.transform.localPosition = nPos;
                        }
                        boss.HL.GetComponent<MBHand>().CatchHand();
                        boss.HL.GetComponent<MBHand>().BulletTime();
                        break;
                    case 4:
                    case 5:
                        Rotate(boss.HL, boss.model);
                        boss.HL.GetComponent<MBHand>().target = boss.MBHL.transform.position;
                        boss.HL.GetComponent<TimeAgent>().rebool = true;
                        break;
                    case 7:
                        boss.Turn(boss.HL.transform.parent.gameObject);
                        break;
                    case 8:
                        Rotate(boss.HR, boss.model);
                        boss.HR.GetComponent<MBHand>().target = boss.HR.transform.position;

                        boss.HR.GetComponent<MBHand>().target.y = 13;
                        Vector3 nPos2 = Vector3.zero;
                        if (theta > -1 && boss.HR.GetComponent<TimeAgent>().set == false)
                        {
                            theta -= 3 * Time.deltaTime;
                            nPos2.x += dis * Mathf.Sin(theta);
                            nPos2.z += dis * Mathf.Cos(theta);
                            nPos2.y = boss.HR.transform.localPosition.y;
                            boss.HR.transform.localPosition = nPos2;
                        }
                        boss.HR.GetComponent<MBHand>().CatchHand();
                        boss.HR.GetComponent<MBHand>().BulletTime();
                        break;
                    case 9:
                    case 10:
                        Rotate(boss.HR, boss.model);
                        boss.HR.GetComponent<MBHand>().target = boss.MBHR.transform.position;
                        boss.HR.GetComponent<TimeAgent>().rebool = true;
                        break;
                }
                break;
            case MekaBoss.MBossFSM.Pattern3:
                switch (boss.progressState)
                {
                    case 1:
                        boss.Turn(boss.model);
                        break;
                    case 2:
                        boss.AttackLaser();
                        break;
                }
                break;
            case MekaBoss.MBossFSM.Pattern4:
                boss.Turn(boss.HL.transform.parent.gameObject);
                break;
            case MekaBoss.MBossFSM.Pattern5:
                boss.Turn(boss.model);
                switch (boss.progressState)
                {
                    case 2:
                        boss.bigSphere.transform.localPosition = Vector3.MoveTowards(boss.bigSphere.transform.localPosition, 
                            new Vector3(0, 82f, 16.41f), 11*Time.deltaTime);
                        break;
                }
                break;
            case MekaBoss.MBossFSM.Pattern6:
                boss.Turn(boss.model);
                break;
            case MekaBoss.MBossFSM.Pattern7:
                boss.Turn(boss.model);
                break;
            case MekaBoss.MBossFSM.Pattern8:
                boss.Turn(boss.model);
                break;
        }
    }
    void HandTimeSetFalse()
    {
        boss.HL.GetComponent<TimeAgent>().set = false;
        boss.HR.GetComponent<TimeAgent>().set = false;
    }
    
    IEnumerator Test()
    {
        
        while (true)
        {
            if(boss.bossFSM == MekaBoss.MBossFSM.Idle)
            {

                SelectPattern();
            }
            yield return new WaitForSeconds(1);
        }
    }

    int prevPattern;
    
    void SelectPattern()
    {

        int a = Random.Range(0, 100);
        if (boss.HP_Point * 100 / boss.HP_PointMax > 80)
        {
            if(a < 50)
            {
                if (prevPattern == 1) return;
                StartCoroutine(Pattern_1());
            }
            else
            {
                if (prevPattern == 2) return;
                StartCoroutine(Pattern_2());
                
            }
        }
        else if (boss.HP_Point * 100 / boss.HP_PointMax > 50)
        {
            if (a < 15)
            {
                if (prevPattern == 1) return;
               StartCoroutine(Pattern_1());
            }
            else if(a < 30)
            {
                if (prevPattern == 2) return;
                StartCoroutine(Pattern_2());
            }
            else if (a < 50)
            {
                if (prevPattern == 3) return;
                StartCoroutine(Pattern_3());
            }
            else if (a < 70)
            {
                if (prevPattern == 4) return;
                StartCoroutine(Pattern_4());
            }
            else if (a < 80)
            {
                if (prevPattern == 6) return;
                StartCoroutine(Pattern_6());
            }
            else if (a < 90)
            {
                if (prevPattern == 7) return;
                StartCoroutine(Pattern_7());
            }
            else
            {
                if (prevPattern == 8) return;
                StartCoroutine(Pattern_8());
            }
        }
        else if (boss.HP_Point * 100 / boss.HP_PointMax > 20)
        {
            if (a < 5)
            {
                if (prevPattern == 1) return;
                StartCoroutine(Pattern_1());
            }
            else if (a < 10)
            {
                if (prevPattern == 2) return;
                StartCoroutine(Pattern_2());
            }
            else if (a < 40)
            {
                if (prevPattern == 3) return;
                StartCoroutine(Pattern_3());
            }
            else if (a < 70)
            {
                if (prevPattern == 4) return;
                StartCoroutine(Pattern_4());
            }
            else if (a < 80)
            {
                if (prevPattern == 6) return;
                StartCoroutine(Pattern_6());
            }
            else if (a < 90)
            {
                if (prevPattern == 7) return;
                StartCoroutine(Pattern_7());
            }
            else
            {
                if (prevPattern == 8) return;
                StartCoroutine(Pattern_8());
            }
        }
        else
        {
            if (prevPattern == 5) return;
            StartCoroutine(Pattern_5());
        }
        
    }

    IEnumerator Pattern_1()
    {
        prevPattern = 1;
        if (eventManager.evtBool[0] == false)
        {
            eventManager.evtBool[0] = true;
            eventManager.EventTrigger("BossP1");
        }

        HandTimeSetFalse();
        boss.bossFSM = MekaBoss.MBossFSM.Pattern1;
        boss.progressState = 1;
        boss.handPosSync = true;
        boss.bossAnimator.SetInteger("AniState", 11);

        yield return new WaitForSeconds(1.5f);
        boss.progressState = 2;
        boss.handPosSync = false;
        boss.DisConnectHandL();
        boss.bossAnimator.SetInteger("AniState", 12);
        boss.HLAnimator.SetInteger("AniState", 0);
        boss.HL.GetComponent<MBHand>().speed = moveSpeed;
        boss.HL.GetComponent<MBHand>().chargeEff.SetActive(true);
        yield return new WaitForSeconds(3.1f);
        boss.HL.GetComponent<MBHand>().chargeEff.SetActive(false);
        boss.progressState = 3;
        boss.bossAnimator.SetInteger("AniState", 13);
        boss.HLAnimator.SetInteger("AniState", 1);
        boss.HL.GetComponent<MBHand>().target = boss.HL.transform.position;
        boss.HL.GetComponent<MBHand>().target.y = 13;
        boss.HL.GetComponent<MBHand>().speed = downSpeed;
        boss.SetEffect(1, boss.HL.GetComponent<MBHand>().effPos.transform.position);
        yield return new WaitForSeconds(0.2f);
        boss.HL.GetComponent<MBHand>().AttackHand();
        //boss.SetEffect(0, boss.HL.GetComponent<MBHand>().effPos.transform.position);
        yield return new WaitForSeconds(1);
        boss.progressState = 4;
        boss.bossAnimator.SetInteger("AniState", 0);
        boss.HLAnimator.SetInteger("AniState", 2);
        boss.HL.GetComponent<MBHand>().speed = backSpeed;
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 7; i++)
        {
            if (boss.HL.GetComponent<TimeAgent>().set == true)
            { 
                yield return new WaitForSeconds(1);
            }
        }
        

        boss.progressState = 5;
        boss.ConnectHandL();
        boss.handPosSync = true;
        boss.bossAnimator.SetInteger("AniState", 14);

        yield return new WaitForSeconds(1.5f);
        boss.progressState = 6;
        boss.handPosSync = false;
        boss.DisConnectHandR();
        boss.bossAnimator.SetInteger("AniState", 15);
        boss.HRAnimator.SetInteger("AniState", 0);
        boss.HR.GetComponent<MBHand>().speed = moveSpeed;
        boss.HR.GetComponent<MBHand>().chargeEff.SetActive(true);
        yield return new WaitForSeconds(3.1f);
        boss.HR.GetComponent<MBHand>().chargeEff.SetActive(false);
        boss.progressState = 7;
        boss.bossAnimator.SetInteger("AniState", 16);
        boss.HRAnimator.SetInteger("AniState", 1);
        boss.HR.GetComponent<MBHand>().target = boss.HR.transform.position;
        boss.HR.GetComponent<MBHand>().target.y = 13;
        boss.HR.GetComponent<MBHand>().speed = downSpeed;
        boss.SetEffect(1, boss.HR.GetComponent<MBHand>().effPos.transform.position);
        yield return new WaitForSeconds(0.2f);
        boss.HR.GetComponent<MBHand>().AttackHand();
        //boss.SetEffect(0, boss.HR.GetComponent<MBHand>().effPos.transform.position);
        yield return new WaitForSeconds(1);
        boss.progressState = 8;
        boss.bossAnimator.SetInteger("AniState", 0);
        boss.HRAnimator.SetInteger("AniState", 2);
        boss.HR.GetComponent<MBHand>().speed = backSpeed;

        yield return new WaitForSeconds(3);
        for (int i = 0; i < 7; i++)
        {
            if (boss.HR.GetComponent<TimeAgent>().set == true)
            {
                yield return new WaitForSeconds(1);
            }
        }

        boss.progressState = 9;
        boss.ConnectHandR();
        boss.handPosSync = true;
        boss.bossAnimator.SetInteger("AniState", 0);
        //boss.DisConnectHandR();

        boss.bossFSM = MekaBoss.MBossFSM.Idle;
        
    }
    IEnumerator Pattern_2()
    {
        prevPattern = 2;
        if (eventManager.evtBool[1] == false)
        {
            eventManager.evtBool[1] = true;
            eventManager.EventTrigger("BossP21");
        }
        HandTimeSetFalse();
        boss.bossFSM = MekaBoss.MBossFSM.Pattern2;
        boss.progressState = 1;
        boss.bossAnimator.SetInteger("AniState", 21);
        yield return new WaitForSeconds(1);

        boss.progressState = 2;
        boss.bossAnimator.SetInteger("AniState", 22);
        boss.handPosSync = true;
        yield return new WaitForSeconds(2);
        
        boss.progressState = 3;
        boss.bossAnimator.SetInteger("AniState", 23);
        boss.handPosSync = false;
        boss.DisConnectHandL();
        theta = -2;
        dis = Vector3.Distance(boss.transform.position, Player.instance.transform.position) - 10;
        boss.HL.GetComponent<MBHand>().speed = downSpeed;
        boss.HL.GetComponent<MBHand>().grabbed = false;
        boss.HLAnimator.SetInteger("AniState", 3);
        boss.SetEffect(12, boss.HL.GetComponent<MBHand>().effPos, 1);
        yield return new WaitForSeconds(3);
        if (boss.HL.GetComponent<TimeAgent>().set == true)
        {
            boss.progressState = 0;
            boss.HLAnimator.SetInteger("AniState", 2);
            for (int i = 0; i < 7; i++)
            {
                if (boss.HL.GetComponent<TimeAgent>().set == true)
                {
                    yield return new WaitForSeconds(1);
                }
            }
        }
        else
        {

            boss.progressState = 4;
            boss.bossAnimator.SetInteger("AniState", 24);
            boss.HLAnimator.SetInteger("AniState", 4);
            yield return new WaitForSeconds(1.5f);

            boss.HL.GetComponent<MBHand>().speed = downSpeed * 2;
            boss.progressState = 5;
            boss.bossAnimator.SetInteger("AniState", 25);
            boss.HLAnimator.SetInteger("AniState", 5);
            yield return new WaitForSeconds(0.3f);
            boss.HL.GetComponent<MBHand>().AttackHand();
            //boss.SetEffect(0, boss.HL.GetComponent<MBHand>().effPos.transform.position);
            yield return new WaitForSeconds(0.5f);
        }
        
        boss.ConnectHandL();
        boss.progressState = 6;
        boss.bossAnimator.SetInteger("AniState", 26);
        yield return new WaitForSeconds(1);

        boss.progressState = 7;
        boss.bossAnimator.SetInteger("AniState", 27);
        boss.handPosSync = true;
        yield return new WaitForSeconds(2);
       
        boss.progressState = 8;
        boss.bossAnimator.SetInteger("AniState", 28);
        boss.handPosSync = false;
        boss.DisConnectHandR();
        theta = 2;
        dis = Vector3.Distance(boss.transform.position, Player.instance.transform.position) - 10;
        boss.HR.GetComponent<MBHand>().grabbed = false;
        boss.HR.GetComponent<MBHand>().speed = downSpeed;
        boss.HRAnimator.SetInteger("AniState", 3);
        boss.SetEffect(12, boss.HR.GetComponent<MBHand>().effPos, 1);
        yield return new WaitForSeconds(3);
        if (boss.HR.GetComponent<TimeAgent>().set == true)
        {
            boss.progressState = 0;
            boss.HRAnimator.SetInteger("AniState", 2);
            for (int i = 0; i < 7; i++)
            {
                if (boss.HR.GetComponent<TimeAgent>().set == true)
                {
                    yield return new WaitForSeconds(1);
                }
            }
        }
        else
        {

            boss.progressState = 9;
            boss.bossAnimator.SetInteger("AniState", 29);
            boss.HRAnimator.SetInteger("AniState", 4);

            yield return new WaitForSeconds(1.5f);
            boss.HR.GetComponent<MBHand>().speed = downSpeed * 2;

            boss.progressState = 10;
            boss.bossAnimator.SetInteger("AniState", 30);
            boss.HRAnimator.SetInteger("AniState", 5);
            yield return new WaitForSeconds(0.3f);
            boss.HR.GetComponent<MBHand>().AttackHand();
            //boss.SetEffect(0, boss.HR.GetComponent<MBHand>().effPos.transform.position);
            yield return new WaitForSeconds(0.5f);
        }

        boss.ConnectHandR();

        boss.bossFSM = MekaBoss.MBossFSM.Idle;
        boss.bossAnimator.SetInteger("AniState", 0);
        
    }
    IEnumerator Pattern_3()
    {
        prevPattern = 3;
        if (eventManager.evtBool[3] == false)
        {
            eventManager.evtBool[3] = true;
            eventManager.EventTrigger("BossP3");
        }
        HandTimeSetFalse();
        boss.bossFSM = MekaBoss.MBossFSM.Pattern3;
        boss.progressState = 1;
        boss.bossAnimator.SetInteger("AniState", 31);
        boss.SetLaserEffect();
        SoundManager.instance.RandomPlay(9, 2, 3);
        yield return new WaitForSeconds(5);
        
        boss.progressState = 2;
        boss.laser.SetActive(true);
        boss.laserTarget.SetActive(true);
        boss.laserTarget.transform.position = Player.instance.transform.position + 0.1f * (boss.transform.position - Player.instance.transform.position);
        boss.bossAnimator.SetInteger("AniState", 32);
        SoundManager.instance.SoundPlay3D(9, 4, boss.laser.transform.position, 11.0);
        yield return new WaitForSeconds(10);
        boss.progressState = 3;
        boss.laser.SetActive(false);
        boss.laserTarget.SetActive(false);
        boss.bossAnimator.SetInteger("AniState", 33);
        boss.weakness[0].SetActive(true);
        boss.weakness[0].GetComponent<WeaknessAgent>().Init();
        yield return new WaitForSeconds(20);
        boss.weakness[0].SetActive(false);
        boss.bossFSM = MekaBoss.MBossFSM.Idle;
        boss.bossAnimator.SetInteger("AniState", 0);
        
    }
    IEnumerator Pattern_4()
    {
        prevPattern = 4;
        if (eventManager.evtBool[4] == false)
        {
            eventManager.evtBool[4] = true;
            eventManager.EventTrigger("BossP4");
        }
        HandTimeSetFalse();
        boss.bossFSM = MekaBoss.MBossFSM.Pattern4;
        boss.progressState = 1;
        boss.bossAnimator.SetInteger("AniState", 41);
        yield return new WaitForSeconds(1);
        boss.progressState = 2;
        boss.bossAnimator.SetInteger("AniState", 42);
        boss.SetEffect(4, boss.missilePos.transform.position);
        yield return new WaitForSeconds(1);
        boss.SetEffect(2, gameObject.transform.position);
        for (int i = 0; i < 100; i++)
        {
            SetMissile();
            yield return new WaitForSeconds(0.01f);
        }
        //yield return new WaitForSeconds(1);
        //boss.SetEffect(2, gameObject.transform.position);
        //for (int i = 0; i < 100; i++)
        //{
        //    SetMissile();
        //    yield return new WaitForSeconds(0.01f);
        //}
        yield return new WaitForSeconds(3);
        boss.bossFSM = MekaBoss.MBossFSM.Idle;
        boss.bossAnimator.SetInteger("AniState", 0);
    }
    IEnumerator Pattern_5()
    {
        prevPattern = 5;
        if (eventManager.evtBool[5] == false)
        {
            eventManager.evtBool[5] = true;
            eventManager.EventTrigger("BossP5");
        }
        SoundManager.instance.SoundPlay(9, 26);
        boss.bossFSM = MekaBoss.MBossFSM.Pattern5;
        boss.bossAnimator.SetInteger("AniState", 51);
        boss.progressState = 1;
        boss.SetEffect(7, boss.transform.position, 20);
        boss.SetEffect(9, boss.transform.position, 20);
        for (int i = 0; i < 4; i++)
        {
            boss.cooler[i].GetComponent<CoolerAgent>().HP_Point = boss.cooler[i].GetComponent<CoolerAgent>().HP_PointMax;
        }
        boss.p5Started = true;
        yield return new WaitForSeconds(6);
        boss.bigSphere.SetActive(true);
        boss.bigSphere.transform.localPosition = new Vector3(0,23.4f, 16.41f);
        
        boss.bossAnimator.SetInteger("AniState", 52);
        boss.progressState = 2;
        yield return new WaitForSeconds(8);
        if(boss.bossFSM != BossStatus.MBossFSM.Dead)
        {
            Player.instance.Hit(10000);
            yield return new WaitForSeconds(0.5f);
            Player.instance.Hit(10000);
            fadeWhite.SetActive(true);
            fadeWhite.GetComponent<Animator>().SetBool("Fade", true);
            ScreenBlur_script.instance.SetBlur(20, 24, 3, 5);
            yield return new WaitForSeconds(0.5f);
            Player.instance.Hit(10000);
        }
    }
    IEnumerator Pattern_6()
    {
        prevPattern = 6;
        if (eventManager.evtBool[6] == false)
        {
            eventManager.evtBool[6] = true;
            eventManager.EventTrigger("BossP6");
        }
        HandTimeSetFalse();
        boss.bossFSM = MekaBoss.MBossFSM.Pattern6;
        
        boss.SetEffect(5, boss.p6EffPos, 10);
        boss.progressState = 1;
        boss.bossAnimator.SetInteger("AniState", 61);
        yield return new WaitForSeconds(4.4f);
        boss.progressState = 2;
        boss.bossAnimator.SetInteger("AniState", 62);
        yield return new WaitForSeconds(0.1f);
        boss.SetEffect(6, boss.p6EffPos, 1);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j< 4; j++)
            {
                SetBall(1,3);
                SetBall(0,5);
                SetBall(0,6);
            }
            SoundManager.instance.SoundPlay3D(9,27,gameObject.transform.position,0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return new WaitForSeconds(2.0f);
        boss.SetEffect(6, boss.p6EffPos, 1);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SetBall(1, 3);
                SetBall(0, 5);
                SetBall(0, 6);
            }
            SoundManager.instance.SoundPlay3D(9, 27, gameObject.transform.position, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        boss.SetEffect(6, boss.p6EffPos, 1);
        for (int j = 0; j < 10; j++)
        {
            boss.InitBall(1, 20, Player.instance.transform.position + new Vector3(0,1.5f,0));
            SoundManager.instance.SoundPlay3D(9, 27, gameObject.transform.position, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1.0f);
        boss.SetEffect(6, boss.p6EffPos, 1);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SetBall(1, 3);
                SetBall(0, 5);
                SetBall(0, 6);
            }
            SoundManager.instance.SoundPlay3D(9, 27, gameObject.transform.position, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
       
        yield return new WaitForSeconds(1.0f);
        boss.progressState = 3;
        boss.bossAnimator.SetInteger("AniState", 63);
        yield return new WaitForSeconds(2);
        boss.bossFSM = MekaBoss.MBossFSM.Idle;
        boss.bossAnimator.SetInteger("AniState", 0);
    }
    IEnumerator Pattern_7()
    {
        prevPattern = 7;
        if (eventManager.evtBool[7] == false)
        {
            eventManager.evtBool[7] = true;
            eventManager.EventTrigger("BossP7");
        }
        HandTimeSetFalse();
        boss.bossFSM = MekaBoss.MBossFSM.Pattern7;
        boss.progressState = 1;
        boss.bossAnimator.SetInteger("AniState", 71);
        boss.InitSphere(0);
        boss.InitSphere(1);
        boss.SetEffect(13, boss.ballPos[0], 5);
        boss.SetEffect(13, boss.ballPos[1], 5);
        SoundManager.instance.SoundPlay(9, 19);
        yield return new WaitForSeconds(3);
        boss.progressState = 2;
        boss.bossAnimator.SetInteger("AniState", 72);
        yield return new WaitForSeconds(1);
        boss.sphereObj[0].GetComponent<BossSphere>().Fire();
        SoundManager.instance.SoundPlay(9, 20);
        yield return new WaitForSeconds(2);
        boss.progressState = 3;
        boss.bossAnimator.SetInteger("AniState", 73);
        yield return new WaitForSeconds(1);
        boss.sphereObj[1].GetComponent<BossSphere>().Fire();
        SoundManager.instance.SoundPlay(9, 21);
        yield return new WaitForSeconds(2);
        boss.bossFSM = MekaBoss.MBossFSM.Idle;
        boss.bossAnimator.SetInteger("AniState", 0);
    }
    IEnumerator Pattern_8()
    {
        prevPattern = 8;
        if (eventManager.evtBool[8] == false)
        {
            eventManager.evtBool[8] = true;
            eventManager.EventTrigger("BossP8");
        }
        HandTimeSetFalse();
        boss.bossFSM = MekaBoss.MBossFSM.Pattern8;
        boss.progressState = 1;
        boss.bossAnimator.SetInteger("AniState", 81);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            { 
                boss.InitBullet();
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.3f);
        boss.progressState = 2;
        boss.FireBullet();
        SoundManager.instance.SoundPlay(9, 28);
        yield return new WaitForSeconds(3);
        boss.bossFSM = MekaBoss.MBossFSM.Idle;
        boss.bossAnimator.SetInteger("AniState", 0);
    }

    void SetMissile()
    {
        Vector3 nPos = Vector3.zero;
        theta = Random.Range(-2.3f, 2.3f);
        dis = Random.Range(20, 43);
        nPos.x = dis * Mathf.Sin(theta);
        nPos.z = dis * Mathf.Cos(theta);
        missleTargetPos.transform.localPosition = nPos;
        nPos = missleTargetPos.transform.position;
        nPos.y = Player.instance.transform.position.y;
        boss.InitMissile(nPos);
    }
    void SetBall(int type,int speed)
    {
        Vector3 nPos = Vector3.zero;
        theta = Random.Range(-1.5f, 1.5f);
        dis = 42;
        nPos.x = dis * Mathf.Sin(theta);
        nPos.z = dis * Mathf.Cos(theta);
        missleTargetPos.transform.localPosition = nPos;
        nPos = missleTargetPos.transform.position;
        nPos.y = Player.instance.transform.position.y + 1;
        boss.InitBall(type,speed,nPos);
    }


    public void ForceStop()
    {

        StartCoroutine(HitState());

    }

    IEnumerator HitState()
    {
        switch (boss.bossFSM)
        {
            case BossStatus.MBossFSM.Pattern1:
            case BossStatus.MBossFSM.Pattern2:
                boss.bossAnimator.SetInteger("HitState", 1);
                break;
            case BossStatus.MBossFSM.Pattern3:
                boss.bossAnimator.SetInteger("HitState", 2);
                break;
        }
        yield return new WaitForSeconds(2);
        boss.bossAnimator.SetInteger("HitState", 0);
    }
}
