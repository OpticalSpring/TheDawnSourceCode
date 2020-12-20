using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector3 desPos;
    public float moveSpeed;
    public PlayerGun gun;
    public GameObject gunHitEffect;
    public GameObject gunHitEffectWall;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 60);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    void Move()
    {
        //gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, gameObject.transform);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, desPos, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(gameObject.transform.position, desPos) < 0.5f)
        {
            GameObject trail = gameObject.transform.GetChild(0).gameObject;

            trail.transform.parent = null;
            Destroy(trail, 0.3f);
            Destroy(gameObject);
        }
        //if (BulletTimeManager.instance.lockState == true)
        //{
        //    if (moveSpeed > 0)
        //    {
        //        moveSpeed -= 500 * Time.deltaTime;
        //    }
        //}
        //else
        //{
        //    moveSpeed += 1000 * Time.deltaTime ;
        //    moveSpeed *= 100 * Time.deltaTime;
        //    CheckEnemy();
        //}
    }


    void CheckEnemy()
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 10;
        mask = ~mask;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out rayHit, moveSpeed * 0.05f, mask))
        {
            if (rayHit.transform.gameObject.GetComponent<EnemyStatus>())
            {
                StartGunHitEffect(rayHit.point, rayHit.normal);
                PlayerUISystem.instance.SetHit();
                //PlayerUISystem.instance.SetHit();
                rayHit.transform.gameObject.GetComponent<Unit>().Hit(100);
                Vector3 forceVector = rayHit.transform.position - gameObject.transform.position;
                rayHit.transform.gameObject.GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);
            }
            else
            {
                StartGunHitEffectWall(rayHit.point, rayHit.normal);
            }
            GameObject trail = gameObject.transform.GetChild(0).gameObject;

            trail.transform.parent = null;
            Destroy(trail, 0.3f);
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.layer == 8)
    //    {

    //    }
    //    else if (other.gameObject.layer == 9)
    //    {
    //        other.gameObject.GetComponent<Unit>().Hit();
    //        StartGunHitEffect(other.contacts[0].point, other.contacts[0].normal);
    //        Destroy(gameObject);
    //    }
    //    else if (other.gameObject.layer == 2)
    //    {
    //    }
    //    else
    //    {

    //        StartGunHitEffect(other.contacts[0].point, other.contacts[0].normal) ;
    //        //Destroy(gameObject.transform.GetChild(0).gameObject, 1);
    //        //gameObject.transform.GetChild(0).transform.parent = null;
    //        Destroy(gameObject);
    //    }
    //}
    

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
}
