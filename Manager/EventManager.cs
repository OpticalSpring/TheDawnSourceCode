using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
   
    public int eventNumber;
    public bool nextReady;
    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    protected void SaveState(int i)
    {
        PlayerUISystem.instance.ActSaveIcon();
        PlayerPrefs.SetInt("CheckPoint", i);
    }

    protected void SetWave(string type, bool sound)
    {
        Invoke(type, 0);
        SpawnManager.instance.StartWave(type, sound);
    }
}
