using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    public void Hovering()
    {
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    public void NotHovering()
    {
        GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }
    
}
