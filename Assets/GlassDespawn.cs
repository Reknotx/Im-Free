using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        GetComponent<Rigidbody>().AddForce(transform.forward * 50f);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}
