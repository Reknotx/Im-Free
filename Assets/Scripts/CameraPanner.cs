using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    Vector3 startPos;


    public float panningSpeed = 5f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset;
    private Vector3 baseOffset;

    public float horizNeutZone = 5f, vertNeutZone = 5f;
    public float offsetLimitX = 1f, offsetLimitZ = 1f;

    private float origOffsetLimitX, origOffsetLimitZ;

    public GameObject playerObj;

    private void Start()
    {
        if (playerObj == null)
        {
            playerObj = Player.Instance.gameObject;
        }

        startPos = transform.position;
        baseOffset = offset = transform.localPosition - Player.Instance.playerTrans.TransformPoint(playerObj.transform.position);

        origOffsetLimitX = offsetLimitX;
        origOffsetLimitZ = offsetLimitZ;
    }

    private void Update()
    {
        transform.position = playerObj.transform.position + Player.Instance.playerTrans.TransformPoint(offset);


        panCamera();
        //CameraMovement();

        offset.x = Mathf.Clamp(offset.x, baseOffset.x - offsetLimitX, baseOffset.x + offsetLimitX);
        offset.z = Mathf.Clamp(offset.z, baseOffset.z - offsetLimitZ, baseOffset.z + offsetLimitZ);
    }

    private void panCamera()
    {


        float xModifier = Mathf.Abs(Input.mousePosition.x - getScrnFrac(true, 2f)) / getScrnFrac(true, 2f);
        float zModifier = Mathf.Abs(Input.mousePosition.y - getScrnFrac(false, 2f)) / getScrnFrac(false, 2f);


        if (Input.mousePosition.x < getScrnFrac(true, 2f) - getScrnFrac(true, horizNeutZone))
        {
            offset.x -= xModifier * panningSpeed * Time.deltaTime;

        }
        else if (Input.mousePosition.x > getScrnFrac(true, 2f) + getScrnFrac(true, horizNeutZone))
        {
            offset.x += xModifier * panningSpeed * Time.deltaTime;

        }
        else
        {
            //offset.x = Mathf.SmoothStep(offset.x, baseOffset.x, panningSpeed * 3f * Time.deltaTime);
            offset.x = Mathf.Lerp(offset.x, baseOffset.x, panningSpeed * 0.5f * Time.deltaTime);
        }

        if (Input.mousePosition.y < getScrnFrac(false, 2f) - getScrnFrac(false, vertNeutZone))
        {
            offset.z -= zModifier * panningSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y > getScrnFrac(false, 2f) + getScrnFrac(false, vertNeutZone))
        {
            offset.z += zModifier * panningSpeed * Time.deltaTime;
        }
        else
        {
            //offset.z = Mathf.SmoothStep(offset.z, baseOffset.z, panningSpeed * 3f * Time.deltaTime);
            offset.z = Mathf.Lerp(offset.z, baseOffset.z, panningSpeed * 0.4f * Time.deltaTime);
        }
    }


    void CameraMovement()
    {


        ///take out 45 degree rot val
        ///calculate
        ///put 45 back in
    }

    private float getScrnFrac(bool widthOrHeight, float amountToDivide)
    {
        float result;
        if (widthOrHeight)
        {
            result = Screen.currentResolution.width / amountToDivide;
        }
        else
        {
            result = Screen.currentResolution.height / amountToDivide;
        }

        return result;
    }

}
