using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScavengeSuppliesScript : MonoBehaviour
{
    public GameObject toggleTemplate;
    [SerializeField] Sprite fuelButton, fuelButtonPressed, medicineButton, medicineButtonPressed, foodButton, foodButtonPressed, geigerCounterButton, geigerCounterButtonPressed;
    [SerializeField] TextMeshProUGUI foodText, fuelText, medicineText;
    private void Awake()
    {
        GiveGeigerCounter();
    }

    public void Scavenge(string type)
    {
        string buttonPath = "ItemScrollBackgroundImage/ItemScrollList/ItemListViewport/ItemListContent/" + type + "Toggle"; //path to item toggle
        int amount;
        if (type == "Fuel")
        {
            amount = GameLogic.searchFuel("city", "light");
            fuelText.SetText("+" + amount + " " + type);
        } 
        else if (type == "Medicine")
        {
            amount = GameLogic.searchMedicine("city");
            medicineText.SetText("+" + amount + " " + type);
        }
        else if (type == "Food")
        {
            amount = GameLogic.searchFood("city");
            foodText.SetText("+" + amount + " " + type);
        }
        else
        {
            Debug.Log("Unknown type of item to scavenge for!");
            return;
        }

        if (GameObject.Find(buttonPath) != null)
        {
            Item item = GameObject.Find(buttonPath).GetComponent<Item>();

            if (item.currentAmount + amount <= item.capacity)
                item.currentAmount += amount;
            else
            {
                GameObject newToggle = AddButtonToScrollList(type);
                newToggle.GetComponent<Item>().currentAmount += amount - (item.capacity - item.currentAmount);
                newToggle.tag = type;
                item.currentAmount = item.capacity;
            }
        }
        else if(amount != 0)
        {
            GameObject newToggle = AddButtonToScrollList(type);
            newToggle.tag = type;
            newToggle.GetComponent<Item>().currentAmount += amount;
        }

        ShowScavengeMessage();
    }

    private void ShowScavengeMessage()
    {
        if (GameObject.Find("ScavengeMessage") == null)
            return;
        GameObject ScavengeMessage = GameObject.Find("ScavengeMessage");
        ScavengeMessage.transform.SetAsLastSibling();

        ScavengeMessage.GetComponent<CanvasGroup>().alpha = 1;
        ScavengeMessage.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void GiveGeigerCounter() //should only run once the whole game when the inventory scene is first loaded, geiger counter should never be lost
    {
        AddButtonToScrollList("GeigerCounter");
    }

    public GameObject AddButtonToScrollList(string type) //adds the respective toggle/button to the item scroll list and returns the toggle it added
    {
        GameObject toggle = Instantiate(toggleTemplate) as GameObject;
        toggle.SetActive(true);
        toggle.transform.SetParent(toggleTemplate.transform.parent, false);
        toggle.name = type + "Toggle";

        if (type.Equals("Fuel"))
        {
            toggle.GetComponentInChildren<Image>().sprite = fuelButton;
            toggle.transform.Find("Background").Find("Checkmark").GetComponent<Image>().sprite = fuelButtonPressed; //currently the only way to change this sprite, ugly
        }
        else if (type.Equals("Medicine"))
        {
            toggle.GetComponentInChildren<Image>().sprite = medicineButton;
            toggle.transform.Find("Background").Find("Checkmark").GetComponent<Image>().sprite = medicineButtonPressed;
        }
        else if (type.Equals("Food"))
        {
            toggle.GetComponentInChildren<Image>().sprite = foodButton;
            toggle.transform.Find("Background").Find("Checkmark").GetComponent<Image>().sprite = foodButtonPressed;
        }
        else if (type.Equals("GeigerCounter"))
        {
            toggle.GetComponentInChildren<Image>().sprite = geigerCounterButton;
            toggle.transform.Find("Background").Find("Checkmark").GetComponent<Image>().sprite = geigerCounterButtonPressed;
        }
        return toggle;
    }

    public static void RemoveButtonFromScrollList(string type)
    {
        string buttonPath = "ItemScrollBackgroundImage/ItemScrollList/ItemListViewport/ItemListContent/" + type + "Toggle";
        if (GameObject.Find(buttonPath) != null)
            Destroy(GameObject.Find(buttonPath));
        else
            Debug.Log("button not found");
    }
}
