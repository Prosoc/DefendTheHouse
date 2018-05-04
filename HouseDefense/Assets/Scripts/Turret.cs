using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turret : Building {

    public GameObject TurretYaw;
    public GameObject Gun;
    public GameObject GunPoint;
    public float turretTurnSpeed = 10;
    [Space()]

    public GameObject Bullet;
    public GameObject MetalImpact;
    public Turrets TurretList;

    [Space()]


    public int DamageLevel = 0;
    public float Damage;
    public float DamageModifier;
    //Shoots per min
    public int AttackSpeedLevel = 0;
    public float AttackSpeed;
    public float AttackSpeedModifier;
    public int AttackRangeLevel = 0;
    public float MaxAttackRange;
    public float MinAttackRange;
    public float BulletSpeed = 30;
    public TargetType TargetType;

    [Space()]
    public Enemies EnemiesList;


    [Space()]
    public bool canShoot;
    public bool betterTargetting = true;
    public BaseEnemy target;

    public float AttackDelay;
    public float currentAttackDelay;

    // Use this for initialization
    void Start () {
        TurretList.Register(this);
        CurrentHealth = Health;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Die();
    }


    private void FixedUpdate()
    {
        target = ChooseTargetFromEnemiesList();
        TurnGunTurretToTarget();
        ShootAtTarget();
    }

    public void TurnGunTurretToTarget()
    {
        if (target == null)
        {
            return;
        }

        if (betterTargetting)
        {
            TurretYaw.transform.LookAt(target.transform.position + target.transform.GetComponent<Rigidbody>().velocity);
        }
        else
        {
            TurretYaw.transform.LookAt(target.transform);
        }
        float y = TurretYaw.transform.rotation.eulerAngles.y;
        TurretYaw.transform.rotation = Quaternion.Euler(0, y, 0);
        Gun.transform.LookAt(target.transform);
    }

    public void ShootAtTarget()
    {
        if (target == null)
        {
            return;
        }
        if (!canShoot)
        {
            return;
        }
        Ray ray = new Ray(GunPoint.transform.position, GunPoint.transform.position + Gun.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MaxAttackRange))
        {
            if (hit.collider.tag != "Enemy")
            {
                return;
            }
        }

        AttackDelay = 60.0f / AttackSpeed;
        if (currentAttackDelay >= AttackDelay)
        {
            currentAttackDelay = 0;
            ShootBullet();
        }
        else
        {
            currentAttackDelay += Time.deltaTime;
        }
    }

    public void ShootBullet()
    {
        if (Bullet == null)
        {
            print("Bullet prefab missing");
            return;
        }
        GameObject go = Instantiate(Bullet, GunPoint.transform.position, GunPoint.transform.rotation, GameObject.FindGameObjectWithTag("GOHolder").transform);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * BulletSpeed, ForceMode.Impulse);
        Bullet b = go.GetComponent<Bullet>();
        b.Damage = Damage;
        b.parentTurret = this;
        b.MetalImpact = MetalImpact;
        Destroy(go, 5);
    }


    public override void Die()
    {
        if (CurrentHealth <= 0)
        {
            TurretList.Remove(this);
            print("[Turret Destroyed]: " + transform.name);
            Destroy(gameObject);
        }
    }

    public BaseEnemy ChooseTargetFromEnemiesList()
    {
        BaseEnemy enemy = null;

        if (canShoot)
        {

            switch (TargetType)
            {
                case TargetType.Closest:
                    {
                        Enemy currentClosest = null;
                        float currentDistance = float.PositiveInfinity;
                        foreach (Enemy e in EnemiesList.List)
                        {
                            float distance = Vector3.Distance(transform.position, e.transform.position);
                            if ((currentClosest == null || distance < currentDistance) && distance < MaxAttackRange && distance >= MinAttackRange)
                            {
                                currentClosest = e;
                                currentDistance = distance;
                            }
                        }
                        enemy = currentClosest;
                        break;
                    }
                case TargetType.Farthest:
                    {
                        Enemy currentFarthest = null;
                        float currentDistance = float.NegativeInfinity;
                        foreach (Enemy e in EnemiesList.List)
                        {
                            float distance = Vector3.Distance(transform.position, e.transform.position);
                            if ((currentFarthest == null || distance > currentDistance) && distance < MaxAttackRange)
                            {
                                currentFarthest = e;
                                currentDistance = distance;
                            }
                        }
                        enemy = currentFarthest;
                        break;
                    }
                case TargetType.HighestDamage:
                    {
                        Enemy currentHighest = null;
                        float currentHighestDamage = float.NegativeInfinity;
                        foreach (Enemy e in EnemiesList.List)
                        {
                            float distance = Vector3.Distance(transform.position, e.transform.position);
                            if ((currentHighest == null || e.Damage > currentHighestDamage) && distance < MaxAttackRange)
                            {
                                currentHighest = e;
                                currentHighestDamage = e.Damage;
                            }
                        }
                        enemy = currentHighest;
                        break;
                    }
                case TargetType.LessHP:
                    {

                        break;
                    }
                case TargetType.MoreHP:
                    {

                        break;
                    }
                default:
                    {
                        BaseEnemy e = EnemiesList.List[Random.Range(0, EnemiesList.List.Count)];
                        if (Vector3.Distance(transform.position, e.transform.position) <= MaxAttackRange)
                        {
                            enemy = e;
                        }
                        else
                        {
                            enemy = ChooseTargetFromEnemiesList();
                        }
                        break;
                    }
            }
        }

        return enemy;
    }


    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, MinAttackRange);
        //Gizmos.DrawWireSphere(transform.position, MaxAttackRange);
        Gizmos.DrawLine(GunPoint.transform.position, GunPoint.transform.position + Gun.transform.forward * MaxAttackRange);
    }


}


[CreateAssetMenu(menuName = "Custom/Turrets List")]
public class Turrets : ScriptableObject
{
    public List<Turret> List = new List<Turret>();

    public void Register(Turret turret)
    {
        List.Add(turret);
    }

    public void Remove(Turret turret)
    {
        List.Remove(turret);
    }
}

public enum TargetType
{
    Closest,
    Farthest,
    HighestDamage,
    LessHP,
    MoreHP
}

public enum TurretBaseType
{



}

public enum TurretGunType
{
    


}
