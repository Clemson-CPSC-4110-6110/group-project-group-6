using System.Net.Sockets;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;  

public class XRGrabInteractableFixed : XRGrabInteractable
{
    [SerializeField] private Transform leftHandAttachTransform;
    [SerializeField] private Transform rightHandAttachTransform;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject.handedness == InteractorHandedness.Left)
        {
            attachTransform = leftHandAttachTransform;
        }
        else if (args.interactorObject.handedness == InteractorHandedness.Right)
        {
            attachTransform = rightHandAttachTransform;
        }
        base.OnSelectEntering(args);
    }   
}
