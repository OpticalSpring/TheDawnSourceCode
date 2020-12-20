using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBHand : MonoBehaviour
{
    public MekaBoss meka;
    public Vector3 target;
    [HideInInspector]
    public TimeAgent timeAgent;
    Animator animator;
    public float speed;

    public float handRange;
    public GameObject grabPos;
    public bool grabbed;
    public GameObject effPos;
    public GameObject pivot;
    public GameObject weakness;
    public GameObject chargeEff;
    public EventManager_2 eventManager;
    // Start is called before the first frame update
    void Awake()
    {
        timeAgent = gameObject.GetComponent<TimeAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = timeAgent.speedFloat;
        if (timeAgent.speedFloat < 0.5f)
        {
            MekaBoss.instance.bossAnimator.SetInteger("AniState", 0);
            MekaBoss.instance.bossAnimator.SetBool("Stop", true);
            return;
        }
        else
        {
            MekaBoss.instance.bossAnimator.SetBool("Stop", false);
        }
        if(meka.handPosSync == false)
        {

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, Time.deltaTime * speed * timeAgent.speedFloat);
        }
        if(grabbed == true)
        {
            if(Player.instance.PlayerFSM == PlayerStatus.EPlayerFSM.Grabbed)
            {

                Player.instance.transform.position= grabPos.transform.position;
                if (timeAgent.set == true)
                {
                    Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Hipfire;
                   
                }
            }
            else
            {
                
            }
        }
    }


    public void AttackHand()
    {
        if(timeAgent.set == true)
        {
            return;
        }
        SoundManager.instance.RandomPlay(9, 0, 1);
        PlayerCameraSystem.instance.Shaking(100 * 0.01f, 0.2f);
        MekaBoss.instance.SetEffect(0, effPos.transform.position);
        Collider[] colliderArray = Physics.OverlapSphere(pivot.transform.position, handRange);

        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].GetComponent<Player>())
            {
                if (colliderArray[i].GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dodge)
                {
                    if(Player.instance.PlayerFSM == PlayerStatus.EPlayerFSM.Grabbed)
                    {
                        colliderArray[i].GetComponent<Player>().Hit(100);
                    }
                    else
                    {

                        colliderArray[i].GetComponent<Player>().Hit(50, true);
                    }
                }
            }
        }
    }
    public void BulletTime()
    {
        if (timeAgent.set == true)
        {
            return;
        }
        if (grabbed == true)
        {
            return;
        }
        Collider[] colliderArray = Physics.OverlapSphere(grabPos.transform.position, handRange);

        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].GetComponent<Player>())
            {
                if (colliderArray[i].GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dodge)
                {
                    if (SkillManager.instance.bulletTimeNow < 0.1f)
                    {
                        SkillManager.instance.bulletTimeNow = 0.1f;

                    }
                }
            }
        }
    }
    public void CatchHand()
    {
        if (timeAgent.set == true)
        {
            return;
        }
        if(grabbed == true)
        {
            return;
        }
        Collider[] colliderArray = Physics.OverlapSphere(pivot.transform.position, handRange/2);

        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].GetComponent<Player>())
            {
                if (colliderArray[i].GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dodge)
                {
                    if (eventManager.evtBool[2] == false)
                    {
                        eventManager.evtBool[2] = true;
                        eventManager.EventTrigger("BossP22");
                    }
                    grabbed = true;
                    Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Grabbed;
                    if(Player.instance.timeRecallDelayTimeNow <= 0)
                    {
                        PlayerUISystem.instance.popupTimeRecall.SetActive(true);
                    }
                }
            }
        }
    }
}
