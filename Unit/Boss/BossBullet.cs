﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public bool enable;
    public int damagePoint;
    public float moveSpeed;
    public float rotateSpeed;
    TimeAgent timeAgent;
    public bool bulletTime;
    public GameObject hitEffect;
    public GameObject target;
    public GameObject[] trailEff;
    bool end;
    // Start is called before the first frame update
    void Start()
    {
        timeAgent = gameObject.GetComponent<TimeAgent>();
        target = Player.instance.gameObject;
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(enable == false)
        {
            return;
        }
        if (timeAgent.set == false)
        {
            if (bulletTime == true)
            {
                RaycastHit rayHit;
                int mask = 1 << 8;
                if (Physics.SphereCast(gameObject.transform.position, 0.1f, gameObject.transform.forward, out rayHit, 3f, mask))
                {
                    if (SkillManager.instance.bulletTimeNow < 0.1f)
                    {
                        SkillManager.instance.bulletTimeNow = 0.1f;

                    }
                }

            }
        }
        if (timeAgent.set == false)
        {
            Turn();
        }
        else
        {
            if (end == false)
            {
                end = true;
                OffEff();
            }
        }
        Move();
    }

    public void OnEff()
    {
        trailEff[0].SetActive(true);

    }

    void OffEff()
    {
        trailEff[0].SetActive(false);

    }

    void Move()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * timeAgent.speedFloat, gameObject.transform);
    }

    void Turn()
    {
        Vector3 vec = target.transform.position + new Vector3(0, 0.1f, 0) - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, q, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.GetComponent<Player>().PlayerFSM == PlayerStatus.EPlayerFSM.Dodge)
            {
                //Debug.Log("dodge");
            }
            else
            {
                if (timeAgent.set == false)
                {
                    
                    StartHitEffect(gameObject.transform.position, gameObject.transform.eulerAngles);
                    other.gameObject.GetComponent<Unit>().Hit(damagePoint);
                    if (end == false)
                    {
                        end = true;
                        OffEff();
                    }
                    Destroy(gameObject);
                }
            }

        }
        else if (other.gameObject.layer == 9)
        {
        }
        else if (other.gameObject.layer == 2)
        {
        }
        else
        {
            
            StartHitEffect(gameObject.transform.position, gameObject.transform.eulerAngles);
            if (end == false)
            {
                end = true;
                OffEff();
            }
            Destroy(gameObject);
        }
    }

    public void StartHitEffect(Vector3 pos, Vector3 rot)
    {
        GameObject eGunHit = Instantiate(hitEffect);
        eGunHit.transform.position = pos;
        eGunHit.transform.rotation = Quaternion.LookRotation(rot);
        Destroy(eGunHit, 1f);
    }
}
