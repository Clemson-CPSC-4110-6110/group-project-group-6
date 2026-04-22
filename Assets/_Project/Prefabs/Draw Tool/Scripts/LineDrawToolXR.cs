using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Unity.VisualScripting;

public class LineDrawToolXR : MonoBehaviour
{
    public XRSocketInteractor colorSocket;
    public GameObject drawPosition;
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
        newLine.layer = LayerMask.NameToLayer("Lines");
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
        else if (inSocket.name == "Orange")
        {
            drawLine.startColor = Color.orange;
            drawLine.endColor = Color.orange;
        }
        else if (inSocket.name == "Yellow")
        {
            drawLine.startColor = Color.yellow;
            drawLine.endColor = Color.yellow;
        }
        else if (inSocket.name == "Green")
        {
            drawLine.startColor = Color.green;
            drawLine.endColor = Color.green;
        }
        else if (inSocket.name == "Teal")
        {
            drawLine.startColor = Color.teal;
            drawLine.endColor = Color.teal;
        }
        else if (inSocket.name == "Blue")
        {
            drawLine.startColor = Color.blue;
            drawLine.endColor = Color.blue;
        }
        else if (inSocket.name == "Purple")
        {
            drawLine.startColor = Color.purple;
            drawLine.endColor = Color.purple;
        }
        else if (inSocket.name == "Pink")
        {
            drawLine.startColor = Color.pink;
            drawLine.endColor = Color.pink;
        }
        else if (inSocket.name == "Brown")
        {
            drawLine.startColor = Color.saddleBrown;
            drawLine.endColor = Color.saddleBrown;
        }
        else if (inSocket.name == "Black")
        {
            drawLine.startColor = Color.black;
            drawLine.endColor = Color.black;
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
        GameObject position = drawPosition;
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