using UnityEngine;
using System.Collections;

public class Entity_VFX : MonoBehaviour
{
    SpriteRenderer sr;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] float onDamageVfxDuration = 0.2f;

    [Header("On Doing Damage")]
    [SerializeField] private GameObject hitVfx;
    [SerializeField] Color hitVfxColor = Color.white;

    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;
    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }
    public void CreateOnHitVFX(Transform target)
    {
      GameObject vfx=  Instantiate(hitVfx,target.position,Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
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
