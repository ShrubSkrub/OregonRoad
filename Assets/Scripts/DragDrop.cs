using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;
    public Sprite droppedImage, draggingImage;
    public bool dropped; //only true when item is anchored on slot
    private RectTransform rectTransform;
    private Vector2 dragDistance;
    private CanvasGroup canvasGroup;
    private List<GameObject> slotsTouching;

    public List<GameObject> SlotsTouching { get => slotsTouching; set => slotsTouching = value; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        slotsTouching = new List<GameObject>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dropped = false;
        GetComponent<Image>().sprite = draggingImage;
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
        RemoveOccupancies();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragDistance += eventData.delta / canvas.scaleFactor; //should only move in whole number increments

        if (dragDistance.x >= 1 || dragDistance.x <= -1)
        {
            rectTransform.anchoredPosition += new Vector2(Mathf.Round(dragDistance.x), 0);
            dragDistance.x -= Mathf.Round(dragDistance.x);
        }
        if (dragDistance.y >= 1 || dragDistance.y <= -1)
        {
            rectTransform.anchoredPosition += new Vector2(0, Mathf.Round(dragDistance.y));
            dragDistance.y -= Mathf.Round(dragDistance.y);
        }
           
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<InventorySlot>() != null)
        {
            slotsTouching.Add(collider.gameObject); //adds the slots that the item is touching to a list so the InventorySlot script can error check all slots it's touching
            if (collider.gameObject.GetComponent<InventorySlot>().OccupiedBy == this.gameObject)
            {
                collider.gameObject.GetComponent<InventorySlot>().OccupiedBy = null; //slots will be occupied later in the InventorySlot script
                collider.gameObject.GetComponent<InventorySlot>().Occupied = false;
            }
        }
    }

    

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<InventorySlot>() != null)
        {
            slotsTouching.Remove(collider.gameObject);
            if (collider.gameObject.GetComponent<InventorySlot>() != null && collider.gameObject.GetComponent<InventorySlot>().OccupiedBy == this.gameObject)
            {
                collider.gameObject.GetComponent<InventorySlot>().OccupiedBy = null;
                collider.gameObject.GetComponent<InventorySlot>().Occupied = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void RemoveOccupancies()
    {
        foreach (GameObject obj in slotsTouching)
        {
            if(obj.GetComponent<InventorySlot>().OccupiedBy == this.gameObject)
            {
                obj.GetComponent<InventorySlot>().OccupiedBy = null;
                obj.GetComponent<InventorySlot>().Occupied = false;
            }

        }
    }
}
