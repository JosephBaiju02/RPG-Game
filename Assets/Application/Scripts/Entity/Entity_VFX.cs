using UnityEngine;
using System.Collections;

public class Entity_VFX : MonoBehaviour
{
    SpriteRenderer sr;
    private Entity entity;



    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] float onDamageVfxDuration = 0.2f;

    [Header("On Doing Damage")]
    [SerializeField] private GameObject hitVfx;
    [SerializeField] Color hitVfxColor = Color.white;
    [SerializeField] private GameObject critHitVfx;

    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;
    private void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }
    public void CreateOnHitVFX(Transform target,bool isCrit)
    {
        GameObject hitPrefab = isCrit ? critHitVfx : hitVfx;
      GameObject vfx=  Instantiate(hitPrefab,target.position,Quaternion.identity);
        if(isCrit==false)
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;

        if (entity.facingDir == -1 && isCrit)
            vfx.transform.Rotate(0, 180, 0);
    }
    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine);
       onDamageVfxCoroutine= StartCoroutine(OnDamageVfxCo());
    }
    private IEnumerator OnDamageVfxCo()
    {

        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }
}
