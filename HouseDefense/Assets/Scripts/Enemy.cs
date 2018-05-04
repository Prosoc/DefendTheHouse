using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Enemy : BaseEnemy{

    public EnemyTargetType TargetType;
    public float AttackCutoffDistance = 3.5f;
    float AttackDelay;
    float CurrentAttackDelay;

    Vector3 Velocity;
    void Start()
    {
        PopulateFromPrefab();
        AttackDelay = 60.0f / AttackSpeed;
    }
    

    void Update()
    {
        Die();
        AiProcedures();
    }

    public override void Die()
    {
        if (CurrentHealth <= 0)
        {
            EnemiesList.Remove(this);
            print("[Enemy Death]: " + transform.name);
            Destroy(gameObject);
        }
    }

    public override void AiProcedures()
    {
        ChooseTarget();
        AttackTarget();
    }

    public void ChooseTarget()
    {
        if (WallsList.List.Count == 0)
        {
            if (TurretsList.List.Count == 0)
            {
                TargetType = EnemyTargetType.House;
            }
            else
            {
                TargetType = EnemyTargetType.Turret;
            }
        }
        else
        {
            TargetType = EnemyTargetType.Wall;
        }

        switch (TargetType)
        {
            case EnemyTargetType.Wall:
                {
                    float currentClosestDistance = float.PositiveInfinity;
                    Wall wall = null;
                    foreach (var w in WallsList.List)
                    {
                        float dist = Vector3.Distance(transform.position, w.transform.position);
                        if (dist < currentClosestDistance ||wall == null)
                        {
                            wall = w;
                            currentClosestDistance = dist;
                        }
                    }
                    targetBuilding = wall;
                    break;
                }
            case EnemyTargetType.Turret:
                {
                    float currentClosestDistance = float.PositiveInfinity;
                    Turret turret = null;
                    foreach (var t in TurretsList.List)
                    {
                        float dist = Vector3.Distance(transform.position, t.transform.position);
                        if (dist < currentClosestDistance || turret == null)
                        {
                            turret = t;
                            currentClosestDistance = dist;
                        }
                    }
                    targetBuilding = turret;
                    break;
                }
            case EnemyTargetType.House:
                {
                    targetBuilding = House;
                    break;
                }
            default:
                break;
        }
    }
    public void AttackTarget()
    {
        if (CurrentAttackDelay >= AttackDelay)
        {
            float dist = MoveTowardsTarget(targetBuilding.transform.position);
            if (dist <= AttackCutoffDistance)
            {
                targetBuilding.TakeDamage(Damage);
                CurrentAttackDelay = 0;
            }

        }
        else if(targetBuilding != null)
        {
            CurrentAttackDelay += Time.deltaTime;
        }
    }

    public float MoveTowardsTarget(Vector3 targetPos)
    {
        float dist = Vector2.Distance(transform.position, targetPos);
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * Speed);
        return dist;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackCutoffDistance);
    }
}

public abstract class BaseEnemy : MonoBehaviour
{
    public abstract void AiProcedures();
    public EnemyPrefab enemyPrefab;

    public Walls WallsList;
    public Turrets TurretsList;
    public House House;

    public Building targetBuilding;

    public int Level = 0;

    public float Health;
    public float CurrentHealth;
    public float Damage;
    public float Speed;
    public float AttackSpeed;

    public Enemies EnemiesList;
    

    public void PopulateFromPrefab()
    {
        Health = enemyPrefab.Health + enemyPrefab.HealthModifier * Level * Level;
        CurrentHealth = Health;
        Damage = enemyPrefab.Damage + enemyPrefab.DamageModifier * Level * Level;
        Speed = enemyPrefab.Speed + enemyPrefab.SpeedModifier * Level * Level;
        AttackSpeed = enemyPrefab.AttackSpeed + enemyPrefab.AttackSpeedModifier * Level * Level;
    }

    public abstract void Die();

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }
}

public enum EnemyTargetType
{
    Wall,
    Turret,
    House
}