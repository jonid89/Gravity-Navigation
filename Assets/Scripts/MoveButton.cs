using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Ship ship;
    public string letter;
    public GameObject startPopUp;

    public bool buttonDown;


    void Update() 
    {
        if (buttonDown) ship.MoveSide(letter);
        if (Input.GetKey("right")) ship.MoveSide("R");
        if (Input.GetKey("left")) ship.MoveSide("L");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonDown = true;
        ship.TurnOnFire(letter);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonDown = false;
        ship.TurnOffFire(letter);
    }

    

}
