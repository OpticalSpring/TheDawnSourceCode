using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
    public float dis;
    public bool lockState;

    public PostProcessVolume[] pp;
    float ppValue;
    float ppValueNow;
    float ppValue2;
    float ppValueNow2;
    public bool stopFieldState;
    public GameObject stopFieldReady;
    public GameObject stopFieldObject;
    public double bulletTimeNow;

    public GameObject[] ultimateEff;
    public GameObject camCinematic;

    public AudioMixer masterMixer;
    public float bgmPitchValue = 1;
    float bgmPitchValueNow = 1;
    public float pitchValue = 1;
    float pitchValueNow = 1;
    public float bgmLowPassValue = 22000;
    float bgmLowPassValueNow = 22000;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResetDistance());
        tPos = new GameObject("tPos");
    }
    GameObject tPos;

    // Update is called once per frame
    void Update()
    {
        CheckBulletTime();
        SetPitch();
        
    }

    void SetPitch()
    {
        
       pitchValueNow = Mathf.Lerp(pitchValueNow, pitchValue, Time.fixedDeltaTime * 2);
        masterMixer.SetFloat("SFX", pitchValueNow);
        bgmPitchValueNow = Mathf.Lerp(bgmPitchValueNow, bgmPitchValue, Time.fixedDeltaTime * 2);
        masterMixer.SetFloat("BGM", bgmPitchValueNow);
        bgmLowPassValueNow = Mathf.Lerp(bgmLowPassValueNow, bgmLowPassValue, Time.fixedDeltaTime * 10);
        SoundManager.instance.SetBGMLowPassFilter((int)bgmLowPassValueNow);
    }

    void CheckBulletTime()
    {
        //포스트 프로세싱 전환 변수 제어
        ppValueNow = Mathf.Lerp(ppValueNow, ppValue, Time.fixedDeltaTime * 3);
        ppValueNow2 = Mathf.Lerp(ppValueNow2, ppValue2, Time.fixedDeltaTime * 3);
        pp[0].weight = 1 - ppValueNow - ppValueNow2;
        pp[1].weight = ppValueNow - ppValueNow2;
        pp[2].weight = ppValueNow2;

        if(pp[1].weight < 0.1f)
        {
            pp[1].weight = 0;
        }
        if (pp[2].weight < 0.1f)
        {
            pp[2].weight = 0;
        }

        if (GameManager.instance.timeStopState == true)
        {
            ppValue2 = 1;
            return;
        }
        else
        {
            ppValue2 = 0;
        }
        //각 상태에 따른 불렛타임 여부 판정
        if (stopFieldState == true && Vector3.Distance(player.transform.position, stopFieldObject.transform.position) < 7)
        {
            pitchValue = 0.5f;
            bgmPitchValue = 0.5f;
            bgmLowPassValue = 22000;
            ppValue = 0;
            Time.timeScale = 1f;
        }
        else if (Player.instance.PlayerFSM == PlayerStatus.EPlayerFSM.Ultimate)
        {
            pitchValue = 1f;
            bgmPitchValue = 1.0f;
            bgmLowPassValue = 22000;
            ppValue = 0;
            Time.timeScale = 1f;
        }
        else
        {
            if (bulletTimeNow > 0 && player.GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dead && player.GetComponent<Player>().PlayerFSM != PlayerStatus.EPlayerFSM.Dodge)
            {
                pitchValue = 0.5f;
                bgmPitchValue = 1.0f;
                bgmLowPassValue = 500;
                Time.timeScale = 0.05f;
                ppValue = 1;
                bulletTimeNow -= Time.fixedDeltaTime;
            }
            else
            {
                pitchValue = 1f;
                bgmPitchValue = 1.0f;
                bgmLowPassValue = 22000;
                Time.timeScale = 1f;
                ppValue = 0;
            }
        }
    }

    public float CheckDistance(Vector3 pos)
    {
        float newDis = Vector3.Distance(player.transform.position + new Vector3(0, 1, 0), pos);
        if (dis > newDis)
        {
            dis = newDis;
        }
        return newDis;
    }

    IEnumerator ResetDistance()
    {
        while (true)
        {
            dis = 100;
            yield return new WaitForSecondsRealtime(1f);
        }
    }


    public void ReadyStopField()
    {
        stopFieldReady.SetActive(true);
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 9 | 1 << 10;
        mask = ~mask;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit, 30f, mask))
        {
            stopFieldReady.transform.position = rayHit.point + rayHit.normal * 2;
            if (Physics.Raycast(stopFieldReady.transform.position, Vector3.down, out rayHit, 100f, mask))
            {
                stopFieldReady.transform.position = rayHit.point;
            }
        }
    }
    public void CancelStopField()
    {
        stopFieldReady.SetActive(false);
    }

    public void StopField()
    {
        SoundManager.instance.SoundPlay(1, 3);
        SoundManager.instance.SoundPlay3D(1, 4, stopFieldReady.transform.position, 7.0);
        stopFieldReady.SetActive(false);
        stopFieldObject.SetActive(true);
        stopFieldObject.transform.position = stopFieldReady.transform.position;
        stopFieldState = true;
    }

    public void FinishStopField()
    {
        stopFieldObject.SetActive(false);
        stopFieldState = false;
    }

    public void UltimateSkill()
    {
        StartCoroutine(UltimateState());
        StartCoroutine(CamSet());
    }
    IEnumerator CamSet()
    {
        GameManager.instance.playUI.SetActive(false);
        PlayerUISystem.instance.SetLetterBox();
        PlayerCameraSystem.instance.CamState = PlayerCameraSystem.CameraState.Cinematic;
        Camera.main.fieldOfView = 45;
        GameObject camC = Instantiate(camCinematic);
        camC.transform.rotation = Player.instance.transform.GetChild(0).rotation;
        camC.transform.GetChild(0).position = Player.instance.transform.position;
        camC.transform.GetChild(1).position = pos[0];
        camC.GetComponent<PlayerUltimationPos>().cam = Camera.main.gameObject;
        camC.GetComponent<PlayerUltimationPos>().oldPos = Camera.main.transform.parent;
        camC.GetComponent<PlayerUltimationPos>().SetPlayer();


        yield return new WaitForSeconds(2.0f);
        camC.GetComponent<PlayerUltimationPos>().SetEffect();
        yield return new WaitForSeconds(3.0f);
        camC.GetComponent<PlayerUltimationPos>().SetPlayer();
        yield return new WaitForSeconds(2.6f);
        camC.GetComponent<PlayerUltimationPos>().ResetPos();
        PlayerCameraSystem.instance.CamState = PlayerCameraSystem.CameraState.GamePlay;
        
        PlayerUISystem.instance.EndLetterBox();
        yield return new WaitForSeconds(1f);
        GameManager.instance.playUI.SetActive(true);

    }
    Vector3[] pos = new Vector3[2];

    IEnumerator UltimateState()
    {
        pos[0] = Vector3.zero;
        pos[1] = Vector3.zero;
        RaycastHit rayHit1;
        RaycastHit rayHit2;
        bool ray1 = false;
        bool ray2 = false;
        int mask = 1 << 2 | 1 << 8 | 1 << 9;
        mask = ~mask;
        if (Physics.Raycast(Player.instance.frontRay.transform.position, Player.instance.frontRay.transform.forward, out rayHit1, 20f, mask))
        {
            ray1 = true;
        }
        if(Physics.Raycast(Player.instance.frontRay.transform.position, Player.instance.frontRay.transform.forward * -1, out rayHit2, 20f, mask))
        {
            ray2 = true;
        }

        if(ray1 == true && ray2 == true)
        {
            Vector3 nPos = (rayHit1.point + rayHit2.point)/2;
            float nF = Vector3.Distance(rayHit1.point, rayHit2.point)/3;
            tPos.transform.position = rayHit1.point;
            tPos.transform.Translate(Player.instance.frontRay.transform.forward * -nF);
            if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit1, 500f, mask))
            {
                pos[1] = rayHit1.point;
                player.transform.position = pos[1];
            }

            tPos.transform.Translate(Player.instance.frontRay.transform.forward * -nF);

            if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit1, 500f, mask))
            {
                pos[0] = rayHit1.point;
            }
        }
        else if(ray1 == true && ray2 == false)
        {
            tPos.transform.position = rayHit1.point;
            tPos.transform.Translate(Player.instance.frontRay.transform.forward * -10);

            if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit1, 500f, mask))
            {
                pos[1] = rayHit1.point;
                player.transform.position = pos[1];
            }

            tPos.transform.Translate(Player.instance.frontRay.transform.forward * -10);

            if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit1, 500f, mask))
            {
                pos[0] = rayHit1.point;
            }
        }
        else if(ray1 == false && ray2 == true)
        {
            tPos.transform.position = rayHit2.point;
            tPos.transform.Translate(Player.instance.frontRay.transform.forward * 10);

            if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit1, 500f, mask))
            {
                pos[1] = rayHit1.point;
                player.transform.position = pos[1];
            }

            tPos.transform.Translate(Player.instance.frontRay.transform.forward * 10);

            if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit1, 500f, mask))
            {
                pos[0] = rayHit1.point;
            }
        }
        else
        {
            tPos.transform.position = Player.instance.frontRay.transform.position + new Vector3(0, 2, 0);
            tPos.transform.Translate(Player.instance.frontRay.transform.forward * 10);

            if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit1, 500f, mask))
            {
                pos[0] = rayHit1.point;
            }
        }

        //if (Physics.Raycast(Player.instance.frontRay.transform.position, Player.instance.frontRay.transform.forward, out rayHit, 20f, mask))
        //{

        //    tPos.transform.position = rayHit.point;
        //    tPos.transform.Translate(Player.instance.frontRay.transform.forward * -10);

        //    if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit, 500f, mask))
        //    {
        //        pos[0] = rayHit.point;
        //    }
        //}
        //else
        //{
        //    tPos.transform.position = Player.instance.frontRay.transform.position + new Vector3(0,2,0);
        //    tPos.transform.Translate(Player.instance.frontRay.transform.forward * 10);

        //    if (Physics.Raycast(tPos.transform.position, Vector3.down, out rayHit, 500f, mask))
        //    {
        //        pos[0] = rayHit.point;
        //    }
        //}
        FastTurn(player.transform.GetChild(0).gameObject, pos[0]);
        SoundManager.instance.SoundPlay(1, 5);
        // SoundManager.instance.SoundPlay(1, 6);
        yield return new WaitForSeconds(0.5f);
        GameObject eff3 = Instantiate(ultimateEff[3]);
        eff3.transform.parent = Player.instance.gun.gunFirePos.transform;
        eff3.transform.position = Player.instance.gun.gunFirePos.transform.position;
        eff3.transform.rotation = gameObject.transform.rotation;
        Destroy(eff3, 2.0f);
        yield return new WaitForSeconds(1.3f);
        //SoundManager.instance.SoundPlay(1, 5);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UltimatePhase1(pos[0]));
        GameObject eff0 = Instantiate(ultimateEff[0]);
        eff0.transform.position = pos[0];
        Destroy(eff0,4f);

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(UltimatePhase2());
        GameObject eff1 = Instantiate(ultimateEff[1]);
        eff1.transform.position = pos[0];
        Destroy(eff1,2f);
       // SoundManager.instance.SoundPlay(1, 7);

        yield return new WaitForSeconds(2.15f);
        ScreenBlur_script.instance.SetBlur(10, 12, 1, 10);
        StartCoroutine(UltimatePhase3());
        GameObject eff2 = Instantiate(ultimateEff[2]);
        eff2.transform.position = pos[0];
        Destroy(eff2,2f);
      //  SoundManager.instance.SoundPlay(1, 8);
        yield return new WaitForSeconds(1.0f);
        Player.instance.effect.EndTimeRecall();
        Player.instance.tacticsModeTimeNow = 0;
        yield return new WaitForSeconds(1.2f);
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Hipfire;
        Player.instance.ActHipFire();
    }
    GameObject[] enemy;
    IEnumerator UltimatePhase1(Vector3 pos)
    {
        //주변에 있는 오브젝트를 판정
        Collider[] hitColliders = Physics.OverlapSphere(pos, 30);
        enemy = new GameObject[hitColliders.Length];
        int j = 0;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //판정한 오브젝트가 적일 경우에만 넣는다.
            if (hitColliders[i].GetComponent<EnemyStatus>())
            {
                enemy[j] = hitColliders[i].gameObject;
                enemy[j].GetComponent<TimeAgent>().speedFloat = 0;
                j++;
            }
        }
        //타이머를 설정하여 해당 시간동안 미리 판정한 적들을 끌어모음
        double timer = 4;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i] != null)
                {
                    enemy[i].transform.position = Vector3.Lerp(enemy[i].transform.position, pos, Time.deltaTime);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator UltimatePhase2()
    {
        for (int j = 0; j < 10; j++) { 

            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i] != null)
                {
                    enemy[i].GetComponent<Unit>().Hit(9);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator UltimatePhase3()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i] != null)
            {
                enemy[i].GetComponent<Unit>().Hit(200);
            }
        }
    }

    void FastTurn(GameObject obj, Vector3 targetPoint)
    {
        float dz = targetPoint.z - obj.transform.position.z;
        float dx = targetPoint.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0, rotateDegree, 0);

    }
}
