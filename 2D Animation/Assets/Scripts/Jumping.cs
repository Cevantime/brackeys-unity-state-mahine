using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : CharacterState
{
    private bool addForce = false;
    public Jumping(StateMachine stateMachine, object referer) : base(stateMachine, referer)
    {
    }

    public override void Enter()
    {
        addForce = true;
        behaviour.animator.SetBool("IsJumping", true);
    }

    public override void Exit()
    {
        behaviour.animator.SetBool("IsJumping", false);
    }

    public override void PhysicUpdate(float delta)
    {
        Rigidbody2D body = behaviour.body;

        if (addForce)
        {
            body.AddForce(new Vector2(0f, behaviour.jumpForce));
            addForce = false;
            return;
        }

        if (behaviour.airControl)
        {
            Move(behaviour.runSpeed * delta);
        }

        if (body.velocity.y > 0)
        {
            return;
        }

        if (Physics2D.OverlapCircle(behaviour.groundCheck.position, CharacterBehaviour.groundedRadius, behaviour.whatIsGround))
        {
            StateMachine.ChangeState(behaviour.running);
        }
    }
}
