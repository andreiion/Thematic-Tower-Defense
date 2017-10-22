using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] tiles;

    public float TileSize
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    

	// Use this for initialization
	void Start () {
        CreateLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void PlaceTile(int tileType, int x, int y, Vector3 StartPosition)
    {
        GameObject newTile = Instantiate(tiles[tileType]);
        newTile.transform.position = new Vector3( StartPosition.x + TileSize * x, StartPosition.y - TileSize * y, 0);
    }

    private void CreateLevel()
    {
        //mapSize (4 patratele din camera = 1 tile)
        int mapX = 22;
        int mapY = 10;

        int tileType;

        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {           
            for (int x = 0; x < mapX; x++)
            {

                //prima linie
                if (y == 0 && x < mapX - 2) 
                {
                    tileType = 0;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if (y == 0 && x == mapX - 2)
                {
                    tileType = 1;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint);
                   // PlaceTile(3, mapX+1, y, TopLeftPoint);
                }
                else if (y == 0 && x == mapX - 1)
                {
                    tileType = 2;
                    PlaceTile(tileType, x, y, TopLeftPoint);    
                }

                //a doua linie + a opta linie
                if ( (y == 1 || y == 7) && x < mapX - 2)
                {
                    tileType = 3;
                    PlaceTile(tileType, x, y, TopLeftPoint);                   
                }
                else if ((y == 1 || y == 7) && x == mapX - 2)
                {
                    tileType = 4;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint); // ramanea o goliciune mica
                   // PlaceTile(3, mapX+1, y, TopLeftPoint); // ramanea o goliciune mica
                }
                else if ((y == 1 || y == 7) && x == mapX - 1)
                {
                    tileType = 2;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }

                //a treia linie + a noua linie
                if ( (y == 2 || y == 8) && x == mapX - 2)
                {
                    tileType = 7;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint); // ramanea o goliciune mica
                    //PlaceTile(3, mapX+1, y, TopLeftPoint); // ramanea o goliciune mica
                }
                else if ((y == 2 || y == 8) && x == mapX - 1)
                {
                    tileType = 2;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if ((y == 2 || y == 8) && x > 0)
                {
                    tileType = 6;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if ((y == 2 || y == 8) && x == 0)
                {
                    tileType = 4;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }

                //a patra linie
                if (y == 3 && x == 0)
                {
                    tileType = 4;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint); // ramanea o goliciune mica
                    //PlaceTile(3, mapX+1, y, TopLeftPoint); // ramanea o goliciune mica
                }
                else if (y == 3 && x == 1)
                {
                    tileType = 17;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if (y == 3 && x == mapX - 1)
                {
                    tileType = 2;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if (y == 3 && x > 1)
                {
                    tileType = 0;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }


                //a cincea linie
                if (y == 4 && x > 1)
                {
                    tileType = 3;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if (y == 4 && x == 0)
                {
                    tileType = 4;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint); // ramanea o goliciune mica
                    //PlaceTile(3, mapX+1, y, TopLeftPoint); // ramanea o goliciune mica
                }
                else if (y == 4 && x == 1)
                {
                    tileType = 2;
                    PlaceTile(tileType, x, y, TopLeftPoint);                    
                }

                //a sasea linie
                if (y == 5 && x == 0)
                {
                    tileType = 4;
                    PlaceTile(tileType, x, y, TopLeftPoint);                    
                }
                if (y == 5 && x == 1)
                {
                    tileType = 5;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if (y == 5 && x == mapX - 1)
                {
                    tileType = 6;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint);
                   // PlaceTile(3, mapX+1, y, TopLeftPoint);
                }
                else if (y == 5 && x > 1)
                {
                    tileType = 6;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }

                //a saptea linie
                if (y == 6 && x == 0)
                {
                    tileType = 4;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint); // ramanea o goliciune mica
                    //PlaceTile(3, mapX+1, y, TopLeftPoint); // ramanea o goliciune mica
                }
                else if (y == 6 && x == mapX - 2)
                {
                    tileType = 1;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if (y == 6 && x == mapX - 1)
                {
                    tileType = 2;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                else if (y == 6 && x >= 1)
                {
                    tileType = 0;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }
                
                // linia a zecea
                if (y == 9 && x == 0)
                {
                    tileType = 4;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                    PlaceTile(3, mapX, y, TopLeftPoint);
                    //PlaceTile(3, mapX+1, y, TopLeftPoint);
                }
                else if (y == 9 && x == 1)
                {
                    tileType = 17;
                    PlaceTile(tileType, x, y, TopLeftPoint);
            
                }
                else if (y == 9 && x > 1)
                {
                    tileType = 0;
                    PlaceTile(tileType, x, y, TopLeftPoint);
                }

            }
        }
        
    }
}
