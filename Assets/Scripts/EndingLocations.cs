using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script to display the ending art at the end of the game
public class EndingLocations : MonoBehaviour
{
    //each game object contains the background and text blurb of each ending
    [SerializeField] GameObject end1;
    [SerializeField] GameObject end2;
    [SerializeField] GameObject end3;
    [SerializeField] GameObject end4;
    [SerializeField] GameObject end5;
    
    void Start()
    {
        //creates a number 1-5
        int prob = Random.Range(0,5);
        
        //judges a number 1-5, and picks an ending based on this
        //we didn't have time to implement any real logic for the endings so i left it as a 20% chance for each
        if(prob == 0){
            end1.SetActive(true);
            end2.SetActive(false);
            end3.SetActive(false);
            end4.SetActive(false);
            end5.SetActive(false);
        }
        else if(prob == 1){
            end1.SetActive(false);
            end2.SetActive(true);
            end3.SetActive(false);
            end4.SetActive(false);
            end5.SetActive(false);
        }
        else if(prob == 2){
            end1.SetActive(false);
            end2.SetActive(false);
            end3.SetActive(true);
            end4.SetActive(false);
            end5.SetActive(false);
        }
        else if(prob == 3){
            end1.SetActive(false);
            end2.SetActive(false);
            end3.SetActive(false);
            end4.SetActive(true);
            end5.SetActive(false);
        }
        else if(prob == 4){
            end1.SetActive(false);
            end2.SetActive(false);
            end3.SetActive(false);
            end4.SetActive(false);
            end5.SetActive(true);
        }
    } 

    //loads up the main menu again
    public void LoadMenu(){
        Initiate.Fade("Menu", Color.black, 2f);
    }
}
