using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawling : CharacterState
{
    public bool stopCrouching = false;
    public Crawling(StateMachine stateMachine, object referer) : base(stateMachine, referer)
    {
    }
    public override void Enter()
    {
        behaviour.animator.SetBool("IsCrouching", true);
        behaviour.crouchDisableCollider.enabled = false;
    }

    public override void Exit()
    {
        behaviour.animator.SetBool("IsCrouching", false);
        behaviour.crouchDisableCollider.enabled = true;
        stopCrouching = false;
    }

    public override void LogicUpdate(float delta)
    {
        if (Input.GetButtonUp("Crouch"))
        {
            stopCrouching = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            stopCrouching = false;
        }
    }

    public override void PhysicUpdate(float delta)
    {
        if (stopCrouching && !Physics2D.OverlapCircle(behaviour.ceilingCheck.position, CharacterBehaviour.ceilingRadius, behaviour.whatIsGround))
        {
            StateMachine.ChangeState(behaviour.running);
            return;
        }

        Move(behaviour.crouchSpeed * delta);
    }
}
