using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathButtons : MonoBehaviour
{
    private static int nextStopIndex = 1;
    public bool altPath, handleVegas, dontIncrementStopIndex, startPath;
    string nameOfNextStop;
    public static Dictionary<string, int> distances = new Dictionary<string, int>() { 
        //path 1
        { "stop1", 0 }, { "stop2", 173 }, { "stop3", 238 }, { "stop4", 197 }, { "stop4a", 80 }, { "stop5", 243 }, { "stop6", 236 }, { "stop7a", 302 }, { "stop8a", 438}, { "stop7", 154 }, { "stop8", 399 }, { "stop9", 151 }, { "stop10", 155 }, { "stop11", 233 },
        //path 2
        { "stop12", 195 }, { "stop21a", 59 }, { "stop13", 226 }, { "stop23a", 308 }, { "stop14", 221 }, { "stop15", 256 }, { "stop16", 181 }, { "stop16a", 157 }, { "stop17", 184 }, { "stop18", 129 }, { "stop19", 171 }, { "stop20", 263 }, { "stop11a", 50 }, 
        //path 3
        { "stop21", 185 }, { "stop12a", 59 }, { "stop22", 181 }, { "stop23", 347 }, { "stop13a", 308 }, { "stop24", 215 }, { "stop25", 245 }, { "stop26", 202 }, { "stop27", 217 }, { "stop20a", 347 }
    };
    public static Dictionary<string, string> sceneNames = new Dictionary<string, string>() { 
        //path 1
        { "stop1", "KansasCityKS" }, { "stop2", "SalinaKS" }, { "stop3", "GoodlandKS" }, { "stop4", "DenverCO" }, { "stop4a", "CheyenneMountainsAirForceStationCO" }, { "stop5", "GrandJunctionCO" }, { "stop6", "ScipioUT" }, { "stop7a", "LasVegasNV" }, { "stop7", "WheelerPeakNV" }, { "stop8", "RenoNV" }, { "stop9", "HatCreekCA" }, { "stop10", "MedfordOR" }, { "stop11", "WillametteValleyOR" },
        //path 2
        { "stop12", "LincolnNE" }, { "stop13", "NorthPlatteNE" }, { "stop14", "CheyenneWY" }, { "stop15", "RockSpringsWY" }, { "stop16", "OgdenUT" }, { "stop17", "TwinFallsID" }, { "stop18", "BoiseID" }, { "stop19", "LaGrandeOR" }, { "stop20", "PortlandOR" },
        //path 3
        { "stop21", "OmahaNE" }, { "stop22", "SiouxFallsSD" }, { "stop23", "RapidCitySD" }, { "stop24", "LameDeerMT" }, { "stop25", "BozemanMT" }, { "stop26", "MissoulaMT" }, { "stop27", "LewistonID" }
    };


    public void nextStop() //handles going to next stops that dont change paths
    {
        if (Initiate.areWeFading) //disables button while fading, fixes button spam
        {
            Debug.Log("Fading, cannot press button!");
            return;
        }

        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName.Equals("OgdenUT"))
            nextStopIndex = 16;
        else if (buttonName.Equals("OmahaNE"))
            nextStopIndex = 21;
        else if (buttonName.Equals("RapidCitySD"))
            nextStopIndex = 23;
        else if (buttonName.Equals("LincolnNE"))
            nextStopIndex = 12;
        else if (buttonName.Equals("NorthPlatteNE"))
            nextStopIndex = 13;
        else if (buttonName.Equals("PortlandOR"))
            nextStopIndex = 20;
        else if (buttonName.Equals("WillametteValleyOR"))
            nextStopIndex = 11;
        else{
            if (!dontIncrementStopIndex)
                nextStopIndex++;
            nameOfNextStop = "stop" + nextStopIndex;
            if (altPath)
                nameOfNextStop += "a";
            if(handleVegas)
                //GameLogic.TotalDistance += distances[nameOfNextStop + "a"]; //adding distance to next stop to the total distance traveled
            //GameLogic.TotalDistance += distances[nameOfNextStop]; //adding distance to next stop to the total distance traveled
            return;
        }
        nameOfNextStop = "stop" + nextStopIndex;
        if(buttonName.Equals("CheyenneMountainsAirForceStationCO"))
            nameOfNextStop = "stop4a";
        //Initiate.Fade(sceneNames[nameOfNextStop], Color.black, 2f);
    }

    public void nextStop(string stopName){
        Initiate.Fade(sceneNames[stopName], Color.black, 2f);
    }
    
    public void changePaths() //handles stops that change paths
    {
        if (Initiate.areWeFading) //disables button while fading, fixes button spam
        {
            Debug.Log("Fading, cannot press button!");
            return;
        }
        Debug.Log("Total Distance Traveled: " + GameLogic.TotalDistance);
        Initiate.Fade(sceneNames[nameOfNextStop], Color.black, 2f);

    }

    public void LoadTravellingScreen(){
        nextStop();
        Debug.Log(nameOfNextStop);
        Debug.Log(distances[nameOfNextStop]);
        Travelling.SetStopName(nameOfNextStop, distances[nameOfNextStop]);
        Initiate.Fade("TravellingScreen", Color.black, 2f);
    }

    public void LoadEnd(){
        Initiate.Fade("End", Color.black, 2f);
    }

}
