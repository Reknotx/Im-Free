using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : SingletonPattern<PlayerUIManager>
{
    #region Fields
    /// <summary> The slider that graphically represents the player's health. </summary>
    [Tooltip("The slider that represents the player's health.")]
    public Slider healthSlider;

    /// <summary> The text component displaying the player's score value. </summary>
    public Text scoreText;

    /// <summary> The canvas group for the death fade UI object. </summary>
    public CanvasGroup deathFade;

    public Text healthText;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        deathFade.alpha = 0f;
        deathFade.gameObject.SetActive(false);

    }

    /// <summary> Updates the health slider to match the player's current health. </summary>
    public void UpdateHealth()
    {
        healthSlider.value = Player.Instance.Health / 100f;
        if (healthText != null)
        {
            healthText.text = Mathf.FloorToInt(Player.Instance.Health) + "/100";
        }
    }

    /// <summary> Activates the death fade when the player's health reaches zero. </summary>
    public void DeathFade()
    {
        deathFade.gameObject.SetActive(true);

        LeanTween.alphaCanvas(deathFade, 1f, 1f);
    }

    /// <summary> Pauses the game. </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    /// <summary> Resumes the game. </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
