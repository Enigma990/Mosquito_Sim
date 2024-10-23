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

    // not nessesary ....
    public UnityEvent OnWin;
    public UnityEvent OnLose;


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



    public void StartMiniGame()
    {
        //setGreen part 
        greenMiddleValue = Random.Range(0,91);
        float containAngle = -1.8f * greenMiddleValue;
        Debug.Log(Mathf.Clamp(containAngle, -162, 0));
        
        GreenPart.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(containAngle, -162, 0));

        isMiniGameRunning = true;
    }

    public void GetResult()
    {
        isMiniGameRunning = false;

        if(greenMiddleValue <= _value && _value <= (greenMiddleValue+10) )
        {
            Debug.Log("You won !!!");
            OnWin.Invoke();

            PlayerController.Instance.MiniGameCompleted(true);
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
