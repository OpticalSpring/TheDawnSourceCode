using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCam : MonoBehaviour
{
    public static CinematicCam instance;
    [Serializable]
    public struct CineSet
    {
        public Transform start;
        public Transform end;
        public Transform target;
    }
    
    public CineSet[] set;
    int state;
    bool ing;
    GameObject cam;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(ing == true)
        {
            if(set[state].target != null)
            {
                cam.transform.LookAt(set[state].target.position);
            }
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, set[state].end.position, Time.deltaTime);
        }
    }

    public void Set(int a)
    {
        state = a;
        ing = true;
        PlayerCameraSystem.instance.CamState = PlayerCameraSystem.CameraState.Cinematic;
        cam.transform.parent = null;
        cam.transform.position = set[a].start.position;
        cam.transform.rotation = set[a].start.rotation;
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(60, 0, 6));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(60, 0, 8));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(60, 0, 10));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(60, 0, 12));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(60, 0, 14));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(62, 0, 6));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(62, 0, 8));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(62, 0, 10));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(62, 0, 12));
        SpawnManager.instance.SpawnEnemy(1, 1, new Vector3(62, 0, 14));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(64, 0, 6));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(64, 0, 8));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(64, 0, 10));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(64, 0, 12));
        SpawnManager.instance.SpawnEnemy(0, 1, new Vector3(64, 0, 14));
    }
}
