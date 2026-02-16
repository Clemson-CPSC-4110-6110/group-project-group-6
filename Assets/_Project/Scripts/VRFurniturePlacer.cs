using UnityEngine;

using UnityEngine.InputSystem;

public class VRFurniturePlacer : MonoBehaviour
{
    [Header("Controller References")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;
    public LineRenderer lineRenderer;

    [Header("Placement Settings")]
    public LayerMask placementSurface;
    public float maxPlacementDistance = 10f;
    public float rotationSpeed = 90f;
    public float rayHitOffset = 0.01f;

    [Header("Input Actions")]
    public InputActionProperty placeAction;
    public InputActionProperty rotateAction;
    public InputActionProperty cancelAction;

    [Header("Materials")]
    public Material normalMaterial;
    public Material previewMaterial;
    public Material invalidMaterial;

    private FurniturePiece currentFurniture;
    private GameObject previewObject;
    private bool isPlacingMode = false;

    void Start()
    {
        // Validate references
        if (rayInteractor == null)
        {
            Debug.LogError("VRFurniturePlacer: XR Ray Interactor not assigned!");
        }

        if (normalMaterial == null || previewMaterial == null || invalidMaterial == null)
        {
            Debug.LogError("VRFurniturePlacer: Materials not assigned!");
        }
    }

    void Update()
    {
        if (isPlacingMode && currentFurniture != null)
        {
            UpdateFurniturePosition();
            HandleRotation();
            HandlePlacement();
            HandleCancel();
        }
    }

    public void StartPlacingFurniture(GameObject furniturePrefab)
    {
        if (furniturePrefab == null)
        {
            Debug.LogError("Furniture prefab is null!");
            return;
        }

        // Cancel any existing placement
        if (currentFurniture != null)
        {
            Destroy(previewObject);
        }

        // Create preview object
        previewObject = Instantiate(furniturePrefab);
        currentFurniture = previewObject.GetComponent<FurniturePiece>();

        if (currentFurniture == null)
        {
            currentFurniture = previewObject.AddComponent<FurniturePiece>();
        }

        // Set materials
        currentFurniture.normalMaterial = normalMaterial;
        currentFurniture.previewMaterial = previewMaterial;
        currentFurniture.invalidMaterial = invalidMaterial;

        currentFurniture.SetPreviewMode(true);
        isPlacingMode = true;

        Debug.Log("Started placing: " + furniturePrefab.name);
    }

    void UpdateFurniturePosition()
    {
        if (rayInteractor == null) return;

        RaycastHit hit;

        if (Physics.Raycast(
            rayInteractor.transform.position,
            rayInteractor.transform.forward,
            out hit,
            maxPlacementDistance,
            placementSurface))
        {
            Vector3 targetPosition = hit.point + (hit.normal * rayHitOffset);
            previewObject.transform.position = targetPosition;
        }
    }

    void HandleRotation()
    {
        if (!rotateAction.action.enabled) return;

        Vector2 rotateInput = rotateAction.action.ReadValue<Vector2>();

        if (Mathf.Abs(rotateInput.x) > 0.1f)
        {
            float rotation = rotateInput.x * rotationSpeed * Time.deltaTime;
            previewObject.transform.Rotate(Vector3.up, rotation, Space.World);
        }
    }

    void HandlePlacement()
    {
        if (!placeAction.action.enabled) return;

        if (placeAction.action.WasPressedThisFrame())
        {
            if (currentFurniture.CanBePlaced())
            {
                currentFurniture.SetPreviewMode(false);
                currentFurniture = null;
                previewObject = null;
                isPlacingMode = false;

                Debug.Log("Furniture placed successfully!");
            }
            else
            {
                Debug.Log("Cannot place furniture - overlapping with objects!");
            }
        }
    }

    void HandleCancel()
    {
        if (!cancelAction.action.enabled) return;

        if (cancelAction.action.WasPressedThisFrame())
        {
            Destroy(previewObject);
            currentFurniture = null;
            previewObject = null;
            isPlacingMode = false;

            Debug.Log("Placement cancelled");
        }
    }

    public void OnFurnitureSelected(GameObject furniturePrefab)
    {
        StartPlacingFurniture(furniturePrefab);
    }
}