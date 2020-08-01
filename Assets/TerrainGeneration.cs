using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    public int width;
    public int height;
    public int distance;
    public int space;

    public GameObject Grass;
    public GameObject Dirt;
    public GameObject Stone;
    public GameObject player;

    public float heightpoint;
    public float heightpoint2;
    private int playerY;
    private int tunelHeight;


    // Use this for initialization
    void Start()
    {
        Stone.layer = 9;
        Grass.layer = 9;
        Dirt.layer = 9;
        Generation();
    }

    void Generation()
    {
        makeWallAtX(0);
        distance = height / 2;

        for (int w = 1; w < width; w++)
        {
           
            int lowernum = distance - 1;
            int heighernum = distance + 2;
            distance = Random.Range(lowernum, heighernum);
            space = Random.Range(12, 20);
            int stonespace = distance - space;

            //floor
            for (int j = 0; j < stonespace; j++)
            {
                Instantiate(Stone, new Vector3(w, j), Quaternion.identity).transform.parent = gameObject.transform;
            }

            for (int j = stonespace; j < distance; j++)
            {
                Instantiate(Dirt, new Vector3(w, j), Quaternion.identity).transform.parent = gameObject.transform;
            }
            Instantiate(Grass, new Vector3(w, distance), Quaternion.identity).transform.parent = gameObject.transform;
            
            if (w == 3)
                playerY = distance + 1;

            //Determine tunelHight




            //Ceilling
            int i = 0;
            //if( distance + tunelHeight > height)


            for (i = distance +10 ; i < Mathf.Min(distance + 10 + stonespace, height) ; i++)
            {
                Instantiate(Stone, new Vector3(w, i), Quaternion.identity).transform.parent = gameObject.transform; ;
            }

            for (int j = i; j < height; j++)
            {
                Instantiate(Dirt, new Vector3(w, j), Quaternion.identity).transform.parent = gameObject.transform; ;

            }

        }
        makeWallAtX(width);

        Instantiate(player, new Vector3(3, playerY, 0), Quaternion.identity);
    } 
    private void makeWallAtX(int x)
    {
        for (int j = 0; j < height; j++)
        {
            Instantiate(Stone, new Vector3(x, j), Quaternion.identity).transform.parent = gameObject.transform;
            

        }
    }
}
