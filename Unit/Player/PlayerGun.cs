using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public Player player;
    Material mat;
    public GameObject gunObj;
    public GameObject gunFirePos;
    public GameObject bulletPrefab;
    public GameObject gunHipFireEffect;
    public GameObject gunShoulderFireEffect;
    public GameObject gunHitEffect;
    public GameObject gunHitEffectWall;
    public GameObject gunHitEffectShield;
    public GameObject enemyExpolsionEff;
    public float dv;
    public float dvNow;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        mat = gunObj.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        DissolveGun();
        CrossHairLock(player.rayDes.transform.position);
    }

    void DissolveGun()
    {
        if(player.PlayerMode == PlayerStatus.EPlayerMode.NonCombatMode)
        {
            dv = 0.8f;
        }
        else
        {
            if(player.PlayerFSM == PlayerStatus.EPlayerFSM.Dead)
            {
                dv = 0.8f;
            }
            else if(player.PlayerFSM == PlayerStatus.EPlayerFSM.Interaction)
            {
                dv = 0.8f;
            }
            else if (player.PlayerFSM == PlayerStatus.EPlayerFSM.TimeStop)
            {
                dv = 0.8f;
            }
            else if (player.PlayerFSM == PlayerStatus.EPlayerFSM.TimeRecall)
            {
                dv = 0.8f;
            }
            else
            {
                dv = 0.3f;
            }
        }
        dvNow = Mathf.Lerp(dvNow, dv, Time.deltaTime * 5);
        
        mat.SetFloat("_DissolveScale", dvNow);
        if(dvNow > 0.7f)
        {
            gunObj.SetActive(false);
        }
        else
        {
            gunObj.SetActive(true);
        }
    }

    public void HipFire(Vector3 desPos)
    {
        bool ef = false;
        PlayerUISystem.instance.SetShot();
        SoundManager.instance.RandomPlayNew(2, 0, 2, Camera.main.transform.position, 0.25f);
        SoundManager.instance.RandomPlayNew(2, 3, 4, Camera.main.transform.position, 0.25f);
        gunFirePos.transform.LookAt(desPos);
        StartGunHipFireEffect();
        RaycastHit rayHit = new RaycastHit();
        int mask = 1 << 2 | 1 << 8 | 1 << 10;
        mask = ~mask;
        //랜덤으로 20개의 산탄 발사
        for (int i = 0; i < 20; i++)
        {
            gunFirePos.transform.LookAt(desPos);
            Vector3 randomRot = gunFirePos.transform.localEulerAngles;
            randomRot.y += Random.Range(5, -5);
            randomRot.x += Random.Range(20, -20);
            gunFirePos.transform.localEulerAngles = randomRot;
            if (Physics.Raycast(gunFirePos.transform.position, gunFirePos.transform.forward, out rayHit, 10f, mask))
            {
                //이펙트는 지저분하지 않게 1/4만 출력
                if(i % 4 == 0)
                {
                    StartGunHitEffectWall(rayHit.point, rayHit.normal);
                }
                //적의 종류 및 부위에 따른 판정
                if (rayHit.collider.gameObject.GetComponent<ShieldAgent>())
                {
                }
                else if (rayHit.transform.gameObject.GetComponent<HeadAgent>())
                {
                    int headDamage = (int)(player.hipFireDamage * 2.5f);
                    if(rayHit.transform.gameObject.GetComponent<HeadAgent>().enemy.GetComponent<Unit>().HP_Point<= 0)
                    {
                        return;
                    }
                    StartGunHitEffect(rayHit.point, rayHit.normal);
                    PlayerUISystem.instance.SetHead();
                    if (rayHit.transform.gameObject.GetComponent<HeadAgent>().enemy.GetComponent<Unit>().Hit(headDamage) <= 0)
                    {
                        if(ef == false)
                        {
                            ef = true;
                        StartEnemyExplosionEffect(rayHit.point, rayHit.normal);
                        }
                    }
                }
                else if (rayHit.transform.gameObject.GetComponent<EnemyStatus>())
                {
                    if (rayHit.transform.gameObject.GetComponent<Unit>().HP_Point <= 0)
                    {
                        return;
                    }
                    StartGunHitEffect(rayHit.point, rayHit.normal);
                    PlayerUISystem.instance.SetHit();
                    if (rayHit.transform.gameObject.GetComponent<Unit>().Hit(player.hipFireDamage) <= 0)
                    {
                        if (ef == false)
                        {
                            ef = true;
                            StartEnemyExplosionEffect(rayHit.point, rayHit.normal);
                        }
                    }
                }
                else if (rayHit.transform.gameObject.GetComponent<WeaknessAgent>())
                {
                    
                    if (rayHit.transform.gameObject.GetComponent<WeaknessAgent>().HP_Point <= 0)
                    {
                        return;
                    }
                    StartGunHitEffect(rayHit.point, rayHit.normal);
                    PlayerUISystem.instance.SetHead();
                    rayHit.transform.gameObject.GetComponent<WeaknessAgent>().Hit(player.hipFireDamage);
                }
                else if (rayHit.transform.gameObject.GetComponent<BossStatus>())
                {
                    if (rayHit.transform.gameObject.GetComponent<Unit>().HP_Point <= 0)
                    {
                        return;
                    }
                    StartGunHitEffect(rayHit.point, rayHit.normal);
                    PlayerUISystem.instance.SetHit();
                    if (rayHit.transform.gameObject.GetComponent<Unit>().Hit(player.hipFireDamage) <= 0)
                    {
                        if (ef == false)
                        {
                            ef = true;
                            StartEnemyExplosionEffect(rayHit.point, rayHit.normal);
                        }
                    }
                }
            }
        }
    }


    void CrossHairLock(Vector3 desPos)
    {
        RaycastHit rayHit = new RaycastHit();
        int mask = 1 << 2 | 1 << 8 | 1 << 10;
        mask = ~mask;
        gunFirePos.transform.LookAt(desPos);
        if (Physics.Raycast(gunFirePos.transform.position, gunFirePos.transform.forward, out rayHit, 8f, mask))
        {
            if (rayHit.transform.gameObject.GetComponent<ShieldAgent>())
            {
                PlayerUISystem.instance.normalLock = true;
            }
            else if (rayHit.transform.gameObject.GetComponent<HeadAgent>())
            {
                PlayerUISystem.instance.normalLock = true;
            }
            else if (rayHit.transform.gameObject.GetComponent<EnemyStatus>())
            {
                PlayerUISystem.instance.normalLock = true;
            }
            else
            {
                PlayerUISystem.instance.normalLock = false;
            }
        }
        else
        {
            PlayerUISystem.instance.normalLock = false;
        }
    }
    public void ShoulderFire(Vector3 desPos)
    {
        SoundManager.instance.RandomPlayNew(2, 5, 7, Camera.main.transform.position, 0.25f);
        SoundManager.instance.RandomPlayNew(2, 8, 9, Camera.main.transform.position, 0.25f);
        SoundManager.instance.SoundPlay3D(2, 10, Camera.main.transform.position, 0.25f);
        StartGunShoulderFireEffect();
        PlayerUISystem.instance.SetShot();
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = gunFirePos.transform.position;
        bullet.transform.LookAt(desPos);
        bullet.GetComponent<PlayerBullet>().gun = this;
        bullet.GetComponent<PlayerBullet>().desPos = desPos;

        gunFirePos.transform.LookAt(desPos);
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 10;
        mask = ~mask;
        if (Physics.Raycast(gunFirePos.transform.position, gunFirePos.transform.forward, out rayHit, 100f, mask))
        {
            //적의 종류 및 부위에 따른 판정
            if (rayHit.collider.gameObject.GetComponent<ShieldAgent>())
            {
                StartGunHitEffectWall(rayHit.point, rayHit.normal);
                
            }
            else if (rayHit.transform.gameObject.GetComponent<HeadAgent>())
            {
                int headDamage = (int)(player.shoulderFireDamage * 2.5f);
                if (rayHit.transform.gameObject.GetComponent<HeadAgent>().enemy.GetComponent<Unit>().HP_Point <= 0)
                {
                    return;
                }
                StartGunHitEffect(rayHit.point, rayHit.normal);
                PlayerUISystem.instance.SetHead();
                if (rayHit.transform.gameObject.GetComponent<HeadAgent>().enemy.GetComponent<Unit>().Hit(headDamage) <= 0)
                {
                    StartEnemyExplosionEffect(rayHit.point, rayHit.normal);
                }
            }
            else if (rayHit.transform.gameObject.GetComponent<EnemyStatus>())
            {
                if (rayHit.transform.gameObject.GetComponent<Unit>().HP_Point <= 0)
                {
                    return;
                }
                StartGunHitEffect(rayHit.point, rayHit.normal);
                PlayerUISystem.instance.SetHit();
                if (rayHit.transform.gameObject.GetComponent<Unit>().Hit(player.shoulderFireDamage) <= 0)
                {
                    StartEnemyExplosionEffect(rayHit.point, rayHit.normal);
                }
            }
            else if (rayHit.transform.gameObject.GetComponent<WeaknessAgent>())
            {
                
                if (rayHit.transform.gameObject.GetComponent<WeaknessAgent>().HP_Point <= 0)
                {
                    return;
                }
                StartGunHitEffect(rayHit.point, rayHit.normal);
                PlayerUISystem.instance.SetHead();
                rayHit.transform.gameObject.GetComponent<WeaknessAgent>().Hit(player.shoulderFireDamage);
            }
            else if (rayHit.transform.gameObject.GetComponent<BossStatus>())
            {
                if (rayHit.transform.gameObject.GetComponent<Unit>().HP_Point <= 0)
                {
                    return;
                }
                StartGunHitEffect(rayHit.point, rayHit.normal);
                PlayerUISystem.instance.SetHit();
                if (rayHit.transform.gameObject.GetComponent<Unit>().Hit(player.shoulderFireDamage) <= 0)
                {
                    StartEnemyExplosionEffect(rayHit.point, rayHit.normal);
                }
            }
            else
            {
                StartGunHitEffectWall(rayHit.point, rayHit.normal);
            }
        }
    }

    public void StartGunHipFireEffect()
    {
        GameObject eGunFire = Instantiate(gunHipFireEffect);
        //eGunFire.transform.parent = gunFirePos.transform;
        eGunFire.transform.position = gunFirePos.transform.position;
        eGunFire.transform.rotation = gunFirePos.transform.rotation;
        Destroy(eGunFire, 5.0f);
    }

    public void StartGunShoulderFireEffect()
    {
        GameObject eGunFire = Instantiate(gunShoulderFireEffect);
        eGunFire.transform.parent = gunFirePos.transform;
        eGunFire.transform.position = gunFirePos.transform.position;
        eGunFire.transform.rotation = gunFirePos.transform.rotation;
        Destroy(eGunFire, 0.2f);
    }

    public void StartGunHitEffect(Vector3 pos, Vector3 rot)
    {
        GameObject eGunHit = Instantiate(gunHitEffect);
        eGunHit.transform.position = pos;
        eGunHit.transform.rotation = Quaternion.LookRotation(rot);
        Destroy(eGunHit, 1f);
    }

    public void StartGunHitEffectWall(Vector3 pos, Vector3 rot)
    {
        GameObject eGunHit = Instantiate(gunHitEffectWall);
        eGunHit.transform.position = pos;
        eGunHit.transform.rotation = Quaternion.LookRotation(rot);
        Destroy(eGunHit, 1f);
    }

    public void StartEnemyExplosionEffect(Vector3 pos, Vector3 rot)
    {
        GameObject eGunHit = Instantiate(enemyExpolsionEff);
        eGunHit.transform.position = pos;
        eGunHit.transform.rotation = Quaternion.LookRotation(rot);
        Destroy(eGunHit, 1f);
    }

}
