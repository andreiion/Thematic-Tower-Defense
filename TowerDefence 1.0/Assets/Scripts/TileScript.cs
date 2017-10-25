using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

	public Point GridPosition { get; private set; }

	public void Setup(Point gridPos, Vector3 worldPos, Transform parent) {
		this.GridPosition = gridPos;
		transform.position = worldPos;
		transform.SetParent (parent);

		LevelManager.Instance.Tiles.Add (gridPos, this);
	}

	public Vector2 worldPos {
		get { 
			return new Vector2 (
				transform.position.x + (GetComponent<SpriteRenderer> ().bounds.size.x / 2),
				transform.position.y - (GetComponent<SpriteRenderer> ().bounds.size.y / 2)); 
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseOver() {

//		Debug.Log (GridPosition.X + ", " + GridPosition.Y);


		if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null) {
			if (Input.GetMouseButtonDown(0)) {
				PlaceTower();
			}
		}
	}

	public void PlaceTower() {

		GameObject tower = Instantiate (GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
		tower.GetComponent<SpriteRenderer> ().sortingOrder = GridPosition.Y;

		tower.transform.SetParent (transform); 	
		Hover.Instance.Deactivate ();

		GameManager.Instance.BuyTower ();
	}

}
