using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MosquitoStates
{
    Moving,
    MiniGame,
    Returning,
    Dead
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance {  get; private set; }

    [SerializeField] private float targetRange = 5f;

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

            case MosquitoStates.MiniGame:
                MoveToTarget();
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
            UpdateMosquitoState(MosquitoStates.MiniGame);
        }
    }

    private void Move()
    {
        float movePosX = transform.localPosition.x + Input.GetAxis("Horizontal") * 5 * Time.deltaTime;

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

        transform.localPosition = new Vector3(movePosX,
            transform.localPosition.y, transform.localPosition.z);

    }

    private void MoveToTarget()
    {
        // Move Towards Target in slow speed while reaction bar mini game is active
        Vector3 targetPosition = LevelManager.Instance.GetCurrentTargetPosition();
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        transform.position += moveDir * Time.deltaTime;
        transform.LookAt(targetPosition);


        // If missied reaction bar time till this distance kill the player
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            // This should be dead state instead of returning -- but keep it as returning to test things and until we add death logic
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

        transform.localPosition += moveDir * Time.deltaTime;
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

            case MosquitoStates.MiniGame:
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
            UpdateMosquitoState(MosquitoStates.Dead);
        }
    }

}