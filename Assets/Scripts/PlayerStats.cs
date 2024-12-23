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
