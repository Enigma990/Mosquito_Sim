using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MasterController : MonoBehaviour
{
    public CinemachineDollyCart cart;

    public float defaultSpeed;

    public float speedTransitionTime;

    public bool carStoped = false; // to stop repeating corotine

    private void Awake()
    {
        cart.m_Speed = defaultSpeed;
    }

    void Update()
    {
        // Testing
        if(Input.GetKeyDown(KeyCode.W))
        {
            StartCart();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopCart();
        }
    }

    IEnumerator Lerp(float startValue, float endValue, float lerpDuration)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            cart.m_Speed = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        cart.m_Speed = endValue;
    }

    [ContextMenu("StartCart")]
    public void StartCart()
    {
        if (!carStoped) return; // to stop repeating corotine
        StopAllCoroutines();
        StartCoroutine(Lerp(0,defaultSpeed, speedTransitionTime));
        carStoped = false;
    }
    [ContextMenu("StopCart")]
    public void StopCart()
    {
        if (carStoped) return; // to stop repeating corotine
        StopAllCoroutines();
        Debug.Log("Stop Cart Called");
        StartCoroutine(Lerp(defaultSpeed, 0, speedTransitionTime));
        carStoped = true;
    }
}
