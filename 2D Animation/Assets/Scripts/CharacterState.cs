using UnityEngine;

public abstract class CharacterState : State
{
    public CharacterBehaviour behaviour;
    public CharacterState(StateMachine stateMachine, object referer) : base(stateMachine, referer)
    {
        behaviour = (CharacterBehaviour)referer;
    }

    public void Move(float movement)
    {
        Rigidbody2D body = behaviour.body;
        Vector3 targetVelocity = new Vector2(behaviour.horizontalMove * movement * 10f, body.velocity.y);
        Vector3 v = body.velocity;
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref v, behaviour.movementSmoothing);
    }
}
