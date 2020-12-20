using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public int damagePoint;
    public float moveSpeed;
    TimeAgent timeAgent;

    public GameObject hitEffect;
    public GameObject target;
    public GameObject[] trailEff;
    bool end;
    // Start is called before the first frame update
    void Start()
    {
        timeAgent = gameObject.GetComponent<TimeAgent>();
        Destroy(gameObject, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAgent.set == false)
        { 
            Turn();
        }
        else
        {
            if(end == false)
            {
                end = true;
            OffEff();
            }
        }
        Move();
    }

    void OffEff()
    {
        trailEff[0].SetActive(false);
        Destroy(trailEff[1], 1);
        Destroy(trailEff[2], 1);
        trailEff[1].transform.parent = null;
        trailEff[2].transform.parent = null;

    }

    void Move()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * timeAgent.speedFloat, gameObject.transform);
    }

    void Turn()
    {
        Vector3 vec = target.transform.position +new Vector3(0,0.1f,0) - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, q, Time.deltaTime * 2);
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
                if (timeAgent.set == false)
                {
                    SoundManager.instance.RandomPlayNew(4, 2, 5, gameObject.transform.position,0.5f);
                    StartHitEffect(gameObject.transform.position, gameObject.transform.eulerAngles);
                    other.gameObject.GetComponent<Unit>().Hit(damagePoint);
                    if (end == false)
                    {
                        end = true;
                        OffEff();
                    }
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
            SoundManager.instance.RandomPlayNew(4, 2, 5, gameObject.transform.position, 0.5f);
            StartHitEffect(gameObject.transform.position, gameObject.transform.eulerAngles);
            if (end == false)
            {
                end = true;
                OffEff();
            }
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
