using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    Entity entity;

    private Entity_Combat entity_Combat;

    protected virtual void Awake()
    {
       entity= GetComponentInParent<Entity>();
        entity_Combat = GetComponentInParent<Entity_Combat>();
    }
    public void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }

    private void AttackTrigger()
    {
        entity_Combat.PerformAttack();
    }
}
