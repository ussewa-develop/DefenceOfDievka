using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] Sprite _activeCell;
    [SerializeField] Sprite _inactiveCell;
    [SerializeField] Image imageInCell;

    public void SetActiveCell(bool value)
    {
        if(value)
        {
            gameObject.GetComponent<Image>().sprite = _activeCell;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = _inactiveCell;
        }
    }

    public void SetImageInCell(Sprite sprite)
    {
        imageInCell.sprite = sprite;
    }
    
}
