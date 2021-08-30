using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

//script to simulate the car actually moving
public class Travelling : MonoBehaviour
{
    //below FIVE serialized fields are used to change what is displayed to the player on the UI
    [SerializeField] TextMeshProUGUI HoursMinutes;
    [SerializeField] TextMeshProUGUI FoodStatus;
    [SerializeField] TextMeshProUGUI DistanceToLandmark;
    [SerializeField] TextMeshProUGUI DistanceTravelled;
    [SerializeField] TextMeshProUGUI Date;
    [SerializeField] PathButtons pathFinder;
    [SerializeField] float clockMultiplier = 1f; //change this in the editor to make the clock go faster or slower (currently settled on a factor of 20)

    //instance variables to calculate time passed
    private float rawTime = 0.0f, hour = 0.0f, minute = 0.0f, lastMinute = 0.0f, gasTime = 0.0f;
    private string hoursDisplay, minutesDisplay, secondsDisplay;
    private int day = 1, month = 11, year = 1983, dayLimit = 30;
    private string[] months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
    static string stopName = "";
    static int landmarkDistance = 0;
    public bool freezeTime; //freezes time when the destination is reached

    void Start(){
        //sets the time and day to what it was before (way to carry over from scene to scene)
        day = GameLogic.Day;
        month = GameLogic.Month;
        year = GameLogic.Year;
        rawTime = GameLogic.Time;
        lastMinute = minute;
        GameLogic.Travelling = true;
        //initialHour = GameLogic.Hour;
        //initialMinute = GameLogic.Minute;
    }
    void Update()
    {
        //checks if the game hasn't started to travel, and if it has, then set it to make it travel.
        //this is used in another script to prevent overloading the console with error messages
        if(!GameLogic.StartedToTravel){
            GameLogic.StartedToTravel = !GameLogic.StartedToTravel;
        }
        //if time isnt frozen, continue to make the car move
        if(!freezeTime){
            SimulateTime();
        }
    }

    //big method to make time move. 
    //makes the clock move forward, along with subtracting fuel along the way
    public void SimulateTime(){
        rawTime += Time.deltaTime * clockMultiplier;
        gasTime += Time.deltaTime;
        if(gasTime >= 3f){
            GameObject ObjectToTakeFuelFrom;
            GameLogic.Fuel--;
            if (GameObject.FindGameObjectsWithTag("Fuel").Length != 0)
            {
                ObjectToTakeFuelFrom = GameObject.FindGameObjectsWithTag("Fuel").OrderBy(go => go.GetComponent<Item>().currentAmount).ToList()[0];
                ObjectToTakeFuelFrom.GetComponent<Item>().currentAmount--;

                if (ObjectToTakeFuelFrom.GetComponent<Item>().currentAmount <= 0)
                {
                    Destroy(ObjectToTakeFuelFrom);
                    Destroy(GameObject.Find("FuelItem"));
                    ObjectToTakeFuelFrom = GameObject.FindGameObjectsWithTag("Fuel").OrderBy(go => go.GetComponent<Item>().currentAmount).ToList()[0];
                }
            } 
            if (GameLogic.Fuel < 0){
                GameLogic.Fuel = 0;
            }
            
            gasTime = 0;
        }
        hour = (int) rawTime / 60;
        minute = (int) rawTime - (int) hour * 60;
        if(minute != lastMinute){
            landmarkDistance--;
            GameLogic.TotalDistance++;
        }
        if(hour == 24){
            rawTime = 0;
            day++;
            if(day > dayLimit){
                month++;
                if(month == 12){
                    month = 0;
                    year++;
                }
                day = 1;
                if(months[month] == "January" || months[month] == "March" || months[month] == "May" || months[month] == "July" || months[month] == "August" || months[month] == "October" || months[month] == "December"){
                    dayLimit = 31;
                }
                else if(months[month] == "April" || months[month] == "June" || months[month] == "September" || months[month] == "November"){
                    dayLimit = 30;
                }
                else{
                    dayLimit = 28;
                }
            }
        }
        if(hour < 10){
            hoursDisplay = "0" + hour;
        }
        else{
            hoursDisplay = "" + hour;
        }
        if (minute < 10){
            minutesDisplay = "0" + minute;
        }
        else{
            minutesDisplay = "" + minute;
        }

        if(landmarkDistance == 0){
            GameLogic.Travelling = false;
            ReachLocation();
        }

        //updating the UI every frame
        HoursMinutes.text = hoursDisplay + ":" + minutesDisplay;
        FoodStatus.text = "We have " + GameLogic.Food + " pounds of food left.";
        DistanceToLandmark.text = "The next landmark is " + landmarkDistance + " miles away.";
        DistanceTravelled.text = "We have travelled " + GameLogic.TotalDistance + " miles so far.";
        Date.text = months[month] + " " + day + ", " + year;
        lastMinute = minute;
    }

    //getter methods for the minutes and hours
    public float getMinutes(){
        return minute;
    }
    public float getHours(){
        return hour;
    }
    //sets the current stop name in the GameLogic scrpt so the stop can get saved
    public static void SetStopName(string stop, int distance){
        stopName = stop;
        landmarkDistance = distance;
    }
    //loads the saved stop from the GameLogic
    public void LoadStop(){
        //Debug.Log(stopName);
        pathFinder.nextStop(stopName);
    }

    //once a location is reached, stop time, store data, and change locations
    public void ReachLocation(){
        LoadStop();
        freezeTime = true;
        GameLogic.Day = day;
        GameLogic.Month = month;
        GameLogic.Year = year;
        GameLogic.Time = hour * 60 + minute;
    }
}
