using System;
using UnityEngine;

public class Running : State
{
    public Running(StateMachine stateMachine, CharacterBehaviour characterController) : base(stateMachine, characterController)
    {
    }

    public override void LogicUpdate(float delta)
    {
        base.LogicUpdate(delta);

        if (Input.GetButtonDown("Jump"))
        {
            StateMachine.ChangeState(Referer.jumping);
        }

        else if (Input.GetButtonDown("Crouch"))
        {
            StateMachine.ChangeState(Referer.crouching);
        }
    }

    public override void PhysicUpdate(float delta)
    {
        base.PhysicUpdate(delta);
        Rigidbody2D body = Referer.body;
        Vector3 targetVelocity = new Vector2(Referer.horizontalMove * Referer.runSpeed * delta * 10f, body.velocity.y);
        Vector3 v = body.velocity;
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref v, Referer.movementSmoothing);
    }
}