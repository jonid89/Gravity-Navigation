using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : MonoBehaviour
{
    public float speed = 7f;
    public float sideSpeed = 5f;
    public float attractionSpeed = 0f;
    public float rotationSpeed = 4f;
    public Transform front;
    public GameObject attractionObj;
    public GameObject rearFire;
    public GameObject rightFire;
    public GameObject leftFire;
    public GameObject explosion;

    private GameObject ship;
    private Vector3 frontDir;
    private Vector3 rightDir;
    private Vector3 movement;
    private Vector3 attractionDir;
    private float sideMove;
    private float angleToFront;
    private float angleToRight;
    private int levelCount;
    private AudioSource audioSource;



    void Start()
    {
        ship = this.gameObject;
        frontDir = front.position - ship.transform.position;
        attractionDir = new Vector3(0, 0, 0);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;

        levelCount = GameSettings.Instance.GetLevelCount();
        if (levelCount > 1) SetLevelVariables();

        StartCoroutine(RearFireTime());
    }

    private void SetLevelVariables()
    {
        speed += 0.4f * (levelCount - 1) + 2 / levelCount;
        sideSpeed += 0.4f * (levelCount - 1) + 2 / levelCount;
        rotationSpeed += 0.4f * (levelCount - 1) + 2 / levelCount;
        //Debug.Log("Speed: " + speed + "SideSpeed: " + sideSpeed);
    }


    void Update()
    {
        //vectors
        frontDir = front.position - ship.transform.position;
        frontDir.Normalize();
        rightDir = new Vector3(frontDir.y, -frontDir.x, 0);
        rightDir.Normalize();

        //rotation
        if (attractionObj != null)
        {

            attractionDir = attractionObj.transform.position - ship.transform.position;
            attractionDir.Normalize();

            angleToFront = Vector3.Angle(frontDir, attractionDir);
            angleToRight = Vector3.Angle(rightDir, attractionDir);


            if (angleToFront >= 90f && angleToRight <= 30f) ship.transform.Rotate(0, 0, -(angleToRight));
            else if (angleToFront < 90f && angleToRight > 150f) ship.transform.Rotate(0, 0, -rotationSpeed);
            else if (angleToFront >= 90f && angleToRight > 150f) ship.transform.Rotate(0, 0, 180f-angleToRight);
            else if (angleToFront < 90f && angleToRight < 30f) ship.transform.Rotate(0, 0, rotationSpeed);
            

        }
        
        //movement
        movement.x = frontDir.x * speed * Time.deltaTime + rightDir.x * sideMove * Time.deltaTime + attractionDir.x * attractionSpeed * Time.deltaTime;
        movement.y = frontDir.y * speed * Time.deltaTime + rightDir.y * sideMove * Time.deltaTime + attractionDir.y * attractionSpeed * Time.deltaTime;

        ship.transform.position = ship.transform.position + movement;

        //reset variables
        sideMove = 0f;
        attractionDir = new Vector3 (0, 0, 0);
    }


    public void MoveSide(string letter)
    {
        if (letter == "R") sideMove += sideSpeed;
        if (letter == "L") sideMove -= sideSpeed;
    }

    public void TurnOnFire(string letter)
    {
        if (letter == "R") leftFire.gameObject.SetActive(true);
        else rightFire.gameObject.SetActive(true);
        audioSource.volume = 1;
        audioSource.Play();
    }

    public void TurnOffFire(string letter)
    {
        if (letter == "R") leftFire.gameObject.SetActive(false);
        else rightFire.gameObject.SetActive(false);
        audioSource.volume = 0; 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objName = collision.gameObject.name;

        if (objName != "StartPoint") 

        switch (objName)
        {
            case "Objective":
                GameSettings.Instance.NewLevel();
                break;
            case "OuterAttraction":
                attractionObj = collision.gameObject;
                attractionSpeed = 0f;
                //Debug.Log("Outer In");
                break;
            case "MiddleAttraction":
                attractionObj = collision.gameObject; 
                attractionSpeed = 1f;
                //Debug.Log("Middle In");
                break;
            case "InnerAttraction":
                attractionObj = collision.gameObject; 
                attractionSpeed = 2f;
                //Debug.Log("Inner In");
                break;
            case "Obstacle(Clone)":
                Death();
                //Debug.Log("Inner In");
                break;
            case "NoGoZone(Clone)":
                Death();
                break;
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
        switch (collision.gameObject.name)
        {
            case "InnerAttraction":
                attractionSpeed = 1f;
                attractionObj = collision.transform.parent.Find("MiddleAttraction").gameObject;
                //Debug.Log("Inner Out");
                break;
            case "MiddleAttraction":
                attractionSpeed = 0f;
                attractionObj = collision.transform.parent.Find("OuterAttraction").gameObject;
                //Debug.Log("Middle Out");
                break;
            case "OuterAttraction":
                attractionObj = null;
                attractionSpeed = 0f;
                //Debug.Log("Outer Out");
                break;
        }
    }

    private void Death()
    {
        explosion.gameObject.SetActive(true);
        speed = 0f;
        sideSpeed = 0f;
        attractionObj = null;
        GameSettings.Instance.OnGameOver();
        this.GetComponent<SpriteRenderer>().sprite = null;
    }

    IEnumerator RearFireTime()
    {
        yield return new WaitUntil(() => movement.x > 0);
        yield return new WaitForSeconds(0.95f);
        rearFire.gameObject.GetComponent<AudioSource>().volume = 0f;
        yield return new WaitForSeconds(0.05f);
        rearFire.gameObject.SetActive(false);
    }

}
