using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject cam;
    [HideInInspector]
    public Transform oldPos;
    public GameObject camPosPlayer;
    public GameObject camTargetPlayer;
    public Animator[] cineAni;
    public int state;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        oldPos = Camera.main.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                cam.transform.LookAt(camTargetPlayer.transform.position);
                break;
        }
    }

    public void SetPlayer()
    {
        cam.transform.parent = camPosPlayer.transform;
        cam.transform.localPosition = Vector3.zero;
        state = 1;
    }


    public void EndCinematic()
    {
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Hipfire;
        PlayerUISystem.instance.EndLetterBox();
        GameManager.instance.playUI.SetActive(true);
        cam.transform.parent = oldPos;
        cam.transform.localPosition = new Vector3(0, 1, 0);
        cam.transform.localEulerAngles = Vector3.zero;
        PlayerCameraSystem.instance.CamState = PlayerCameraSystem.CameraState.GamePlay;
        gameObject.SetActive(false);
    }

    public void StartCinematic(int num)
    {
        Player.instance.transform.position = new Vector3(0, 0, 92);
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Cinematic;
        PlayerUISystem.instance.SetLetterBox();
        GameManager.instance.playUI.SetActive(false);
        PlayerCameraSystem.instance.CamState = PlayerCameraSystem.CameraState.Cinematic;
        SetPlayer();
        Camera.main.fieldOfView = 45;
        for (int i = 0; i < cineAni.Length; i++)
        {
            cineAni[i].SetInteger("AniState", num);
        }
    }

    public void StartCinematic(int num, bool noLetter)
    {
        Player.instance.transform.position = new Vector3(0, 0, 92);
        Player.instance.PlayerFSM = PlayerStatus.EPlayerFSM.Cinematic;
        //PlayerUISystem.instance.SetLetterBox();
        GameManager.instance.playUI.SetActive(false);
        PlayerCameraSystem.instance.CamState = PlayerCameraSystem.CameraState.Cinematic;
        SetPlayer();
        Camera.main.fieldOfView = 45;
        for (int i = 0; i < cineAni.Length; i++)
        {
            cineAni[i].SetInteger("AniState", num);
        }
    }
}
