using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MosquitoStates
{
    Moving,
    EnterMiniGame,
    InMiniGame,
    Returning,
    Dead,
    Completed
}

public class PlayerController : MonoBehaviour
{
    public event EventHandler<bool> OnGameFinished;

    public static PlayerController Instance {  get; private set; }

    [SerializeField] private Joystick joystick;
    [SerializeField] private Canvas joystickCanvas; 

    [SerializeField] private float targetRange = 5f;
    [SerializeField] private float miniGameRange = 0.5f;
    [SerializeField] private float miniGameTimer = 3f;
    [SerializeField] private float speedOfTrans = 2f;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private TMP_Text coinsText;
    private int coinsCollected;


    private float miniGameStartTimer = 0;

    [SerializeField] private ReactionBarMechanic reactionBarObject;
    private HealthSystem health;


    private GameObject nextTarget;
    private MasterController masterController;
    private PlayerStats playerStats;

    private MosquitoStates currentState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        masterController = GetComponentInParent<MasterController>();
        health = GetComponentInParent<HealthSystem>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        masterController.OnPathFinished += MasterController_OnPathFinished;

        health.OnDead += Health_OnDead;
        health.SetArmourAmount(playerStats.GetArmourAmount());

        joystick.enabled = true;

        float speedModifier = playerStats.GetSpeedAmount() * 0.2f;
        if(speedModifier > 2)
        {
            speedModifier = 2;
        }


        masterController.SetMasterSpeed(speedModifier);
        moveSpeed += speedModifier;
        Debug.Log(playerStats.GetSpeedAmount());
        Debug.Log(moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case MosquitoStates.Moving:
                CheckDistanceToTarget();
                Move();
                break;

            case MosquitoStates.EnterMiniGame:
                MoveToTarget();
                break;

            case MosquitoStates.InMiniGame:
                MiniGameTimer();
                break;

            case MosquitoStates.Returning:
                MoveToRoot();
                break;

            case MosquitoStates.Dead:   
                break;

            case MosquitoStates.Completed:
                break;
        }
    }

    private void CheckDistanceToTarget()
    {
        Debug.Log(LevelManager.Instance.GetCurrentTargetPosition());


        if (!LevelManager.Instance.HasTarget())
            return;

        float rootDistanceToNextTarget = Vector3.Distance(transform.parent.position, LevelManager.Instance.GetCurrentTargetPosition());
        float distanceToNextTarget = Vector3.Distance(transform.position, LevelManager.Instance.GetCurrentTargetPosition());


        // Check distance to next target and move towards it
        if (distanceToNextTarget <= targetRange || rootDistanceToNextTarget <= targetRange)
        {
            // Start Moving Towards target
            masterController.StopCart();// only stop logic after implementing states we can use start also (use W to move again).
            UpdateMosquitoState(MosquitoStates.EnterMiniGame);
        }
    }

    private void Move()
    {
        float movePosX;
        float movePosY;
//#if UNITY_EDITOR

//        movePosX = transform.localPosition.x + Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
//        movePosY = transform.localPosition.y + Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

//#endif

        movePosX = transform.localPosition.x + joystick.Horizontal * moveSpeed * Time.deltaTime;
        movePosY = transform.localPosition.y + joystick.Vertical * moveSpeed * Time.deltaTime;


        // Check max right Position
        if (movePosX > 1.25f)
        {
            movePosX = 1.25f;
        }

        // Check max left position
        if (movePosX < -1.25f)
        {
            movePosX = -1.25f;
        }

        if (movePosY > 1.25f)
        {
            movePosY = 1.25f;
        }

        if (movePosY < -1.25f)
        {
            movePosY = -1.25f;
        }

        transform.localPosition = new Vector3(movePosX,
            movePosY, transform.localPosition.z);

    }

    private void MoveToTarget()
    {
        // Move Towards Target in slow speed while reaction bar mini game is active
        Vector3 targetPosition = LevelManager.Instance.GetCurrentTargetPosition();
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        transform.position += moveDir * speedOfTrans * Time.deltaTime;
        transform.LookAt(targetPosition);


        // If missied reaction bar time till this distance kill the player
        // If we are in minigame range start minigame
        if (Vector3.Distance(transform.position, targetPosition) < miniGameRange)
        {
            UpdateMosquitoState(MosquitoStates.InMiniGame);
        }
    }

    private void MiniGameTimer()
    {

        miniGameStartTimer += Time.deltaTime;

        if (miniGameStartTimer >= miniGameTimer)
        {
            UpdateMosquitoState(MosquitoStates.Returning);
        }
    }


    private void MoveToRoot()
    {
        if (Vector3.Distance(transform.localPosition, Vector3.zero) < 0.5f)
        {
            transform.localPosition = Vector3.zero;

            transform.localRotation = Quaternion.identity;   

            UpdateMosquitoState(MosquitoStates.Moving);

            return;
        }
        Vector3 moveDir = (Vector3.zero - transform.localPosition).normalized;

        transform.localPosition += moveDir * speedOfTrans * Time.deltaTime;
        transform.localEulerAngles = Vector3.zero;

        transform.LookAt(moveDir);

    }

    private void UpdateMosquitoState(MosquitoStates mosquitoStates)
    {
        currentState = mosquitoStates;

        switch (currentState)
        {
            case MosquitoStates.Moving:
                masterController.StartCart();
                joystick.enabled = true;
                joystickCanvas.enabled = true;
                break;

            case MosquitoStates.EnterMiniGame:
                //reactionBarObject.gameObject.SetActive(true);
                //reactionBarObject.StartMiniGame();
                miniGameStartTimer = 0f;
                joystick.enabled = false;
                joystickCanvas.enabled = false;
                break;

            case MosquitoStates.InMiniGame:
                reactionBarObject.gameObject.SetActive(true);
                reactionBarObject.StartMiniGame(false);
                break;

            case MosquitoStates.Returning:

                LevelManager.Instance.UpdateCurrentTarget();
                reactionBarObject.gameObject.SetActive(false);
                break;

            case MosquitoStates.Dead:
                OnGameFinished?.Invoke(this, false);
                joystick.enabled = false;
                masterController.StopCart();
                break;

            case MosquitoStates.Completed:
                OnGameFinished?.Invoke(this, true);
                joystick.enabled = false;
                GameManager.Instance.AddCoin(coinsCollected);
                break;
        }
    }

    // Start mini game again with warned state if reaction bar is stopped at yellow bar first time
    public void StartMiniGameWithWarning()
    {
        reactionBarObject.StartMiniGame(true);
        miniGameStartTimer = 0;
    }

    public void MiniGameCompleted(bool youWon)
    {
        if (youWon)
        {
            UpdateMosquitoState(MosquitoStates.Returning);
        }
        else
        {
            health.Damage(10);

            UpdateMosquitoState(MosquitoStates.Returning);
        }
    }

    private void MasterController_OnPathFinished(object sender, EventArgs eventArgs)
    {
        UpdateMosquitoState(MosquitoStates.Completed);
    }

    private void Health_OnDead(object sender, System.EventArgs eventArgs)
    {

        UpdateMosquitoState(MosquitoStates.Dead);
        //transform.parent.gameObject.SetActive(false);
        //Destroy(transform.parent.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coins"))
        {
            Debug.Log(other.gameObject);
            Destroy(other.gameObject);

            coinsCollected += 1;
            coinsText.text = coinsCollected.ToString();

        }

        if (other.CompareTag("Rings"))
        {
            Debug.Log(other.gameObject);
            Destroy(other.gameObject);

            coinsCollected += 10;
            coinsText.text = coinsCollected.ToString();
        }
    }

    public int GetBloodVailAmount() => playerStats.GetBloodAmount();
}