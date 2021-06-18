using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public Transform ship;

    void Start()
    {
        
    }

 
    void Update()
    {
        this.transform.position = new Vector3(ship.position.x,ship.position.y,this.transform.position.z);
    }
}
