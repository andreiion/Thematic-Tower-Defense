using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour {

	[SerializeField]
	private GameObject towerPrefab;

	[SerializeField]
	private Sprite sprite;

	private int price;

	[SerializeField]
	public Text priceText; 

	public int Price {

		get { return price;}
		set { 
			this.price = value;
			this.priceText.text = value.ToString () + "<color=lime>$</color>";
		}
	}

	public Sprite Sprite {
		get {
			return sprite;
		}
	}

	public GameObject TowerPrefab {
		get {
			return towerPrefab;
		}
	}

	public void Start() {
		Price = 2;
	}
}
