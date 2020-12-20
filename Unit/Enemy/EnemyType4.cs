using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType4 : EnemyStatus
{
    public GameObject rayObj;
    public GameObject readyEff;
    public GameObject laserEff;
    public GameObject laserEff2;
    public GameObject fireEff;
    float rayWidth;
    Vector3 targetPos;
    public GameObject gun;
    public Vector3 offset;

    Vector3 lookTarget;
    bool shotReady;
    public override void Update()
    {
        base.Update();
    }
    public void LateUpdate()
    {
        
        if (timeAgent.speedFloat < 0.5f)
        {
            return;
        }
        if (shotReady == true)
        {
            lookTarget = targetPos;
        }
        else
        {

            lookTarget = Player.instance.transform.position + new Vector3(0, 1, 0);
        }
        gun.transform.LookAt(lookTarget);
        gun.transform.eulerAngles += offset;
       
    }
    public void FireRaser()
    {
        shotReady = true;
        StartSkillSign();
        StartGunFireEffect();
        SoundManager.instance.SoundPlay3D(4, 0, gameObject.transform.position);
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 9 | 1 << 10;
        mask = ~mask;
        targetPos = player.transform.position + new Vector3(0, 1, 0);
        rayObj.transform.LookAt(targetPos);
        if (Physics.Raycast(rayObj.transform.position, rayObj.transform.forward, out rayHit, 50, mask))
        {
           rayWidth =  Vector3.Distance(rayObj.transform.position, rayHit.point);
        }
        GameObject eff = GameObject.Instantiate(readyEff);
        eff.transform.position = rayObj.transform.position;
        eff.transform.rotation = rayObj.transform.rotation;
        Destroy(eff, 1f);
        Invoke("RaserHit", 1f);
    }


    void RaserHit()
    {
        if (EnemyFSM == EnemyStatus.EEnemyFSM.Dead)
        {
            return;
        }
        SoundManager.instance.SoundPlay3D(4, 1, gameObject.transform.position);
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 9 | 1 << 10;
        mask = ~mask;
        rayObj.transform.LookAt(targetPos);
        if (Physics.Raycast(rayObj.transform.position, rayObj.transform.forward, out rayHit, 50, mask))
        {
            if (rayHit.transform.gameObject.GetComponent<Player>())
            {
                rayHit.transform.gameObject.GetComponent<Player>().Hit(50, true);
            }
        }
        GameObject eff = GameObject.Instantiate(laserEff);
        eff.transform.position = rayObj.transform.position;
        eff.transform.rotation = rayObj.transform.rotation;
        eff.transform.GetChild(0).localScale = new Vector3(1, 1, rayWidth);
        Destroy(eff, 3);
        GameObject eff2 = GameObject.Instantiate(laserEff2);
        eff2.transform.position = rayObj.transform.position;
        Destroy(eff2, 3);
        shotReady = false;
    }

    public void StartGunFireEffect()
    {
        GameObject eGunFire = Instantiate(fireEff);
        eGunFire.transform.parent = rayObj.transform;
        eGunFire.transform.position = rayObj.transform.position;
        eGunFire.transform.rotation = rayObj.transform.rotation;
        Destroy(eGunFire, 1f);
    }
}
