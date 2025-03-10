using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ReactionBarMechanic : MonoBehaviour
{
    public RectTransform pointer;
    public RectTransform GreenPart;


    [Header("")]
    [Range(0f, 100f)]
    public float _value = 0f;

    public float minValue = 0f;
    public float maxValue = 100f;

    public float lerpSpeed = 2f;

    public bool isMiniGameRunning = true;

    public int greenMiddleValue = 0;
    public int yellowMiddleValue = 0;

    // not nessesary ....
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private bool isWarned = false;

    private void Start()
    {
        //StartMiniGame();


    }


    private void Update()
    {

        if (isMiniGameRunning)
        {
            // oscillating value between 0 and 1
            float t = Mathf.PingPong(Time.time * lerpSpeed, 1);
            _value = Mathf.Lerp(minValue, maxValue, t);
            _value = Mathf.Clamp(_value, 0f, 100f);
        }

        ValueChange(_value);
    }



    public void StartMiniGame(bool _isWarned)
    {
        //setGreen part 
        //greenMiddleValue = Random.Range(0,91);
        //greenMiddleValue = 45;
        //float containAngle = -1.8f * greenMiddleValue;
        //Debug.LogError(Mathf.Clamp(containAngle, -162, 0));

        //GreenPart.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(containAngle, -162, 0));

        isWarned = _isWarned;

        if (isWarned)
        {
            GreenPart.gameObject.SetActive(false);

            yellowMiddleValue = 22; // + 56

        }
        else
        {
            GreenPart.gameObject.SetActive(true);

            greenMiddleValue = 45;
            yellowMiddleValue = 22; // + 56
        }



        isMiniGameRunning = true;
    }

    public void GetResult()
    {
        isMiniGameRunning = false;

        if (isWarned)
            CheckForWarnedState();
        else
            CheckForNormalState();


        //if (greenMiddleValue <= _value && _value <= (greenMiddleValue + 10))
        //{
        //    Debug.Log("You won !!!");
        //    OnWin.Invoke();

        //    PlayerController.Instance.MiniGameCompleted(true);
        //    GameManager.Instance.MiniGameCompleted(true, 10);
        //}
        //else if (_value >= yellowMiddleValue && _value <= (yellowMiddleValue + 56))
        //{
        //    Debug.Log("YELLOW");
        //    //OnWin.Invoke();

        //    //PlayerController.Instance.MiniGameCompleted(true);
        //    //GameManager.Instance.MiniGameCompleted(true, 5);

        //    if (isWarned)
        //    {
        //        OnWin.Invoke();

        //        PlayerController.Instance.MiniGameCompleted(true);
        //        GameManager.Instance.MiniGameCompleted(true, 5);
        //    }
        //    else
        //    {
        //        PlayerController.Instance.StartMiniGameWithWarning();
        //    }

        //}
        //else
        //{
        //    Debug.Log("You lost !!!");
        //    OnLose.Invoke();

        //    PlayerController.Instance.MiniGameCompleted(false);
        //    GameManager.Instance.MiniGameCompleted(false, 10);
        //}

        //Debug.Log(_value);

    }

    private void CheckForNormalState()
    {
        if (greenMiddleValue <= _value && _value <= (greenMiddleValue + 10))
        {
            Debug.Log("You won !!!");
            OnWin.Invoke();

            PlayerController.Instance.MiniGameCompleted(true);
            GameManager.Instance.MiniGameCompleted(10);
        }
        else if (_value >= yellowMiddleValue && _value <= (yellowMiddleValue + 56))
        {
            Debug.Log("YELLOW");
            PlayerController.Instance.StartMiniGameWithWarning();
        }
        else
        {
            Debug.Log("You lost !!!");
            OnLose.Invoke();

            PlayerController.Instance.MiniGameCompleted(false);
        }

        Debug.Log(_value);
    }

    private void CheckForWarnedState()
    {
        if (_value >= yellowMiddleValue && _value <= (yellowMiddleValue + 56))
        {
            Debug.Log("YELLOW");
            OnWin.Invoke();

            PlayerController.Instance.MiniGameCompleted(true);
            GameManager.Instance.MiniGameCompleted(5);
        }
        else
        {
            Debug.Log("You lost !!!");
            OnLose.Invoke();

            PlayerController.Instance.MiniGameCompleted(false);
        }

        Debug.Log(_value);
    }


    void ValueChange(float value)
    {
        float containAngle = -1.8f * value;
        pointer.localEulerAngles = new Vector3(0, 0, containAngle);
    }
}
