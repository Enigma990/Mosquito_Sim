using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct LevelData
{
    [Header("Human Target")]
    public List<GameObject> humanTargetSpawnPoints;
    public int numOfHumanTarget;
    public GameObject humanTargetPrefab;

    [Header("Electric Lamp Obstacle")]
    public GameObject electricLampPrefab;
    public List<GameObject> electricLampSpawnPoints;
    public int numOfElectricLamp;

    [Header("Human Racket Obstacle")]
    public GameObject humanRacketPrefab;
    public List<GameObject> humanRacketSpawnPoints;
    public int numOfHumanRacket;

    [Header("Human Spray Obstacle")]
    public GameObject humanSprayPrefab;
    public List<GameObject> humanSpraySpawnPoints;
    public int numOfHumanSpray;

    [Header("Spider Obstacle")]
    public GameObject spiderPrefab;
    public List<GameObject> spiderSpawnPoints;
    public int numOfSpider;
}

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    private GameObject[] humanTargetArray;
    private GameObject[] electricLampArray;
    private GameObject[] humanRacketArray;
    private GameObject[] humanSprayArray;
    private GameObject[] spiderArray;

    public int currentTarget;

    private void Start()
    {
        SpawnHumanTarget();
    }

    #region Spawner Logic

    private void SpawnHumanTarget()
    {
        humanTargetArray = new GameObject[levelData.numOfHumanTarget];

        //System.Random rng = new System.Random();
        //List<GameObject> randomSpawnPoints = levelData.humanTargetSpawnPoints.OrderBy(x => rng.Next()).ToList();

        //for (int i = 0; i < levelData.numOfHumanTarget; i++)
        //{
        //    //if (levelData.humanTargetSpawnPoints.Count > 0)
        //    //{
        //    //    //int index = UnityEngine.Random.Range(0, levelData.humanTargetSpawnPoints.Count);
        //    //    //humanTargetArray[i] = Instantiate(levelData.humanTargetPrefab, levelData.humanTargetSpawnPoints[index].transform);
        //    //    //levelData.humanTargetSpawnPoints.RemoveAt(index);
        //    //}

        //    humanTargetArray[i] = Instantiate(levelData.humanTargetPrefab, randomSpawnPoints[i].transform);
        //}

        for (int i = 0; i < levelData.numOfHumanTarget; i++)
        {
            humanTargetArray[i] = Instantiate(levelData.humanTargetPrefab, levelData.humanTargetSpawnPoints[i].transform);
        }
    }

    #region Obstacles

    private void SpawnElectricLamps()
    {
        electricLampArray = new GameObject[levelData.numOfElectricLamp];

        for (int i = 0; i < levelData.numOfElectricLamp; i++)
        {
            if (levelData.electricLampSpawnPoints.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, levelData.electricLampSpawnPoints.Count);
                electricLampArray[i] = Instantiate(levelData.electricLampPrefab, levelData.electricLampSpawnPoints[index].transform);
                levelData.electricLampSpawnPoints.RemoveAt(index);
            }
        }
    }
    private void SpawnHumanRacket()
    {
        humanRacketArray = new GameObject[levelData.numOfHumanRacket];

        for (int i = 0; i < levelData.numOfHumanRacket; i++)
        {
            if (levelData.humanRacketSpawnPoints.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, levelData.humanRacketSpawnPoints.Count);
                humanRacketArray[i] = Instantiate(levelData.humanRacketPrefab, levelData.humanRacketSpawnPoints[index].transform);
                levelData.humanRacketSpawnPoints.RemoveAt(index);
            }
        }
    }

    private void SpawnHumanSpray()
    {
        humanSprayArray = new GameObject[levelData.numOfHumanSpray];

        for (int i = 0; i < levelData.numOfHumanSpray; i++)
        {
            if (levelData.humanSpraySpawnPoints.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, levelData.humanSpraySpawnPoints.Count);
                humanSprayArray[i] = Instantiate(levelData.humanRacketPrefab, levelData.humanSpraySpawnPoints[index].transform);
                levelData.humanSpraySpawnPoints.RemoveAt(index);
            }
        }
    }

    private void SpawnSpider()
    {
        spiderArray = new GameObject[levelData.numOfSpider];

        for (int i = 0; i < levelData.numOfSpider; i++)
        {
            if (levelData.spiderSpawnPoints.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, levelData.spiderSpawnPoints.Count);
                spiderArray[i] = Instantiate(levelData.humanRacketPrefab, levelData.spiderSpawnPoints[index].transform);
                levelData.spiderSpawnPoints.RemoveAt(index);
            }
        }
    }

    #endregion

    #endregion

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
