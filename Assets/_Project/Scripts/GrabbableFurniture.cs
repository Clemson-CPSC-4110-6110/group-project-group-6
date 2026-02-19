using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class GrabbableFurniture : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    [Header("Physics Settings")]
    public float mass = 10f;
    public float drag = 5f;
    public float angularDrag = 5f;

    void Awake()
    {
        // Set up Rigidbody
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.linearDamping = drag;
        rb.angularDamping = angularDrag;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Set up XR Grab Interactable
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Configure grab settings
        grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        grabInteractable.throwOnDetach = false;
        grabInteractable.smoothPosition = true;
        grabInteractable.smoothRotation = true;
        grabInteractable.tightenPosition = 0.5f;
        grabInteractable.throwSmoothingDuration = 0f;
    }

    void Start()
    {
        // Freeze rotation to keep furniture upright
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    public void OnHoverEnter()
    {
        // You can add a highlight effect here if you want
        Debug.Log("Hovering over: " + gameObject.name);
    }

    public void OnHoverExit()
    {
        Debug.Log("No longer hovering");
    }
}