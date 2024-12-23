using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int armourAmount;
    private int speed;
    private int stingMeter;
    private int bloodAmount;

    private void Start()
    {
        armourAmount += StatsManager.Instance.GetArmourAmount();
        speed += StatsManager.Instance.GetSpeed();
        stingMeter += StatsManager.Instance.GetStingMeter();
        bloodAmount += StatsManager.Instance.GetBloodAmount();
    }

    public void UpdateStats(PowerUpType powerUpType, int amount)
    {
        switch (powerUpType)
        {
            case PowerUpType.Armour:
                armourAmount += amount;
                break;
            case PowerUpType.Speed:
                speed += amount;
                break;
            case PowerUpType.StingMeter:
                stingMeter += amount;
                break;
            case PowerUpType.BloodAmount:
                bloodAmount += amount;
                break;  
        }
    }

    public int GetArmourAmount()
    {
        armourAmount += StatsManager.Instance.GetArmourAmount();
        return armourAmount;
    }
    public int GetBloodAmount()
    {
        bloodAmount += StatsManager.Instance.GetBloodAmount();
        return bloodAmount;
    }
    public int GetSpeedAmount()
    {
        speed += StatsManager.Instance.GetSpeed();

        return speed;
    }

    public GameObject GetSelectedMosquito() => StatsManager.Instance.GetSelectedMosquito();
}
