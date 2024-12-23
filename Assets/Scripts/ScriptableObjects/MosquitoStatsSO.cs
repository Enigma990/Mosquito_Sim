using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MosquitoType
{
    Assassin,
    Warrior,
    Queen
}

[CreateAssetMenu]
public class MosquitoStatsSO : ScriptableObject
{
    public MosquitoType mosquitoType;

    public GameObject mosquitoPrefab;

    public int armourAmount;
    public int speed;
    public int stingMeter;
    public int bloodAmount;
}
