using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType1AI : MonoBehaviour
{
    public EnemyType1 enemy;
    public Vector3 targetPos;
    public float stateTime;
    public float stateTimeNow;
    public GameObject supporter;
    public GameObject backPos;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyType1>();
        StartCoroutine(InvisibleTrace());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.EnemyFSM == EnemyStatus.EEnemyFSM.Dead)
        {
            return;
        }
        enemy.animator.SetAniSpeed(enemy.timeAgent.speedFloat);
        if (enemy.timeAgent.speedFloat < 0.5f)
        {
            return;
        }
        Delay();
        CheckFSM();
        Command();
    }

    void CheckFSM()
    {
        switch (enemy.EnemyFSM)
        {
            case EnemyStatus.EEnemyFSM.Idle:
                enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Defense;
                break;
            case EnemyStatus.EEnemyFSM.Defense:
                if (enemy.targetDis < 3)
                {
                    enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Attack;
                }
                else if (stateTimeNow <= 0)
                {
                    if (supporter != null)
                    {
                        targetPos = RandomTargetPos(15);
                    }
                    else
                    {
                        targetPos = RandomTargetPos(5);
                    }
                    enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Move;
                    stateTimeNow = stateTime;
                    enemy.navAgent.SetDestination(targetPos);
                }
                else
                {
                    stateTimeNow -= Time.deltaTime;
                }
                
                break;
            case EnemyStatus.EEnemyFSM.Move:
                if (enemy.targetDis < 3)
                {
                    enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Attack;
                }
                else if (Vector3.Distance(gameObject.transform.position, enemy.navAgent.destination) < 1)
                {
                    enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Defense;
                }
               
                break;
            case EnemyStatus.EEnemyFSM.Attack:
                if (enemy.targetDis >= 3)
                {
                    enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Idle;
                }
                break;
        }

    }

    void Command()
    {
        switch (enemy.EnemyFSM)
        {
            case EnemyStatus.EEnemyFSM.Idle:
                break;
            case EnemyStatus.EEnemyFSM.Defense:
                enemy.navAgent.isStopped = true;
                enemy.navAgent.velocity = Vector3.zero;
                enemy.animator.SetAniState(0);
                enemy.Turn(enemy.player.transform.position);
                if (enemy.attackDelayTimeNow <= 0)
                {
                    enemy.attackDelayTimeNow = enemy.attackDelayTime + Random.Range(-3, 3); ;
                    RaycastHit rayHit;
                    int mask = 1 << 2 | 1 << 9;
                    mask = ~mask;
                    enemy.firePos.transform.LookAt(enemy.player.transform.position + new Vector3(0, 1, 0));

                    if (Physics.Raycast(enemy.firePos.transform.position, enemy.firePos.transform.forward, out rayHit, 50, mask))
                    {
                        if (rayHit.transform.gameObject.GetComponent<Player>())
                        {
                            enemy.animator.SetAniState(3);
                            enemy.Fire();
                        }
                    }
                }
                else
                {
                    enemy.attackDelayTimeNow -= Time.deltaTime;
                }
                break;
            case EnemyStatus.EEnemyFSM.Move:
                enemy.navAgent.isStopped = false;
                enemy.animator.SetAniState(1);
                break;
            case EnemyStatus.EEnemyFSM.Attack:
                enemy.navAgent.isStopped = true;
                enemy.navAgent.velocity = Vector3.zero;
                enemy.animator.SetAniState(0);
                enemy.Turn(enemy.player.transform.position);
                enemy.navAgent.SetDestination(gameObject.transform.position);
                if (enemy.closeAttackDelayTimeNow <= 0)
                {
                    enemy.StartSkillSign();
                    enemy.animator.SetAniState(2);
                    enemy.closeAttackDelayTimeNow = enemy.closeAttackDelayTime + Random.Range(-2, 2);
                }
                else
                {
                    enemy.closeAttackDelayTimeNow -= Time.deltaTime;
                }
                break;
        }
    }
    void Delay()
    {
        if (enemy.attackDelayTimeNow > 0)
        {
            enemy.attackDelayTimeNow -= Time.deltaTime;

        }
        

        if(enemy.closeAttackDelayTimeNow > 0)
        {

            enemy.closeAttackDelayTimeNow -= Time.deltaTime;
        }
    }
    Vector3 RandomTargetPos(float range)
    {
        Vector3 rTarget = enemy.player.transform.position;
        rTarget += Random.onUnitSphere * range;
        rTarget.y = enemy.player.transform.position.y;

        return rTarget;
    }

    IEnumerator InvisibleTrace()
    {
        while (true)
        {
            RaycastHit rayHit;
            int mask = 1 << 2 | 1 << 9;
            mask = ~mask;
            enemy.firePos.transform.LookAt(enemy.player.transform.position + new Vector3(0, 1, 0));

            if (Physics.Raycast(enemy.firePos.transform.position, enemy.firePos.transform.forward, out rayHit, 50, mask))
            {
                if (rayHit.transform.gameObject.GetComponent<Player>())
                {
                   
                }
                else
                {

                    targetPos = RandomTargetPos(5);
                    enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Move;
                    stateTimeNow = stateTime;
                    enemy.navAgent.SetDestination(targetPos);
                }
            }

            yield return new WaitForSeconds(10f);
        }
    }
}
