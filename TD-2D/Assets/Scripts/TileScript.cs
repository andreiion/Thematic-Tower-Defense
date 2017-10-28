using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

	private Color32 fullColor = new Color32 (255, 118, 118, 255);
	private Color32 emptyColor = Color.white;

	public bool IsEmpty { get; set; }
	public bool Debugging { get; set; }

	public SpriteRenderer SpriteRenderer { get; set;}

	public Point GridPosition { get; private set; }

	public void Setup(Point gridPos, Vector3 worldPos, Transform parent) {
		Walkable = true;
		IsEmpty = true;

		this.GridPosition = gridPos;
		transform.position = worldPos;
		transform.SetParent (parent);

		LevelManager.Instance.Tiles.Add (gridPos, this);
	}

	public bool Walkable { get; set;}

	public Vector2 worldPos {
		get { 
			return new Vector2 (
				transform.position.x + (GetComponent<SpriteRenderer> ().bounds.size.x / 2),
				transform.position.y - (GetComponent<SpriteRenderer> ().bounds.size.y / 2)); 
		}
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer = GetComponent<SpriteRenderer> (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseOver() {

//		Debug.Log (GridPosition.X + ", " + GridPosition.Y);

		ColorTile (fullColor);
		if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null) {
			if (IsEmpty && !Debugging) {
				ColorTile (emptyColor);
			}
			if (!IsEmpty && !Debugging){
				ColorTile (fullColor);
			} else if (Input.GetMouseButtonDown(0)) {
					if (GameManager.Instance.Currency >= GameManager.Instance.ClickedBtn.Price)
				PlaceTower();
			}
		}
	}

	private void OnMouseExit() {
		if (!Debugging)
			ColorTile (Color.white);
	}

	public void PlaceTower() {

		GameObject tower = Instantiate (GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);	

		tower.GetComponent<SpriteRenderer> ().sortingOrder = GridPosition.Y;
		tower.transform.SetParent (transform); 	

		Hover.Instance.Deactivate ();

		IsEmpty = false;
		ColorTile (Color.white);
		Walkable = false;

		GameManager.Instance.BuyTower ();
	}

	private void ColorTile(Color newColor) {
		SpriteRenderer.color = newColor;
	}

}
