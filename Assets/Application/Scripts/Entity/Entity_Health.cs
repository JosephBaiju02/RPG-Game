using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamagable
{

    private Slider healthBar;
    Entity entity;
    private Entity_VFX entity_VFX;

    private Entity_Stats stats;


    [SerializeField] protected float currentHp;
   
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
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<Entity_Stats>();
        currentHp = stats.GetMaxHealth();
        UpdateSlider();
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

    public void UpdateSlider()
    {
        if (healthBar == null)
            return;
        healthBar.value = currentHp / stats.GetMaxHealth();
    }

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        UpdateSlider();
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
    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHealth() > heavyDamageThreshold;
    
}
 