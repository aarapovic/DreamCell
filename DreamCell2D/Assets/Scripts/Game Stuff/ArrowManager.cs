using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : MonoBehaviour
{
    public Slider arrowSlider;
    public Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        arrowSlider.maxValue = playerInventory.maxArrows;
        arrowSlider.value = playerInventory.maxArrows;
        playerInventory.currentArrows = playerInventory.maxArrows;
    }
    public void AddArrows()
    {

        arrowSlider.value = playerInventory.currentArrows;
        if (arrowSlider.value>=arrowSlider.maxValue)
        {
            arrowSlider.value = arrowSlider.maxValue;
            playerInventory.currentArrows = playerInventory.maxArrows;
        }
    }

    public void DecreaseArrows()
    {
        arrowSlider.value = playerInventory.currentArrows;
        if (arrowSlider.value<0)
        {
            arrowSlider.value = 0;
            playerInventory.currentArrows = 0;
        }
    }
    
}
