using UnityEngine;

public class FurnitureMenuUI : MonoBehaviour
{
    [Header("References")]
    public VRFurniturePlacer furniturePlacer;

    [Header("Furniture Prefabs")]
    public GameObject testA;
    public GameObject testB;

    // These methods will be called by UI buttons
    public void OnTestAClicked()
    {
        if (testA != null && furniturePlacer != null)
        {
            furniturePlacer.OnFurnitureSelected(testA);
        }
    }

    public void OnTestBClicked()
    {
        if (testB != null && furniturePlacer != null)
        {
            furniturePlacer.OnFurnitureSelected(testB);
        }
    }

}