using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] int minStoneheight, maxStoneheight, min;


    //TileMaps
    [SerializeField] Tilemap DirtTile, GrassTile, WaterTile, StoneTile;
    [SerializeField] Tile Dirt, Grass, Water, Stone;

    //PerlinNosie
    [Range(0,100)]
    [SerializeField] float heightValue, smoothness;
    float seed;
    

    void Start()

    {
        seed = Random.Range(-10000000, 1000000);
        Generation();   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            seed = Random.Range(-10000000, 1000000);
            Generation();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            StoneTile.ClearAllTiles();
            DirtTile.ClearAllTiles();
            GrassTile.ClearAllTiles();
        }
    }

    void Generation()
    {

        for (int x = 0; x < width; x++){

            //Height Generation
            height = Mathf.RoundToInt(heightValue * Mathf.PerlinNoise(x / smoothness, seed));


            //Max Stone Spawn Distance
            int minStoneSpawnDistance = height - minStoneheight;
            int maxStoneSpawnDistance = height - maxStoneheight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);


            int maxGrassHeightDistance = height;
            int maxWaterSpawnDistance = maxGrassHeightDistance;
            int mimWaterSpawnDistance = height - 0; 

            for (int y = 0; y < height; y++)
            {

               if(y < totalStoneSpawnDistance)
                {
                    StoneTile.SetTile(new Vector3Int(x, y, 0), Stone);
                }

                else
                {

                    DirtTile.SetTile(new Vector3Int(x, y, 0), Dirt);

                }
                    
               if (totalStoneSpawnDistance == height)
                {
                    StoneTile.SetTile(new Vector3Int(x, height, 0), Stone);
                }

                else 
                {
                    GrassTile.SetTile( new Vector3Int(x, height, 0), Grass);
                }

              
                
                
            }

            
        }
    }

}//End
