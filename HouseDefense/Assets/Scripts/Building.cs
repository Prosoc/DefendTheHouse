using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {
    
    public int HealthLevel = 0;
    public float Health;
    public float CurrentHealth;
    public float HealthModifier;
    public int ArmorLevel = 0;
    public float Armor;
    public float ArmorModifier;
    
    public abstract void Die();

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
