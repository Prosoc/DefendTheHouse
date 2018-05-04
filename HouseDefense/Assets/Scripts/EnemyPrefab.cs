using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Enemy Prefab", menuName = "Custom/Enemy Prefab")]
public class EnemyPrefab : ScriptableObject {

    public string Name;
    public GameObject BasePrefab;
    public float Health;
    public float CurrentHealth;
    public float HealthModifier;
    public float Damage;
    public float DamageModifier;
    public float Speed;
    public float SpeedModifier;
    public float AttackSpeed;
    public float AttackSpeedModifier;
}




[CreateAssetMenu(menuName = "Custom/Enemies List")]
public class Enemies : ScriptableObject
{
    public List<BaseEnemy> List = new List<BaseEnemy>();

    public void Register(BaseEnemy enemy)
    {
        List.Add(enemy);
    }

    public void Remove(BaseEnemy enemy)
    {
        List.Remove(enemy);
    }
}