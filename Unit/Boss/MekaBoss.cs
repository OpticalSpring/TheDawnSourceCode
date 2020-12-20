using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MekaBoss : BossStatus
{
    public static MekaBoss instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject MBHLRenderer;
    public GameObject MBHRRenderer;
    public GameObject MBHLRenderer2;
    public GameObject MBHRRenderer2;
    public GameObject MBHL;
    public GameObject MBHR;
    public GameObject HL;
    public GameObject HR;
    public Animator bossAnimator;
    public Animator HLAnimator;
    public Animator HRAnimator;
    public GameObject laserTarget;
    public GameObject laser;
    public float laserSpeed;

    public int progressState;
    public bool handPosSync;
    public GameObject[] effect;
    public GameObject p6EffPos;
    public GameObject missile;
    public GameObject missilePos;
    public GameObject[] weakness;
    public GameObject bullet;
    List<GameObject> bulletList = new List<GameObject>();
    public GameObject ball;
    public GameObject[] ballPos;
    public GameObject sphere;
    public GameObject[] sphereObj;
    public GameObject bigSphere;
    public ObjectPoolManager ballPool;
    public GameObject[] cooler;
    public bool p5Started;

    public override void Update()
    {
        if(handPosSync == true)
        {
            HL.transform.position = MBHL.transform.position;
            HR.transform.position = MBHR.transform.position;
            HL.transform.eulerAngles = MBHL.transform.eulerAngles + new Vector3(90, -90, 0);
            HR.transform.eulerAngles = MBHR.transform.eulerAngles + new Vector3(-90,-90, 0);
        }
        if(p5Started == true)
        {
            
            for (int i = 0; i < 4; i++)
            {
                if (cooler[i].GetComponent<CoolerAgent>().HP_Point > 0)
                {
                    return;
                }
            }
            Hit(100000);
        }
    }

    public void ConnectHandL()
    {
        HL.SetActive(false);
       
        MBHL.SetActive(true);
        
        MBHLRenderer.SetActive(true);
        MBHLRenderer2.SetActive(true);
    }

    public void DisConnectHandL()
    {
        HL.SetActive(true);
       
        MBHL.SetActive(false);
        
        MBHLRenderer.SetActive(false);
        MBHLRenderer2.SetActive(false);
    }

    public void ConnectHandR()
    {
       
        HR.SetActive(false);
       
        MBHR.SetActive(true);
      
        MBHRRenderer.SetActive(true);
        MBHRRenderer2.SetActive(true);
    }

    public void DisConnectHandR()
    {
       
        HR.SetActive(true);
       
        MBHR.SetActive(false);
        
        MBHRRenderer.SetActive(false);
        MBHRRenderer2.SetActive(false);
    }

    public void AttackLaser()
    {
        laser.transform.GetChild(0).LookAt(laserTarget.transform.position);
        laser.transform.GetChild(1).LookAt(laserTarget.transform.position);
        laserTarget.transform.position = Vector3.MoveTowards(laserTarget.transform.position, Player.instance.transform.position, Time.deltaTime * laserSpeed);
        LaserTurn(model);
        
        LaserHit();
    }


    void LaserTurn(GameObject obj)
    {

        float dz = laserTarget.transform.position.z - obj.transform.position.z;
        float dx = laserTarget.transform.position.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), rotationSpeed * Time.deltaTime);
    }

    float laserDelay;
    void LaserHit()
    {
       
        if(laserDelay > 0)
        {
            laserDelay -= Time.deltaTime;
           
        }
        else
        {
            laserDelay = 0.1f;
            Collider[] colliderArray = Physics.OverlapSphere(laserTarget.transform.position, 1);

            for (int i = 0; i < colliderArray.Length; i++)
            {
                if (colliderArray[i].GetComponent<Player>())
                {
                    if (colliderArray[i].GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dodge)
                    {
                        colliderArray[i].GetComponent<Player>().Hit(10);
                    }
                }
            }
        }
    }

    public void SetEffect(int i, Vector3 pos)
    {
        GameObject eff = Instantiate(effect[i]);
        eff.transform.position = pos;
        eff.transform.rotation = gameObject.transform.rotation;
        Destroy(eff, 3);
    }

    public void SetEffect(int i, Vector3 pos, float time)
    {
        GameObject eff = Instantiate(effect[i]);
        eff.transform.position = pos;
        Destroy(eff, time);
    }

    public void SetEffect(int i, GameObject pos, float time)
    {
        GameObject eff = Instantiate(effect[i]);
        eff.transform.position = pos.transform.position;
        eff.transform.parent = pos.transform;
        Destroy(eff, time);
    }


    public void SetDieEff()
    {
        GameObject eff = Instantiate(effect[11]);
        eff.transform.position = gameObject.transform.position;
        Destroy(eff, 3);
    }

    public void SetHandEffLeft()
    {
        GameObject eff = Instantiate(effect[10]);
        eff.transform.position = ballPos[2].transform.position;
        eff.transform.parent = ballPos[2].transform;
        Destroy(eff, 2);
    }

    public void SetHandEffRight()
    {
        GameObject eff = Instantiate(effect[10]);
        eff.transform.position = ballPos[3].transform.position;
        eff.transform.parent = ballPos[3].transform;
        Destroy(eff, 2);
    }

    public void SetLaserEffect()
    {
        
        GameObject eff = Instantiate(effect[3]);
        eff.transform.position = laser.transform.position;
        eff.transform.rotation = laser.transform.rotation;
        eff.transform.parent = laser.transform.parent;
        Destroy(eff, 3);
    }

    public void InitMissile(Vector3 target)
    {
        SoundManager.instance.RandomPlayNew(9, 5, 6, missilePos.transform.position, 0.5f);
        GameObject misObj = Instantiate(missile);
        misObj.transform.position = missilePos.transform.position + Vector3.one * Random.Range(-5,5);
        misObj.GetComponent<BossMissile>().realTarget = target;
        misObj.GetComponent<BossMissile>().state = 1;
    }
    public void InitBullet()
    {
        
        GameObject obj = Instantiate(bullet);
        Vector3 rPos = Random.onUnitSphere * 10;
        rPos.y = Mathf.Abs(rPos.y);
        
        obj.transform.position = Player.instance.transform.position + rPos;
        obj.transform.LookAt(Player.instance.transform.position + new Vector3(0, 1, 0));
        obj.transform.parent = Player.instance.transform;
        bulletList.Add(obj);
    }

    public void InitBall(int type, int speed, Vector3 target)
    {

        GameObject obj = ballPool.ActiveClone();

        if (obj)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(type).gameObject.SetActive(true);
            obj.transform.position = ballPos[3].transform.position;
            ballPos[3].transform.LookAt(target);
            obj.transform.rotation = ballPos[3].transform.rotation;
            obj.GetComponent<BossBall>().moveSpeedEnd = speed;
        }
        
    }

    public void InitSphere(int type)
    {

        GameObject obj = Instantiate(sphere);
        sphereObj[type] = obj;
        obj.transform.position = ballPos[type].transform.position;
        obj.transform.rotation = ballPos[type].transform.rotation;
        obj.transform.parent = ballPos[type].transform;
        obj.GetComponent<BossSphere>().scaleEnd = 10;
    }

    public void FireBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if(bulletList[i]!= null)
            {
                bulletList[i].transform.parent = null;
                bulletList[i].GetComponent<BossBullet>().enable = true;
                bulletList[i].GetComponent<BossBullet>().OnEff();
            }
        }
    }
}
