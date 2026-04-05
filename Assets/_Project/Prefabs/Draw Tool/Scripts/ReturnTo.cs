using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ReturnTo : MonoBehaviour
{
    Vector3 defPosition;
    Quaternion defRotation;
    public XRSocketInteractor isSocketed;
    public XRGrabInteractable isHeld;
    public GameObject originalPosition;

    void Start()
    {
        defPosition = originalPosition.transform.position;
        defRotation = originalPosition.transform.rotation;
    }
    
    void Update()
    {
        if (isHeld.isSelected)
        {
            Debug.Log("Held");
        }
        else
        {
            Debug.Log("Not Held");
            if (isSocketed != null && isSocketed.hasSelection && isSocketed.gameObject.activeInHierarchy)
            {
                Debug.Log("Socketed");
            }
            else
            {
                Debug.Log("Not Socketed");
                transform.position = defPosition;
                transform.rotation = defRotation;
            }
        }
        defPosition = originalPosition.transform.position;
        defRotation = originalPosition.transform.rotation;
    }
}
