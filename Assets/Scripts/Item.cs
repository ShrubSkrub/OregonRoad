using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int capacity, currentAmount, prevAmount;

    private void Start()
    {
        prevAmount = currentAmount;
        switch (tag)
        {
            case "Fuel":
                capacity = 100;
                break;
            case "Medicine":
                capacity = 2;
                break;
            case "Food":
                capacity = 20;
                break;
        }
    }

    private void Update()
    {
        if(GetComponent<Toggle>().isOn && currentAmount != prevAmount)
        {
            switch (tag)
            {
                case "Fuel":
                    GameLogic.Fuel -= prevAmount;
                    GameLogic.Fuel += currentAmount;
                    break;
                case "Medicine":
                    GameLogic.Medicine -= prevAmount;
                    GameLogic.Medicine += currentAmount;
                    break;
                case "Food":
                    GameLogic.Food -= prevAmount;
                    GameLogic.Food += currentAmount;
                    break;
                default:
                    Debug.Log("Unknown item type!");
                    break;
            }
            prevAmount = currentAmount;
        }
    }

    public void OnDestroy()
    {
        if (GetComponent<Toggle>().isOn) //take away the amount added if not placed in inventory (amount is only added when toggle is on)
        {
            switch (tag)
            {
                case "Fuel":
                    GameLogic.Fuel -= currentAmount;
                    break;
                case "Medicine":
                    GameLogic.Medicine -= currentAmount;
                    break;
                case "Food":
                    GameLogic.Food -= currentAmount;
                    break;
                default:
                    Debug.Log("Unknown item type!");
                    break;
            }
        }
    }

}
