using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseAttack()
    {
        if(gameObject.transform.parent == null)
        {
            return;
        }
        gameObject.transform.parent.parent.gameObject.GetComponent<EnemyType1>().Attack();
    }
}
