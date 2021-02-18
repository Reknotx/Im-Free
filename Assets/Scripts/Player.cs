using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/1/2021
/// <summary> The player class. </summary>
/// <remarks>Handles movement, rotating the player, and all of their commands.</remarks>
public class Player : SingletonPattern<Player>
{
    #region Fields
    #region Public
    /// <summary> The attack zone of the player. </summary>
    [Tooltip("The attack zone of the player.")]
    public GameObject attackZone;

    /// <summary> The transform reference to the player's parent. </summary>
    [HideInInspector] public Transform playerTrans;

    /// <summary> The speed of the player. </summary>
    [Tooltip("The speed of the player")]
    public float speed = 0.25f;

    /// <summary> Reduces player speed by fraction when attacking. </summary>
    [Tooltip("The amount of reduction applied to player speed while attacking.")]
    [Range(0.1f, 0.5f)]
    public float speedReducer = .3f;

    /// <summary> The multiplier used to increase 
    /// the force objects are punched at. </summary>
    [Tooltip("The force at which objects are punched.")]
    [Range(500, 1500)]
    public float forceModifier = 500f;
    #endregion

    #region Private
    /// <summary> The player's rigidbody on the parent. </summary>
    private Rigidbody playerRB;

    // Keeps track of our relative forward and right vectors
    Vector3 forward, right;

    /// <summary> The private field of the player's health. </summary>
    private float _health = 100f;

    public GameObject suckedEnemy;
    #endregion
    #endregion

    #region Properties
    /// <summary> Is the player attacking. </summary>
    /// <value> A value of true indicates the player is attacking. </value>
    private bool IsAttacking { get; set; } = false;

    /// <summary> The player's current health </summary>
    public float Health
    {
        get => _health;
        set
        {
            _health = value;

            UIManager.Instance.UpdateHealth();

            if (_health <= 0f)
            {
                IsDead = true;

                UIManager.Instance.DeathFade();
            }
        }
    }

    /// <summary> Flag for if the player is dead or alive. </summary>
    /// <value>The value is true if the player's health is at or below zero.</value>
    private bool IsDead { get; set; } = false;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        playerTrans = transform.parent;

        playerRB = transform.parent.GetComponent<Rigidbody>();

        // Set forward to equal the camera's forward vector
        forward = Camera.main.transform.forward;

        // make sure y is 0
        forward.y = 0;

        // make sure the length of vector is set to a max of 1.0
        forward = Vector3.Normalize(forward);

        // set the right-facing vector to be facing right relative to the camera's forward vector
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        attackZone.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D))
        {
            Move();
        }
    }

    private void Update()
    {
        if (IsDead) return;
        
        Rotate();

        if (Input.GetMouseButtonDown(0) && IsAttacking == false)
        {
            IsAttacking = true;
            StartCoroutine(Attack());
        }
        else if (Input.GetMouseButtonDown(1))
        {

        }

        Health -= Time.deltaTime;
    }

    /// Author: Chase O'Connor
    /// Date: 2/1/2021
    /// <summary> Moves the player in an isometric fashion based on their input. </summary>
    void Move()
    {
        // Our right movement is based on the right vector, movement speed, and our GetAxis command. 
        // We multiply by Time.deltaTime to make the movement smooth.
        Vector3 rightMovement = right * Input.GetAxis("Horizontal");

        // Up movement uses the forward vector, movement speed, and the vertical axis inputs.
        Vector3 upMovement = forward * Input.GetAxis("Vertical");

        // This creates our new direction. By combining our right and forward movements and normalizing them, 
        // we create a new vector that points in the appropriate direction with a length no greater than 1.0
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        if (IsAttacking)
            playerRB.MovePosition(playerTrans.position + heading * (speed * speedReducer) * Time.deltaTime);
        else
            playerRB.MovePosition(playerTrans.position + heading * speed * Time.deltaTime);

    }

    /// Author: Chase O'Connor
    /// Date 2/1/2021
    /// <summary> Rotates the player to face a the position of the mouse cursor. </summary>
    void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << 8);

        if (hit.collider == null) return;

        transform.LookAt(new Vector3(hit.point.x,
                                                     transform.position.y,
                                                     hit.point.z));
    }

    /// <summary>
    /// 
    /// </summary>
    void Suck()
    {


    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> Activates the attack zone of the player for 0.1 seconds. </summary>
    IEnumerator Attack()
    {
        attackZone.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        attackZone.SetActive(false);
        IsAttacking = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.gameObject.layer != 12 && other.gameObject.layer != 14) return;
        
        //Debug.Log("Trigger hit the shit.");

        if(other.GetComponent<Rigidbody>() != null)
        {
            Rigidbody punchedObj = other.GetComponent<Rigidbody>();

            Vector3 startVector = transform.forward;

            //Debug.Log(startVector.ToString());

            Vector3 punchDir = new Vector3(startVector.x, startVector.y += .5f, startVector.z);

            //Debug.Log("Punch direction is: (" + punchDir.x + ", " + punchDir.y + ", " + punchDir.z + ")");
            
            if (other.gameObject.layer == 14)
            {
                List<GameObject> punchedChildren = new List<GameObject>();

                foreach (Transform child in punchedObj.transform)
                {
                    child.GetComponent<Collider>().enabled = true;
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    child.GetComponent<Rigidbody>().useGravity = true;
                    punchedChildren.Add(child.gameObject);
                }

                foreach (GameObject obj in punchedChildren)
                {
                    obj.transform.parent = null;
                    obj.GetComponent<Rigidbody>().AddForce(punchDir.normalized * forceModifier);
                }

                Destroy(punchedObj.gameObject);
            }
            else
            {
                punchedObj.useGravity = true;
                punchedObj.isKinematic = false;
                punchedObj.AddForce(punchDir.normalized * forceModifier);
            }

        }
    }
}