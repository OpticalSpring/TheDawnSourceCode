using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run()
    {
        switch (GameManager.instance.sceneNum)
        {
            case 3:
                SoundManager.instance.RandomPlayNew(3, 0, 2, gameObject.transform.position);
                break;
            case 4:
                SoundManager.instance.RandomPlayNew(3, 3, 5, gameObject.transform.position);
                break;
            case 5:
                SoundManager.instance.RandomPlayNew(3, 6, 8, gameObject.transform.position);
                break;
        }
        
    }
}
