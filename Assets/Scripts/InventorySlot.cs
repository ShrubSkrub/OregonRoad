using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject inventorySlotParent, occupiedBy;
    private bool occupied, offsetX, offsetY;
    [SerializeField] private bool isLastSlotInRow, isLastSlotInColumn;
    public GameObject OccupiedBy { get => occupiedBy; set => occupiedBy = value; }
    public bool Occupied { get => occupied; set => occupied = value; }

    private void Awake()
    {
        inventorySlotParent = this.transform.parent.gameObject;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject slotToDropOn = eventData.pointerDrag.GetComponent<DragDrop>().SlotsTouching.OrderBy(go => go.name).ToList()[0];
        Vector2 oldPos = eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition;
        Vector2 newPos = slotToDropOn.GetComponent<RectTransform>().anchoredPosition + inventorySlotParent.GetComponent<RectTransform>().anchoredPosition + Offset(eventData.pointerDrag);
        if (eventData.pointerDrag != null && CanBeDropped(slotToDropOn))
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = newPos;
            eventData.pointerDrag.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<DragDrop>().droppedImage;
            eventData.pointerDrag.GetComponent<DragDrop>().dropped = true;
            StartCoroutine(OccupySlot(eventData.pointerDrag, oldPos));
        }
    }

    IEnumerator OccupySlot(GameObject item, Vector2 pos)
    {
        foreach (GameObject obj in item.GetComponent<DragDrop>().SlotsTouching)
        {
            if (obj.name.Equals("Trash")) //if the item is over the trash button
            {
                if (item.name.Equals("GeigerCounterItem"))
                {
                    item.GetComponent<RectTransform>().anchoredPosition = pos; //returns back to position before it was dropped
                    CantDrop(item);
                    Debug.Log("Cannot trash Geiger Counter! You'll need that to survive!");
                    yield break;
                }
                else
                {
                    ScavengeSuppliesScript.RemoveButtonFromScrollList(item.name.Substring(0, item.name.IndexOf("Item")));
                    Destroy(item);
                    yield break;
                }
            }
            else if (obj.GetComponent<InventorySlot>().occupied)
            {
                item.GetComponent<RectTransform>().anchoredPosition = pos; //returns back to position before it was dropped
                CantDrop(item);
                yield break;
            }
            else if(item.GetComponent<RectTransform>().anchoredPosition.x + item.GetComponent<RectTransform>().rect.width/2 > -145.5 + 168) //if the item will be placed to the right of the slots, -145.5 is the anchored x position of the slots, 165 is their width
            {
                item.GetComponent<RectTransform>().anchoredPosition = pos;
                CantDrop(item);
                yield break;
            }
            else if (item.GetComponent<RectTransform>().anchoredPosition.y - item.GetComponent<RectTransform>().rect.height / 2 < 83.5 - 168) //if the item will be placed below the slots, 83.5 is the anchored y position of the slots, 165 is their width
            {
                item.GetComponent<RectTransform>().anchoredPosition = pos;
                CantDrop(item);
                yield break;
            }
            obj.GetComponent<InventorySlot>().occupied = true;
            obj.GetComponent<InventorySlot>().occupiedBy = item;
        }
    }

    private void CantDrop(GameObject item)
    {
        item.GetComponent<DragDrop>().dropped = false;
        Debug.Log("ITEM CANNOT BE PLACED HERE");
        item.GetComponent<Image>().sprite = item.GetComponent<DragDrop>().draggingImage;
    }

    private bool CanBeDropped(GameObject slot)
    {
        if (slot.GetComponent<InventorySlot>().isLastSlotInRow && offsetX)
            return false;
        if (slot.GetComponent<InventorySlot>().isLastSlotInColumn && offsetY)
            return false;
        if (occupied)
            return false;
        return true;
    }

    private Vector2 Offset(GameObject obj)
    {
        Vector2 offsetBy = new Vector2(0, 0);
        float x_scale = obj.GetComponent<RectTransform>().localScale.x, y_scale = obj.GetComponent<RectTransform>().localScale.y;
        float oddWidthCorrection = (obj.GetComponent<RectTransform>().rect.width * x_scale) % 2 == 0 ? 0 : .5f; //items with an odd size need to be positioned on a half pixel to prevent distortion
        float oddHeightCorrection = (obj.GetComponent<RectTransform>().rect.height * y_scale) % 2 == 0 ? 0 : .5f;

        if (obj.GetComponent<RectTransform>().rect.width * x_scale > 25)
        {
            offsetX = true;
            offsetBy.x += (14.5f - oddWidthCorrection) * Mathf.Floor(obj.GetComponent<RectTransform>().rect.width * x_scale / 28f); //multiplies 14.5 (half the size of the slot (25 pixels) + spacing (3 pixels)) by the factor it must be offset by
        }

        if (obj.GetComponent<RectTransform>().rect.height * y_scale > 25)
        {
            offsetY = true;
            offsetBy.y -= (14.5f - oddHeightCorrection) * Mathf.Floor(obj.GetComponent<RectTransform>().rect.height * y_scale / 28f); //multiplies 14.5 (half the size of the slot (25 pixels) + spacing (3 pixels)) by the factor it must be offset by
        }

        return offsetBy;
    }
}
