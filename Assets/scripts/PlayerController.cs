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
    Dead
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance {  get; private set; }

    [SerializeField] private float targetRange = 5f;
    [SerializeField] private float miniGameRange = 0.5f;
    [SerializeField] private float miniGameTimer = 3f;
    [SerializeField] private float speedOfTrans = 2f;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private TMP_Text coinsText;
    private int coinsCollected;


    private float miniGameStartTimer = 0;

    [SerializeField] private ReactionBarMechanic reactionBarObject;


    private GameObject nextTarget;
    private MasterController _mController;

    private MosquitoStates currentState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _mController = GetComponentInParent<MasterController>();
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
        }
    }

    private void CheckDistanceToTarget()
    {
        if (!LevelManager.Instance.HasTarget())
            return;

        float distanceToNextTarget = Vector3.Distance(transform.position, LevelManager.Instance.GetCurrentTargetPosition());

        // Check distance to next target and move towards it
        if (distanceToNextTarget <= targetRange)
        {
            // Start Moving Towards target
            _mController.StopCart();// only stop logic after implementing states we can use start also (use W to move again).
            UpdateMosquitoState(MosquitoStates.EnterMiniGame);
        }
    }

    private void Move()
    {
        float movePosX = transform.localPosition.x + Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float movePosY = transform.localPosition.y + Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Check max right Position
        if (movePosX > 3.25f)
        {
            movePosX = 3.25f;
        }

        // Check max left position
        if (movePosX < -3.25f)
        {
            movePosX = -3.25f;
        }

        if (movePosY > 3.25f)
        {
            movePosY = 3.25f;
        }

        if (movePosY < -3.25f)
        {
            movePosY = -3.25f;
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

        Debug.Log(miniGameStartTimer);
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

            UpdateMosquitoState(MosquitoStates.Moving);

            return;
        }
        Vector3 moveDir = (Vector3.zero - transform.localPosition).normalized;

        transform.localPosition += moveDir * speedOfTrans * Time.deltaTime;
        transform.localEulerAngles = Vector3.zero;
    }

    private void UpdateMosquitoState(MosquitoStates mosquitoStates)
    {
        currentState = mosquitoStates;

        switch (currentState)
        {
            case MosquitoStates.Moving:
                _mController.StartCart();
                break;

            case MosquitoStates.EnterMiniGame:
                //reactionBarObject.gameObject.SetActive(true);
                //reactionBarObject.StartMiniGame();
                miniGameStartTimer = 0f;
                break;

            case MosquitoStates.InMiniGame:
                reactionBarObject.gameObject.SetActive(true);
                reactionBarObject.StartMiniGame();
                break;

            case MosquitoStates.Returning:

                LevelManager.Instance.UpdateCurrentTarget();
                reactionBarObject.gameObject.SetActive(false);
                break;

            case MosquitoStates.Dead:
                break;
        }
    }

    public void MiniGameCompleted(bool youWon)
    {
        if (youWon)
        {
            UpdateMosquitoState(MosquitoStates.Returning);
        }
        else
        {
            UpdateMosquitoState(MosquitoStates.Returning);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coins"))
        {
            Debug.Log(other.gameObject);
            Destroy(other.gameObject);

            coinsCollected += 1;
            coinsText.text =coinsCollected.ToString();
        }

        if (other.CompareTag("Rings"))
        {
            Debug.Log(other.gameObject);
            Destroy(other.gameObject);

            coinsCollected += 10;
            coinsText.text = coinsCollected.ToString();
        }
    }
}