using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : EnemyStatus
{
    public GameObject fireEff;
    public GameObject firePos;
    public GameObject energyBall;

    public void FireManyBullet()
    {
        StartSkillSign();
        Invoke("FireEnergyBall", 0.3f);
        Invoke("FireEnergyBall", 0.4f);
        Invoke("FireEnergyBall", 0.5f);
        SoundManager.instance.RandomPlayNew(4, 6, 8, gameObject.transform.position, 0.5f);
    }

    public void FireEnergyBall()
    {
        if (EnemyFSM == EnemyStatus.EEnemyFSM.Dead)
        {
            return;
        }
        firePos.transform.LookAt(player.transform.position + new Vector3(0, 1, 0));
        GameObject ball = Instantiate(energyBall);
        ball.transform.position = firePos.transform.position;
        ball.transform.rotation = firePos.transform.rotation;
        Vector3 newRot = ball.transform.eulerAngles;
        newRot.y += Random.Range(3, -3);
        ball.transform.eulerAngles = newRot;
        StartGunFireEffect();
    }

    public void StartGunFireEffect()
    {
        GameObject eGunFire = Instantiate(fireEff);
        eGunFire.transform.parent = firePos.transform;
        eGunFire.transform.position = firePos.transform.position;
        eGunFire.transform.rotation = firePos.transform.rotation;
        Destroy(eGunFire, 1f);
    }
}
