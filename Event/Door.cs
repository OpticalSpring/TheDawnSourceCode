using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public float openTime;
    public Text tTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(openTime > 0)
        {
            openTime -= Time.deltaTime;
            tTime.text = (int)openTime + "s";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
