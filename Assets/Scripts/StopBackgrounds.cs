using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBackgrounds : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject backgroundBombed;
    void Start()
    {
        int prob = Random.Range(0,2);
        if(prob == 0){
            background.SetActive(true);
            backgroundBombed.SetActive(false);
        }
        else if(prob == 1){
            background.SetActive(false);
            backgroundBombed.SetActive(true);
        }
    } 

}
