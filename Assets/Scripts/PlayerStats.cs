using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int armourAmount;
    private float speed = 0.5f;
    private int stingMeter;
    private int bloodAmount;

    private int armourBase = 1;
    private float speedBase = 0.2f;
    private float stingMeterBase = 0.2f;
    private float bloodAmountBase = 0.2f;

    private void Start()
    {
        armourAmount += StatsManager.Instance.GetArmourAmount();
        speed += StatsManager.Instance.GetSpeed();
        stingMeter += StatsManager.Instance.GetStingMeter();
        bloodAmount += StatsManager.Instance.GetBloodAmount();
    }

    public void ActivatePowerUp(PowerUpType powerUpType, int amount, Action UpdatePlayerStatsAction)
    {
        switch (powerUpType)
        {
            case PowerUpType.Armour:
                armourAmount += amount;
                break;
            case PowerUpType.Speed:
                speed += (amount * 0.1f);
                break;
            case PowerUpType.StingMeter:
                stingMeter += amount;
                break;
            case PowerUpType.BloodAmount:
                bloodAmount += amount;
                break;  
        }

        UpdatePlayerStatsAction();
        StartCoroutine(DeactivatePowerUp(powerUpType, amount, UpdatePlayerStatsAction)); 
    }

    private IEnumerator DeactivatePowerUp(PowerUpType powerUpType, int amount, Action UpdatePlayerStatsAction)
    {
        yield return new WaitForSeconds(2f);

        switch (powerUpType)
        {
            case PowerUpType.Armour:
                armourAmount -= amount;
                break;
            case PowerUpType.Speed:
                speed -= (amount * 0.1f);
                break;
            case PowerUpType.StingMeter:
                stingMeter -= amount;
                break;
            case PowerUpType.BloodAmount:
                bloodAmount -= amount;
                break;
        }


        UpdatePlayerStatsAction();
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
    public float GetSpeedAmount()
    {
        speed += StatsManager.Instance.GetSpeed();

        return speed;
    }

    public GameObject GetSelectedMosquito() => StatsManager.Instance.GetSelectedMosquito();
}
