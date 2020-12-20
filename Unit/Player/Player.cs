using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerStatus
{
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }
    public Vector2 inputAxis;
    public Vector2 inputAxisWalk;
    public GameObject rotationPoint;
    public GameObject aimPoint;

    public PlayerCameraSystem playerCamera;
    public GameObject rayDes;
    public GameObject[] motionScript;
    public GameObject[] rim;
    public GameObject characterModel;
    GameObject checkPoint;
    [HideInInspector]
    public GameObject frontRay;
    GameObject inclinePoint1;
    GameObject inclinePoint2;
    GameObject backRay;
    [HideInInspector]
    public AnimationSystem animator;
    [HideInInspector]
    public PlayerEffectSystem effect;
    [HideInInspector]
    public PlayerGun gun;
    public override void Start()
    {
        base.Start();
        characterModel = gameObject.transform.GetChild(0).gameObject;
        checkPoint = gameObject.transform.GetChild(1).gameObject;
        frontRay = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        inclinePoint1 = gameObject.transform.GetChild(1).GetChild(1).gameObject;
        inclinePoint2 = gameObject.transform.GetChild(1).GetChild(2).gameObject;
        backRay = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        animator = GetComponent<AnimationSystem>();
        effect = GetComponent<PlayerEffectSystem>();
        gun = GetComponent<PlayerGun>();
        tacticsModeTimeNow = tacticsModeTime;
    }
    public override void Update()
    {
        CheckDelayTime();
        CheckState();
        if(gameObject.transform.position.y < -20)
        {
            Hit(100);
        }
    }
    public override int Hit(int damage)
    {
        if (PlayerFSM == EPlayerFSM.Cinematic)
        {
            return 0;
        }
        if (PlayerFSM == EPlayerFSM.Dead)
        {
            return 0;
        }
        if (noDeathTimeNow > 0)
        {
            return 0;
        }
        base.Hit(damage);
        playerCamera.Shaking(damage * 0.01f, 0.15f);
        SoundManager.instance.SoundPlay(5, 0, 0.5f);
        SetTacticsMode();
        return 0;
    }

    public override int Hit(int damage, bool stun)
    {
        if (PlayerFSM == EPlayerFSM.Cinematic)
        {
            return 0;
        }
        if (PlayerFSM == EPlayerFSM.Interaction)
        {
            Hit(damage);
            return 0;
        }
        if (PlayerFSM == EPlayerFSM.Dead)
        {
            return 0;
        }
        if (noDeathTimeNow > 0)
        {
            return 0;
        }
        playerCamera.Shaking(damage * 0.01f, 0.15f);
        SoundManager.instance.SoundPlay(5, 0, 0.5f);
        CancelTimeStopField();
        PlayerFSM = EPlayerFSM.KnockBack;
        knockBackTimeNow = knockBackTime;
        noDeathTimeNow = 1.5f;
        animator.SetAniState(13);
        animator.SetUpperState(0);
        SetTacticsMode();
        CloseOutCam();
        base.Hit(damage);
        return 0;
    }

    public override void Die()
    {
        StartCoroutine(DelayDead());
        PlayerFSM = EPlayerFSM.Dead;
        animator.SetAniState(-1);
        animator.SetUpperState(0);
    }

    IEnumerator DelayDead()
    {

        CheckPointManager.instance.glicth.zero = true;

        yield return new WaitForSeconds(2);
        CheckPointManager.instance.glicth.zero = false;
        GameManager.instance.SetDead();
    }

    void CheckDelayTime()
    {
        if (PlayerFSM == EPlayerFSM.Cinematic)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.Dead)
        {
            return;
        }

        if (dodgeStack < 5)
        {
            if (dodgeDelayTimeNow > 0)
            {
                dodgeDelayTimeNow -= Time.deltaTime;
            }
            else
            {

                PlayerUISystem.instance.ActFlashSpace(dodgeStack);
                dodgeStack++;
                dodgeDelayTimeNow = dodgeDelayTime;
            }
        }

        if (shotDelayTimeNow1 > 0)
        {
            shotDelayTimeNow1 -= Time.deltaTime;
        }
        if (shotDelayTimeNow2 > 0)
        {
            shotDelayTimeNow2 -= Time.deltaTime;
        }

        if (reloadDelayTimeNow > 0)
        {
            reloadDelayTimeNow -= Time.deltaTime;
        }
        else if (reloadDelayTimeNow < 0)
        {
            reloadDelayTimeNow = 0;
            ammo = ammoMax;
            PlayerFSM = EPlayerFSM.Hipfire;
        }
        else
        {
            if (ammo <= 0)
            {
                ActReload();
            }
        }

        if (jumpDelayTimeNow > 0)
        {
            jumpDelayTimeNow -= Time.deltaTime;
        }

        if (dodgeStateTimeNow > 0)
        {
            dodgeStateTimeNow -= Time.deltaTime;

        }
        else if (dodgeStateTimeNow < 0)
        {
            PlayerFSM = EPlayerFSM.Hipfire;
            dodgeStateTimeNow = 0;
        }


        if (tacticsModeTimeNow > 0)
        {
            tacticsModeTimeNow -= Time.deltaTime;
        }
        else
        {
            PlayerMode = EPlayerMode.NonCombatMode;
        }

        if (timeStopFieldTimeNow > 0)
        {
            timeStopFieldTimeNow -= Time.deltaTime;
        }
        else if (timeStopFieldTimeNow < 0)
        {
            timeStopFieldTimeNow = 0;
            SkillManager.instance.FinishStopField();
        }
        

        if (timeStopFieldDelayTimeNow > 0)
        {
            timeStopFieldDelayTimeNow -= Time.deltaTime;
        }

        if (timeRecallDelayTimeNow > 0)
        {
            timeRecallDelayTimeNow -= Time.deltaTime;
        }

        if (knockBackTimeNow > 0.2f)
        {
            BackMove(runningSpeed, backRay.transform.position);
            knockBackTimeNow -= Time.deltaTime;
        }
        else if (knockBackTimeNow > 0.0f)
        {
            knockBackTimeNow -= Time.deltaTime;
        }
        else if(PlayerFSM == EPlayerFSM.KnockBack)
        {
            PlayerFSM = EPlayerFSM.Hipfire;
        }

        if (noDeathTimeNow > 0)
        {
            noDeathTimeNow -= Time.deltaTime;
        }
    }

    public void SetTacticsMode()
    {
        tacticsModeTimeNow = tacticsModeTime;
        PlayerMode = EPlayerMode.TacticsMode;
    }

    void CheckState()
    {
        if(PlayerFSM == EPlayerFSM.Cinematic)
        {
            return;
        }

        if (HP_Point <= 0)
        {
            PlayerFSM = EPlayerFSM.Dead;
            animator.SetAniState(-1);
            animator.SetUpperState(0);
        }

        if(PlayerFSM == EPlayerFSM.Dead)
        {
            CloseOutCam();
            return;
        }

        if(PlayerFSM == EPlayerFSM.Grabbed)
        {
            animator.SetAniState(14);
            animator.SetUpperState(0);
            CloseOutCam();
            return;
        }


        inputAxisWalk.x = Mathf.Lerp(inputAxisWalk.x, inputAxis.x, Time.deltaTime * 10);
        inputAxisWalk.y = Mathf.Lerp(inputAxisWalk.y, inputAxis.y, Time.deltaTime * 10);
        animator.SetWalkState(inputAxisWalk);
        if (Mathf.Abs(inputAxis.x) + Mathf.Abs(inputAxis.y) > 0)
        {
            inputTimeNow = 0.1f;
        }
        if (inputTimeNow > 0)
        {
            inputTimeNow -= Time.deltaTime;
            animator.SetInput(true);
        }
        else
        {
            animator.SetInput(false);
        }
        if (PlayerFSM == EPlayerFSM.Dodge)
        {
            return;
        }



        if (jumpDelayTimeNow > 0)
        {
            JumpState();
        }
        else
        {

            RaycastHit rayHit;
            int mask = 1 << 2 | 1 << 8 | 1 << 9;
            mask = ~mask;
            if (Physics.Raycast(inclinePoint1.transform.position, inclinePoint1.transform.forward, out rayHit, 1.5f, mask))
            {
                jumpVelocity = 0;
                PlayerJump = EPlayerJump.NonJump;
                //animator.SetJump(false);
            }
            else if (Physics.Raycast(inclinePoint2.transform.position, inclinePoint2.transform.forward, out rayHit, 1.5f, mask))
            {
                jumpVelocity = 0;
                PlayerJump = EPlayerJump.NonJump;
                //animator.SetJump(false);
            }
            else
            {
                PlayerJump = EPlayerJump.Flight;
                //animator.SetJump(true);
                JumpState();
            }
        }

        if (timeStopFieldState == true)
        {
            ReadyTimeStopField();
        }
        else
        {
            SkillManager.instance.CancelStopField();
        }
    }

    public void CommandMovement()
    {
        SetRotationPoint();
        CheckPointTurn();
        switch (PlayerFSM)
        {
            case EPlayerFSM.Run:
                Run();
                break;
            case EPlayerFSM.Hipfire:
                WalkFast();
                break;
            case EPlayerFSM.Shoulderfire:
                WalkSlow();
                break;
            case EPlayerFSM.Reload:
                WalkFast();
                Turn(aimPoint.transform.position);
                break;
            case EPlayerFSM.TimeStop:
                WalkFast();
                Turn(aimPoint.transform.position);
                break;
            case EPlayerFSM.TimeRecall:
                Turn(aimPoint.transform.position);
                break;
            case EPlayerFSM.Dodge:
                Dodge();
                break;
            default:
                break;
        }

    }


    void CloseOnCam()
    {
        playerCamera.CloseOnCam();
    }

    void CloseOutCam()
    {
        playerCamera.CloseOutCam();
    }

    public void ActInteraction(Vector3 pos)
    {
        Turn(pos);
        PlayerFSM = EPlayerFSM.Interaction;
        animator.SetAniState(9);
        animator.SetUpperState(0);
        CancelTimeStopField();
    }

    public void DoneInteraction()
    {
        if (PlayerFSM == EPlayerFSM.Interaction)
        {
            tacticsModeTimeNow = 0;
            PlayerFSM = EPlayerFSM.Hipfire;
            PlayerMode = EPlayerMode.NonCombatMode;
            animator.SetAniState(11);
            animator.SetUpperState(0);
        }
    }

    public void ActHipFire()
    {
        if (PlayerFSM == EPlayerFSM.Dodge)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.Reload)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.TimeStop)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.TimeRecall)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.Ultimate)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.KnockBack)
        {
            return;
        }
        CloseOutCam();
        PlayerFSM = EPlayerFSM.Hipfire;
        if (PlayerMode == EPlayerMode.TacticsMode)
        {
            animator.SetAniState(0);
            animator.SetUpperState(1);
            Turn(aimPoint.transform.position);
        }
        else
        {
            animator.SetAniState(11);
            animator.SetUpperState(0);
        }
    }

    public void ActShoulderFire()
    {
        if (PlayerFSM == EPlayerFSM.Dodge)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.Reload)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.TimeStop)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.TimeRecall)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.Ultimate)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.KnockBack)
        {
            return;
        }
        if (doneAim == true)
        {
            return;
        }
        SetTacticsMode();
        CloseOnCam();
        PlayerFSM = EPlayerFSM.Shoulderfire;
        animator.SetAniState(0);
        animator.SetUpperState(3);
        Turn(aimPoint.transform.position);
    }

    public void ActFire()
    {
        if (PlayerMode == EPlayerMode.NonCombatMode)
        {
            SetTacticsMode();
            return;
        }
        SetTacticsMode();

        if (PlayerFSM == EPlayerFSM.Hipfire)
        {
            HipFireShot();
        }
        else if (PlayerFSM == EPlayerFSM.Shoulderfire)
        {
            ShoulderFireShot();
        }
    }

    public void ActDodge()
    {
        //닷지 가능한지 체크
        if (dodgeStack <= 0)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.TimeRecall)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.Ultimate)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.KnockBack)
        {
            return;
        }
        
        //닷지에 필요한 요소들 처리
        ActMotionTrail();
        SoundManager.instance.SoundPlay3D(1, 0, gameObject.transform.position, 0.25f);
        dodgeStack--;
        CloseOutCam();
        PlayerFSM = EPlayerFSM.Dodge;
        
        dodgeStateTimeNow = dodgeStateTime;

        //전투/비전투에 따라 다른종류의 애니메이션 출력
        if (PlayerMode == EPlayerMode.NonCombatMode)
        {
            animator.SetAniState(1);
            animator.SetUpperState(0);
        }
        else
        {
            animator.SetAniState(21);
        }
    }
    public void ActReload()
    {
        if (reloadDelayTimeNow > 0)
        {
            return;
        }
        if (ammo >= ammoMax)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.KnockBack)
        {
            return;
        }
        SetTacticsMode();
        CloseOutCam();
        effect.StartGunReloadEffect();
        reloadDelayTimeNow = reloadDelayTime;
        SoundManager.instance.SoundPlay(1, 1);
        PlayerFSM = EPlayerFSM.Reload;
        animator.SetAniState(10);
        animator.SetUpperState(10);

    }
    public void ActTacticsMode()
    {
        PlayerMode = EPlayerMode.TacticsMode;
    }
    public void ActNonCombatMode()
    {
        PlayerMode = EPlayerMode.NonCombatMode;
    }

    public void ActTimeRecall()
    {
        if (timeRecallDelayTimeNow > 0)
        {
            return;
        }
        noDeathTimeNow = noDeathTime;
        timeRecallDelayTimeNow = timeRecallDelayTime;
        CancelTimeStopField();
        PlayerFSM = EPlayerFSM.TimeRecall;
        CloseOutCam();
        dodgeStateTimeNow = 0;
        effect.StartTimeRecall();
        animator.SetAniState(7);
        animator.SetUpperState(0);
        CheckPointManager.instance.LoadCheckPoint();
        ActMotionTrail();
    }

    public void ActMotionTrail()
    {
        for (int i = 0; i < motionScript.Length; i++)
        {

            motionScript[i].GetComponent<MotionTrail>().enabled = false;
            motionScript[i].GetComponent<MotionTrail>().enabled = true;
        }
    }

    bool doneAim;
    public void SetTimeStopField()
    {
        if (PlayerFSM == EPlayerFSM.KnockBack)
        {
            return;
        }
        if (timeStopFieldDelayTimeNow > 0)
        {
            return;
        }
        if(timeStopFieldState == true)
        {
           
        }
        else
        {
            PlayerFSM = EPlayerFSM.TimeStop;
            CloseOutCam();
            CancelTimeStopField();
            timeStopFieldState = true;
            doneAim = true;
        }
    }

    public void CancelTimeStopField()
    {
        if (timeStopFieldState == true)
        {
            PlayerFSM = EPlayerFSM.Hipfire;
            timeStopFieldState = false;
        }
        else
        {
            doneAim = false;
        }
    }

    public void ReadyTimeStopField()
    {
        if (timeStopFieldDelayTimeNow > 0)
        {
            return;
        }
        
        animator.SetAniState(0);
        animator.SetUpperState(5);
        SetTacticsMode();
        Turn(aimPoint.transform.position);
        SkillManager.instance.ReadyStopField();
    }
    public void ActTimeStopField()
    {
        if (timeStopFieldState == false)
        {
            return;
        }
        timeStopFieldDelayTimeNow = timeStopFieldDelayTime;
        timeStopFieldState = false;
        timeStopFieldTimeNow = timeStopFieldTime;
        StartCoroutine(TimeStopFieldActDelayFSM());
        SetTacticsMode();
        animator.SetAniState(0);
        animator.SetUpperState(6);
        effect.StartTimeStopField();
        SkillManager.instance.StopField();
    }

    IEnumerator TimeStopFieldActDelayFSM()
    {
        yield return new WaitForSeconds(1);
        PlayerFSM = EPlayerFSM.Hipfire;
    }

    public void ActUltimateSkill()
    {
        if(ultimateGage < ultimateGageMax)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.KnockBack)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.TimeRecall)
        {
            return;
        }
        dodgeStateTimeNow = 0;
        ultimateGage = 0;
        noDeathTimeNow = 10;
        SetTacticsMode();
        tacticsModeTimeNow = 10;
        CancelTimeStopField();
        PlayerFSM = EPlayerFSM.Ultimate;
        animator.SetAniState(8);
        animator.SetUpperState(0);
        SkillManager.instance.UltimateSkill();
    }

    void JumpState()
    {
        jumpVelocity = jumpVelocity - jumpGravity * Time.deltaTime;
        gameObject.transform.position += new Vector3(0, jumpVelocity * Time.deltaTime, 0);
    }
    void SetRotationPoint()
    {
        rotationPoint.transform.localPosition = new Vector3(inputAxis.x * 100, 0, inputAxis.y * 100);
    }
    void CheckPointTurn()
    {
        float dz = rotationPoint.transform.position.z - checkPoint.transform.position.z;
        float dx = rotationPoint.transform.position.x - checkPoint.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        checkPoint.transform.rotation = Quaternion.Lerp(checkPoint.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), rotationSpeed * Time.deltaTime);
    }
    bool CheckObstacle()
    {
        float alpha = 0;
        if (PlayerFSM == EPlayerFSM.Dodge)
        {
            if (CheckFPS.instance._meanFps < 60)
            {
                alpha = 60 - CheckFPS.instance._meanFps;
                alpha *= 0.2f;
            }
        }
        //이동속도에 비해 프레임이 낮을 경우 벽을 뚫는 경우가 발생하여 프레임에 따른 안전장치

        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 9;
        mask = ~mask;

        if (Physics.SphereCast(frontRay.transform.position, 0.2f,  frontRay.transform.forward, out rayHit, 0.7f + alpha, mask))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    bool BackCheckObstacle()
    {
        float alpha = 0;
        if (PlayerFSM == EPlayerFSM.Dodge)
        {
            if (CheckFPS.instance._meanFps < 60)
            {
                alpha = 60 - CheckFPS.instance._meanFps;
                alpha *= 0.2f;
            }
        }

        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 9;
        mask = ~mask;

        if (Physics.SphereCast(frontRay.transform.position, 0.2f, backRay.transform.position - gameObject.transform.position, out rayHit, 0.7f + alpha, mask))
        {
            return true;
        }
        else
        {
            return false;
        }

    }



    void Turn(Vector3 targetPoint)
    {
        float dz = targetPoint.z - characterModel.transform.position.z;
        float dx = targetPoint.x - characterModel.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;
        characterModel.transform.rotation = Quaternion.Lerp(characterModel.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), rotationSpeed * Time.deltaTime);

    }
    void Move(float speed, Vector3 point)
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 9;
        mask = ~mask;

        if (CheckObstacle() == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, point, speed * Time.deltaTime);
            
        }
        else
        {
            //앞에 장애물이 타밎될 경우 슬라이딩 벡터를 통해 벽에 미끄러지는 것을 구현하여 답답하지 않은 조작을 의도
            Vector3 S;
            Vector3 V = Vector3.Normalize(frontRay.transform.forward);
            Vector3 nPoint = gameObject.transform.position;
            if (Physics.SphereCast(frontRay.transform.position, 0.2f, frontRay.transform.forward, out rayHit, 0.8f, mask))
            {
                S = V - rayHit.normal * (Vector3.Dot(V, rayHit.normal));
                if (Physics.Raycast(frontRay.transform.position, S, out rayHit, 0.5f, mask))
                {

                }
                else
                {

                    nPoint += S * 10;
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nPoint, Mathf.Abs(S.x + S.y + S.z) * speed * Time.deltaTime * 1.0f);
                }
            }
            
        }


        if (PlayerJump == EPlayerJump.Flight)
        {
            return;
        }
        //계단이동 및 충돌 때 공중에 캐릭터가 뜨는 현상을 방지하기 위해 Raycast로 높이를 보정
        if (PlayerFSM == EPlayerFSM.Dodge)
        {
            if (Physics.Raycast(inclinePoint1.transform.position, inclinePoint1.transform.forward, out rayHit, 2.0f, mask) && CheckObstacle() == false)
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = newPos;
            }
            else if (Physics.Raycast(inclinePoint2.transform.position, inclinePoint2.transform.forward, out rayHit, 2.0f, mask))
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = newPos;
            }
        }
        else
        {
            if (Physics.Raycast(inclinePoint1.transform.position, inclinePoint1.transform.forward, out rayHit, 2.0f, mask) && CheckObstacle() == false)
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, Time.deltaTime * 10);

            }
            else if (Physics.Raycast(inclinePoint2.transform.position, inclinePoint2.transform.forward, out rayHit, 2.0f, mask))
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, Time.deltaTime * 10);

            }
        }
    }

    void BackMove(float speed, Vector3 point)
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 9;
        mask = ~mask;

        if (BackCheckObstacle() == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, point, speed * Time.deltaTime);

        }
        else
        {
            Vector3 S;
            Vector3 V = Vector3.Normalize(frontRay.transform.forward);
            Vector3 nPoint = gameObject.transform.position;
            if (Physics.SphereCast(frontRay.transform.position, 0.2f, frontRay.transform.forward, out rayHit, 0.8f, mask))
            {
                S = V - rayHit.normal * (Vector3.Dot(V, rayHit.normal));
                if (Physics.Raycast(frontRay.transform.position, S, out rayHit, 0.5f, mask))
                {

                }
                else
                {

                    nPoint += S * 10;
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nPoint, Mathf.Abs(S.x + S.y + S.z) * speed * Time.deltaTime * 1.0f);
                }
            }

        }


        if (PlayerJump == EPlayerJump.Flight)
        {
            return;
        }
        if (PlayerFSM == EPlayerFSM.Dodge)
        {
            if (Physics.Raycast(inclinePoint1.transform.position, inclinePoint1.transform.forward, out rayHit, 2.0f, mask) && CheckObstacle() == false)
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = newPos;
            }
            else if (Physics.Raycast(inclinePoint2.transform.position, inclinePoint2.transform.forward, out rayHit, 2.0f, mask))
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = newPos;
            }
        }
        else
        {
            if (Physics.Raycast(inclinePoint1.transform.position, inclinePoint1.transform.forward, out rayHit, 2.0f, mask) && CheckObstacle() == false)
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, Time.deltaTime * 10);

            }
            else if (Physics.Raycast(inclinePoint2.transform.position, inclinePoint2.transform.forward, out rayHit, 2.0f, mask))
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.y = rayHit.point.y;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, Time.deltaTime * 10);

            }
        }

    }

    void WalkFast()
    {
        if (PlayerMode == EPlayerMode.TacticsMode)
        {
            switch (inputAxis.y)
            {
                case 1:
                    Move(tacticsWalkingSpeedF, rotationPoint.transform.position);
                    break;
                case 0:
                    Move(tacticsWalkingSpeedM, rotationPoint.transform.position);
                    break;
                case -1:
                    Move(tacticsWalkingSpeedB, rotationPoint.transform.position);
                    break;
            }


        }
        else
        {
            Turn(rotationPoint.transform.position);
            animator.SetAniState(12);
            animator.SetUpperState(0);
            Move(walkingFastSpeed, rotationPoint.transform.position);
        }
    }

    void WalkSlow()
    {
        Move(walkingSlowSpeed, rotationPoint.transform.position);
    }

    void Run()
    {
        Turn(rotationPoint.transform.position);
        Move(runningSpeed, rotationPoint.transform.position);
    }

    void Dodge()
    {
        if(PlayerMode == EPlayerMode.TacticsMode)
        {
            
            Move(dodgeSpeed, rotationPoint.transform.position);
        }
        else
        {
            
            Turn(rotationPoint.transform.position);
            Move(dodgeSpeed, rotationPoint.transform.position);
        }
    }


    void HipFireShot()
    {
        if (reloadDelayTimeNow > 0)
        {
            return;
        }
        if (ammo <= 0)
        {
            
            return;
        }
        if (shotDelayTimeNow1 > 0)
        {
            return;
        }
        animator.SetAniState(0);
        animator.SetUpperState(2);
      //  animator.SetRebound(true);
        StartCoroutine(ReboundHipFire());
        shotDelayTimeNow1 = shotDelayTime1;
        ammo--;
        gun.HipFire(rayDes.transform.position);
        playerCamera.Shaking(8 * 0.01f, 0.05f);
    }

    void ShoulderFireShot()
    {
        if (reloadDelayTimeNow > 0)
        {
            return;
        }
        if (ammo <= 0)
        {
            
            return;
        }
        if (shotDelayTimeNow2 > 0)
        {
            return;
        }
        animator.SetAniState(0);
        animator.SetUpperState(4);
       // animator.SetRebound(true);
        StartCoroutine(ReboundShoulderFire());
        shotDelayTimeNow2 = shotDelayTime2;
        ammo--;
        gun.ShoulderFire(rayDes.transform.position);
        playerCamera.Shaking(4f * 0.01f, 0.05f);
    }


    IEnumerator ReboundHipFire()
    {
        float re = playerCamera.rotateValue.x - 50;
        for (int i = 0; i < 5; i++)
        {
            playerCamera.rotateValue.x = Mathf.Lerp(playerCamera.rotateValue.x, re, Time.fixedDeltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        re = playerCamera.rotateValue.x + 30;
        for (int i = 0; i < 10; i++)
        {
            playerCamera.rotateValue.x = Mathf.Lerp(playerCamera.rotateValue.x, re, Time.fixedDeltaTime * 0.5f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
        //animator.SetRebound(false);
    }

    IEnumerator ReboundShoulderFire()
    {
        float re = playerCamera.rotateValue.x - 50;
        for (int i = 0; i < 5; i++)
        {
            playerCamera.rotateValue.x = Mathf.Lerp(playerCamera.rotateValue.x, re, Time.fixedDeltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        re = playerCamera.rotateValue.x + 30;
        for (int i = 0; i < 10; i++)
        {
            playerCamera.rotateValue.x = Mathf.Lerp(playerCamera.rotateValue.x, re, Time.fixedDeltaTime * 0.5f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
        //animator.SetRebound(false);
    }

    public void OnRim()
    {
        rim[0].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_TimeReset", 1);
        rim[1].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_TimeReset", 1);
    }

    public void OffRim()
    {
        StartCoroutine(OffRimLerp());
    }

    IEnumerator OffRimLerp()
    {
        float rimp = 1;
        while(rimp > 0.1f)
        {
            rimp = Mathf.Lerp(rimp, 0, Time.deltaTime * 2);
            rim[0].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_TimeReset", rimp);
            rim[1].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_TimeReset", rimp);
            yield return new WaitForEndOfFrame();
        }
        rimp = 0;
        rim[0].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_TimeReset", rimp);
        rim[1].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_TimeReset", rimp);
    }
}
