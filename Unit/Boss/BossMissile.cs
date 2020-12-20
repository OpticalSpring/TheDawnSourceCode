using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : MonoBehaviour
{
    public int damagePoint;
    public float moveSpeed;
    TimeAgent timeAgent;
    public Vector3 realTarget;
    public Vector3 target;

    public int state;
    public GameObject explosiveEff;
    public GameObject floorEff;
    // Start is called before the first frame update
    void Start()
    {
        timeAgent = gameObject.GetComponent<TimeAgent>();
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        switch (state)
        {
            case 0:
                break;
            case 1:
                target = (realTarget + gameObject.transform.position) / 2;
                target.y = 60;
                state = 2;
                break;
            case 2:
                if (Vector3.Distance(gameObject.transform.position, target) < 1)
                {
                    state = 3;
                    target = realTarget;
                }
                break;
            case 3:
                if(Vector3.Distance(gameObject.transform.position, target) < 40)
                {
                    state = 4;
                    SetFloEff();
                }
                break;
            case 4:
                if (Vector3.Distance(gameObject.transform.position, target) < 1)
                {
                    SoundManager.instance.RandomPlayNew(9, 22, 24, gameObject.transform.position, 0.5f);
                    Attack();
                    SetExpEff();
                    Destroy(eff1);
                    Destroy(gameObject);
                }
                break;
        }
    }
    bool eff1Set;
    void Move()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * timeAgent.speedFloat, gameObject.transform);
    }

    void Turn()
    {
        Vector3 vec = target - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, q, Time.deltaTime * 2.5f);
    }

    void SetExpEff()
    {

        GameObject eff = Instantiate(explosiveEff);
        eff.transform.position = realTarget;
        Destroy(eff, 3);
    }
    GameObject eff1;
    void SetFloEff()
    {
        eff1 = Instantiate(floorEff);
        eff1.transform.position = realTarget;
        
    }

    void Attack()
    {
        Collider[] colliderArray = Physics.OverlapSphere(gameObject.transform.position, 5);

        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].GetComponent<Player>())
            {
                if (colliderArray[i].GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dodge)
                {
                    if (Player.instance.PlayerFSM == PlayerStatus.EPlayerFSM.Grabbed)
                    {
                        colliderArray[i].GetComponent<Player>().Hit(100);
                    }
                    else
                    {

                        colliderArray[i].GetComponent<Player>().Hit(50, true);
                    }
                }
            }
        }
    }
}
