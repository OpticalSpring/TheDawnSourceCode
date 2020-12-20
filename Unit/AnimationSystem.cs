using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    public void SetAniState(int state)
    {
        animator.SetInteger("AniState", state);
    }

    public void SetWalkState(Vector2 inputAxis)
    {
        Vector2 input = inputAxis;
        if (inputAxis.y > 0.9f)
        {
            input.y = 0.9f;
        }
        if (inputAxis.y < -0.9f)
        {
            input.y = -0.9f;
        }
        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);
    }

    public void SetUpperState(int state)
    {
        animator.SetInteger("UpperState", state);
    }

    public void SetJump(bool state)
    {
        animator.SetBool("Jump", state);
    }

    public void SetRebound(bool state)
    {
        animator.SetBool("Rebound", state);
    }

    public void SetAniSpeed(float speed)
    {
        animator.speed = speed;
    }

    public void SetInput(bool ip)
    {
        animator.SetBool("Input", ip);
    }
}
