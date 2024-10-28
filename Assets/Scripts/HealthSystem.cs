using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;

    [SerializeField] private int health = 100;

    private int maxHealth = 100;

    private void Awake()
    {
        maxHealth = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
            health = 0;



        if (health == 0)
        {
            // Invoke Death Event
            OnDead?.Invoke(this, EventArgs.Empty);
        }
    }
}
