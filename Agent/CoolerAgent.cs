using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerAgent : WeaknessAgent
{
    public GameObject HPBar;
    public GameObject eff;
    public Animator ani;
    public GameObject[] mat;
    private void Update()
    {
        Vector3 newPos = gameObject.transform.position;
        if(HP_Point > 0)
        {
            HPBar.SetActive(true);
            HPBar.transform.LookAt(Camera.main.transform.position);
            HP_Text.text = 100 * (float)HP_Point / HP_PointMax + " %";
            HP_Bar.fillAmount = (float)HP_Point / HP_PointMax;
            newPos.y = 10;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, newPos) < 0.1f)
            {
                ani.SetInteger("AniState", 1);
            }

        }
        else if(HP_Point == 0)
        {
            eff.SetActive(true);
            HPBar.SetActive(false);
            mat[0].GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            mat[1].GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            GetComponent<CoolerAgent>().enabled = false;
        }
        else
        {
            
        }

        
    }

    public override void Hit(int damage)
    {

        HP_Point -= damage;
        if (HP_Point < 0)
        {

            HP_Point = 0;
            
        }
        else
        {
            MekaBoss.instance.Hit(damage);
        }
    }
}
