using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat Combat;
    private bool counteredSomebody;
    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

        Combat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = Combat.GetCounterRecoveryDuration();

        counteredSomebody = Combat.CounterAttackPerformed();
        anim.SetBool("counterAttackPerformed", counteredSomebody);
    }

    public override void Update()
    {
        base.Update();

       

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

        if (stateTimer < 0 && counteredSomebody==false)
            stateMachine.ChangeState(player.idleState);
    }
}
