using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDieEff()
    {
        //MekaBoss.instance.SetDieEff();
        MekaBoss.instance.SetEffect(11, MekaBoss.instance.gameObject, 3);
        SoundManager.instance.SoundPlay(9, 25);
    }

    public void SetHandEffLeft()
    {
        //MekaBoss.instance.SetHandEffLeft();
        MekaBoss.instance.SetEffect(10, MekaBoss.instance.ballPos[2], 3);
    }

    public void SetHandEffRight()
    {
        //MekaBoss.instance.SetHandEffRight();
        MekaBoss.instance.SetEffect(10, MekaBoss.instance.ballPos[3], 3);
    }

}
