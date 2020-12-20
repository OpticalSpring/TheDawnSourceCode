using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : DoneEvent
{
    public int[] hackingCheckNum;
    public int[] eventCheckNum;
    public override void ActEvent_1()
    {
        gameObject.GetComponent<LongHacking>().stateNum = 1;
        gameObject.GetComponent<LongHacking>().AutoStop();
        for (int i = 0; i < hackingCheckNum.Length; i++)
        {
            if (EventManager.instance.eventNumber == hackingCheckNum[i])
            {
                if (EventManager.instance.nextReady == false)
                {
                    EventManager.instance.nextReady = true;
                }
            }
        }
    }

    public override void ActEvent_2()
    {
        for (int i = 0; i < eventCheckNum.Length; i++)
        {

            if (EventManager.instance.eventNumber == eventCheckNum[i])
            {
                if (EventManager.instance.nextReady == false)
                {
                    EventManager.instance.nextReady = true;
                }
            }
        }
    }

    public override void ActEvent_3()
    {
        gameObject.GetComponent<LongHacking>().stateNum = 3;
        gameObject.GetComponent<LongHacking>().iTime = 0.02f;
    }


}
