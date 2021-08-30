using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script that allows the player to change how fast their characters are eating
public class HungerManager : MonoBehaviour
{

    //three options for feeding
    static Button tiny, meager, full;

    void Start(){
        //initiating each button
        tiny = GameObject.Find("Tiny").GetComponent<Button>();
        meager = GameObject.Find("Meager").GetComponent<Button>();
        full = GameObject.Find("Full").GetComponent<Button>();
        //full.Select();
    }

    //the following three methods just change the game's overall "eating rate" of each player
    public void changeToTiny(){
        GameLogic.EatRate = 1;
        Debug.Log("Changing eating!");
    }

    public void changeToMeager(){
        GameLogic.EatRate = 2;
        Debug.Log("Changing eating!");
    }

    public void changeToFull(){
        GameLogic.EatRate = 3;
        Debug.Log("Changing eating!");
    }

    //way to keep the buttons pressed down even if you click off of them
    public static void loadButtons(){
        if(GameLogic.EatRate == 1){
            tiny.Select();
        }
        else if(GameLogic.EatRate == 2){
            meager.Select();
        }
        else{
            full.Select();
        }
    }
}
