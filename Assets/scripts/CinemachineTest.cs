using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

[ExecuteInEditMode]
public class CinemachineTest : MonoBehaviour
{
    CinemachineSmoothPath smoothPath;

    public GameObject ObjectHolder;

    public CinemachineSmoothPath.Waypoint[] placeholders;

    public CinemachineWP[] cinemachineWPs;

    private void Awake()
    {
        smoothPath = GetComponent<CinemachineSmoothPath>();
    }

    private void Update()
    {
        if (smoothPath == null) smoothPath = GetComponent<CinemachineSmoothPath>();

        if (ObjectHolder == null) return;

        cinemachineWPs = ObjectHolder.GetComponentsInChildren<CinemachineWP>();

        if (cinemachineWPs == null || cinemachineWPs.Length == 0)
            return;

        if (placeholders == null || placeholders.Length != cinemachineWPs.Length)
        {
            placeholders = new CinemachineSmoothPath.Waypoint[cinemachineWPs.Length];
        }

        for (int i = 0; i < cinemachineWPs.Length; i++)
        {
            placeholders[i] = cinemachineWPs[i].Waypoint;
        }

        smoothPath.m_Waypoints = placeholders;

    }


}
