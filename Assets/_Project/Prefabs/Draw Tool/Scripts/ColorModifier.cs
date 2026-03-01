using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ColorModifier : MonoBehaviour
{
    public Material currentColor;
    public XRSocketInteractor colorSocket;

    private GameObject inSocket = null;

    public void Update()
    {
        IXRSelectInteractable socketedInteractable = colorSocket.GetOldestInteractableSelected();

        if (socketedInteractable != null && colorSocket.hasSelection && colorSocket.gameObject.activeInHierarchy)
        {
            inSocket = socketedInteractable.transform.gameObject;
            if (inSocket.name == "Red")
            {
                currentColor.color = Color.red;
            }
            else if (inSocket.name == "Green")
            {
                currentColor.color = Color.green;
            }
            else if (inSocket.name == "Blue")
            {
                currentColor.color = Color.blue;
        }
        }
        else
        {
            currentColor.color = Color.white;
        
        }
    }
}