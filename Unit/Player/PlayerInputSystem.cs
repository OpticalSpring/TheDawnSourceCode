using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }
    

    // Update is called once per frame
    void Update()
    {

        if (player.PlayerFSM == PlayerStatus.EPlayerFSM.Dead)
        {
            return;
        }
        if (player.PlayerFSM == PlayerStatus.EPlayerFSM.Ultimate)
        {
            return;
        }
        if (player.PlayerFSM == PlayerStatus.EPlayerFSM.Cinematic)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.timeStopState == true)
            {
                if (OptionManager.instance.optionState == false)
                {
                    GameManager.instance.OutESC();
                }
            }
            else
            {

                GameManager.instance.SetESC();
            }
        }

        if(player.PlayerFSM == PlayerStatus.EPlayerFSM.Grabbed)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.ActTimeRecall();
                PlayerUISystem.instance.popupTimeRecall.SetActive(false);
            }
            return;
        }

        if (GameManager.instance.timeStopState == true)
        {
            return; 
        }




        

        if (player.PlayerFSM == PlayerStatus.EPlayerFSM.Interaction)
        {
            return;
        }
        
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetMouseButton(1))
        {
            player.ActShoulderFire();
        }
        else
        {
            player.ActHipFire();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(player.timeStopFieldState == true)
            {
                player.ActTimeStopField();
            }
            else
            {
                player.ActFire();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ActDodge();
        }
            
        player.inputAxis.x = Input.GetAxisRaw("Horizontal");
        player.inputAxis.y = Input.GetAxisRaw("Vertical");
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            player.CommandMovement();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ActReload();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.ActTimeRecall();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.SetTimeStopField();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            player.ActUltimateSkill();
        }

        if (Input.GetMouseButtonDown(1))
        {
            player.CancelTimeStopField();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerUISystem.instance.SetTab();
        }
       
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            PlayerCameraSystem.instance.SetMaxDistance(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}
