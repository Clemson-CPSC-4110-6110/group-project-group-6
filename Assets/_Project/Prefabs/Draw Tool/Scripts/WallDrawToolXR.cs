using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Unity.VisualScripting;
using UnityEngine.UI;

public class WallDrawToolXR : MonoBehaviour
{
    private XRGrabInteractableFixed grabInteractableFixed;

    public GameObject drawPosition;
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;


    List<Vector3> linePoints;
    private float timer;
    public float timerDelay;

    private GameObject newLine;
    private LineRenderer drawLine;
    public float lineWidth;
    
    private bool isTouchingSurface = false;
    
    private void Awake()
    {
        grabInteractableFixed = GetComponent<XRGrabInteractableFixed>();

        linePoints = new List<Vector3>();
        timer = timerDelay;
    }

    private void Start()
    {
        redSlider.value = 0.5f;
        greenSlider.value = 0.5f;
        blueSlider.value = 0.5f;

        grabInteractableFixed.selectExited.AddListener(Dropped);
    }

    private void OnTriggerEnter(Collider other)
    {
        isTouchingSurface = true;
        newLine = new GameObject();
        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.material = new Material(Shader.Find("Sprites/Default"));
        drawLine.startWidth = lineWidth;
        drawLine.endWidth = lineWidth;

        drawLine.startColor = new Color(redSlider.value,greenSlider.value,blueSlider.value);
        drawLine.endColor = new Color(redSlider.value,greenSlider.value,blueSlider.value);
    }

    private void OnTriggerExit(Collider other)
    {
        isTouchingSurface = false;

        linePoints.Clear();

        Debug.Log("Trigger Released");
    }

    private void Dropped(SelectExitEventArgs args)
    {
        isTouchingSurface = false;

        linePoints.Clear();

        Debug.Log("Object Dropped");
    }

    public void Update()
    {
        if (isTouchingSurface)
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