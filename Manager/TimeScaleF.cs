using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleF : MonoBehaviour
{
    public float timef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timef;
    }
}
