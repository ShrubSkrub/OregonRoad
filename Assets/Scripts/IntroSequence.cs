using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI continueText = GameObject.Find("Continue").GetComponent<TextMeshProUGUI>();
        continueText.color = new Color(continueText.color.r, continueText.color.g, continueText.color.b, 0);
        StartCoroutine(WaitForKeyPressOrTime());
    }

    

    IEnumerator WaitForKeyPressOrTime() //allows user to skip the wait for continue text to show
    {
        bool stopWaiting = false;
        float startTime = Time.time;
        while (!stopWaiting) //waits for 5 seconds or key press, whichever comes first
        {
            if (Input.anyKeyDown)
            {
                stopWaiting = true;
            }
            else if (Time.time - startTime >= 5f)
            {
                stopWaiting = true;
            }
            yield return null;
        }

        while (Input.anyKeyDown) //waits for key down to reset
        {
            yield return null;
        }

        StartCoroutine(FadeTextToFullAlpha(2f, GameObject.Find("Continue").GetComponent<TextMeshProUGUI>()));
        StartCoroutine(waitForKeyPress());
    }

    IEnumerator waitForKeyPress() 
    {
        while (!Input.anyKeyDown) //waits for key to be pressed
        {
            yield return null;
        }

        string currentSceneName = SceneManager.GetActiveScene().name;
        while (currentSceneName == SceneManager.GetActiveScene().name) //try fade until we reach the next scene
        {
            Initiate.Fade(SceneNameFinder.GetNextSceneName(), Color.black, 2f);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
