using UnityEngine;

public class Entity_Combat : MonoBehaviour
{


    public float damage = 1;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadious=1f;
    [SerializeField] private LayerMask targetLayerMask;



    public void PerformAttack()
    {
        GetDetectedColliders();
        foreach (Collider2D collider in GetDetectedColliders())
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage, transform);


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
