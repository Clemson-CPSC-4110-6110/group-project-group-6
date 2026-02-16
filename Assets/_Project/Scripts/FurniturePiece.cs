using UnityEngine;

public class FurniturePiece : MonoBehaviour
{
    [Header("Materials")]
    public Material normalMaterial;
    public Material previewMaterial;
    public Material invalidMaterial;

    [Header("Placement Settings")]
    public bool isPlaced = false;
    public bool snapToFloor = true;

    private Renderer furnitureRenderer;
    private Collider furnitureCollider;
    private bool isValidPlacement = true;
    private int overlapCount = 0;

    void Start()
    {
        furnitureRenderer = GetComponent<Renderer>();
        furnitureCollider = GetComponent<Collider>();

        // Start in preview mode
        SetPreviewMode(true);
    }

    public void SetPreviewMode(bool preview)
    {
        if (preview)
        {
            furnitureRenderer.material = previewMaterial;
            furnitureCollider.isTrigger = true;
        }
        else
        {
            furnitureRenderer.material = normalMaterial;
            furnitureCollider.isTrigger = false;
            isPlaced = true;
        }
    }

    public void UpdatePlacementValidity(bool valid)
    {
        isValidPlacement = valid;

        if (!isPlaced)
        {
            furnitureRenderer.material = valid ? previewMaterial : invalidMaterial;
        }
    }

    public bool CanBePlaced()
    {
        return isValidPlacement;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check for overlaps with other furniture or walls
        if ((other.CompareTag("Furniture") || other.CompareTag("Wall")) && !isPlaced)
        {
            overlapCount++;
            UpdatePlacementValidity(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Furniture") || other.CompareTag("Wall")) && !isPlaced)
        {
            overlapCount--;
            if (overlapCount <= 0)
            {
                overlapCount = 0;
                UpdatePlacementValidity(true);
            }
        }
    }
}
