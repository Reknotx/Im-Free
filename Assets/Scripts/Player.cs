using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/1/2021
/// <summary> The player class. </summary>
public class Player : SingletonPattern<Player>
{
    #region Field
    [Tooltip("The speed of the player")]
    public float speed = 0.25f;

    //[Tooltip("Modifer to how quick the player lurches on their attack")]
    //[Range(0.5f, 5f)]
    //public float attackLurchSpeed = 3f;

    [Tooltip("The amount of reduction applied to player speed while attacking.")]
    [Range(0.1f, 0.5f)]
    public float speedReducer = .3f;

    [Tooltip("The force at which objects are punched.")]
    [Range(500, 1500)]
    public float forceModifier = 500f;

    /// <summary> The attack zone of the player. </summary>
    [Tooltip("The attack zone of the player.")]
    public GameObject attackZone;

    bool isAttacking = false;

    /// <summary> The transform reference to the player's parent. </summary>
    [HideInInspector] public Transform playerTrans;

    //[HideInInspector] public bool moving = false;
    
    Vector3 forward, right; // Keeps track of our relative forward and right vectors
    #endregion

    protected override void Awake()
    {
        base.Awake();

        playerTrans = transform.parent;

        forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector

        forward.y = 0; // make sure y is 0
        
        forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
        
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector

        attackZone.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D))
        {
            Move();
        }

        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    void Move()
    {
        // Our right movement is based on the right vector, movement speed, and our GetAxis command. We multiply by Time.deltaTime to make the movement smooth.
        Vector3 rightMovement = right * Input.GetAxis("Horizontal");

        // Up movement uses the forward vector, movement speed, and the vertical axis inputs.
        Vector3 upMovement = forward * Input.GetAxis("Vertical");

        // This creates our new direction. By combining our right and forward movements and normalizing them, we create a new vector that points in the appropriate direction with a length no greater than 1.0
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        if (isAttacking)
            playerTrans.position += heading * (speed * speedReducer) * Time.deltaTime;
        else
            playerTrans.position += heading * speed * Time.deltaTime;

    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary>
    /// Activates the attack zone of the player for 0.1 seconds.
    /// </summary>
    IEnumerator Attack()
    {
        attackZone.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        attackZone.SetActive(false);
        isAttacking = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 12) return;
        
        Debug.Log("Trigger hit the shit.");

        if(other.GetComponent<Rigidbody>() != null)
        {
            Rigidbody punchedObj = other.GetComponent<Rigidbody>();

            punchedObj.useGravity = true;
            punchedObj.isKinematic = false;

            Vector3 startVector = transform.forward;

            Debug.Log(startVector.ToString());

            Vector3 punchDir = new Vector3(startVector.x, startVector.y += .5f, startVector.z);

            Debug.Log("Punch direction is: (" + punchDir.x + ", " + punchDir.y + ", " + punchDir.z + ")");

            punchedObj.AddForce(punchDir.normalized * forceModifier);
        }
    }
}