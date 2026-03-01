using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketActiveChecker : MonoBehaviour
{
    public LineDrawToolXR LineDrawToolXR;
    public XRSocketInteractor socketed;

    void Update()
    {
        if (socketed != null && socketed.hasSelection && socketed.gameObject.activeInHierarchy)
        {
            LineDrawToolXR.enabled = false;
        }
        else
        {
            LineDrawToolXR.enabled = true;
        }
    }
}
