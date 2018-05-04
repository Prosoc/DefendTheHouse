using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class WeaponController : MonoBehaviour
{
    public List<WeaponBasePrefab> weapons;
    public WeaponBasePrefab currentWeapon;
    int CurrentBullets;
    float shootDelay;
    float currentShootDelay;
    bool Reloaded = true;
    bool Bolted = true;
    float currentReloadTime;
    void Start()
    {
        weapons.AddRange((WeaponBasePrefab[])Resources.FindObjectsOfTypeAll(typeof(WeaponBasePrefab)));
        foreach (var w in weapons)
        {
            EditorUtility.SetDirty(w);
        }
        weapons = weapons.OrderBy(x => x.Level).ToList();
        currentWeapon = weapons[1];
        CurrentBullets = currentWeapon.MagazineSize;
    }

    void Update()
    {
        
        if (currentWeapon != null)
        {
            shootDelay = 60.0f / currentWeapon.FireRate;
            currentShootDelay += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R))
            {
                print("Reloading");
                Reload();
            }

            switch (currentWeapon.ShootType)
            {
                case ShootType.Bolt_Action:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            FireWeapon();
                            Bolted = false;
                        }
                        break;
                    }
                case ShootType.Semi_Auto:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            FireWeapon();
                        }
                        break;
                    }
                case ShootType.Auto:
                    {
                        if (Input.GetMouseButton(0))
                        {
                            FireWeapon();
                        }
                        break;
                    }
                default:
                    break;
            }


        }
    }
    public void FireWeapon()
    {
        if (CanShoot())
        {
            Vector3 hitPoint;
            ShootRayFromCam(Input.mousePosition, out hitPoint);
            currentShootDelay = 0;
            CurrentBullets--;
        }

    }
    public void Reload()
    {
        switch (currentWeapon.ReloadType)
        {
            case ReloadType.OneByOne:
                {
                    if (currentWeapon.MagazineSize - CurrentBullets > 0)
                    {

                        CurrentBullets++;
                    }
                    break;
                }
            case ReloadType.FullMag:
                {
                    if (currentWeapon.MagazineSize - CurrentBullets > 0)
                    {

                        CurrentBullets = currentWeapon.MagazineSize;
                    }
                    break;
                }
            default:
                break;
        }
    }
    public void CreateProjectile()
    {
    }
    public void ShootRayFromCam(Vector3 point, out Vector3 hitPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //print(hit.point);
            hitPoint = hit.point;
            Enemy e = hit.collider.gameObject.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(currentWeapon.Damage);

                print(e.CurrentHealth);
            }
            if (currentWeapon.NormalImpactEffect != null)
            {
                InstantiateEffectGO(currentWeapon.NormalImpactEffect, hit.point);
            }
        }
        hitPoint = new Vector3();
    }

    public void InstantiateEffectGO(GameObject effectGo, Vector3 point)
    {
        if (effectGo != null)
        {
            GameObject obj = Instantiate(effectGo, point, Quaternion.Euler(-Camera.main.transform.rotation.eulerAngles), GameObject.FindGameObjectWithTag("GOHolder").transform);
        }
        else
        {
            print("Effect go missing");
        }
    }

    public bool CanShoot()
    {
        switch (currentWeapon.ShootType)
        {
            case ShootType.Bolt_Action:
                {
                    if (Bolted && Reloaded && CurrentBullets > 0 && currentShootDelay >= shootDelay)
                    {
                        return true;
                    }
                    break;
                }
            case ShootType.Semi_Auto:
                {
                    if (Reloaded && CurrentBullets > 0 && currentShootDelay >= shootDelay)
                    {
                        return true;
                    }
                    break;
                }
                
            case ShootType.Auto:
                {
                    if (Reloaded && CurrentBullets > 0 && currentShootDelay >= shootDelay)
                    {
                        return true;
                    }
                    break;
                }
            default:
                break;
        }
        return false;
    }
}