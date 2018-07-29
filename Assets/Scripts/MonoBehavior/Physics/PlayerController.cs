using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerController : PhysicsObject
{
    private struct _State {
        public bool isFacingRight;
        public bool flipX;
    }

    public float crouchWalkSpeed = 5f;
    public float runSpeed = 7f;
    public float jumpSpeed = 7f;

    private Animator animator;

    private bool isFacingRight = true;

    private bool isCrouching = false;

    private bool isTransition = false;

    public UnityEvent OnPlayerDead;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = InputManager.Instance.AxisX;
        float verticalInput = InputManager.Instance.AxisY;

        isCrouching = InputManager.Instance.CrouchKeyDown;

        if (isTransition)
            move.x = 0.0f;

        if (InputManager.Instance.JumpKeyDown && grounded && !isCrouching)
        {
            ///一般跳跃
            velocity += -jumpSpeed*GravityManager.Instance.direction;
            //velocity += -GravityManager.gravity * jumpSpeed;
        }
        else if (InputManager.Instance.JumpKeyUp)
        {
            ///一般引力缩减速度
            if (velocity.y > .0f)
                velocity.y = velocity.y * .01f;
        }


        if ((isFacingRight && move.x< -0.01f)||(!isFacingRight && move.x>0.01f))
        {
            //transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
            var sp = GetComponent<SpriteRenderer>();
            if (sp.flipX)
                sp.flipX = false;
            else
                sp.flipX = true;
            isFacingRight = !isFacingRight;
        }


        animator.SetFloat("HorizontalSpeed", Mathf.Abs(velocity.x) / runSpeed);
        animator.SetFloat("VerticalSpeed", -velocity.y*GravityManager.Instance.direction.y);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Crouching", isCrouching);

        targetVelocity = isCrouching?move*crouchWalkSpeed:move * runSpeed;       
    }

    public void DelayDead(float delay)
    {
        Invoke("Dead", delay);
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    public void ExeDead()
    {
        Debug.Log("死了 死了 死了 LoadScene(this)");
        OnPlayerDead.Invoke();
    }

    public void BeginTransition()
    {
        isTransition = true;
    }

    public void EndTransition()
    {
        isTransition = false;
    }

    protected override void LateUpdate() {
        _State _state = new _State() {
            isFacingRight = isFacingRight,
        };
        currentInverseFrames += () => {
            isFacingRight = _state.isFacingRight;
        };
        base.LateUpdate();
    }

}
