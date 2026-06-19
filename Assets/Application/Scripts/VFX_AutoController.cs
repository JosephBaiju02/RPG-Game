using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] bool autoDestroy = true;
    [SerializeField] float destroyDelay = 1;
    [Space]
    [SerializeField] bool randomOffset = true;
    [SerializeField] bool randomRotation = true;

    [Header("Random Positions")]
    [SerializeField] private float xMinOffset = -.3f;
    [SerializeField] private float xMaxOffset = .3f;
    [Space]
    [SerializeField] private float yMinOffset = -.3f;
    [SerializeField] private float yMaxOffset = .3f;

    private void Start()
    {
        ApplyRandomOffset();
        ApplyRandomRotation();
        if (autoDestroy)
            Destroy(gameObject,destroyDelay);
    }

    private void ApplyRandomOffset()
    {
        if (randomOffset == false)
            return;

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset,yMaxOffset);

        transform.position = transform.position + new Vector3(xOffset,yOffset);
    }

    private void ApplyRandomRotation()
    {
        if (randomRotation == false)
            return;

        float zRotation = Random.Range(0, 360);
        transform.Rotate(0,0,zRotation);
    }
}
