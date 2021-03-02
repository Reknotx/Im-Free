using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    Vector3 startPos;

    Transform target;

    float dampTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        startPos = transform.position;
        target = Player.Instance.transform;
    }

    private void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        RaycastHit info;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out info, 1000f, 1 << 8)) return;

        float maxScreenPoint = 0.8f;

        //Vector3 mousePos = Input.mousePosition * maxScreenPoint + new Vector3(Screen.width, 0f, Screen.height) * ((1f - maxScreenPoint) * 0.5f);

        //Vector3 position = (Player.Instance.transform.localPosition + Camera.main.ScreenToWorldPoint(mousePos)) / 2f;

        //Vector3 destination = new Vector3(position.x, 0, position.z);

        //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, destination, ref velocity, dampTime);


        Vector3 mousePos = Input.mousePosition * maxScreenPoint + new Vector3(Screen.width, 0f, Screen.height) * ((1f - maxScreenPoint) * 0.5f);
        //Vector3 position = (target.position + Camera.main.ScreenToWorldPoint(Input.mousePosition)) / 2f;
        Vector3 camPos = Camera.main.ScreenToWorldPoint(mousePos);

        //if (Vector3.Distance(camPos, target.position) < 5f) return;

        Vector3 position = new Vector3(target.position.x + camPos.x, 0f, target.position.z + camPos.z) / 2f;
        Vector3 destination = new Vector3(position.x, 0, position.z);
        transform.parent.localPosition = Vector3.SmoothDamp(transform.parent.localPosition, destination, ref velocity, dampTime);

        ///take out 45 degree rot val
        ///calculate
        ///put 45 back in
    }

}
