using System;
using UnityEngine;

class Crouching : State
{
    public bool stopCrouching = false;
    public Crouching(StateMachine stateMachine, CharacterBehaviour characterController) : base(stateMachine, characterController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Referer.animator.SetBool("IsCrouching", true);
        if (Referer.crouchDisableCollider != null)
            Referer.crouchDisableCollider.enabled = false;
    }

    public override void LogicUpdate(float delta)
    {
        base.LogicUpdate(delta);
        if (Input.GetButtonUp("Crouch"))
        {
            stopCrouching = true;
        }
    }

    public override void PhysicUpdate(float delta)
    {
        base.PhysicUpdate(delta);
        if (stopCrouching && !Physics2D.OverlapCircle(Referer.ceilingCheck.position, CharacterBehaviour.ceilingRadius, Referer.whatIsGround))
        {
            StateMachine.ChangeState(Referer.running);
            return;
        }
        Rigidbody2D body = Referer.body;
        Vector3 targetVelocity = new Vector2(Referer.horizontalMove * Referer.crouchSpeed * delta * 10f, body.velocity.y);
        Vector3 v = body.velocity;
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref v, Referer.movementSmoothing);
    }

    public override void Exit()
    {
        base.Exit();
        stopCrouching = false;
        Referer.animator.SetBool("IsCrouching", false);
        if (Referer.crouchDisableCollider != null)
            Referer.crouchDisableCollider.enabled = true;
    }
}