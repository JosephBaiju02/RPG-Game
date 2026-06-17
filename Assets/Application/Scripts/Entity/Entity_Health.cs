using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamagable
{
    Entity entity;
    private Entity_VFX entity_VFX;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;


    [Header("On Damage Knock Back")]
    [SerializeField] private Vector2 knockBackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockBackPower = new Vector2(7f, 7f);
    [SerializeField] private float knockBackDuration = .2f;
    [SerializeField] private float HeavyKnockBackDuration = .5f;

    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = .3f;//percentage of health you should lose to consdider damage as heavy


    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entity_VFX = GetComponent<Entity_VFX>();
        currentHp = maxHp;
    }
    public virtual void TakeDamage(float damage,Transform damageDealer)
    {
        Debug.Log("Take Damage Called");
        if (isDead)
            return;
        Vector2 knockBack = CalculateKnockBack(damage,damageDealer);
        float duration = CalculateDuration(damage);
        entity?.ReciveKnockBack(knockBack,duration);
        entity_VFX?.PlayOnDamageVfx();
        ReduceHp(damage);
    }


    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        isDead=true;
        entity.EntityDeath();
        Debug.Log("Entity Died");
    }


    private Vector2 CalculateKnockBack(float damage,Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockBack = IsHeavyDamage(damage)? heavyKnockBackPower: knockBackPower;
        knockBack.x *= direction;

        return knockBack;
    }
    private float CalculateDuration(float damage) => IsHeavyDamage(damage)? HeavyKnockBackDuration:knockBackDuration;
    private bool IsHeavyDamage(float damage) => damage / maxHp > heavyDamageThreshold;
    
}
