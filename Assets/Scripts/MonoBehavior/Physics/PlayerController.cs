using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float maxSpeed = 7f;
    public float jumpSpeed = 7f;

    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            ///一般跳跃
            velocity += jumpSpeed* (-GravityManager.Instance.direction);
            //velocity += -GravityManager.gravity * jumpSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            ///一般引力缩减速度
            if (velocity.y > .0f)
                velocity.y = velocity.y * .5f;
            //Vector2 gravityVelocity = -Vector2.Dot(velocity, GravityManager.gravity)*GravityManager.gravity;
            //if (gravityVelocity.magnitude > 0.0f)
            //    velocity -= 0.5f * gravityVelocity;
        }

        //bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : move.x < 0.01f);
        //if (flipSprite)
        //{
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //}

        if ((isFacingRight && move.x< -0.01f)||(!isFacingRight && move.x>0.01f))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            isFacingRight = !isFacingRight;
        }

        targetVelocity = move * maxSpeed;

    }
}
