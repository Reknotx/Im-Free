using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableObj : MonoBehaviour
{
    public enum ScoreTier
    {
        Low = 10,
        Mid = 100,
        High = 1000
    }

    [Tooltip("The score tier of this punchable object.")]
    public ScoreTier tier = ScoreTier.Low;

    public bool BeenPunched { get; set; } = false;
    public GameObject kapowPrefab;

    public virtual void Punched()
    {
        if (BeenPunched) return;

        Instantiate(kapowPrefab, transform.position, Quaternion.identity);

        ScoreManager.Instance.AddScore((int)tier);
        BeenPunched = true;
    }

}
