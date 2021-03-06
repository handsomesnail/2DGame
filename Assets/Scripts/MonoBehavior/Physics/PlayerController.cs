using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0)
            isCrouching = true;
        else
            isCrouching = false;
        
        if (Input.GetButtonDown("Jump") && grounded && !isCrouching)
        {
            ///一般跳跃
            velocity += -jumpSpeed*GravityManager.Instance.direction;
            //velocity += -GravityManager.gravity * jumpSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
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
        animator.SetFloat("VerticalSpeed", velocity.y);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Crouching", isCrouching);

        targetVelocity = isCrouching?move*crouchWalkSpeed:move * runSpeed;       
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
