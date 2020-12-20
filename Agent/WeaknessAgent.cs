using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaknessAgent : MonoBehaviour
{
    public int HP_Point;
    public int HP_PointMax;
    public Text HP_Text;
    public Image HP_Bar;
    public GameObject eXeff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HP_Text.text =  100*(float)HP_Point / HP_PointMax + " %";
        HP_Bar.fillAmount = (float)HP_Point / HP_PointMax;
    }

    public void Init()
    {
        HP_Point = HP_PointMax;
    }

    public virtual void Hit(int damage)
    {
        
        HP_Point -= damage;
        if(HP_Point <= 0)
        {
            
            HP_Point = 0;
            MekaBoss.instance.Hit(MekaBoss.instance.HP_PointMax/10);
            MekaBoss.instance.GetComponent<MekaBossAI>().ForceStop();
            SoundManager.instance.RandomPlayNew(9, 22, 24, Camera.main.transform.position, 0.5f);
            SoundManager.instance.RandomPlayNew(10, 0, 4, Camera.main.transform.position, 0.25f);
            StartExplosionEffect();
        }
        else
        {
           MekaBoss.instance.Hit(damage);
        }
    }

    public void StartExplosionEffect()
    {
        GameObject eGunHit = Instantiate(eXeff);
        eGunHit.transform.position = gameObject.transform.position;
        Destroy(eGunHit, 1f);
    }
}
