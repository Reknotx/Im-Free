using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : SingletonPattern<Tutorial>
{
    int index;

    protected override void Awake()
    {
        base.Awake();
        index = 0;
        gameObject.SetActive(false);
    }

    public void ProgressTutorial()
    {
        if (index == 2)
        {
            CloseTutorial();
        }
        else
        {
            transform.GetChild(index).gameObject.SetActive(false);
            index++;
            transform.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void CloseTutorial()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
