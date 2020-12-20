using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int HP_Point;
    public int HP_PointMax;
    // Start is called before the first frame update
    public virtual void Start()
    {
        HP_PointMax = HP_Point;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual int Hit(int damage)
    {
        HP_Point -= damage;
        if(HP_Point <= 0)
        {
            HP_Point = 0;
            Die();
        }
        return HP_Point;
    }

    public virtual int Hit(int damage, bool stun)
    {
        HP_Point -= damage;
        if (HP_Point <= 0)
        {
            HP_Point = 0;
            Die();
        }
        return HP_Point;
    }

    public virtual void Die()
    {

    }
}
