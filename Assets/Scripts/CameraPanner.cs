using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    public float panningSpeed = 5f;
    public Vector3 offset;
    private Vector3 baseOffset;

    public float horizNeutZone = 6f, vertNeutZone = 4f;
    public float offsetLimitX = 3f, offsetLimitZ = 3f;

    private float origOffsetLimitX, origOffsetLimitZ;

    public GameObject playerObj;

    float horizScrnFrac, vertScrnFrac;

    private void Start()
    {
        if (playerObj == null)
            playerObj = Player.Instance.gameObject;

        offset = transform.localPosition - Player.Instance.playerTrans.TransformPoint(playerObj.transform.position);
        baseOffset = offset;

        origOffsetLimitX = offsetLimitX;
        origOffsetLimitZ = offsetLimitZ;
        horizScrnFrac = GetScrnFrac(true, 2f);
        vertScrnFrac = GetScrnFrac(false, 2f);
    }

    private void Update()
    {
        transform.position = playerObj.transform.position + Player.Instance.playerTrans.TransformPoint(offset);

        PanCamera();

        offset.x = Mathf.Clamp(offset.x, baseOffset.x - offsetLimitX, baseOffset.x + offsetLimitX);
        offset.z = Mathf.Clamp(offset.z, baseOffset.z - offsetLimitZ, baseOffset.z + offsetLimitZ);
    }

    private void PanCamera()
    {


        float xModifier = Mathf.Abs(Input.mousePosition.x - horizScrnFrac) / horizScrnFrac;
        float zModifier = Mathf.Abs(Input.mousePosition.y - vertScrnFrac) / vertScrnFrac;

        if (Input.mousePosition.x < horizScrnFrac - GetScrnFrac(true, horizNeutZone))
        {
            offset.x -= xModifier * panningSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x > horizScrnFrac + GetScrnFrac(true, horizNeutZone))
        {
            offset.x += xModifier * panningSpeed * Time.deltaTime;
        }
        else
        {
            offset.x = Mathf.Lerp(offset.x, baseOffset.x, panningSpeed * 0.5f * Time.deltaTime);
        }

        if (Input.mousePosition.y < vertScrnFrac - GetScrnFrac(false, vertNeutZone))
        {
            offset.z -= zModifier * panningSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y > vertScrnFrac + GetScrnFrac(false, vertNeutZone))
        {
            offset.z += zModifier * panningSpeed * Time.deltaTime;
        }
        else
        {
            offset.z = Mathf.Lerp(offset.z, baseOffset.z, panningSpeed * 0.4f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Returns either width or height divided by amountToDivide
    /// </summary>
    /// <param name="widthOrHeight">True to get width, false to get height</param>
    /// <param name="amountToDivide">Number to divide the width or height by</param>
    /// <returns></returns>
    private float GetScrnFrac(bool widthOrHeight, float amountToDivide)
    {
        if (widthOrHeight)
            return Screen.currentResolution.width / amountToDivide;
        else
            return Screen.currentResolution.height / amountToDivide;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Rect rect = new Rect(GetScrnFrac(true, 2f) - GetScrnFrac(true, horizNeutZone),
                             GetScrnFrac(false, 2f) - GetScrnFrac(false, vertNeutZone),
                             (GetScrnFrac(true, 2f) + GetScrnFrac(true, horizNeutZone)) - (GetScrnFrac(true, 2f) - GetScrnFrac(true, horizNeutZone)),
                             (GetScrnFrac(false, 2f) + GetScrnFrac(false, vertNeutZone)) - (GetScrnFrac(false, 2f) - GetScrnFrac(false, vertNeutZone)));
        UnityEditor.Handles.BeginGUI();
        UnityEditor.Handles.DrawSolidRectangleWithOutline(rect, Color.clear, Color.red);
        UnityEditor.Handles.EndGUI();
    }
#endif
}
