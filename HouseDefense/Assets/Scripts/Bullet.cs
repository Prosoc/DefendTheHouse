using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Damage;
    public Turret parentTurret;
    public GameObject MetalImpact;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy")
        {
            InstantiateEffectGO(MetalImpact);
            collision.collider.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (collision.collider.transform.tag == "Wall")
        {
            InstantiateEffectGO(MetalImpact);
            Destroy(gameObject);
        }
        else if (collision.collider.transform.tag == "Turret" && collision.collider.transform.root.GetComponent<Turret>() != parentTurret)
        {
            InstantiateEffectGO(MetalImpact);
            Destroy(gameObject);
        }
        else if (collision.collider.transform.tag == "House")
        {
            InstantiateEffectGO(MetalImpact);
            Destroy(gameObject);
        }
        else if (collision.collider.transform.tag == "Platform")
        {
            InstantiateEffectGO(MetalImpact);
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public void InstantiateEffectGO(GameObject effectGo)
    {
        GameObject obj = Instantiate(effectGo, transform.position, Quaternion.Euler(-transform.rotation.eulerAngles), GameObject.FindGameObjectWithTag("GOHolder").transform);
    }
}
