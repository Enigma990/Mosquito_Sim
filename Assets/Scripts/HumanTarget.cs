using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTarget : MonoBehaviour
{
    [SerializeField] Transform bitePosition;

    private bool bVisited = false;

    

    public void SetVisited()
    {
        bVisited = true;
    }

    public Vector3 GetBitePosition() => bitePosition.position;
    public bool GetVisited() => bVisited;
}
