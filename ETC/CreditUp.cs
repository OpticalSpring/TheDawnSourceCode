﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition += new Vector3(0, 120, 0) * Time.deltaTime;
    }
}
