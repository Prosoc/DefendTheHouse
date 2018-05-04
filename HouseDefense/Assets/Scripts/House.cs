using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building {
    

    public override void Die()
    {
        if (CurrentHealth <= 0)
        {
            print("Game over");
        }
    }



    // Use this for initialization
    void Start ()
    {
        CurrentHealth = Health;
    }
	
	// Update is called once per frame
	void Update () {
        Die();
	}
    
}
