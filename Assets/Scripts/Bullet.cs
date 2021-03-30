using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    bool moving = true;

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
            transform.parent = Player.Instance.transform;
            Player.Instance.TranqDartStack++;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
