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

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out PunchableObj punchable))
            {
                punchable.enabled = false;
                punchable.GetComponent<Rigidbody>().isKinematic = true;
                punchable.GetComponent<Rigidbody>().useGravity = false;
                punchable.GetComponent<Collider>().enabled = false;
            }
            else if (child.TryGetComponent(out GlassDespawn despawn))
            {
                despawn.enabled = false;
                despawn.GetComponent<Rigidbody>().isKinematic = true;
                despawn.GetComponent<Rigidbody>().useGravity = false;
                despawn.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public virtual void Punched()
    {
        if (BeenPunched) return;

        Instantiate(kapowPrefab, transform.position, Quaternion.identity);

        ScoreManager.Instance.AddScore((int)tier);
        BeenPunched = true;
    }

}
