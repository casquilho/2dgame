using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class v2TerrainGeneration : MonoBehaviour
{

    public int width;
    public int height;
    public int nTunnels;
    public int tunelHeight;
    public int minBlocksBetweenTunnels;

    public GameObject Grass;
    public GameObject Dirt;
    public GameObject Stone;
    public GameObject player;

    public float heightpoint;
    public float heightpoint2;
    private int[][] matrix;
    


    // Use this for initialization
    void Start()
    {
        Stone.layer = 9;
        Grass.layer = 9;
        Dirt.layer = 9;
        minBlocksBetweenTunnels = 3;
        matrix = new int[width][];
        for (int i = 0; i < matrix.Length; i++)
            matrix[i] = new int[height];
        Instantiate(player, new Vector3(3, 20, 0), Quaternion.identity);
        ArrayList[]  tunnels = GenerateTunnels();
        Generation(tunnels);

        

    }

    void Generation(ArrayList[] tunnels)
    {

         for (int i = 0; i < width; i++)
         {
             for (int j = 0; j < height; j++)
                 switch (matrix[i][j])
                 {

                     case 3: Instantiate(Stone, new Vector2(i, j), Quaternion.identity).transform.parent = gameObject.transform; break;
                     case 2: Instantiate(Grass, new Vector2(i, j), Quaternion.identity).transform.parent = gameObject.transform; break;
                 }


         }

        /*for (int i = 0; i < nTunnels; i++)
         {
             int count = 0;
             foreach (YPair pair in tunnels[i])
             {

                 Instantiate(Stone, new Vector2(count, pair.bottom), Quaternion.identity).transform.parent = gameObject.transform; ;
                 Instantiate(Stone, new Vector2(count, pair.top), Quaternion.identity).transform.parent = gameObject.transform;
                 count++;
             }
         }*/




        //if (w == 3)
        // playerY = distance + 1;
        makeWallAtX(0);
        makeWallAtX(width);
        


    }

    private ArrayList[] GenerateTunnels()
    {
        int[] tunnels = new int[nTunnels];
        ArrayList[] tunnelsPairs = new ArrayList[nTunnels];

        while (height - nTunnels * (tunelHeight + minBlocksBetweenTunnels) < 10) 
        {
            nTunnels--;
        }

        for(int i = 0; i < nTunnels; i++)
        {
            tunnels[i] = height + 10 - (i+1)*height/ nTunnels;
            tunnelsPairs[i] = new ArrayList();
        }


        int oldBottom = 0;
        int oldTop = 0;
        int bottom;
        int top;
        for (int i = 0; i < nTunnels; i++)
        {
            int currentTunnel = tunnels[i];

            for (int j = 0; j < width; j++)
            {
                bottom = currentTunnel - Random.Range(2, (tunelHeight / 2));
                top = currentTunnel + Random.Range(2, (tunelHeight / 2));
                
                if (j != 0) {
                    if ((Mathf.Abs(bottom) - Mathf.Abs(oldBottom)) > 2)
                        bottom = oldBottom - 2; //alterar isto para ser + ou - em funçao da direçao do bottom
                    if ((Mathf.Abs(top) - Mathf.Abs(oldTop)) > 2)
                        top = oldTop + 2; //alterar isto para ser + ou - em funçao da direçao do bottom

                    /*if (bottom < 0)
                        bottom = 0;
                    if (top > height)
                        top = height-1;*/
                    //define the values of the cave's blocks on the matrix
                    try
                    {
                        for (int k = bottom + 1; k < top; k++)
                            matrix[j][k] = 1;
                        matrix[j][bottom] = 2;
                        matrix[j][top] = 3;
                    }
                    catch(System.Exception e)
                    {
                        Debug.Log("exception: " + e.ToString());
                    }
                    
                }
                
                oldBottom = bottom;
                oldTop = top;
                tunnelsPairs[i].Add(new YPair(bottom, top));
            }
        }


        //Fill missing spots with stone(3)
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
                if (matrix[i][j] == 0)
                    matrix[i][j] = 3;

        }
        return tunnelsPairs;
    }


    private void makeWallAtX(int x)
    {
        for (int j = 0; j < height; j++)
        {
            Instantiate(Stone, new Vector3(x, j), Quaternion.identity).transform.parent = gameObject.transform;


        }
    }
}
