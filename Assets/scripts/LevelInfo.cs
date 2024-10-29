using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelData
{   
    public List<GameObject> humanTargetSpawnPoints;
    public int numOfHumanTarget;
    public GameObject humanTargetPrefab;
}

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    private GameObject[] humanTargetArray;

    public int currentTarget;

    private void Start()
    {
        humanTargetArray = new GameObject[levelData.numOfHumanTarget];

        for (int i = 0; i < levelData.numOfHumanTarget; i++)
        {
            if (levelData.humanTargetSpawnPoints.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, levelData.humanTargetSpawnPoints.Count);
                humanTargetArray[i] = Instantiate(levelData.humanTargetPrefab, levelData.humanTargetSpawnPoints[index].transform);
                levelData.humanTargetSpawnPoints.RemoveAt(index);
            }
        }
    }

    public Vector3 GetCurrentTargetPosition()
    {
        if (currentTarget >= humanTargetArray.Length)
        {
            return Vector3.zero;
        }

        return humanTargetArray[currentTarget].transform.position;
    }

    public bool HasTarget() => currentTarget < humanTargetArray.Length;

    public void IncreamentTarget() => currentTarget += 1;

}
