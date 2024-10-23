using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
public class CinemachineWP : MonoBehaviour
{
    public CinemachineSmoothPath.Waypoint Waypoint;

    private void Awake()
    {
        Waypoint.position = transform.position;
    }

    private void Update()
    {
        Waypoint.position = transform.position;
    }

}
