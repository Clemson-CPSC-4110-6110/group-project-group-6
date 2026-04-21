using UnityEngine;
using UnityEngine.UI;

public class ActiveColorModifier : MonoBehaviour
{
    public Material currentColor;
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;

    public void Update()
    {
        currentColor.color = new Color(redSlider.value, greenSlider.value, blueSlider.value);
    }
}