using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour


{
    [SerializeField] int width, height;
    [SerializeField] int minStoneheight, maxStoneheight;

    //TileMaps
    [SerializeField] Tilemap DirtTile, GrassTile, WaterTile, StoneTile;
    [SerializeField] Tile Dirt, Grass, Water, Stone;

    void Start()
    {
        Generation();   
    }

    void Generation()
    {

        for (int x = 0; x < width; x++){

            int minHeight = height - 1;
            int maxHeight = height + 2;


            height = Random.Range(minHeight, maxHeight);
            int minStoneSpawnDistance = height - minStoneheight;
            int maxStoneSpawnDistance = height - maxStoneheight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);

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
