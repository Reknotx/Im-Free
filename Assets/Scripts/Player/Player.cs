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

    /// <summary> The duration that we suck an enemy for. </summary>
    [Tooltip("The duration that we suck an enemy for.")]
    public float suckDuration = 3f;

    public Transform dartHolder;

    public Animator animController;

    #endregion

    #region Private
    /// <summary> The player's rigidbody on the parent. </summary>
    private Rigidbody playerRB;

    // Keeps track of our relative forward and right vectors
    Vector3 forward, right;

    /// <summary> The private field of the player's health. </summary>
    private float _health = 5f;

    private int _tranqDartStack = 0;

    private float _tranqDartSlow = 0;

    private GameObject suckedEnemy;


    private int PunchLayerMask = ((1 << 9) | (1 << 12) | (1 << 14));

    [SerializeField]
    private GameObject deathAnim;
    #endregion
    #endregion

    #region Properties
    #region Public

    /// <summary> The player's current health </summary>
    public float Health
    {
        get => _health;
        set
        {
            _health = value;

            _health = Mathf.Clamp(_health, 0f, 100f);

            PlayerUIManager.Instance?.UpdateHealth();

            if (_health <= 0f)
            {
                IsDead = true;

                deathAnim.SetActive(true);

                animController.SetBool("IsWalking", false);
                animController.SetBool("IsDead", true);

                animController.applyRootMotion = true;

                //PlayerUIManager.Instance?.DeathFade();

                //LeaderBoard.Instance.GameOver(ScoreManager.Instance.Score);
            }
        }
    }

    public int TranqDartStack
    {
        get => _tranqDartStack;
        set
        {
            _tranqDartStack = Mathf.Clamp(value, 0, 4);
        }
    }

    public float TranqDartSlow
    {
        get
        {
            float total = 0f;

            foreach (Transform dart in dartHolder)
            {
                total += dart.gameObject.GetComponent<Bullet>().decayValue;
            }

            return total;
        }
    }
    #endregion

    #region Private
    /// <summary> Is the player attacking. </summary>
    /// <value> A value of true indicates the player is attacking. </value>
    private bool IsAttacking { get; set; } = false;

    /// <summary> Is the player lurching. </summary>
    /// <value> A value of true indicates the player is lurching forward. </value>
    private bool IsLurching { get; set; } = false;

    /// <summary> Is the player sucking an enemy. </summary>
    /// <value> A value of true indicates the player is sucking an enemy currently.</value>
    private bool IsSucking { get; set; } = false;

    /// <summary> Flag for if the player is dead or alive. </summary>
    /// <value>The value is true if the player's health is at or below zero.</value>
    private bool IsDead { get; set; } = false;
    #endregion
    #endregion

    protected override void Awake()
    {
        base.Awake();

        playerTrans = transform.parent;

        playerRB = transform.parent.GetComponent<Rigidbody>();

        // Set forward to equal the camera's forward vector
        forward = Camera.main.transform.forward;
        //Debug.Log(forward);

        // make sure y is 0
        forward.y = 0;

        // make sure the length of vector is set to a max of 1.0
        forward = Vector3.Normalize(forward);

        // set the right-facing vector to be facing right relative to the camera's forward vector
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        attackZone.SetActive(false);

        deathAnim.SetActive(false);
    }

    #region Updates
    private void FixedUpdate()
    {
        if (IsDead || Tutorial.Instance.gameObject.activeSelf) return;

        if (IsLurching || IsSucking) return;

        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D))
        {
            animController.SetBool("IsWalking", true);
            Move();
        }
        else
        {
            animController.SetBool("IsWalking", false);
        }
    }

    private void Update()
    {
        if (IsDead
            || Tutorial.Instance.gameObject.activeSelf
            /*|| MenuManager.Instance.gameObject.activeSelf*/) 
            return;
        

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu.Instance.gameObject.SetActive(!PauseMenu.Instance.gameObject.activeSelf);

        if (Time.timeScale == 0f
            || PauseMenu.Instance.gameObject.activeSelf)
            return;

        Rotate();

        if (!IsLurching && Input.GetMouseButtonDown(1))
        {
            ///Lurch forward
            IsLurching = true;
            StartCoroutine(Lurch());
        }
        else if (IsSucking && Input.GetMouseButtonUp(1))
        {
            //Debug.Log("suck");
            ///End the suck early
            IsSucking = false;

            StopAllCoroutines();

            if (suckedEnemy != null)
            {
                Destroy(suckedEnemy);
            }

        }
        else if (Input.GetMouseButtonDown(0) && IsAttacking == false)
        {
            ///Attack the enemy
            IsAttacking = true;
            StartCoroutine(Attack());
        }

        Health -= Time.deltaTime;
    }
    #endregion

    #region Movement
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

        float tranqReducer = Mathf.Clamp(TranqDartSlow, 0f, 0.8f);


        if (IsAttacking)
            playerRB.MovePosition(playerTrans.position + heading * ((speed * speedReducer) - ((speed * speedReducer) * tranqReducer)) * Time.deltaTime);
        else
            playerRB.MovePosition(playerTrans.position + heading * (speed - (speed * tranqReducer)) * Time.deltaTime);

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
    #endregion

    #region Attacks
    /// <summary>
    /// 
    /// </summary>
    IEnumerator Suck()
    {
        float timeStart = Time.time;

        bool sucking = true;

        float p0 = Health;
        float p1 = Health + 30f;
        float p01 = 0;

        while (sucking)
        {
            float u = (Time.time - timeStart) / suckDuration;

            if (u >= 1)
            {
                u = 1;
                sucking = false;
            }

            p01 = (1 - u) * p0 + u * p1;

            Health = p01;

            yield return new WaitForFixedUpdate();
        }

        IsSucking = false;
        Destroy(suckedEnemy);

    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> Activates the attack zone of the player for 0.1 seconds. </summary>
    IEnumerator Attack()
    {
        attackZone.SetActive(true);

        animController.SetTrigger("CastAttack");

        #region Dart Removal
        TranqDartStack = 0;


        List<Bullet> dartRemove = new List<Bullet>();

        foreach (Transform darts in dartHolder)
        {
            dartRemove.Add(darts.GetComponent<Bullet>());
        }

        foreach (Bullet dart in dartRemove)
        {
            dart.GetComponent<Bullet>().ShakeOff();
        }
        
        #endregion

        yield return new WaitForSeconds(0.1f);

        attackZone.SetActive(false);
        IsAttacking = false;

    }

    IEnumerator Lurch()
    {
        attackZone.SetActive(true);
        animController.SetTrigger("CastLurch");
        Vector3 p0 = playerTrans.position;
        Vector3 p1 = playerTrans.position + transform.forward;
        Vector3 p01;
        Vector3 heading = p1 - playerTrans.position;

        bool moving = true;

        float timeStart = Time.time;

        while (moving)
        {
            float u = (Time.time - timeStart) / .1f;

            if (u >= 1)
            {
                u = 1;
                moving = false;
            }

            p01 = (1 - u) * p0 + u * p1;

            playerRB.position = p01;

            yield return new WaitForFixedUpdate();
        }

        IsLurching = false;
        attackZone.SetActive(false);
    }

    private void PunchLogic(Collider other)
    {
        ///Sanity check just in case
        ///
        //Debug.Log(other.gameObject.layer);
        //Debug.Log(PunchLayerMask);
        if (other.gameObject.layer == 15)
        {
            Destroy(other.gameObject);
            return;
        }

        if ((
                other.gameObject.layer != 9
                || other.gameObject.layer != 12
                || other.gameObject.layer != 14
            )
            && other.gameObject.GetComponent<Rigidbody>() == null) return;
        
        Rigidbody punchedObj = other.GetComponent<Rigidbody>();
        
        Vector3 startVector = transform.forward;

        //Debug.Log(startVector.ToString());

        Vector3 punchDir = new Vector3(startVector.x, startVector.y += .5f, startVector.z);

        //Debug.Log("Punch direction is: (" + punchDir.x + ", " + punchDir.y + ", " + punchDir.z + ")");
        
        ///Punched a punchable parent obj
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
            ///Punched a punchable obj or enemy

            punchedObj.useGravity = true;
            punchedObj.isKinematic = false;

            if (punchedObj.gameObject.layer == 9 || punchedObj.gameObject.layer == 18)
            {
                Debug.Log("Punched enemy");
                punchedObj.GetComponent<Enemy>().IsAttacked = true;
            }
            else
            {
                punchedObj.GetComponent<PunchableObj>().Punched();
            }

            punchedObj.AddForce(punchDir.normalized * forceModifier);
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (IsLurching && (other.gameObject.layer == 9 || other.gameObject.layer == 18) && !IsSucking)
        {
            Debug.Log("hit enemy in lurch");
            IsLurching = false;
            StopCoroutine(Lurch());
            suckedEnemy = other.gameObject;
            other.GetComponent<Enemy>().IsAttacked = true;
            IsSucking = true;
            StartCoroutine(Suck());

        }
        else if (IsAttacking)
        {
            PunchLogic(other);
        }
    }
}