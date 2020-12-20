using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall : MonoBehaviour
{
    public int damagePoint;
    public float moveSpeedStart;
    public float moveSpeedEnd;
    float moveSpeed;
    TimeAgent timeAgent;

    public GameObject hitEffect;
    // Start is called before the first frame update

    private void Awake()
    {
        
        timeAgent = gameObject.GetComponent<TimeAgent>();
    }
    void OnEnable()
    {
        moveSpeed = moveSpeedStart;
        StartCoroutine(AutoDisable());
    }

    // Update is called once per frame
    void Update()
    {
        Speed();
        Move();
    }

    void Speed()
    {
        moveSpeed = Mathf.Lerp(moveSpeed, moveSpeedEnd, 5 * Time.deltaTime);
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
                if (timeAgent.set == false)
                {

                    StartHitEffect(gameObject.transform.position, gameObject.transform.eulerAngles);
                    other.gameObject.GetComponent<Unit>().Hit(damagePoint);
                    SetDisable();
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
            SetDisable();
        }
    }

    public void StartHitEffect(Vector3 pos, Vector3 rot)
    {
        GameObject eGunHit = Instantiate(hitEffect);
        eGunHit.transform.position = pos;
        eGunHit.transform.rotation = Quaternion.LookRotation(rot);
        Destroy(eGunHit, 1f);
    }

    void SetDisable()
    {
        gameObject.SetActive(false);
    }

    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(20);
        SetDisable();
    }
}
