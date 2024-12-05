using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour{

    public GameObject nextLvlMenu;

    public void NextLevel(){
        FindObjectOfType<AudioManager>().Play("Completed Level");
        nextLvlMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartNextLevel(){
        nextLvlMenu.SetActive(false);
        SceneManager.LoadScene("BasementMain");
    }

    public void LoadMenu(){
        nextLvlMenu.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
