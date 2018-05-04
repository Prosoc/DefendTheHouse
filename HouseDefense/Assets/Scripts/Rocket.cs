using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public BaseEnemy enemy;
    public Vector3 lastPos;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void TargetEnemy()
    {
        if (enemy == null)
        {
            
            return;
        }
    }

    public void BlowUp()
    {
        print("Rocket blown up");
    }

    public void PlayBlowUpAnimation()
    {

    }
}

