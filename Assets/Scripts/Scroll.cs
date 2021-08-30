using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private bool dontTransition;
    public float speed = 0.0f, transitionIndex = 0.0f;
    public bool stop = false, transition = false;
    private float offsetDist, transitionDist, total;

    private void Start()
    {
        offsetDist = transitionIndex * -1;
    }

    void Update()
    {
        
        /*if (!dontTransition && Mathf.Abs(GetComponent<RawImage>().uvRect.x % 1) >= transitionIndex && Mathf.Abs(GetComponent<RawImage>().uvRect.x % 1) <= transitionIndex + .001)
        {
            transition = true;
        }*/
        if (total <= -400)
        {
            transition = false;
            GameObject.Find("Parallax").GetComponent<TravelingTransition>().transition = false;
            stop = false;
            total = 0;
        }

        if (GetComponent<RectTransform>().anchoredPosition.x >= 400)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x - 800, 53.5f);
        }

        else if (transition)
        {
            stop = true;
            transitionDist -= (Time.deltaTime * speed) * 40;
            if (transitionDist <= -1)
            {
                GetComponent<RectTransform>().anchoredPosition -= new Vector2(Mathf.Round(transitionDist), 0);
                total += Mathf.Round(transitionDist);
                transitionDist -= Mathf.Round(transitionDist);
            }
        }

        else if (!stop)
        {
            offsetDist -= (Time.deltaTime * speed) / 10;
            GetComponent<RawImage>().uvRect = new Rect(offsetDist, 0, 1, 1);
        }

    }
}
