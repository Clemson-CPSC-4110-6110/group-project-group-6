using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LineErase : MonoBehaviour
{
    public XRGrabInteractableFixed grabInteractableFixed;
    public GameObject eraserHead;
    public Material eraserColor;

    private void Start()
    {
        grabInteractableFixed.activated.AddListener(Erase);
        grabInteractableFixed.deactivated.AddListener(Release);
        grabInteractableFixed.selectExited.AddListener(Dropped);
    }

    private void Erase(ActivateEventArgs args)
    {
        eraserColor.color = Color.black;
        eraserHead.layer = LayerMask.NameToLayer("Lines");
    }

    private void Release(DeactivateEventArgs args)
    {
        eraserColor.color = Color.white;
        eraserHead.layer = LayerMask.NameToLayer("None");
    }

    private void Dropped(SelectExitEventArgs args)
    {
        eraserColor.color = Color.white;
        eraserHead.layer = LayerMask.NameToLayer("None");
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
