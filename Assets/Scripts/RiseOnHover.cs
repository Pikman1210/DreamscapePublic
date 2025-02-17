using UnityEngine;

public class RiseOnHover : MonoBehaviour
{
    [SerializeField]
    private float transformOffset = 1.15f;

    /*[SerializeField]
    private float transformOffsetDOT = 0.15f;*/

    private float initialTransformX;
    private float initialTransformY;
    private float initialTransformZ;

    // Mouse detection (needs a box collider)
    private void OnMouseEnter()
    {
        IncreaseTransform(true);
    }

    private void OnMouseExit()
    {
        IncreaseTransform(false);
    }

    private void Awake()
    {
        initialTransformX = transform.position.x;
        initialTransformY = transform.position.y;
        initialTransformZ = transform.position.z;
    }

    // Increase transform (y value)
    private void IncreaseTransform(bool status)
    {
        Vector3 finalTransform = new Vector3(initialTransformX, initialTransformY, initialTransformZ);

        // Only if status true
        if (status)
        {
            float offset = initialTransformY * transformOffset;

            finalTransform = new Vector3(transform.position.x, offset, transform.position.z);
        } else
        {
            finalTransform = new Vector3(initialTransformX, initialTransformY, initialTransformZ);
        }
        transform.localPosition = finalTransform;
        // transform.DOScale(finalTransform, transformOffsetDOT);
    }
}