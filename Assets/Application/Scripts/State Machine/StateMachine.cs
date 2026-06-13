using UnityEngine;

public class StateMachine
{

    //This variable is encapsuled and protected : it can be accessed publically but only can be set from this script
    public EntityState currentState { get; private set; }
    public bool canEnterNewState=true;

    public void Intialize(EntityState startState)
    {
        currentState = startState ;
        currentState.Enter();
    }
    public void ChangeState(EntityState newState)
    {
        if (canEnterNewState == false)
            return;
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }

    public void SwitchOffStateMachine() => canEnterNewState = false;
}
