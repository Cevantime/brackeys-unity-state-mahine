using System;

public class StateMachine
{
    public State currentState;

    public void ChangeState(State state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = state;
        state.Enter();
    }
}