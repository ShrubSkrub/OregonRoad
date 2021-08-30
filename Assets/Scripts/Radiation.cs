using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script that calculates radiation that stacks on the player.
public class Radiation : MonoBehaviour
{
    //instance variables for the level of radiation, time passed in game, and radiation time passed
    private static string radLevel = "green";
    float timePassed, radTime = 0f;
    //variables below are used for the geiger / roen counter
    [SerializeField] GameObject[] radiationLevels = new GameObject[3];
    [SerializeField] GameObject radMeter;
    GameObject activeCounter;
    bool shake, mouse_over;


    void Start(){
        calculateRadiation();
        Debug.Log("Current radiation level: " + radLevel);
    }
    void Update(){
        timePassed += Time.deltaTime;
        radTime += Time.deltaTime;
        if (timePassed >= 1f){
            timePassed = timePassed % 1f;
            addRadiation();
            Debug.Log(Player.Radiation);
        }

        //making the geiger counter shake
        if(radTime > .3f){
            if(!shake){
                activeCounter.transform.rotation = Quaternion.Euler(0, 0, activeCounter.transform.rotation.z - 2);
                shake = true;
            }
            else{
            activeCounter.transform.rotation = Quaternion.Euler(0, 0, activeCounter.transform.rotation.z + 2);
                shake = false;
            }
            radTime = 0;
        }


        radMeter.transform.localPosition = new Vector3(Player.Radiation / 50f, radMeter.transform.position.y, radMeter.transform.position.z);
        //Debug.Log(Player.Radiation);
    }


    //calculates what zone the current travelling zone is
    public void calculateRadiation()
    {
        //calculates if the current zone is green, yellow, or red
        // 25% chance for green; 50% chance for yellow; 25% chance for red
        int prob = Random.Range(0,100);
        if(radLevel != "red")
        {
            if(prob < 25)
            {
                radLevel = "green";
                radiationLevels[0].SetActive(true);
                activeCounter = radiationLevels[0];
            }
            else if(prob < 75)
            {
                radLevel = "yellow";
                radiationLevels[1].SetActive(true);
                activeCounter = radiationLevels[1];
            }
            else if(prob < 100)
            {
                radLevel = "red";
                radiationLevels[2].SetActive(true);
                activeCounter = radiationLevels[2];   
            }
        }
        else{ //prevents the player from getting two red zones in a row, as a balancing measure
            if(prob < 35)
            {
                radLevel = "green";
                radiationLevels[0].SetActive(true);
                activeCounter = radiationLevels[0];
            }
            else if(prob < 65)
            {
                radLevel = "yellow";
                radiationLevels[1].SetActive(true);
                activeCounter = radiationLevels[1];
            }
        }
    }

    //adds radiation to the player's total
    public void addRadiation()
    {
        if(radLevel == "green")
        {
            //nothing, no radiation!
        }
        else if(radLevel == "yellow")
        {
            Player.Radiation += 5;
        }
        else if(radLevel == "red")
        {
            Player.Radiation += 10;
        }
    }

}
