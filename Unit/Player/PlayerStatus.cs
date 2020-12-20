using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Unit
{
    

    //상태
    public enum EPlayerFSM
    {
        Hipfire,
        Shoulderfire,
        Dodge,
        Run,
        TimeStop,
        TimeRecall,
        Ultimate,
        Reload,
        Interaction,
        KnockBack,
        Dead,
        Grabbed,
        Cinematic
    }

    public enum EPlayerMode
    {
        TacticsMode,
        NonCombatMode
    }

    public enum EPlayerJump
    {
        Flight,
        NonJump
    }
    public EPlayerFSM PlayerFSM;
    public EPlayerMode PlayerMode;
    public EPlayerJump PlayerJump;


    //스펙
    public int hipFireDamage;
    public int shoulderFireDamage;


    public float walkingFastSpeed;
    public float walkingSlowSpeed;
    public float tacticsWalkingSpeedF;
    public float tacticsWalkingSpeedM;
    public float tacticsWalkingSpeedB;
    public float runningSpeed;
    public float rotationSpeed;
    public float dodgeSpeed;


    public int ammo;
    public int ammoMax;
    public int dodgeStack;
    public float attackRange;

    public float jumpVelocity;
    public float jumpGravity;

    public int ultimateGage;
    public int ultimateGageMax;

    //쿨타임
    public double timeRecallDelayTime;
    public double timeRecallDelayTimeNow;
    public double timeStopFieldDelayTime;
    public double timeStopFieldDelayTimeNow;

    //딜레이시간
    public double shotDelayTime1;
    public double shotDelayTimeNow1;
    public double shotDelayTime2;
    public double shotDelayTimeNow2;
    public double dodgeDelayTime;
    public double dodgeDelayTimeNow;
    public double reloadDelayTime;
    public double reloadDelayTimeNow;
    public double jumpDelayTime;
    public double jumpDelayTimeNow;
    public double dodgeStateTime;
    public double dodgeStateTimeNow;
    public double tacticsModeTime;
    public double tacticsModeTimeNow;
    public double timeStopFieldTime;
    public double timeStopFieldTimeNow;
    public double knockBackTime;
    public double knockBackTimeNow;
    public double noDeathTime;
    public double noDeathTimeNow;

    //판정
    public bool timeStopFieldState;
    public double inputTimeNow;
}
