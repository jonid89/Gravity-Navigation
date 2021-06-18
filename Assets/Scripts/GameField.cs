using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameField : MonoBehaviour
{
    public int rows = 80;
    public int cols = 100;
    public int gapSize = 1;
    public float width = 1.1f;
    public int verticalRange = 3;
    
    public bool setLevel = true;

    public GameObject obstacle;
    public GameObject objective;
    public GameObject noGoZone;

    private int levelCount;
    

    void Start()
    {
        if (setLevel)
        {
            SetLevelVariables(); 
            GenerateNoGoZone();
            GenerateGrid();
        }
    }


    
    private void SetLevelVariables()
    {
        levelCount = GameSettings.Instance.GetLevelCount();
        rows += 12 * levelCount;
        cols += 18 * levelCount;
        
        objective.transform.position = new Vector3((cols - 40) * gapSize, (rows - 40) * gapSize, 0);
    }


    private void GenerateNoGoZone()
    {

        for (int row = 0; row < (rows + 59); row+=60)
        {
            for (int col = 0; col < (cols + 59); col+=60)
            {
                int posX = col * gapSize;
                int posY = row * gapSize;
                Vector3 position = new Vector3(posX, posY, 0);

                if (row == 0 || col == 0 || row >= rows || col >= cols )
                    if ( posX % 60 == 0 && posY % 60 == 0)
                    {
                        GameObject noGoInst = Instantiate(noGoZone, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
            }
        }
    }


    private void GenerateGrid()
    {

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int posX = col * gapSize;
                int posY = row * gapSize + Random.Range(-verticalRange, verticalRange);

                Vector3 position = new Vector3(posX, posY, 0);
                int scale = Random.Range(Random.Range(Random.Range(Random.Range(3, 7), 8), 9), 10);
                float radio = scale * width;
               
                if ( Physics2D.OverlapCircle(position, radio) == null)
                {
                GameObject obstacleInst = Instantiate(obstacle, transform);

                obstacleInst.transform.position = position;
                obstacleInst.transform.localScale = new Vector3(scale, scale, 1);
                obstacleInst.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

                float circleScale = 0.5f + (0.1f / scale);
                    obstacleInst.transform.GetChild(3).transform.localScale = new Vector3(circleScale, circleScale, 1);


                }
                //else { Debug.Log("Found object at:/pos" + position + "/r:" + radio + "/s" + scale); }
            }
        }
    }


}
