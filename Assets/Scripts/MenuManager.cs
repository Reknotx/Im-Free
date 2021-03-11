using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private void Awake()
    {
        Time.timeScale = 0f;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

    }


    /// <summary>Plays the game.</summary>
    public void PlayGame()
    {
        Time.timeScale = 1f;

        transform.Find("Main Menu").gameObject.SetActive(false);
    }

    
    /// <summary> Resets the game. </summary>
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenMenu(string menuName)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.Find(menuName).gameObject.SetActive(true);
    }

    /// <summary> Quits the game. </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    /// <summary> Quits to main menu. </summary>
    public void QuitMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
