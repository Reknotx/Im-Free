using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : SingletonPattern<MenuManager>
{

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    /// <summary>Plays the game.</summary>
    public void PlayGame()
    {
        //Time.timeScale = 1f;

        //transform.Find("Main Menu").gameObject.SetActive(false);
        Tutorial.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
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

    public void OpenSurvey(string surveyLink)
    {
        Application.OpenURL(surveyLink);

    }
}
