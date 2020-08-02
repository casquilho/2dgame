using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum GroundType
{
    NotSeen = 0, 
    Empty   = 1, 
    Grass   = 2, 
    Stone   = 3,
    Dirt    = 4, 
    BedRock = 5
}

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
    public GameObject BedRock;

    public GameObject player;
    public GameObject Portal;


    //Matrix that maps the terrain: 
    //0: "not seen" , 1: "Empty", 
    //2: "Grass"    , 3: "Stone",
    //4: "Dirt"     , 5: "BedRock";
    private int[][] matrix;
    


    // Use this for initialization
    void Start()
    {
        Stone.layer     = 9;
        Grass.layer     = 9;
        Dirt.layer      = 9;
        BedRock.layer   = 9;

        matrix = new int[width][];
        for (int i = 0; i < matrix.Length; i++)
            matrix[i] = new int[height];
        
        GenerateTunnels();
        MakeWall();
        Generation();

        SpawnPlayer();
        SpawnFinish();
    }

    void Generation()
    {
         for (int i = 0; i < width; i++)
         {
             for (int j = 0; j < height; j++)
                 switch (matrix[i][j])
                 {
                    case 2: Instantiate(Grass,   new Vector2(i, j), Quaternion.identity).transform.parent = gameObject.transform; break;
                    case 3: Instantiate(Stone,   new Vector2(i, j), Quaternion.identity).transform.parent = gameObject.transform; break;
                    case 4: Instantiate(Dirt,    new Vector2(i, j), Quaternion.identity).transform.parent = gameObject.transform; break;
                    case 5: Instantiate(BedRock, new Vector2(i, j), Quaternion.identity).transform.parent = gameObject.transform; break;
                    default: break;
                 }
         }  
    }

    private void GenerateTunnels()
    {
        

        while (height - (nTunnels * (tunelHeight + minBlocksBetweenTunnels) + minBlocksBetweenTunnels ) < 2) 
        {
            nTunnels--;
        }
        int[] tunnels = new int[nTunnels];

        tunnels[0] = (minBlocksBetweenTunnels + tunelHeight / 2);
        for (int i = 1; i < nTunnels; i++)
        {
            //tunnels[i] = height + m - (i+1)*height/ nTunnels;
            tunnels[i] = tunnels[i - 1] + tunelHeight + minBlocksBetweenTunnels;//(minBlocksBetweenTunnels+tunelHeight/2) + (i + 1) * height / nTunnels;
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
                
                if (j != 0) 
                {
                    if ((Mathf.Abs(bottom) - Mathf.Abs(oldBottom)) > 2)
                    {
                        if (bottom > oldBottom)
                            bottom = oldBottom + 2;
                        else
                            bottom = oldBottom - 2;
                    }

                    if ((Mathf.Abs(top) - Mathf.Abs(oldTop)) > 2)
                    {
                        if (top > oldTop)
                            top = oldTop + 2;
                        else
                            top = oldTop - 2; 
                    }


                    int dirtLenght = Random.Range(2, minBlocksBetweenTunnels);
                    try
                    {   //Map the spaces between top and bottom with 1:"EmptySpace"
                        for (int k = bottom + 1; k < top; k++)
                        {
                            matrix[j][k] = 1;
                        } 
                        matrix[j][bottom] = 2;
                        matrix[j][top] = 3;

                        for(int l = 0; l < dirtLenght; l++)
                        {
                            matrix[j][bottom-(l+1)] = (int) GroundType.Dirt;
                        }
                    }
                    catch(System.Exception e)
                    {
                        Debug.Log("exception: " + e.ToString() + "j: " + j );
                    }
                    
                }
                oldBottom = bottom;
                oldTop = top;
            }
        }
        //Fill missing spots with stone(3)
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
                if (matrix[i][j] == (int) GroundType.NotSeen)
                    matrix[i][j] = 3;
        }
    }


    private void MakeWall()
    {
        for (int i = 0; i < height; i++)
        {
            matrix[0][i] = (int)GroundType.BedRock;
            matrix[width-1][i] = (int)GroundType.BedRock;
        }
        for (int j = 0; j < width; j++)
        {
            matrix[j][0] = (int)GroundType.BedRock;
            matrix[j][height-1] = (int)GroundType.BedRock;
        }

    }

    private void SpawnPlayer()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (matrix[i][j] == 1)
                {
                    Instantiate(player, new Vector2(i, j), Quaternion.identity);
                    return;
                }
            }
        }
    }

    private void SpawnFinish()
    {
        for (int i = width-1; i >= 0; i--)
        {
            for (int j = height-1; j >= 0; j--)
            {
                if (matrix[i][j] == 1)
                {
                    do
                    {
                        i--;
                    } while (matrix[i][j] != 1);
                    do
                    {
                        j--;
                    } while (matrix[i][j] == 1);

                    j += 2;

                    Instantiate(Portal, new Vector2(i, j), Quaternion.identity);
                    return;
                }
            }
        }
    }
}
