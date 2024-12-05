using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour{

    public GameObject deathMenu;

    public static bool isDeathMenuActive = false;

    public void GameOver(){
        deathMenu.SetActive(true);
        isDeathMenuActive = true;
        Time.timeScale = 0f;
    }

    public void Retry(){
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("BasementMain");
        isDeathMenuActive = false;
    }

    public void LoadMenu(){
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        isDeathMenuActive = false;
    }
}
