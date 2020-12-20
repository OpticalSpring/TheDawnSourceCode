using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotAgent : MonoBehaviour
{
    public float aliveTime;
    public Vector3 markerTargetPos;
    public GameObject marker;
    public GameObject markerPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckAliveTime();
        SetUI();
    }

    void CheckAliveTime()
    {
        if(aliveTime > 0)
        {
            aliveTime -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void SetUI()
    {
        marker.SetActive(true);
        Vector3 newMarkerPos = Camera.main.WorldToScreenPoint(markerTargetPos);
        float dis = Vector3.Distance(Player.instance.transform.position, markerTargetPos);

        if (newMarkerPos.z > 0)
        {
            marker.SetActive(false);

        }
        else
        {
            marker.SetActive(true);

            if (newMarkerPos.x < 100)
            {
                newMarkerPos.x = 100;
            }
            else if (newMarkerPos.x > Screen.width - 100)
            {
                newMarkerPos.x = Screen.width - 100;
            }
            else
            {
                if (newMarkerPos.y < Screen.height / 2)
                {
                    newMarkerPos.y = 100;
                }
                else
                {
                    newMarkerPos.y = Screen.height - 100;
                }
            }

            if (newMarkerPos.y < 100)
            {
                newMarkerPos.y = 100;
            }
            else if (newMarkerPos.y > Screen.height - 100)
            {
                newMarkerPos.y = Screen.height - 100;
            }
            newMarkerPos.x = Screen.width - newMarkerPos.x;
            newMarkerPos.y = Screen.height - newMarkerPos.y;

            float dy = markerPoint.transform.position.y - Screen.height / 2;
            float dx = markerPoint.transform.position.x - Screen.width / 2;

            float rotateDegree = Mathf.Atan2(-dx, dy) * Mathf.Rad2Deg;

            markerPoint.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
        }
        marker.transform.position = newMarkerPos;
    }
}
