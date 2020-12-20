using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType4AI : MonoBehaviour
{
    NavMeshAgent agent;
    AnimationSystem animator;
    public GameObject player;
    public GameObject rayObj;
    public EnemyType4 enemy;
    public float stateTime;
    public Vector3 targetPos;

    public Vector2 yPosRange;
    public float yPos;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EnemyType4>();
        animator = GetComponent<AnimationSystem>();
        player = GameObject.Find("PlayerObject");
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
        CheckFSM();
        Command();
        //enemy.Move(targetPos);
    }



    void CheckFSM()
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 9 | 1 << 10;
        mask = ~mask;
        rayObj.transform.LookAt(player.transform.position + new Vector3(0, 1, 0));

        if (Physics.Raycast(rayObj.transform.position, rayObj.transform.forward, out rayHit, 50, mask))
        {
            if (rayHit.transform.gameObject.GetComponent<Player>())
            {
                enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Fire;
                return;
            }
            else
            {

            }
        }
        else
        {

        }


        if (stateTime > 10 && enemy.EnemyFSM != EnemyStatus.EEnemyFSM.Idle)
        {
            enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Idle;
            agent.SetDestination(gameObject.transform.position);
            targetPos = gameObject.transform.position;
        }
        else if (stateTime <= 10 && enemy.EnemyFSM != EnemyStatus.EEnemyFSM.Chase)
        {
            enemy.EnemyFSM = EnemyStatus.EEnemyFSM.Chase;
            agent.SetDestination(RandomTargetPos(5));
            targetPos = RandomTargetPos(5);
        }
        if (stateTime > 0)
        {
            stateTime -= Time.deltaTime * enemy.timeAgent.speedFloat;
        }
        else
        {
            stateTime = 15;
        }
    }

    void Command()
    {
        switch (enemy.EnemyFSM)
        {
            case EnemyStatus.EEnemyFSM.Idle:
                animator.SetAniState(0);
                break;
            case EnemyStatus.EEnemyFSM.Chase:
                animator.SetAniState(1);
                break;
            case EnemyStatus.EEnemyFSM.Fire:
                if (enemy.attackDelayTimeNow > 1)
                {
                    animator.SetAniState(0);
                    agent.SetDestination(player.transform.position);
                    targetPos = player.transform.position;
                    enemy.attackDelayTimeNow -= Time.deltaTime * enemy.timeAgent.speedFloat;
                }
                else if (enemy.attackDelayTimeNow > 0)
                {
                    animator.SetAniState(0);
                    agent.SetDestination(gameObject.transform.position);
                    targetPos = gameObject.transform.position;
                    enemy.Turn(player.transform.position);
                    enemy.attackDelayTimeNow -= Time.deltaTime * enemy.timeAgent.speedFloat;
                }
                else
                {
                    animator.SetAniState(3);
                    enemy.attackDelayTimeNow = enemy.attackDelayTime;

                    enemy.FireRaser();
                }
                break;
            case EnemyStatus.EEnemyFSM.Dead:
                //animator.SetAniState(3);
                break;
        }
    }

    Vector3 RandomTargetPos(float range)
    {
        Vector3 rTarget = player.transform.position;
        rTarget += Random.onUnitSphere * range;
        rTarget.y = player.transform.position.y;

        return rTarget;
    }
}
