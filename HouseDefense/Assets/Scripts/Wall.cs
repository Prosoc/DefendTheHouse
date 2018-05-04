using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Building {

    public Walls WallsList;

    [Space()]
    public WallType WallType;
    // Use this for initialization
    void Start () {
        WallsList.Register(this);
        CurrentHealth = Health;
	}
	
	// Update is called once per frame
	void Update () {
        Die();

    }


    public override void Die()
    {
        if (CurrentHealth <= 0)
        {
            WallsList.Remove(this);
            print("[Wall Destroyed]: " + transform.name);
            Destroy(gameObject);
        }
    }
}

public enum WallType
{
    Normal,
    Poison,
    Fire,
    Cold,
    Repair
}



[CreateAssetMenu(menuName = "Custom/Walls List")]
public class Walls : ScriptableObject
{
    public List<Wall> List = new List<Wall>();

    public void Register(Wall wall)
    {
        List.Add(wall);
    }

    public void Remove(Wall wall)
    {
        List.Remove(wall);
    }
}