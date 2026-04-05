using System.Diagnostics.Tracing;
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
            else if (inSocket.name == "Orange")
            {
                currentColor.color = Color.orange;
            }
            else if (inSocket.name == "Yellow")
            {
                currentColor.color = Color.yellow;
            }
            else if (inSocket.name == "Green")
            {
                currentColor.color = Color.green;
            }
            else if (inSocket.name == "Teal")
            {
                currentColor.color = Color.teal;
            }
            else if (inSocket.name == "Blue")
            {
                currentColor.color = Color.blue;
            }
            else if (inSocket.name == "Purple")
            {
                currentColor.color = Color.purple;
            }
            else if (inSocket.name == "Pink")
            {
                currentColor.color = Color.pink;
            }
            else if (inSocket.name == "Brown")
            {
                currentColor.color = Color.brown;
            }
            else if (inSocket.name == "Black")
            {
                currentColor.color = Color.black;
            }
        }
        else
        {
            currentColor.color = Color.white;
        
        }
    }
}