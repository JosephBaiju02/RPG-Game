using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats Stats;

    

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadious=1f;
    [SerializeField] private LayerMask targetLayerMask;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        Stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        GetDetectedColliders();
        foreach (Collider2D collider in GetDetectedColliders())
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable == null)
                continue;
           
            float damage = Stats.GetPhyscicalDamage(out bool isCrit);
           bool targetGotHit= damagable.TakeDamage(damage, transform);
            if(targetGotHit)
                vfx.CreateOnHitVFX(collider.transform,isCrit);

            
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position,targetCheckRadious,targetLayerMask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position,targetCheckRadious);
    }
}
