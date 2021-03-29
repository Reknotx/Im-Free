using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    public float panningSpeed = 5f;
    
    /// <summary> The offset of the camera from the player. </summary>
    public Vector3 offset;
    
    /// <summary> The offset of the camera from the player at start. </summary>
    private Vector3 baseOffset;


    Vector3 forward, right;


    public float horizNeutZone = 6f, vertNeutZone = 4f;
    public float offsetLimitX = 3f, offsetLimitZ = 3f;

    private float origOffsetLimitX, origOffsetLimitZ;

    public GameObject playerObj;

    float horizScrnFrac, vertScrnFrac;

    private void Start()
    {
        #region Isometric stuff from player.
        // Set forward to equal the camera's forward vector
        forward = Camera.main.transform.forward;

        // make sure y is 0
        //forward.y = Camera.main.transform.position.y;
        forward.y = 0;

        // make sure the length of vector is set to a max of 1.0
        forward = Vector3.Normalize(forward);

        // set the right-facing vector to be facing right relative to the camera's forward vector
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        #endregion

        if (playerObj == null)
            playerObj = Player.Instance.gameObject;

        offset = transform.localPosition - playerObj.transform.position;
        baseOffset = offset;

        origOffsetLimitX = offsetLimitX;
        origOffsetLimitZ = offsetLimitZ;
        horizScrnFrac = GetScrnFrac(true, 2f);
        vertScrnFrac = GetScrnFrac(false, 2f);
    }

    private void Update()
    {
        #region Isometric stuff from player

        #endregion

        transform.localPosition = playerObj.transform.position + offset;

        PanCamera();

        offset.x = Mathf.Clamp(offset.x, baseOffset.x - offsetLimitX, baseOffset.x + offsetLimitX);
        offset.z = Mathf.Clamp(offset.z, baseOffset.z - offsetLimitZ, baseOffset.z + offsetLimitZ);
    }

    /// <summary> Determines how far and in what direction the camera needs to move from the player. </summary>
    private void PanCamera()
    { 
        float xModifier = Mathf.Abs(Input.mousePosition.x - horizScrnFrac) / horizScrnFrac;
        float zModifier = Mathf.Abs(Input.mousePosition.y - vertScrnFrac) / vertScrnFrac;

        // Our right movement is based on the right vector, movement speed, and our GetAxis command. 
        // We multiply by Time.deltaTime to make the movement smooth.
        Vector3 rightMovement = right * xModifier;

        // Up movement uses the forward vector, movement speed, and the vertical axis inputs.
        Vector3 upMovement = forward * zModifier;

        // This creates our new direction. By combining our right and forward movements and normalizing them, 
        // we create a new vector that points in the appropriate direction with a length no greater than 1.0
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        if (Input.mousePosition.x < horizScrnFrac - GetScrnFrac(true, horizNeutZone))
        {
            //offset.x -= xModifier * panningSpeed * Time.deltaTime;
            offset -= heading * panningSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x > horizScrnFrac + GetScrnFrac(true, horizNeutZone))
        {
            offset += heading * panningSpeed * Time.deltaTime;
        }
        else
        {
            offset.x = Mathf.Lerp(offset.x, baseOffset.x, panningSpeed * 0.5f * Time.deltaTime);
        }

        if (Input.mousePosition.y < vertScrnFrac - GetScrnFrac(false, vertNeutZone))
        {
            offset -= heading * panningSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y > vertScrnFrac + GetScrnFrac(false, vertNeutZone))
        {
            offset += heading * panningSpeed * Time.deltaTime;
            //offset.z += zModifier * panningSpeed * Time.deltaTime;
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
