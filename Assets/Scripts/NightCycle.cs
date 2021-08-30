using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NightCycle : MonoBehaviour
{
    public float time = 1;
    public int nightTime = 20, dayTime = 6;
    private Travelling clock;
    
    void Start()
    {
        clock  = GameObject.Find("TravellingUI").GetComponent<Travelling>();
    }

    void Update()
    {
        if(clock.getHours() >= nightTime)
        {
            StartCoroutine(fadeToNight());
        }
        else if(clock.getHours() >= dayTime)
        {
            StartCoroutine(fadeToDay());
        }
    }

    IEnumerator fadeToNight()
    {
        while(GetComponent<CanvasGroup>().alpha > 0)
        {
            GetComponent<CanvasGroup>().alpha -= Time.deltaTime / time;
            yield return null;
        }
    }

    IEnumerator fadeToDay()
    {
        while (GetComponent<CanvasGroup>().alpha < 1)
        {
            GetComponent<CanvasGroup>().alpha += Time.deltaTime / time;
            yield return null;
        }
    }
}
