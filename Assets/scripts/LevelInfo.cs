using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelData
{
    public GameObject[] humanTarget;

    public int currentTarget;
}

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private LevelData levelData;


    public Vector3 GetCurrentTargetPosition()
    {
        if (levelData.currentTarget >= levelData.humanTarget.Length)
        {
            return Vector3.zero;
        }

        return levelData.humanTarget[levelData.currentTarget].transform.position;
    }

    public bool HasTarget() => levelData.currentTarget < levelData.humanTarget.Length;

    public void IncreamentTarget() => levelData.currentTarget += 1;

}
