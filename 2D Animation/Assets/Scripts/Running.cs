using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : CharacterState
{
    public Running(StateMachine stateMachine, object referer) : base(stateMachine, referer)
    {
    }

    public override void LogicUpdate(float delta)
    {
        if (Input.GetButtonDown("Jump"))
        {
            StateMachine.ChangeState(behaviour.jumping);
        }

        else if (Input.GetButtonDown("Crouch"))
        {
            StateMachine.ChangeState(behaviour.crawling);
        }
    }
    public override void PhysicUpdate(float delta)
    {
        Move(behaviour.runSpeed * delta);
    }
}
