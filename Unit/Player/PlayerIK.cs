using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    public Vector3 offset;

    public Transform chest;
    public Player player;
    public PlayerCameraSystem cam;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
    }

    public Vector3 newPos;
    private void LateUpdate()
    {
        SetOffset();
    }


    void SetOffset()
    {
        if (player.PlayerMode == PlayerStatus.EPlayerMode.TacticsMode)
        {
            if (player.PlayerFSM == PlayerStatus.EPlayerFSM.Hipfire || player.PlayerFSM == PlayerStatus.EPlayerFSM.Shoulderfire
                || player.PlayerFSM == PlayerStatus.EPlayerFSM.Reload || player.PlayerFSM == PlayerStatus.EPlayerFSM.TimeStop)
            {
                newPos = chest.localEulerAngles;
                newPos.y = -cam.rotateValue.x + offset.y;
                chest.localEulerAngles = newPos;

            }
            else
            {
                chest.localEulerAngles = Vector3.zero;
            }
        }
        else
        {
            chest.localEulerAngles = Vector3.zero;
        }
    }
}
