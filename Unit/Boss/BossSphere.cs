using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSphere : MonoBehaviour
{
    public int damagePoint;
    public float moveSpeedStart;
    public float moveSpeedEnd;
    float moveSpeed;
    public float scaleStart;
    public float scaleEnd;
    public bool enable;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeedStart;
        gameObject.transform.localScale = scaleStart * Vector3.one;
        Destroy(gameObject, 30);
        Invoke("DestroySclaeFacter", 28);
    }

    // Update is called once per frame
    void Update()
    {
        ScaleFacter();
        if (enable == false)
        {
            return;
        }
        Speed();
        Move();
    }

    void Speed()
    {
        moveSpeed = Mathf.Lerp(moveSpeed, moveSpeedEnd, 10 * Time.deltaTime);
    }

    void Move()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, gameObject.transform);
        if(Vector3.Distance(gameObject.transform.position, target) < 10)
        {
            moveSpeedEnd = 1;
        }
    }

    void ScaleFacter()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, scaleEnd * Vector3.one, Time.deltaTime);
        for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {

            gameObject.transform.GetChild(0).GetChild(i).localScale = Vector3.Lerp(gameObject.transform.localScale, scaleEnd * Vector3.one, Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.GetComponent<Player>().PlayerFSM == PlayerStatus.EPlayerFSM.Dodge)
            {
                
            }
            else
            {
                    other.gameObject.GetComponent<Unit>().Hit(damagePoint);
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
        }
    }

    void DestroySclaeFacter()
    {
        scaleEnd = 0.1f;
    }
    public void Fire()
    {
        enable = true;
        target = Player.instance.transform.position;
        gameObject.transform.LookAt(target);
        scaleEnd = 25;
        gameObject.transform.parent = null;
    }
}
