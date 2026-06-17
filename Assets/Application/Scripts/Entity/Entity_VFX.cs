using UnityEngine;
using System.Collections;

public class Entity_VFX : MonoBehaviour
{
    SpriteRenderer sr;

    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] float onDamageVfxDuration = 0.2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;
    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
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
