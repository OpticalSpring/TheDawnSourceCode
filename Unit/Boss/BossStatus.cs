using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : Unit
{
    public GameObject model;
    public float rotationSpeed;
    // Start is called before the first frame update
    public enum MBossFSM
    {
        Idle,
        Pattern1,
        Pattern2,
        Pattern3,
        Pattern4,
        Pattern5,
        Pattern6,
        Pattern7,
        Pattern8,
        Stop,
        Dead
    }
    public MBossFSM bossFSM;

    public void Turn(GameObject obj)
    {
        
        float dz = Player.instance.transform.position.z - obj.transform.position.z;
        float dx = Player.instance.transform.position.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), rotationSpeed * Time.deltaTime);
    }
    public override int Hit(int damage)
    {
        if(bossFSM == MBossFSM.Stop)
        {
            return 1;
        }
        
        return base.Hit(damage);
    }
    public override void Die()
    {
        if (bossFSM == MBossFSM.Dead)
        {
            return;
        }
        //animator.SetAniState(10);
        bossFSM = MBossFSM.Dead;
        //SoundManager.instance.SoundPlay3D(5, 9, gameObject.transform.position);
        //SoundManager.instance.RandomPlayNew(10, 0, 4, gameObject.transform.position, 0.5f);
        EventManager_2.instance.nextReady = true;
        MekaBoss.instance.ConnectHandL();
        MekaBoss.instance.ConnectHandR();
        gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);
        gameObject.GetComponent<MekaBossAI>().StopAllCoroutines();
        PlayerUISystem.instance.SetKill();
        PlayerUISystem.instance.bossUI.GetComponent<BossUISystem>().aniState = 2;
        SoundManager.instance.SoundStop(9, 26);
        //GameObject model = gameObject.transform.GetChild(0).gameObject;
        //model.transform.parent = null;
        //Destroy(gameObject, 5);
        // GetComponent<DetachAgent>().Detach();
    }
}
