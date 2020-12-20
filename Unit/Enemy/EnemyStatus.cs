using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : Unit
{
    public GameObject player;
    public GameObject model;
    public float attackDelayTime;
    public float attackDelayTimeNow;
    public float moveSpeed;
    public float rotationSpeed;
    public float targetDis;
    [HideInInspector]
    public NavMeshAgent navAgent;
    [HideInInspector]
    public AnimationSystem animator;
    public GameObject mat;
    public GameObject spawnEff;
    public enum EEnemyFSM
    {
        Idle,
        Defense,
        Move,
        Attack,
        Chase,
        Fire,
        Dead
    }
    public EEnemyFSM EnemyFSM;
    [HideInInspector]
    public TimeAgent timeAgent;

    public GameObject skillSignEff;
    public GameObject skillSignPos;
    // Start is called before the first frame update
    public override void Start()
    {
          navAgent = gameObject.GetComponent<NavMeshAgent>();
        timeAgent = gameObject.GetComponent<TimeAgent>();
        player = GameObject.Find("PlayerObject");
        animator = gameObject.GetComponent<AnimationSystem>();
        StartSpawnEffect();
       // StartCoroutine(Teleport());
    }

    // Update is called once per frame
    public override void Update()
    {
        navAgent.speed = moveSpeed * timeAgent.speedFloat;
        targetDis = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    public override int Hit(int damage)
    {
        if(Player.instance.PlayerFSM != PlayerStatus.EPlayerFSM.Ultimate)
        {
            Player.instance.ultimateGage += damage;
            if (Player.instance.ultimateGage > Player.instance.ultimateGageMax)
            {
                Player.instance.ultimateGage = Player.instance.ultimateGageMax;
            }
        }
        StartCoroutine(HitMesh());
        return base.Hit(damage);
    }

    IEnumerator HitMesh()
    {
        for (int i = 0; i < mat.transform.childCount; i++)
        {
            mat.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Shot", 1);
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < mat.transform.childCount; i++)
        {
            mat.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Shot", 0);
        }
    }

    public override void Die()
    {
        if(EnemyFSM == EEnemyFSM.Dead)
        {
            return;
        }
        //animator.SetAniState(10);
        EnemyFSM = EEnemyFSM.Dead;
        //SoundManager.instance.SoundPlay3D(5, 9, gameObject.transform.position);
        SoundManager.instance.RandomPlayNew(10, 0, 4, gameObject.transform.position, 0.25f);
        model.GetComponent<Animator>().enabled = false;
        PlayerUISystem.instance.SetKill();
        //GameObject model = gameObject.transform.GetChild(0).gameObject;
        //model.transform.parent = null;
        Destroy(gameObject,5);
        GetComponent<DetachAgent>().Detach();
    }



    public void Turn(Vector3 targetPoint)
    {
        float dz = targetPoint.z - gameObject.transform.position.z;
        float dx = targetPoint.x - gameObject.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), rotationSpeed * Time.deltaTime * timeAgent.speedFloat);

    }

    public void StartSkillSign()
    {
        PlayerUISystem.instance.SetSpotUI(gameObject.transform.position);
        GameObject eff = Instantiate(skillSignEff);
        eff.transform.parent = skillSignPos.transform;
        eff.transform.position = skillSignPos.transform.position;
        eff.transform.rotation = skillSignPos.transform.rotation;
        Destroy(eff, 1);
    }

    public void StartSpawnEffect()
    {
        GameObject eff = Instantiate(spawnEff);
        eff.transform.parent = gameObject.transform;
        eff.transform.position = gameObject.transform.position + new Vector3(0,1,0);
        eff.transform.rotation = gameObject.transform.rotation;
        Destroy(eff, 1);
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(60);
        while (true)
        {
            if(Vector3.Distance(gameObject.transform.position, Player.instance.transform.position) > 20)
            {
                Vector3 newPos = Player.instance.transform.position + Vector3.one * Random.Range(-3, 3);
                newPos.y = Player.instance.transform.position.y;
                gameObject.transform.position = newPos;
                StartSpawnEffect();
            }
            yield return new WaitForSeconds(30);
        }
    }
}
