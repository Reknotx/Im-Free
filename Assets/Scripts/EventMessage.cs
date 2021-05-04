using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMessage : MonoBehaviour
{
    public void TriggerAttack()
    {
        
    }

    public void ResetPos()
    {
        transform.localPosition = Vector3.zero;
    }

    public void EnableRootMotion()
    {
        gameObject.GetComponent<Animator>().applyRootMotion = true;
    }

    public void DisableRootMotion()
    {
        gameObject.GetComponent<Animator>().applyRootMotion = false;
        transform.localPosition = Vector3.zero;
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    public void TriggerLeaderboardFade()
    {
        PlayerUIManager.Instance?.DeathFade();
        LeaderBoard.Instance.GameOver(ScoreManager.Instance.Score);

    }
}
