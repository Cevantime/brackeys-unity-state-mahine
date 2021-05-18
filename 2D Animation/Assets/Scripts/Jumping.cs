using System;
using UnityEngine;

class Jumping : State
{
    private bool addForce = false;
    public Jumping(StateMachine stateMachine, CharacterBehaviour characterController) : base(stateMachine, characterController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Referer.animator.SetBool("IsJumping", true);
        addForce = true;
    }

    public override void Exit()
    {
        base.Exit();
        Referer.animator.SetBool("IsJumping", false);
    }

    public override void PhysicUpdate(float delta)
    {
        base.PhysicUpdate(delta);

        Rigidbody2D body = Referer.body;

        if (addForce)
        {
            body.AddForce(new Vector2(0f, Referer.jumpForce));
            addForce = false;
            return;
        }

        if (Referer.airControl)
        {
            Vector3 targetVelocity = new Vector2(Referer.horizontalMove * Referer.runSpeed * delta * 10f, body.velocity.y);
            Vector3 v = body.velocity;
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref v, Referer.movementSmoothing);
        }

        if (body.velocity.y > 0)
        {
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(Referer.groundCheck.position, CharacterBehaviour.groundedRadius, Referer.whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != Referer.gameObject)
            {
                StateMachine.ChangeState(Referer.running);
                break;
            }
        }
    }
}