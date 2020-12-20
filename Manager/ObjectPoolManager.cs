using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject originalPrefab;
    public int objectCount;
    public GameObject[] clone;
    int objectNum;
    // Start is called before the first frame update
    void Awake()
    {
        clone = new GameObject[objectCount];
        for (int i = 0; i < objectCount; i++)
        {
            clone[i] = Instantiate(originalPrefab);
            clone[i].transform.parent = gameObject.transform;
            clone[i].SetActive(false);
        }
    }

    public GameObject ActiveClone()
    {
        if (objectNum >= objectCount)
        {
            objectNum = 0;
        }
        for (int i = 0; i < objectCount; i++)
        {
            if (clone[objectNum].activeSelf == false)
            {
                clone[objectNum].SetActive(true);
                return clone[objectNum++];
            }
            else
            {
                objectNum++;
                if (objectNum >= objectCount)
                {
                    objectNum = 0;
                }
            }
        }
        return null;
    }
}
