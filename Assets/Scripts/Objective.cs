using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Objective : MonoBehaviour
{
   
    Renderer rd;

    public GameObject indicator;
    public GameObject ship;

    private void Start()
    {
        rd = GetComponent<Renderer>();
    }


    void Update()
    {

            Vector2 direction = ship.transform.position - this.transform.position;


            RaycastHit2D ray = Physics2D.Raycast(this.transform.position, direction, Mathf.Infinity, LayerMask.NameToLayer("EdgeCollider"));
            Debug.Log(LayerMask.NameToLayer("EdgeCollider"));
            Debug.Log("Dir: " + direction + "Ray: " + ray + "Collider: " + ray.collider + "Point: " + ray.point);
            

            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
            }


    }
}
