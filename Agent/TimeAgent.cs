using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public enum EType
    {
        Mob,
        Bullet,
        MBH
    };
    public EType Type;
    public float speedFloat;
    public bool set;
    Rigidbody rig;
    public bool rebool;
    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
        speedFloat = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(rebool == true)
        {
            rebool = false;
            return;
        }
        if(SkillManager.instance.stopFieldState == true)
        {
            if (Type == EType.MBH&&Vector3.Distance(GetComponent<MBHand>().pivot.transform.position, SkillManager.instance.stopFieldObject.transform.position) < 7)
            {
               if(set == false)
                {
                    GetComponent<MBHand>().weakness.SetActive(true);
                    GetComponent<MBHand>().weakness.GetComponent<WeaknessAgent>().Init();
                }
                set = true;
                speedFloat = Mathf.Lerp(speedFloat, 0, Time.deltaTime * 20);
            }
            else if (Vector3.Distance(gameObject.transform.position, SkillManager.instance.stopFieldObject.transform.position) < 7)
            {
                set = true;
                speedFloat = Mathf.Lerp(speedFloat, 0, Time.deltaTime * 8);

            }
            else
            {
                if (set == true)
                {
                    switch (Type)
                    {
                        case EType.Bullet:
                            speedFloat = Mathf.Lerp(speedFloat, 0, Time.deltaTime * 8);
                            rig.useGravity = true;
                            break;
                        case EType.Mob:
                            speedFloat = Mathf.Lerp(speedFloat, 1, Time.deltaTime * 1);
                            break;
                        case EType.MBH:
                            break;
                        default:
                            break;
                    }

                }
            }

        }
        else
        {
            if(set == true)
            {
                switch (Type)
                {
                    case EType.Bullet:
                        speedFloat = Mathf.Lerp(speedFloat, 0, Time.deltaTime * 8);
                        rig.useGravity = true;
                        break;
                    case EType.Mob:
                        speedFloat = Mathf.Lerp(speedFloat, 1, Time.deltaTime * 1);
                        break;
                    case EType.MBH:
                        speedFloat = Mathf.Lerp(speedFloat, 1, Time.deltaTime * 1);
                        GetComponent<MBHand>().weakness.SetActive(false);
                        break;
                    default:
                        break;
                }
               
            }
        }
    }
}
