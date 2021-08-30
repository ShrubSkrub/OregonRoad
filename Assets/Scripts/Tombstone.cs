using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//script that displays tombstone screen to player when a character dies
public class Tombstone : MonoBehaviour
{   
    TextMeshProUGUI deadName;
    static string dead;

    //initializes the deadName, which is what inevitably goes onto the tombstone
    void Start(){
        deadName = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
    }

    //changes the name on the tombstone to whoever is dead
    void Update(){
        deadName.text = dead;
    }
    
    //loads the death screen to the player
    public static void LoadDeath(string playerName){
        dead = playerName;
        GameLogic.CurrentStop = SceneManager.GetActiveScene().buildIndex;
        Initiate.Fade("Tombstone", Color.black, 2f);
    }

    //returns back to the stop after the tombstone is created
    public void returnToStop(){
        SceneManager.LoadScene(GameLogic.CurrentStop);
    }
}
