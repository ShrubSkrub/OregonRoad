using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//overall script that reaches to every scene in the game, contains all global variables throughout the game
public static class GameLogic {

    //intance variables for the game's overall things to keep track of
    private static double totalDays, fuel;
    private static int currentStop, food, members, travelSpeed, medicine, totalDistance, day = 1, month = 3, year = 1983, eatRate = 3;
    private static float time = 480;

    private static bool travelling, startedToTravel, changingFood = false;

    //getters and setters for each instance variable
    public static double TotalDays { 
        get => totalDays; 
        set => totalDays = value; 
    }

    public static double Fuel { 
        get => fuel; 
        set => fuel = value; 
    }

    public static int Food { 
        get => food; 
        set => food = value; 
    }

    public static int Members { 
        get => members;
        set => members = value; 
    }

    public static int TravelSpeed { 
        get => travelSpeed;
        set => travelSpeed = value; 
    }

    public static int Medicine { 
        get => medicine; 
        set => medicine = value; 
    }

    public static int TotalDistance { 
        get => totalDistance; 
        set => totalDistance = value; 
    }

    public static int Day{
        get => day;
        set => day = value;
    }
    
    public static int Month{
        get => month;
        set => month = value;
    }

    public static int Year{
        get => year;
        set => year = value;
    }
    public static float Time{
        get => time;
        set => time = value;
    }
    public static int CurrentStop{
        get => currentStop;
        set => currentStop = value;
    }
    public static bool Travelling {
        get => travelling;
        set => travelling = value; 
    }
    public static bool StartedToTravel
    { 
        get => startedToTravel; 
        set => startedToTravel = value; 
    }
    public static bool ChangingFood
    {
        get => changingFood; 
        set => changingFood = value; 
    }
    public static int EatRate
    { 
        get => eatRate; 
        set => eatRate = value; 
    }

    //logic for searching supplies (NEVER USED)
    public static void searchSupplies(string locationType, string blockage)
    {
        food += searchFood(locationType);
        medicine += searchMedicine(locationType);
        fuel += searchFuel(locationType, blockage);
    }

    //logic for searching medicine
    public static int searchMedicine(string locationType)
    {
        int prob = Random.Range(0,100);
        switch(locationType)
        {
            case "city":
                if(prob < 10)
                {
                    return 0; //nothing found
                }
                else if(prob < 55)
                {
                    return 1;
                }
                else if(prob < 100)
                {
                    return 2;
                }
                break;
            case "highway":
                if(prob < 70)
                {
                    return 0; //nothing found
                }
                else if(prob < 90)
                {
                    return 1;
                }
                else if(prob < 100)
                {
                    return 2;
                }
                break;
            case "wilderness":
                if(prob < 90)
                {
                    return 0;
                }
                else if(prob < 98)
                {
                    return 1;
                }
                else if(prob < 100)
                {
                    return 2;
                }
                break;
            case "roadstop":
                if(prob < 30)
                {
                    return 0; //nothing found
                }
                else if(prob < 70)
                {
                    return 1;
                }
                else if(prob < 100)
                {
                    return 2;
                }
                break;
        }
        return -1;
    }

    //logic for searching fuel
    public static int searchFuel(string locationType, string blockage) //fuel can be stored from a range of 0-100, and it can decrease like -2 over each turn? 
    {
        int prob = Random.Range(0,100);
        switch(locationType)
        {
            case "city":
                if(prob < 30)
                {
                    return 0; //nothing found
                }
                else if(prob < 60)
                {
                    return 25;
                }
                else if(prob < 90)
                {
                    return 50;
                }
                else if(prob < 100)
                {
                    return 100;
                }
                break;
            case "highway":
                switch(blockage)
                {
                    case "none":
                        if(prob < 90)
                        {
                            return 0; //nothing found
                        }
                        else if(prob < 100)
                        {
                            return 25;
                        }
                        break;
                    case "light":
                        if(prob < 50)
                        {
                            return 0; //nothing found
                        }
                        else if(prob < 100)
                        {
                            return 50;
                        }
                        break;
                    case "heavy":
                        if(prob < 25)
                        {
                            return 0; //nothing found
                        }
                        else if(prob < 50)
                        {
                            return 50;
                        }
                        else if(prob < 100)
                        {
                            return 100;
                        }
                        break;
                }
                break;
            case "wilderness":
                if(prob < 75)
                {
                    return 0; //nothing found
                }
                else if(prob < 95)
                {
                    return 25;
                }
                else if(prob < 100)
                {
                    return 50;
                }
                break;
            case "roadstop":
                if(prob < 20)
                {
                    return 0; //nothing found
                }
                else if(prob < 55)
                {
                    return 25;
                }
                else if (prob < 80)
                {
                    return 50;
                }
                else if(prob < 100)
                {
                    return 100;
                }
                break;
        }
        return -1;
    }

    //logic for searching food
    public static int searchFood(string locationType)
    {
        int prob = Random.Range(0, 100);
        switch(locationType)
        {
            case "city":
                if(prob<15)
                {
                    return 0; //nothing found
                } 
                else if(prob<30)
                {
                    return Random.Range(5, 10);
                }
                else if(prob<60)
                {
                    return Random.Range(10, 15);
                }
                else if(prob<85)
                {
                    return Random.Range(15, 20);
                }
                else if(prob<100)
                {
                    return 20;
                }
                break;
            case "highway":
                if(prob<40)
                {
                    return 0; //nothing found
                }
                else if(prob<80)
                {
                    return Random.Range(5, 10);
                }
                else if(prob<90)
                {
                    return Random.Range(10, 15);
                }
                else if(prob<95)
                {
                    return Random.Range(15, 20);
                }
                else if(prob<100)
                {
                    return 20;
                }
                break;
            case "wilderness":
                if(prob<65)
                {
                    return 0; //nothing found
                }
                else if(prob<80)
                {
                    return Random.Range(5, 10);
                }
                else if(prob<90)
                {
                    return Random.Range(10, 15);
                }
                else if(prob<95)
                {
                    return Random.Range(15, 20);
                }
                else if(prob<100)
                {
                    return 20;
                }
                break;
            case "roadstop":
                if(prob<30)
                {
                    return 0; //nothing found
                }
                else if(prob<55)
                {
                    return Random.Range(5, 10);
                }
                else if(prob<80)
                {
                    return Random.Range(10, 15);
                }
                else if(prob<95)
                {
                    return Random.Range(15, 20);
                }
                else if(prob<100)
                {
                    return 20;
                }
                break;
        }
        return -1;
        //create probabilities for each food being found

       //make cases? or if statements
    }

    //logic for receiving an injury when scavenging (NEVER USED)
    public static string scavengeInjury(string locationType, string blockage)
    {
        int injuryProb = Random.Range(0, 100);
        int injServProb = Random.Range(0, 100);
        switch (locationType)
        {
            case "highway":
                switch(blockage)
                {
                    case "none":
                        return "none";
                    case "light":
                        return injuryProb < 10 ? (injServProb < 30 ? "major injury" : "minor injury") : "none";
                    case "heavy":
                        return injuryProb < 35 ? (injServProb < 30 ? "major injury" : "minor injury") : "none";
                }
                break;
            case "wilderness":
                switch (blockage)
                {
                    case "none":
                        return "none";
                    case "light":
                        return injuryProb < 5 ? (injServProb < 30 ? "major injury" : "minor injury") : "none";
                    case "heavy":
                        return injuryProb < 15 ? (injServProb < 30 ? "major injury" : "minor injury") : "none";
                }
                break;
        }
        return "none";
    }
}
