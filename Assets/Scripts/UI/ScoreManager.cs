using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Notes
///1. up to three categories of items that give different values
///2. Timer ticks down immediately after you hit an object
///3. Circle on side goes down
///4. EVERY item increases the multiplier and refreshes timer.
///5. Timer decays more rapidly as time goes on
public class ScoreManager : SingletonPattern<ScoreManager>
{
    public float MAX_TIMER = 5f;

    float timer;

    float timeStart = 0f;

    private int _score;

    public List<Sprite> multiSprites;

    public Text scoreText;

    public Image multiplierImage;

    public Slider multiplierSlider;

    /// <summary> The player's current score. </summary>
    public int Score 
    {
        get => _score;

        set
        {
            _score = value;
            if (scoreText != null)
            {
                scoreText.text = _score.ToString(); ;
            }
        }
    }

    private int _multiValue;

    /// <summary> The value of the current score multiplier. </summary>
    public int MultiValue 
    {
        get => _multiValue;
        set
        {
            _multiValue = Mathf.Clamp(value, 0, 5);
            if (_multiValue == 1)
            {
                multiplierImage.gameObject.SetActive(true);
                StartCoroutine(MultiplierDecay());
            }
            
            if (_multiValue >= 1 && multiplierImage != null)
            {
                multiplierImage.sprite = multiSprites[_multiValue - 1];
            }
            else if (_multiValue == 0)
            {
                multiplierImage.gameObject.SetActive(false);
                multiplierImage.sprite = null;
            }

        }
    }



    protected override void Awake()
    {
        base.Awake();

        Score = 0;
        MultiValue = 0;
        timer = MAX_TIMER;
    }

    public void AddScore(int score)
    {
        //if (scoreMultiplierCR == null) scoreMultiplierCR = StartCoroutine(MultiplierDecay());

        timeStart = Time.time;
        timer = MAX_TIMER;

        MultiValue++;
        Score += score * MultiValue;
    }

    IEnumerator MultiplierDecay()
    {
        bool decaying = true;
        multiplierSlider.gameObject.SetActive(true);

        while (decaying)
        {
            float u = (Time.time - timeStart) / timer;

            if (u >= 1f)
            {
                u = 1f;
                decaying = false;
            }

            multiplierSlider.value = (1 - u) * multiplierSlider.maxValue + u * 0;

            yield return new WaitForFixedUpdate();
        }
        multiplierSlider.gameObject.SetActive(false);
        MultiValue = 0;
    }
}
