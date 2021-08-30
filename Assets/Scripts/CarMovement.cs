using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    private float time = 0;
    void Start()
    {
        StartCoroutine(negateSpeed());
    }

    void Update()
    {
        if (GetComponent<RawImage>().uvRect.x < -0.09 || GetComponent<RawImage>().uvRect.x > 0.25)
        {
            GetComponent<Scroll>().speed *= -1;
            time = Time.time;
        }
    }

    IEnumerator negateSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            if (Time.time - time > 2)
            {
                float speed = GetComponent<Scroll>().speed;
                while (Mathf.Abs(GetComponent<Scroll>().speed) <= Mathf.Abs(speed))
                {
                    yield return new WaitForSeconds(.2f);
                    if (Mathf.Abs(GetComponent<Scroll>().speed) < .1)
                        GetComponent<Scroll>().speed *= -9f;
                    else
                        GetComponent<Scroll>().speed /= 2f;
                }
                GetComponent<Scroll>().speed = speed * -1;
            }
        }
    }
}
