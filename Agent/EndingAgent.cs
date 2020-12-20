using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAgent : MonoBehaviour
{
    public int start;
    public int end;
    public GameObject stopEff;
    bool se;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.eulerAngles = new Vector3(0, Random.Range(-20, 20), 0);
        GetComponent<AnimationSystem>().SetUpperState(start);
    }

    // Update is called once per frame
    void Update()
    {
        if(se == true)
        {
            return;
        }
        if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < 40)
        {
            se = true;
            GetComponent<AnimationSystem>().SetUpperState(end);
            GameObject eff = Instantiate(stopEff);
            eff.transform.position = gameObject.transform.position;
        }
    }
}
