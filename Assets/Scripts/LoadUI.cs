using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

//script that helps the main UI out when at a stop (e.g., the map button)
public class LoadUI : MonoBehaviour
{   
    //map object to be displayed from game
    [SerializeField] GameObject map;
    //temporary backpack, as inventory is half functional as this comment is being written
    TextMeshProUGUI backpack;
    void Start(){
        //map = GameObject.Find("Map");
        if (GameObject.Find("BackpackInfo") != null)
        {
            backpack = GameObject.Find("BackpackInfo").GetComponent<TextMeshProUGUI>();
            backpack.text = "Fuel: " + GameLogic.Fuel + "\t Food: " + GameLogic.Food + "\n Medicine: " + GameLogic.Medicine;
        }
        
    }

    //Displays the map to the player.
    public void LoadMap(){
        map.SetActive(true);
        Debug.Log("showing map");
    }

    //Hides the map from the player.
    public void CloseMap(){
        map.SetActive(false);
        Debug.Log("hiding map");
    }

    //loads the inventory
    public void LoadInventory(){
        GameLogic.CurrentStop = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(35);
    }
    
    private void ShowGeigerCounterError()
    {
        if (GameObject.Find("GeigerCounterErrorMessage") == null)
            return;
        GameObject GeigerCounterErrorMessage = GameObject.Find("GeigerCounterErrorMessage");
        GeigerCounterErrorMessage.transform.SetAsLastSibling();
        GeigerCounterErrorMessage.GetComponent<CanvasGroup>().alpha = 1;
        GeigerCounterErrorMessage.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //returns to previous stop after exiting inventory
    public void CloseInventory(){
        foreach (UIScrollButton toggle in FindObjectsOfType<UIScrollButton>())
        {
            if (!toggle.GetComponent<Toggle>().isOn)
                if (toggle.name.Equals("GeigerCounterToggle"))
                {
                    ShowGeigerCounterError();
                    return;
                }
            else
                Destroy(toggle.gameObject); //destroys all toggles that are unchecked when exiting inventory screen, except geiger counter, which cannot be lost
        }
        foreach (DragDrop item in FindObjectsOfType<DragDrop>())
        {
            if (!item.dropped)
            {
                if(item.name.Equals("GeigerCounterItem"))
                {
                    ShowGeigerCounterError();
                    return;
                }
                ScavengeSuppliesScript.RemoveButtonFromScrollList(item.name.Substring(0, item.name.IndexOf("Item")));
                Destroy(item.gameObject); //destroys all items that are not dropped when exiting
            }
        }
        SceneManager.LoadScene(GameLogic.CurrentStop);
    }

    //loads the food manager items
    public void LoadFoodManager(){
        GameLogic.ChangingFood = true;
        HungerManager.loadButtons();
    }
}
