using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    bool moving = true;

    private float minVal = 0.05f, maxVal = 0.2f;

    /// <summary>
    /// The amount of speed decay put on the player.
    /// </summary>
    [Range(0.05f, 0.2f)]
    [HideInInspector] public float decayValue;

    // Update is called once per frame
    void Update()
    {
        if (!moving) return;
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * Time.deltaTime * 15f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            moving = false;
            transform.parent = Player.Instance.dartHolder;
            Player.Instance.TranqDartStack++;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(EffectDecay());
        }
    }

    IEnumerator EffectDecay()
    {
        yield return null;
        bool decaying = true;
        float startTime = Time.time;
        float timeDecay = 1f;

        float p0 = maxVal, p1 = minVal, p01;

        while(decaying)
        {
            float u = (Time.time - startTime) / timeDecay;

            if (u >= 1)
            {
                decaying = false;
                u = 1;
            }

            p01 = (1 - u) * p0 + u * p1;

            decayValue = p01;

            yield return new WaitForFixedUpdate();

        }
    }
}
