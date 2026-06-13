using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    public float lastTimeAttacked;

    private bool comboAttackQueued;
    private int attackDir;
    private int comboIndex = 1;
    private int comboLimit=3;
    private const int FirstComboIndex = 1; // we start combo index with 1, this parameter is used in the animator

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length;
    }


    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        // define attack direction according to input 
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;


        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplayAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();



        if (input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
           HandleStateExit();

    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();

        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
    }
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if(attackVelocityTimer<0)
            player.SetVelocity(0,rb.linearVelocity.y);
    }

    private void ApplayAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x*attackDir,attackVelocity.y);
    }
    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;
        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }
}
