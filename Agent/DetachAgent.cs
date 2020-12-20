using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachAgent : MonoBehaviour
{
    public GameObject parts;
    public Transform[] allChild;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Detach()
    {
        //분해할 자식 오브젝트들을 받아온다.
        allChild = parts.GetComponentsInChildren<Transform>();
        for (int i = 0; i < allChild.Length; i++)
        {
            //자식 오브젝트들에 물리 연산을 위한 리지드바디 처리 및 분해
            allChild[i].gameObject.AddComponent<Rigidbody>();
            allChild[i].parent = null;
            Vector3 pos = Player.instance.transform.position;
            pos.x += Random.Range(-1, 1);
            pos.z += Random.Range(-1, 1);
            pos.y += Random.Range(-1, 1);
            Vector3 bom = allChild[i].position - pos;
            bom.Normalize();
            bom *= (300 - Vector3.Distance(allChild[i].position, pos) * 3);
            //폭발
            allChild[i].gameObject.GetComponent<Rigidbody>().AddForce(bom, ForceMode.Force);
            Destroy(allChild[i].gameObject, 5);
        }
        StartCoroutine(ScaleAnimation());
    }


    IEnumerator ScaleAnimation()
    {
        yield return new WaitForSeconds(3);
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            for (int i = 0; i < allChild.Length; i++)
            {
                if(allChild[i] != null)
                allChild[i].transform.localScale = Vector3.Lerp(allChild[i].transform.localScale, Vector3.zero , 3 * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
