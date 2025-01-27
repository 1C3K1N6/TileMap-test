using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private Healthbar healthbar;
    private float currHealth;

    private void Start()
    {
        currHealth = maxHealth;
        healthbar.UpdateHealthBar(currHealth, maxHealth);
    }

    private void Awake()
    {
        healthbar = GetComponentInChildren<Healthbar>();
    }

    public void Damage(float damageAmount)
    {
        currHealth -= damageAmount;
        healthbar.UpdateHealthBar(currHealth, maxHealth);
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
