using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Unity.VisualScripting;

public class LineDrawToolXR : MonoBehaviour
{
    public XRSocketInteractor colorSocket;
    private GameObject inSocket = null;
    private XRGrabInteractableFixed grabInteractableFixed;

    List<Vector3> linePoints;
    private float timer;
    public float timerDelay;

    private GameObject newLine;
    private LineRenderer drawLine;
    public float lineWidth;
    
    private bool isTriggerHeld = false;
    
    private void Awake()
    {
        grabInteractableFixed = GetComponent<XRGrabInteractableFixed>();

        linePoints = new List<Vector3>();
        timer = timerDelay;
    }

    private void Start()
    {
        grabInteractableFixed.activated.AddListener(Draw);
        grabInteractableFixed.deactivated.AddListener(Release);
        grabInteractableFixed.selectExited.AddListener(Dropped);
    }

    private void Draw(ActivateEventArgs args)
    {
        IXRSelectInteractable socketedInteractable = colorSocket.GetOldestInteractableSelected();
        
        isTriggerHeld = true;
        newLine = new GameObject();
        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.material = new Material(Shader.Find("Sprites/Default"));
        drawLine.startWidth = lineWidth;
        drawLine.endWidth = lineWidth;

        inSocket = socketedInteractable.transform.gameObject;

        if (inSocket.name == "Red")
        {
            drawLine.startColor = Color.red;
            drawLine.endColor = Color.red;
        }
        else if (inSocket.name == "Green")
        {
            drawLine.startColor = Color.green;
            drawLine.endColor = Color.green;
        }
        else if (inSocket.name == "Blue")
        {
            drawLine.startColor = Color.blue;
            drawLine.endColor = Color.blue;
        }
        else
        {
            drawLine.startColor = Color.white;
            drawLine.endColor = Color.white;
        }

        Debug.Log("Trigger Pressed");
    }

    private void Release(DeactivateEventArgs args)
    {
        isTriggerHeld = false;

        linePoints.Clear();

        Debug.Log("Trigger Released");
    }

    private void Dropped(SelectExitEventArgs args)
    {
        isTriggerHeld = false;

        linePoints.Clear();

        Debug.Log("Object Dropped");
    }

    public void Update()
    {
        if (isTriggerHeld)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                linePoints.Add(GetDrawPosition());
                drawLine.positionCount = linePoints.Count;
                drawLine.SetPositions(linePoints.ToArray());

                timer = timerDelay;
            }
            Debug.Log("Trigger is being held down");
        }

        
    }

    private void OnDestroy()
    {
        grabInteractableFixed.activated.RemoveListener(Draw);
        grabInteractableFixed.deactivated.RemoveListener(Release);
    }

    private Vector3 GetDrawPosition()
    {
        GameObject position = GameObject.Find("DrawPosition");
        if (position != null)
        {
            return position.transform.position;
        }
        else
        {
            Debug.LogError("DrawPosition GameObject not found!");
            return Vector3.zero;
        }
    }
}