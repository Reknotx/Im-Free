using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float MAX_TIMER = 5f;

    float timer;

    float timeStart = 0f;

    public Slider multiplierSlider;

    public GameObject ScoreMultiplierUI;

    private void Awake()
    {
        timer = MAX_TIMER;
    }

    public void AddScore(float score)
    {
        timeStart = Time.time;
        timer = MAX_TIMER;
    }

    IEnumerator MultiplierDecay()
    {
        bool decaying = true;

        ScoreMultiplierUI.SetActive(true);

        while (decaying)
        {
            float u = (Time.time - timeStart) / timer;

            if (u >= 1f)
            {
                u = 1f;
                decaying = false;
            }

            yield return new WaitForFixedUpdate();
        }


        ScoreMultiplierUI.SetActive(false);
    }
}
