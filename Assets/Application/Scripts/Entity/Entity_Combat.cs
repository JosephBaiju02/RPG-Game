using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;

    public float damage = 1;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadious=1f;
    [SerializeField] private LayerMask targetLayerMask;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
    }

    public void PerformAttack()
    {
        GetDetectedColliders();
        foreach (Collider2D collider in GetDetectedColliders())
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable == null)
                continue;

            damagable.TakeDamage(damage, transform);
            vfx.CreateOnHitVFX(collider.transform);

            Debug.Log("Taken Damage" + damagable);
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
