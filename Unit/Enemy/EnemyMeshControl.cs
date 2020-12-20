using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeshControl : MonoBehaviour
{
    public GameObject matObj;
    Material mat;
    public bool dead;
    public float hideFloat;

    // Start is called before the first frame update
    void Start()
    {
        hideFloat = 1;
        mat = matObj.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        SetDissolve();
    }

    void SetDissolve()
    {
        if (dead == true)
        {
            hideFloat = Mathf.Lerp(hideFloat, 1, Time.deltaTime * 2f);
        }
        else
        {
            hideFloat = Mathf.Lerp(hideFloat, 0, Time.deltaTime * 2f);
        }
        mat.SetFloat("_DissolveScale", hideFloat);
    }
}
