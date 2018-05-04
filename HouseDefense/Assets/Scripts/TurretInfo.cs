using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretInfo {

    public int HealthLevel = 0;
    public int ArmorLevel = 0;
    public int DamageLevel = 0;
    public int AttackSpeedLevel = 0;
    public int AttackRangeLevel = 0;
    public int BulletSpeedLEvel = 0;
    public WeaponType WeaponType;
    public TurretBase TurretBase;


}

public enum WeaponType
{
    Kinetic,
    Rocket,
    Magic,
    Laser
}

public enum TurretBase
{
    Fire,
    Ice,
    Poison
}