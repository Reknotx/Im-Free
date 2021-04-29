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

    /// <summary> The canvas group for the death fade UI object. </summary>
    public CanvasGroup leaderBoardFade;

    public Text healthText;

    public List<Sprite> headSprites;

    public Image headImage;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        leaderBoardFade.alpha = 0f;
        leaderBoardFade.gameObject.SetActive(false);

    }

    /// <summary> Updates the health slider to match the player's current health. </summary>
    public void UpdateHealth()
    {
        healthSlider.value = Player.Instance.Health / 100f;

        if (Player.Instance.Health >= 75)
            headImage.sprite = headSprites[0];

        else if (Player.Instance.Health >= 50)
            headImage.sprite = headSprites[1];

        else if (Player.Instance.Health >= 25)
            headImage.sprite = headSprites[2];

        else
            headImage.sprite = headSprites[3];



        if (healthText != null)
        {
            healthText.text = Mathf.FloorToInt(Player.Instance.Health) + "/100";
        }
    }

    /// <summary> Activates the death fade when the player's health reaches zero. </summary>
    public void DeathFade()
    {
        leaderBoardFade.gameObject.SetActive(true);

        LeanTween.alphaCanvas(leaderBoardFade, 1f, 1f);
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
