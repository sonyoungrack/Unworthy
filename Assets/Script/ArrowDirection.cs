using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {

    public GameObject Planet;       
    public float speed;             

    private void Update()
    {
        OrbitAround();
    }

    void OrbitAround()
    {
        transform.RotateAround(Planet.transform.position, Planet.transform.position, speed * Time.deltaTime);
    }
}
