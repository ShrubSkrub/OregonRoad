using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollButton : MonoBehaviour
{
    [SerializeField] GameObject foodTemplate, medicineTemplate, fuelTemplate, geigerCounterTemplate;
    
    public void ToggleItems()
    {
        if (this.name.Equals("FuelToggle"))
            ToggleFuel();
        else if (this.name.Equals("MedicineToggle"))
            ToggleMedicine();
        else if (this.name.Equals("FoodToggle"))
            ToggleFood();
        else if (this.name.Equals("GeigerCounterToggle"))
            ToggleGeigerCounter();
    }

    private void ToggleFuel()
    {
        if(GetComponent<Toggle>().isOn)
        {
            GameObject item = Instantiate(fuelTemplate) as GameObject;
            item.transform.SetParent(fuelTemplate.transform.parent, false);
            item.name = "FuelItem";
            item.SetActive(true);
            GameLogic.Fuel += GetComponent<Item>().currentAmount;
        }
        else if (GameObject.Find("FuelItem")) {
            GameLogic.Fuel -= GetComponent<Item>().currentAmount;
            Destroy(GameObject.Find("FuelItem"));
        }
    }
    private void ToggleMedicine()
    {
        if (GetComponent<Toggle>().isOn)
        {
            GameObject item = Instantiate(medicineTemplate) as GameObject;
            item.transform.SetParent(medicineTemplate.transform.parent, false);
            item.name = "MedicineItem";
            item.SetActive(true);
            GameLogic.Medicine += GetComponent<Item>().currentAmount;
        }
        else if (GameObject.Find("MedicineItem"))
        {
            GameLogic.Medicine -= GetComponent<Item>().currentAmount;
            Destroy(GameObject.Find("MedicineItem"));
        }
    }
    private void ToggleFood()
    {
        if (GetComponent<Toggle>().isOn)
        {
            GameObject item = Instantiate(foodTemplate) as GameObject;
            item.transform.SetParent(foodTemplate.transform.parent, false);
            item.name = "FoodItem";
            item.SetActive(true);
            GameLogic.Food += GetComponent<Item>().currentAmount;
        }
        else if (GameObject.Find("FoodItem"))
        {
            GameLogic.Food -= GetComponent<Item>().currentAmount;
            Destroy(GameObject.Find("FoodItem"));
        }
    }

    private void ToggleGeigerCounter()
    {
        if (GetComponent<Toggle>().isOn)
        {
            GameObject item = Instantiate(geigerCounterTemplate) as GameObject;
            item.transform.SetParent(geigerCounterTemplate.transform.parent, false);
            item.name = "GeigerCounterItem";
            item.SetActive(true);
        }
        else if (GameObject.Find("GeigerCounterItem"))
        {
            Destroy(GameObject.Find("GeigerCounterItem"));
        }
    }
}
