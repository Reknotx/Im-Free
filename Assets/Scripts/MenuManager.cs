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

    /// <summary> Opens the options panel.</summary>
    public void Options()
    {

    }

    /// <summary> Opens the credits panel. </summary>
    public void Credits()
    {

    }

    /// <summary> Opens the achievements. </summary>
    public void Achievements()
    {

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
