using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectSystem : MonoBehaviour
{
    public GameObject[] reloadEffect;
    public GameObject reloadPos;

    public GameObject timeStopFieldStart;
    public GameObject timeStopFieldObject;
    public GameObject readyTimeStop;

    public GameObject[] TimeRecallEff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGunReloadEffect()
    {
        StartCoroutine(ReloadEffect());
    }

    IEnumerator ReloadEffect()
    {
        GameObject eGunReload = Instantiate(reloadEffect[0]);
        eGunReload.transform.parent = reloadPos.transform;
        eGunReload.transform.position = reloadPos.transform.position;
        eGunReload.transform.rotation = reloadPos.transform.rotation;
        Destroy(eGunReload, 1.0f);
        yield return new WaitForSeconds(0.8f);
        GameObject eGunReload2 = Instantiate(reloadEffect[1]);
        eGunReload2.transform.parent = reloadPos.transform;
        eGunReload2.transform.position = reloadPos.transform.position;
        eGunReload2.transform.rotation = reloadPos.transform.rotation;
        Destroy(eGunReload2, 1.0f);
    }

    public void StartTimeStopField()
    {
        GameObject eff = Instantiate(timeStopFieldStart);
        eff.transform.position = readyTimeStop.transform.position;
        float dz = Player.instance.transform.position.z - eff.transform.position.z;
        float dx = Player.instance.transform.position.x - eff.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        eff.transform.rotation = Quaternion.Euler(0, rotateDegree, 0);
        Destroy(eff, 3);
        StartCoroutine(DelayTimeStopField());
    }

    IEnumerator DelayTimeStopField()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject eff = Instantiate(timeStopFieldObject);
        eff.transform.position = readyTimeStop.transform.position;
        Destroy(eff, 7f);
    }

    public void StartTimeRecall()
    {
        GameObject eff0 = Instantiate(TimeRecallEff[0]);
        GameObject eff1 = Instantiate(TimeRecallEff[1]);
        GameObject eff2 = Instantiate(TimeRecallEff[2]);
        eff0.transform.parent = Camera.main.gameObject.transform;
        eff1.transform.parent = Camera.main.gameObject.transform;
        eff2.transform.parent = Camera.main.gameObject.transform;
        eff0.transform.localPosition = new Vector3(0.45f, 0.735f, 1.77f);
        eff0.transform.localEulerAngles = new Vector3(0, -180f, 0);
        eff0.transform.localScale = new Vector3(0.1895f, 0.1895f, 0);

        eff1.transform.localPosition = new Vector3(-1.53f, -0.819f, 1.8f);
        eff1.transform.localEulerAngles = new Vector3(0, -90f, 0);
        eff1.transform.localScale = new Vector3(1, 1, 1);

        eff2.transform.localPosition = new Vector3(1.47f, -0.819f, 1.8f);
        eff2.transform.localEulerAngles = new Vector3(0, -90f, 0);
        eff2.transform.localScale = new Vector3(1, 1, 1);

        Destroy(eff0, 1);
        Destroy(eff1, 2);
        Destroy(eff2, 2);
    }

    public void EndTimeRecall()
    {
        GameObject eff3 = Instantiate(TimeRecallEff[3]);
        eff3.transform.position = Player.instance.transform.position;
        Destroy(eff3, 1);
    }
}

