using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    private int armourAmount = 0;
    private int speed = 0;
    private int stingMeter = 0;
    private int bloodAmount = 0;

    private GameObject selectedMosquito;

    [SerializeField] private MosquitoStatsSO[] mosquitoStatsSO;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public void SelectedMosquito(MosquitoType mosquitoType)
    {
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
        armourAmount += 1;
    }
    public void UpgradeSpeed()
    {
        speed += 1;
    }
    public void UpgradeStingMeter()
    {
        stingMeter += 1;
    }
    public void UpgradeBloodAmount()
    {
        bloodAmount += 1;
    }

    public int GetArmourAmount() => armourAmount;
    public int GetSpeed() => speed;
    public int GetStingMeter() => stingMeter;
    public int GetBloodAmount() => bloodAmount;

    public GameObject GetSelectedMosquito() => selectedMosquito;
}
