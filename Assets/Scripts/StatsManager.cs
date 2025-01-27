using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    // values in percentage
    private int armourAmount = 0;
    private float speed = 0;
    private int stingMeter = 0;
    private int bloodAmount = 0;

    private int speedLevel = 0;
    private int armourLevel = 0;
    private int stingMeterLevel = 0;
    private int bloodAmountLevel = 0;

    private GameObject selectedMosquito;

    [SerializeField] private MosquitoStatsSO[] mosquitoStatsSO;
    [SerializeField] private float[] speedUpgradeLevels;
    [SerializeField] private int[] armourUpgradeLevels;
    [SerializeField] private int[] stingMeterUpgradeLevels;
    [SerializeField] private int[] bloodAmountUpgradeLevels;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public void SelectedMosquito(MosquitoType mosquitoType)
    {
        speed = speedUpgradeLevels[speedLevel];
        armourAmount = armourUpgradeLevels[armourLevel];
        stingMeter = stingMeterUpgradeLevels[stingMeterLevel];
        bloodAmount = bloodAmountUpgradeLevels[bloodAmountLevel];

        switch (mosquitoType)
        {
            case MosquitoType.Queen:
                speed += mosquitoStatsSO[0].speed;
                armourAmount += mosquitoStatsSO[0].armourAmount;
                stingMeter += mosquitoStatsSO[0].stingMeter;
                bloodAmount += mosquitoStatsSO[0].bloodAmount;
                selectedMosquito = mosquitoStatsSO[0].mosquitoPrefab;
                break;

            case MosquitoType.Assassin:
                speed += mosquitoStatsSO[1].speed;
                armourAmount += mosquitoStatsSO[1].armourAmount;
                stingMeter += mosquitoStatsSO[1].stingMeter;
                bloodAmount += mosquitoStatsSO[1].bloodAmount;
                selectedMosquito = mosquitoStatsSO[1].mosquitoPrefab;
                break;

            case MosquitoType.Warrior:
                speed += mosquitoStatsSO[2].speed;
                armourAmount += mosquitoStatsSO[2].armourAmount;
                stingMeter += mosquitoStatsSO[2].stingMeter;
                bloodAmount += mosquitoStatsSO[2].bloodAmount; 
                selectedMosquito = mosquitoStatsSO[2].mosquitoPrefab;
                break;
        }
    }

    public void UpgradeArmour()
    {
        armourLevel += 1;
        if (armourLevel >= armourUpgradeLevels.Length)
        {
            armourLevel = armourUpgradeLevels.Length - 1;
        }
    }
    public void UpgradeSpeed()
    {
        speedLevel += 1;
        if (speedLevel >= speedUpgradeLevels.Length)
        {
            speedLevel = speedUpgradeLevels.Length - 1;
        }
    }
    public void UpgradeStingMeter()
    {
        stingMeterLevel += 1;
        if (stingMeterLevel >= stingMeterUpgradeLevels.Length)
        {
            stingMeterLevel = stingMeterUpgradeLevels.Length - 1;
        }
    }
    public void UpgradeBloodAmount()
    {
        bloodAmountLevel += 1;
        if (bloodAmountLevel >= bloodAmountUpgradeLevels.Length)
        {
            bloodAmountLevel = bloodAmountUpgradeLevels.Length - 1;
        }
    }

    public int GetArmourAmount() => armourAmount;
    public float GetSpeed() => speed;
    public int GetStingMeter() => stingMeter;
    public int GetBloodAmount() => bloodAmount;

    public GameObject GetSelectedMosquito() => selectedMosquito;
}
