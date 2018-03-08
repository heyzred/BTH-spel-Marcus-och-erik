﻿using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

        [SyncVar]

    [SerializeField]
    private int maxHealth = 100;


    [SyncVar]
    private int currentHealth;

     void Awake()
    {
        SetDefaults();
        
    }

    [ClientRpc]
    public void RpcTakeDamage (int _amount)
    {

    if (isDead)
        return;


        currentHealth -= _amount;

        Debug.Log(transform.name + " now has" + currentHealth + "health.");

            if(currentHealth <= 0) { Die(); }
    }

    private void Die()
    {
        isDead = true;
        //Disable Components

        Debug.Log(transform.name + " is DEAD!");

        // Call respawn method
    }


    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }


}
