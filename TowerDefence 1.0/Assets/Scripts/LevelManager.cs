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

	private Point blueSpawn;
	private Point redSpawn;

	[SerializeField]
	private GameObject bluePortalPrefab; 
	[SerializeField]
	private GameObject redPortalPrefab; 

	public Dictionary<Point, TileScript> Tiles { get; set; }


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
		Vector3 maxTile = Vector3.zero;
		Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint (new Vector3(0, Screen.height));
		Tiles = new Dictionary<Point, TileScript> ();
		for (int y = 0; y < mapYSize; y++) {
			char[] newTiles = mapData [y].ToCharArray ();
			for (int x = 0; x < mapXSize; x++) {
				
				PlaceTile (newTiles[x].ToString(), x, y, worldStartPosition);

			}
		}
		maxTile = Tiles [new Point (mapXSize - 1, mapYSize - 1)].transform.position;
		cameraMovement.SetLimits (new Vector3(maxTile.x + TileSize, maxTile.y - TileSize)) ;
		SpawnPortals ();
	}


	private void PlaceTile(string tileType, int x, int y, Vector3 worldStartPosition) {
		int tileIndex = int.Parse (tileType);
		TileScript  newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

		newTile.Setup (new Point (x, y), new Vector3 (worldStartPosition.x + (TileSize * x), worldStartPosition.y - (TileSize * y), 0), map);
//		Tiles.Add (new Point(x, y), newTile);
	}

	private string[] ReadLevelText() {
		TextAsset bindData = Resources.Load ("Level") as TextAsset;
		string data = bindData.text.Replace (Environment.NewLine, string.Empty);

		return data.Split('-');
	}

	private void SpawnPortals() {
		blueSpawn = new Point (0, 0);
		redSpawn = new Point (11, 6);

		Instantiate (bluePortalPrefab, Tiles [blueSpawn].GetComponent<TileScript>().worldPos , Quaternion.identity);
		Instantiate (redPortalPrefab, Tiles [redSpawn].transform.position, Quaternion.identity);


	}
}
  