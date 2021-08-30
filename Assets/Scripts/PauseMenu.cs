using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script to load pause menu at travelling screen
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool paused = false;

    public void Resume(){
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void Pause(){
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void LoadMenu(){
        Initiate.Fade("Menu", Color.black, 2f);
        Time.timeScale = 1f;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
