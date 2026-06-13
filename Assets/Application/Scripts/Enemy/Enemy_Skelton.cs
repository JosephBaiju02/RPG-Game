using UnityEngine;

public class Enemy_Skelton : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this,stateMachine,"attack");
        battleState = new Enemy_BattleState(this,stateMachine, "battle");
        deathState = new Enemy_DeathState(this,stateMachine,"idle");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Intialize(idleState);
    }
}
