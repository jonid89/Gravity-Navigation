using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Transform ship;
    public Transform boom1;
    public Transform boom2;
    public Transform boom3;

    private Transform tr;
    private float scMain;
    private float scB1;
    private float scB2;
    private float scB3;
    private bool growing;

    void Start()
    {
        tr = this.transform;
        tr.position = ship.position;
        growing = true;
    }

    void Update()
    {
        if (growing)
        {
            if (scMain <= 2 && growing)
            {
                scMain += 0.05f;
                tr.localScale = new Vector3(scMain, scMain, 1);
            }

            if (scB3 <= 1.5)
            {
                scB3 += 0.02f;
                boom3.localScale = new Vector3(scB3, scB3, 1);
            }

            if (scB2 <= 1)
            {
                scB2 += 0.1f;
                boom2.localScale = new Vector3(scB2, scB2, 1);
            }

            if (scB1 <= 2.8)
            {
                scB1 += 0.03f;
                boom1.localScale = new Vector3(scB1, scB1, 1);
            }
            else growing = false;
        }
        else if (scMain > 0)
        {
            scMain -= 0.02f;
            tr.localScale = new Vector3(scMain, scMain, 1);
        }

    }
}
