using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;
    private void Awake()
    {
        instance = this;
    }
    struct SaveData
    {
        public Vector3 pPos;
        public Quaternion pRot;
    }
    SaveData[] sData = new SaveData[20];
    public Image blackImage;
    public GlitchControl glicth;
   
    public GameObject player;
    public GameObject cam;
    public int num;
    int count;
    // Start is called before the first frame update

    void Start()
    {
        InitSave();
        //InvokeRepeating("Save", 0, 0.2f);
        StartCoroutine(SaveState());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitSave()
    {
        for (int i = 0; i < 20; i++)
        {
            sData[i].pPos = player.transform.position;
        }
    }

    public void Save()
    {
        if(count > 0)
        {
            return;
        }
        num++;
        if(num >= sData.Length)
        {
            num = 0;
        }
        sData[num].pPos = player.transform.position;
        sData[num].pRot = player.transform.rotation;
    }

    public void LoadCheckPoint()
    {
        StartCoroutine(LoadProduction());
    }

    IEnumerator LoadProduction()
    {
        //시간역행 효과 적용
        Player.instance.OnRim();
        int sNum = num;
        player.GetComponent<Player>().HP_Point = player.GetComponent<Player>().HP_PointMax;
        player.GetComponent<Player>().ammo = player.GetComponent<Player>().ammoMax;
        player.GetComponent<Player>().dodgeStack = 5;
        SoundManager.instance.SoundPlay(1, 2);

        //시간역행 연출
        count = 20;
        Vector3 pos = sData[sNum].pPos;
        double timer = 0.5f;
        while (timer > 0)
        {
            if (count > 0)
            {
                //저장된 위치에 순차적으로 이동
                player.transform.position = Vector3.MoveTowards(player.transform.position, pos, 100 * Time.deltaTime);
                if (Vector3.Distance(player.transform.position, pos) < 0.5f)
                {
                    count--;
                    sNum--;
                    if (sNum < 0)
                    {
                        sNum = 19;
                    }
                    pos = sData[sNum].pPos;
                }
            }
            Player.instance.ActMotionTrail();
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Player.instance.effect.EndTimeRecall();
        yield return new WaitForSeconds(0.5f);
        Player.instance.OffRim();
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Hipfire;
        count = 0;
    }

    IEnumerator SaveState()
    {
        while (true)
        {
            Save();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
