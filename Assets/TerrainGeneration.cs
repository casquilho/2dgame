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

    public float heightpoint;
    public float heightpoint2;


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
        distance = height;
        for (int w = 0; w < width; w++)
        {
           
            int lowernum = distance - 1;
            int heighernum = distance + 2;
            distance = Random.Range(lowernum, heighernum);
            space = Random.Range(12, 20);
            int stonespace = distance - space;


            for (int j = 0; j < stonespace; j++)
            {
                Instantiate(Stone, new Vector3(w, j), Quaternion.identity);
            }

            for (int j = stonespace; j < distance; j++)
            {
                Instantiate(Dirt, new Vector3(w, j), Quaternion.identity);
            }
            Instantiate(Grass, new Vector3(w, distance), Quaternion.identity);





            int i = 0;
            for (i = distance +10 ; i < distance + 10 + stonespace; i++)
            {
                Instantiate(Stone, new Vector3(w, i), Quaternion.identity);
            }

            for (int j = i; j < i+stonespace; j++)
            {
                Instantiate(Dirt, new Vector3(w, j), Quaternion.identity);
            }
            
        }
    }
}
