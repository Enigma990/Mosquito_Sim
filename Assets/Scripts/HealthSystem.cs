using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static event EventHandler OnAnyHit;

    public event EventHandler OnDead;

    [SerializeField] private int health = 100;

    private int maxHealth = 100;
    private int armourAmount = 0;

    private void Awake()
    {
        maxHealth = health;
    }

    public void Damage(int damageAmount)
    {
        health -= (damageAmount - armourAmount);

        OnAnyHit?.Invoke(this, EventArgs.Empty);

        if (health < 0)
            health = 0;



        if (health == 0)
        {
            // Invoke Death Event
            OnDead?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetArmourAmount(int armourAmount)
    {
        this.armourAmount += armourAmount;
    }
}
