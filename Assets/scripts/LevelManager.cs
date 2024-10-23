using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private LevelInfo[] levelInfoArray;

    private int currentLevel = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Vector3 GetCurrentTargetPosition() => levelInfoArray[currentLevel].GetCurrentTargetPosition();

    public void UpdateCurrentTarget() => levelInfoArray[currentLevel].IncreamentTarget();

    public bool HasTarget() => levelInfoArray[currentLevel].HasTarget();

}
