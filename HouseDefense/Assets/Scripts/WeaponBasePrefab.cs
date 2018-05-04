using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "[Weapon]", menuName = "Custom/Weapon Prefab")]
public class WeaponBasePrefab : ScriptableObject
{
    public int Level;
    public string Name;

    public int FireRate;
    public float Damage;
    public float AOEDamage;
    public float AOERange;
    public int MagazineSize;
    public ReloadType ReloadType;
    public ShootType ShootType;
    public float ReloadTime;
    public GameObject NormalImpactEffect;
}


public enum ReloadType
{
    OneByOne,
    FullMag
}

public enum ShootType
{
    Bolt_Action,
    Semi_Auto,
    Auto
}