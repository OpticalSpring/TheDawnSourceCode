using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyStatus
{
    public GameObject fireEff;
    public GameObject energyBall;
    public GameObject firePos;
    public GameObject attackPoint;
    public GameObject attackEff;
    public float attackRange;
    public float closeAttackDelayTime;
    public float closeAttackDelayTimeNow;

    public void Attack(GameObject target)
    {
        target.GetComponent<Player>().Hit(50);
    }

    public void Attack()
    {
        StartAttackEffect();
        
        Collider[] colliderArray = Physics.OverlapSphere(attackPoint.transform.position, attackRange);
        
        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].GetComponent<Player>())
            {
                if (colliderArray[i].GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dodge)
                {
                    colliderArray[i].GetComponent<Player>().Hit(50, true);
                }
            }
        }
    }

    public void Fire()
    {
        StartSkillSign();
        Invoke("FireEnergyBall", 0.3f);
    }
    public void FireEnergyBall()
    {
        if (EnemyFSM == EnemyStatus.EEnemyFSM.Dead)
        {
            return;
        }
        firePos.transform.LookAt(player.transform.position + new Vector3(0, 1, 0));
        SoundManager.instance.RandomPlayNew(4, 6, 8, gameObject.transform.position,0.5f);
        GameObject ball = Instantiate(energyBall);
        ball.transform.position = firePos.transform.position;
        ball.transform.rotation = firePos.transform.rotation;
        Vector3 newRot = ball.transform.eulerAngles;
        newRot.y += Random.Range(5, -5);
        ball.transform.eulerAngles = newRot;
        StartGunFireEffect();
    }

    public void StartGunFireEffect()
    {
        GameObject eGunFire = Instantiate(fireEff);
        eGunFire.transform.position = firePos.transform.position;
        eGunFire.transform.rotation = firePos.transform.rotation;
        Destroy(eGunFire, 1f);
    }

    public void StartAttackEffect()
    {
        GameObject eGunFire = Instantiate(attackEff);
        eGunFire.transform.position = attackPoint.transform.position;
        eGunFire.transform.rotation = attackPoint.transform.rotation;
        Destroy(eGunFire, 1f);
    }
}
