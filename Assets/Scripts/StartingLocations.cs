using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//script that changes what supplies are given to the player when they pick a starting location
public class StartingLocations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] GameObject tooltip;
    bool mouse_over;
    // Update is called once per frame
    void Update()
    {   
        //creates little blurbs under each starting option to explain their weight to the player
        if(mouse_over){
            tooltip.SetActive(true);
        }
        else{
            tooltip.SetActive(false);
        }
    }
    
    //below two methods are just logic methods to calculate if the mouse is over / has left the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }

    //three methods below give different amounts of supplies to the player based on what is chosen
    public void ChooseHospital(){
        GameLogic.Medicine += 4;
        GameLogic.Fuel += 10;
        GameLogic.Food += 50;
    }

    public void ChooseSchool(){
        GameLogic.Medicine += 2;
        GameLogic.Fuel += 20;
        GameLogic.Food = 50;
    }

    public void ChooseMarket(){
        GameLogic.Medicine += 2;
        GameLogic.Fuel += 20;
        GameLogic.Food = 150;
    }
}

