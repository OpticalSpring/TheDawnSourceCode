using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeControl : MonoBehaviour
{
    public Material[] mat;
    public bool on;
    public float val;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mat.Length; i++)
        {
            mat[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(gameObject.transform.position, Player.instance.transform.position);
        if (dis < 10 && val < 1f)
        {
            val = (10-dis)/10f;
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i].color = new Color(1, 1, 1, (float)val);
            }
        }
        else if (dis >= 10 && val >= -0.1f)
        {
            
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i].color = new Color(1, 1, 1, 0);
            }
        }

    }
}
