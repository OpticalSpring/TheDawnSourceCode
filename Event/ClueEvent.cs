using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueEvent : DoneEvent
{
    public int[] eventCheckNum;
    public override void ActEvent_4()
    {
        if(EventManager.instance.eventNumber == eventCheckNum[0])
        {
            if (EventManager.instance.nextReady == false)
            {
                EventManager.instance.nextReady = true;
            }
        }
    }

    public override void ActEvent_1()
    {
        if (EventManager.instance.eventNumber == eventCheckNum[1])
        {
            if (EventManager.instance.nextReady == false)
            {
                EventManager.instance.nextReady = true;
            }
        }
    }
}
