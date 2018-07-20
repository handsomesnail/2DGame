using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : TimeBacker
{
    private struct _State {
        public Vector2 targetVelocity;
        public bool grounded;
        public Vector2 groundNormal;
        public Vector2 velocity;
    }

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;
    
    protected Vector2 targetVelocity;
    protected bool grounded;
    public Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>();


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    protected override void Start()
    {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    protected virtual void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();

        velocity += gravityModifier * GravityManager.Instance.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(-groundNormal.y * GravityManager.Instance.direction.y, -groundNormal.x * GravityManager.Instance.direction.y);
   

        ///一般情况下 平行移动
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        ///一般情况下的重力下落
        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    protected override void LateUpdate() {
        _State _state = new _State() {
            targetVelocity = targetVelocity,
            velocity = velocity,
            grounded = grounded,
            groundNormal = groundNormal,
        };
        currentInverseFrames += () => {
            targetVelocity = _state.targetVelocity;
            velocity = _state.velocity;
            grounded = _state.grounded;
            groundNormal = _state.groundNormal;
        };
        base.LateUpdate();
    }

    protected virtual void ComputeVelocity() { }


    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (Mathf.Abs(currentNormal.y) > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}