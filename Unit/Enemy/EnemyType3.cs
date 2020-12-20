using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : EnemyStatus
{
    public GameObject rayObj;
    public GameObject energyBall;
    public GameObject gun;
    public GameObject fireEff;
    public Vector3 offset;
    public void LateUpdate()
    {
        if (timeAgent.speedFloat < 0.5f)
        {
            return;
        }
        gun.transform.LookAt(Player.instance.transform.position + new Vector3(0, 1, 0));
        gun.transform.eulerAngles += offset;
    }
    public void FireManyBullet(GameObject obj)
    {
        StartSkillSign();
        StartGunFireEffect();
        SoundManager.instance.RandomPlayNew(4, 6, 8, gameObject.transform.position, 0.5f);
        StartCoroutine(DelayShot(obj));
    }
    public void FireEnergyBall(GameObject obj)
    {
        if (EnemyFSM == EnemyStatus.EEnemyFSM.Dead)
        {
            return;
        }
        GameObject ball = Instantiate(energyBall);
        ball.transform.position = obj.transform.position;
        ball.transform.rotation = obj.transform.rotation;
        Vector3 newRot = ball.transform.eulerAngles;
        newRot.y += Random.Range(60, -60);
        ball.transform.eulerAngles = newRot;
        ball.GetComponent<EnemyMissile>().target = player;
    }

    IEnumerator DelayShot(GameObject obj)
    {
        for (int i = 0; i < 10; i++)
        {
            
            FireEnergyBall(obj);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Move(Vector3 pos)
    {
        Vector2 tPos;
        Vector2 gPos;
        tPos.x = pos.x;
        tPos.y = pos.z;
        gPos.x = gameObject.transform.position.x;
        gPos.y = gameObject.transform.position.z;
        if(Vector2.Distance(tPos, gPos) > 5)
        {
            Turn(pos);
            gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, gameObject.transform);
        }
        
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
