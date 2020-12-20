using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    private void Awake()
    {
        instance = this;
    }
    public int enemyCount;
    public int[] enemyCountType;
    public GameObject[] enemyPrefab;
    public GameObject spawnPoint;
    public GameObject[] enemy = new GameObject[100];
    int eCount;

    public int waveState;
    public int longWave;
    public double peaceTime;
    [Serializable]
    public struct SpawnStatus
    {
        public int enemyMaxCount;
        public int enemySpawnCount;
        public int[] unitCountMax;
        public int[] unitRatio;
    }
    public SpawnStatus status;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnWave();
        CountEnemy();

    }
    void SpawnWave()
    {
        if (waveState == 1)
        {
            if (enemyCount >= status.enemyMaxCount)
            {
                return;
            }
            if (status.enemySpawnCount <= 0)
            {
                return;
            }
            int ratio = Random.Range(0, 100);
            int spawnPointNum = Random.Range(0, spawnPoint.transform.childCount);
            if (Vector3.Distance(Player.instance.transform.position, spawnPoint.transform.GetChild(spawnPointNum).position) > 50)
            {
                return;
            }

            if (ratio < status.unitRatio[0])
            {
                if(enemyCountType[0] < status.unitCountMax[0])
                {

                    SpawnEnemy(0, 1, spawnPointNum);
                    status.enemySpawnCount--;
                }
            }
            else if (ratio < status.unitRatio[0] + status.unitRatio[1])
            {
                if (enemyCountType[1] < status.unitCountMax[1])
                {

                SpawnEnemy(1, 1, spawnPointNum);
                    status.enemySpawnCount--;
                }
            }
            else if (ratio < status.unitRatio[0] + status.unitRatio[1] + status.unitRatio[2])
            {
                if (enemyCountType[2] < status.unitCountMax[2])
                {

                SpawnEnemy(2, 1, spawnPointNum);
                    status.enemySpawnCount--;
                }
            }
            else
            {
                if (enemyCountType[3] < status.unitCountMax[3])
                {

                SpawnEnemy(3, 1, spawnPointNum);
                status.enemySpawnCount--;
                }
            }

            return;
        }
       
    }
    void CountEnemy()
    {
        enemyCount = 0;
        enemyCountType[0] = 0;
        enemyCountType[1] = 0;
        enemyCountType[2] = 0;
        enemyCountType[3] = 0;
        for (int i = 0; i < 100; i++)
        {
            if (enemy[i] != null)
            {
                enemyCount++;
                if (enemy[i].GetComponent<EnemyType1>())
                {
                    enemyCountType[0]++;
                }
                else if (enemy[i].GetComponent<EnemyType2>())
                {
                    enemyCountType[1]++;
                }
                else if (enemy[i].GetComponent<EnemyType3>())
                {
                    enemyCountType[2]++;
                }
                else if (enemy[i].GetComponent<EnemyType4>())
                {
                    enemyCountType[3]++;
                }
            }
        }
        if (waveState == 1)
        {
            if (status.enemySpawnCount <= 0 && enemyCount <= 0)
            {

                waveState = 2;
                if(waveSound == true)
                {
                    waveSound = false;
                    SoundManager.instance.assultValue = 0;
                }
                PlayerUISystem.instance.EndWaveUI();
                if(wave != null)
                {
                    StopCoroutine(wave);
                }

                if (longWave == 0)
                {
                    EventManager.instance.nextReady = true;
                }
            }

        }
    }

    public void ForceStopWave()
    {
        waveState = 2;
        if (waveSound == true)
        {
            waveSound = false;
            SoundManager.instance.assultValue = 0;
        }
        PlayerUISystem.instance.EndWaveUI();
        if (wave != null)
        {
            StopCoroutine(wave);
        }
    }

    public void DestroyAllEnemy()
    {
        eCount = 0;
        while (true)
        {
            eCount++;
            if (eCount >= 100)
            {
                eCount = 0;
                break;
            }
            if (enemy[eCount] != null)
            {
                Destroy(enemy[eCount]);
            }
        }
    }

    void RegisterEnemy(GameObject obj)
    {
        while (true)
        {
            eCount++;
            if (eCount >= 100)
            {
                eCount = 0;
            }
            if (enemy[eCount] == null)
            {
                enemy[eCount] = obj;
                break;
            }
        }

    }

    public void SpawnEnemy(int num, int count, int pos)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab[num]);
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemy.transform.position = spawnPoint.transform.GetChild(pos).position;
            if (num >= 2)
            {
                enemy.transform.position += new Vector3(0, 3, 0);
            }
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            RegisterEnemy(enemy);
        }
    }

    public void SpawnEnemy(int num, int count, Vector3 pos)
    {
       
        for (int i = 0; i < count; i++)
        {
            
            GameObject enemy = Instantiate(enemyPrefab[num]);
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemy.transform.position = pos;
            if (num >= 2)
            {
                enemy.transform.position += new Vector3(0, 3, 0);
            }
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            RegisterEnemy(enemy);
        }
    }



    public void StartWave(string type, bool sound)
    {
        status.enemySpawnCount = 1;
        //Invoke(type, 0);
        waveState = 1;
        PlayerUISystem.instance.StartWaveUI();
        if(sound == true)
        {
            wave = StartCoroutine(StartWaveSound());
        }
    }

    

    Coroutine wave;
    bool waveSound;
    IEnumerator StartWaveSound()
    {
        waveSound = true;
        SoundManager.instance.assultValue = 1;
        yield return new WaitForEndOfFrame();
    }


}
