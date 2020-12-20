using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnergyBall : MonoBehaviour
{
    public int damagePoint;
    public float moveSpeed;
    TimeAgent timeAgent;
    public bool bulletTime;

    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        timeAgent = gameObject.GetComponent<TimeAgent>();
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAgent.set == false)
        {
            if (bulletTime == true)
            {
                RaycastHit rayHit;
                int mask = 1 << 8;
                if (Physics.SphereCast(gameObject.transform.position, 0.1f, gameObject.transform.forward, out rayHit, 3f, mask))
                {
                    if (SkillManager.instance.bulletTimeNow < 0.1f)
                    {
                        SkillManager.instance.bulletTimeNow = 0.1f;

                    }
                }

            }
        }
            Move();
    }

    void Move()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * timeAgent.speedFloat, gameObject.transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.GetComponent<Player>().PlayerFSM == PlayerStatus.EPlayerFSM.Dodge)
            {
                //Debug.Log("dodge");
            }
            else
            {
                if(timeAgent.set == false)
                {

                StartHitEffect(gameObject.transform.position, gameObject.transform.eulerAngles);
                other.gameObject.GetComponent<Unit>().Hit(damagePoint);
                Destroy(gameObject);
                }
            }

        }
        else if (other.gameObject.layer == 9)
        {
        }
        else if (other.gameObject.layer == 2)
        {
        }
        else
        {
            StartHitEffect(gameObject.transform.position, gameObject.transform.eulerAngles);
            Destroy(gameObject);
        }
    }

    public void StartHitEffect(Vector3 pos, Vector3 rot)
    {
        GameObject eGunHit = Instantiate(hitEffect);
        eGunHit.transform.position = pos;
        eGunHit.transform.rotation = Quaternion.LookRotation(rot);
        Destroy(eGunHit, 1f);
    }

}
