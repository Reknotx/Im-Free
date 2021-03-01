using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    /// <summary>Plays the game.</summary>
    public void PlayGame(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    /// <summary> Pauses the game. </summary>
    public void PauseGame()
    {

    }

    /// <summary> Resumes the game. </summary>
    public void ResumeGame()
    {

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
        SceneManager.LoadScene("MainMenu");
    }
}
