using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectAgent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MekaBoss.instance.bossFSM == BossStatus.MBossFSM.Dead)
            Destroy(gameObject);
    }
}
