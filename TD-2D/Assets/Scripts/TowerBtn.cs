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
	private int rickPrice;
	private int mortyPrice;
	private int summerPrice;

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
		rickPrice = 200;
		mortyPrice = 150;
		summerPrice = 100;
		Price = 2;
	}
}
