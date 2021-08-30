using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

//script that simulates the player's behaviors and statistics.
public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] string idNumber;
    [SerializeField] Sprite[] portraits;
    GameObject conditionPortrait;
    Image sprite;
    static Button tinyButton, meagerButton, fullButton;
    private int health = 100, daysSick, hunger, pf;
    private static int radiation;
    private float timePassed;
    private bool isDead = false;
    private float timePassedRad, timePassedHunger;

    public int Health { // health of a player
        get => health; 
        set => health = value; 
    }
    public int DaysSick { // number of days a player has been sick
        get => daysSick; 
        set => daysSick = value; 
    }
    public static int Radiation { // amount of radiation absorbed by the player
        get {return radiation;}
        set {radiation = value;}
    }
    public int Hunger { // hunger value for the player used to determine when the player starts taking damage from being hungry
        get => hunger; 
        set => hunger = value; 
    }
    public int Pf { // protection factor: protection from radiation fallout 
        get => pf; 
        set => pf = value; 
    }

    public bool IsDead { // true if player is dead, false otherwise
        get => isDead;
        set => isDead = value;
    }

    void Update()
    {
        //Debug.Log("Tiny" + idNumber);
        //Debug.Log(playerName + " is now eating " + eatRate + " every hour");
        if(GameLogic.Travelling){
            timePassed += Time.deltaTime;
            if(timePassed >= 3){ //CHANGE THIS NUMBER IF THE WATCH TIME VALUE CHANGES
                Debug.Log("Eating food!" + GameLogic.Food);
                eatFood();
                timePassed = 0;
            }
        }
        //ticking radiation and hunger damage
        radiationDamage();
        hungerDamage();
        
        //each time the screen is loaded at a stop, change the portrait around the character
        //to reflect their current health
        if(!GameLogic.Travelling && GameLogic.StartedToTravel){
            if(health > 75){
                conditionPortrait = GameObject.Find(idNumber);
                sprite = conditionPortrait.GetComponent<Image>();
                sprite.sprite = portraits[0];
            }
            else if(health > 50){
                conditionPortrait = GameObject.Find(idNumber);
                sprite = conditionPortrait.GetComponent<Image>();
                sprite.sprite = portraits[1];
            }
            else if(health > 25){
                conditionPortrait = GameObject.Find(idNumber);
                sprite = conditionPortrait.GetComponent<Image>();
                sprite.sprite = portraits[2];
            }
            else if(isDead){
                conditionPortrait = GameObject.Find(idNumber);
                sprite = conditionPortrait.GetComponent<Image>();
                sprite.sprite = portraits[3];
            }
        }
        
    }

    private void radiationDamage() // accounts for the different amount of damage each party member takes depending on how much radiation the party has absorbed over the course of the trip
    {
        timePassedRad += Time.deltaTime;
        if (timePassedRad >= 10f) // ticks damage every 10 seconds
        {
            timePassedRad = timePassedRad % 10f;
            Debug.Log(playerName + "'s health:" + health + " radiation:" + radiation + " eatRate: " + GameLogic.EatRate);
            if (!isDead)
            {
                if (radiation > 1000) // 1000 rems radiation absorbed is lethal
                {
                    health = 0;
                    isDead = true;

                }
                else if (radiation > 400) // 400 rems absorbed is damaging (4 health per 10 seconds)
                {
                    health -= 4;
                }
                else if (radiation > 100) // 100 rems absorbed is slightly damaging (1 health per 10 seconds)
                {
                    health -= 1;
                }
                if (health == 0) // if health has been depleted, the character is dead
                {
                    isDead = true;
                    Tombstone.LoadDeath(playerName);
                }
            }

        }
    }

    private void hungerDamage() // accounts for damage the character takes due to hunger
    {
        timePassedHunger += Time.deltaTime;
        if(timePassedHunger>=30f)
        {
            timePassedHunger = timePassedHunger % 30f;
            if(!isDead)
            {
                if(hunger > 10)
                {
                    health = 0;
                    isDead = true;
                } else if(hunger > 5)
                {
                    health -= 3;
                } else if(hunger > 3)
                {
                    health -= 1;
                }
                if(health==0)
                {
                    isDead = true;
                    Tombstone.LoadDeath(playerName);
                }
            }
        }
    }

    private void eatFood() //allows character to actually "consume" food from the menu; happens every in game hour
    {
        GameObject ObjectToTakeFoodFrom;
        if (GameObject.FindGameObjectsWithTag("Food").Length != 0)
        {
            ObjectToTakeFoodFrom = GameObject.FindGameObjectsWithTag("Food").OrderBy(go => go.GetComponent<Item>().currentAmount).ToList()[0];
            ObjectToTakeFoodFrom.GetComponent<Item>().currentAmount -= GameLogic.EatRate;

            if (ObjectToTakeFoodFrom.GetComponent<Item>().currentAmount <= 0)
            {
                Destroy(ObjectToTakeFoodFrom);
                Destroy(GameObject.Find("FoodItem"));
                ObjectToTakeFoodFrom = GameObject.FindGameObjectsWithTag("Fuel").OrderBy(go => go.GetComponent<Item>().currentAmount).ToList()[0];
            }
        }
        if (GameLogic.Food - GameLogic.EatRate > 0){
            GameLogic.Food -= GameLogic.EatRate;
        }
        else{
            GameLogic.Food -= GameLogic.Food;
        }
        if(GameLogic.Food == 0){
            hunger++;
        }
    }
}
