using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;



public class LevelManager : Singleton<LevelManager> {

	[SerializeField]
	private GameObject[] tilePrefabs;
	   
	[SerializeField]
	private CameraMovement cameraMovement;

	[SerializeField]
	private Transform map;

	[SerializeField]
	private GameObject bluePortalPrefab; 
	[SerializeField]
	private GameObject redPortalPrefab; 

	int RED_PORTAL = 0;
	int BLUE_PORTAL = 1;
	private Point blueSpawn;
	private Point redSpawn;

	public Dictionary<Point, TileScript> Tiles { get; set; }

	private Point mapSize;

	public float TileSize {
		get { return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x; }
	}

	// Use this for initialization
	void Start () {
		CreateLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLevel() {

		string[] mapData = ReadLevelText();

		int mapXSize = mapData [0].ToCharArray ().Length;
		int mapYSize = mapData.Length;

		mapSize = new Point (mapXSize, mapYSize);
			
		Vector3 maxTile = Vector3.zero;
		Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint (new Vector3(0, Screen.height));

		Tiles = new Dictionary<Point, TileScript> ();
		for (int y = 0; y < mapYSize; y++) {
			char[] newTiles = mapData [y].ToCharArray ();
			for (int x = 0; x < mapXSize; x++) {
				if ((newTiles [x] >= '0') && (newTiles [x] <= '9')) {
					PlaceTile (newTiles [x].ToString (), x, y, worldStartPosition);
				}
				else if (newTiles [x] == 'x'){
					//This is portal
					PlaceTile (newTiles [3].ToString (), x, y, worldStartPosition);
					SpawnPortals (new Point(x, y), RED_PORTAL);
				}
				else if (newTiles [x] == 'y')
				{
					PlaceTile (newTiles [3].ToString (), x, y, worldStartPosition);
					SpawnPortals (new Point(x, y), BLUE_PORTAL);
				}
			}
		}
		maxTile = Tiles [new Point (mapXSize - 1, mapYSize - 1)].transform.position;
		//cameraMovement.SetLimits (new Vector3(maxTile.x + TileSize, maxTile.y - TileSize)) ;

	}


	private void PlaceTile(string tileType, int x, int y, Vector3 worldStartPosition) {
		int tileIndex = int.Parse (tileType);
		TileScript  newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

		newTile.Setup (new Point (x, y), new Vector3 (worldStartPosition.x + (TileSize * x), worldStartPosition.y - (TileSize * y), 0), map);
	}

	private string[] ReadLevelText() {
		TextAsset bindData = Resources.Load ("Level") as TextAsset;
		string data = bindData.text.Replace (Environment.NewLine, string.Empty);
		return data.Split('-');
	}

	private void SpawnPortals(Point start, int portal) {

		if (portal == RED_PORTAL) {
			redSpawn = start;
			Instantiate (redPortalPrefab, Tiles [redSpawn].transform.position, Quaternion.identity);
		} else {
			blueSpawn = start;
			Instantiate (bluePortalPrefab, Tiles [blueSpawn].GetComponent<TileScript>().worldPos , Quaternion.identity);
		}
	}

	public bool InBounds(Point position) {
		return position.X >= 0 && position.Y >= 0 &&
			position.X < mapSize.X && position.Y < mapSize.Y;
	}

}
  