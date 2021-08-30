using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void FadeIntoNextScene ()
    {
        Initiate.Fade(SceneNameFinder.GetNextSceneName(), Color.black, 2f);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
