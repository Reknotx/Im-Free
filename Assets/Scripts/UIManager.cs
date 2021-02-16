using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : SingletonPattern<UIManager>
{
    #region Fields
    /// <summary> The slider that graphically represents the player's health. </summary>
    [Tooltip("The slider that represents the player's health.")]
    public Slider healthSlider;

    /// <summary> The text component displaying the player's score value. </summary>
    public Text scoreText;

    /// <summary> The canvas group for the death fade UI object. </summary>
    public CanvasGroup deathFade;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        deathFade.alpha = 0f;

    }

    /// <summary> Updates the health slider to match the player's current health. </summary>
    public void UpdateHealth()
    {
        healthSlider.value = Player.Instance.Health / 100f;
    }

    public void DeathFade()
    {
        LeanTween.alphaCanvas(deathFade, 1f, 1f);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApp()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
