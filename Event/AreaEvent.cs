using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEvent : DoneEvent
{
    public int eventCheckNum;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnTriggerStay(Collider other)
    {
        if(done == true)
        {
            return;
        }
        if (other.gameObject.GetComponent<Player>())
        {

                
        if(EventManager.instance.eventNumber == eventCheckNum)
        {
            if (EventManager.instance.nextReady == false)
            {
                    done = true;
                    EventManager.instance.nextReady = true;
            }
        }
        }
    }
}
