using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Indicator : MonoBehaviour
{
    public Transform shipTr;
    public Transform objectiveTr;

    private Transform tr;
    private Vector3 dir;
    private float angleToFront;
    private float angleToRight;


    void Start()
    {
        tr = this.gameObject.transform;
    }

    
    void Update()
    {
        dir = objectiveTr.position - shipTr.position;

        angleToFront = Vector3.Angle(dir, Vector3.up);
        angleToRight = Vector3.Angle(dir, Vector3.right);

        if ((angleToFront >= 90f && angleToRight <= 90f) || (angleToFront >= 90f && angleToRight >= 90f))
        {
            tr.localEulerAngles = new Vector3(0, 0, - angleToRight -90f );
        }
        else if ((angleToFront <= 90f && angleToRight <= 90f) || (angleToFront <= 90f && angleToRight >= 90f))
        {
            tr.localEulerAngles = new Vector3(0, 0, angleToRight - 90f);
        }

    }

}
