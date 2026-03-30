using UnityEngine;
using UnityEngine.UI;

public class RoomColorChanger : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private Renderer[] roomRenderers; 
    private MaterialPropertyBlock _propBlock;

    [Header("Sliders")]
    [SerializeField] private Slider hueSlider;
    [SerializeField] private Slider satSlider;
    [SerializeField] private Slider valSlider;

    [Header("Preview")]
    [SerializeField] private Image colorPreview;

    private void Start()
    {
        _propBlock = new MaterialPropertyBlock();

        // Set default slider values
        hueSlider.value = 0.6f;
        satSlider.value = 0.5f;
        valSlider.value = 0.8f;

        // Register listeners
        hueSlider.onValueChanged.AddListener(_ => UpdateColor());
        satSlider.onValueChanged.AddListener(_ => UpdateColor());
        valSlider.onValueChanged.AddListener(_ => UpdateColor());

        UpdateColor();
    }

    private Color _currentColor;

    private void UpdateColor()
    {
        Color targetColor = Color.HSVToRGB(
            hueSlider.value,
            satSlider.value,
            valSlider.value
        );

        StopAllCoroutines();
        StartCoroutine(LerpColor(_currentColor, targetColor, 0.3f));

        if (colorPreview != null)
            colorPreview.color = targetColor;
    }

    private System.Collections.IEnumerator LerpColor(Color from, Color to, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            Color blended = Color.Lerp(from, to, t);
            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Floor");
            Renderer[] roomRenderersAll = new Renderer[rooms.Length];
            for (int i = 0; i < rooms.Length; i++)            {
                roomRenderersAll[i] = rooms[i].GetComponent<Renderer>();
            }

            foreach (Renderer r in roomRenderers)
            {
                r.GetPropertyBlock(_propBlock);
                _propBlock.SetColor("_BaseColor", blended);
                r.SetPropertyBlock(_propBlock);
            }
            yield return null;
        }
        _currentColor = to;
    }
}