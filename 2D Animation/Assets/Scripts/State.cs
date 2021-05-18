using System;

public abstract class State
{
    public StateMachine StateMachine { get; set; }
    public CharacterBehaviour Referer { get; set; }

    public State(StateMachine stateMachine, CharacterBehaviour referer)
    {
        this.StateMachine = stateMachine;
        this.Referer = referer;
    }

    public virtual void Enter()
    {

    }

    public virtual void LogicUpdate(float delta)
    {

    }

    public virtual void PhysicUpdate(float delta)
    {

    }

    public virtual void Exit()
    {

    }
}