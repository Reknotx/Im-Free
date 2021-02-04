using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << 8);

        if (hit.collider == null) return;

        Player.Instance.transform.LookAt(new Vector3(hit.point.x,
                                                     Player.Instance.transform.position.y,
                                                     hit.point.z));
    }
}
