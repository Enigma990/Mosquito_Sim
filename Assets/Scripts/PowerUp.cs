using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Armour,
    Speed,
    StingMeter,
    BloodAmount
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private int powerUpValue;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out PlayerStats playerStats))
        //{
        //    playerStats.ActivatePowerUp(powerUpType, powerUpValue);
        //}

        //Destroy(gameObject);
    }

}
