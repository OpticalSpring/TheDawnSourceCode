using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimationPos : MonoBehaviour
{
    [HideInInspector]
    public GameObject cam;
    [HideInInspector]
    public Transform oldPos;
    public GameObject camPosPlayer;
    public GameObject camPosEffect;
    public GameObject camTargetPlayer;
    public GameObject camTargetEffect;
    public int state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                cam.transform.LookAt(camTargetPlayer.transform.position);
                break;
            case 2:
                cam.transform.LookAt(camTargetEffect.transform.position);
                break;
        }
    }

    public void SetPlayer()
    {
        cam.transform.parent = camPosPlayer.transform;
        cam.transform.localPosition = Vector3.zero;
        state = 1;
    }

    public void SetEffect()
    {
        cam.transform.parent = camPosEffect.transform;
        cam.transform.localPosition = Vector3.zero;
        state = 2;
    }

    public void ResetPos()
    {
        cam.transform.parent = oldPos;
        cam.transform.localPosition = new Vector3(0, 1, 0);
        cam.transform.localEulerAngles = Vector3.zero;
        Destroy(gameObject);
    }
}
