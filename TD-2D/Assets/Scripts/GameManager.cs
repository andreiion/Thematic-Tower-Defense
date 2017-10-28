using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager > {


	public TowerBtn ClickedBtn { get; set; }

	private int currency;

	public int Currency { 
		get { return currency; } 
		set {
			this.currency = value;
			this.currencyText.text = value.ToString() + "<color=lime>$</color>";
		} }

	[SerializeField]
	private Text currencyText;

	// Use this for initialization
	void Start () {
		Currency = 5; 
	}
	
	// Update is called once per frame
	void Update () {
		HandleEscape ();
	} 

	public void PickTower(TowerBtn towerBtn) {

		if (Currency >= towerBtn.Price) { 
			this.ClickedBtn = towerBtn;
			Hover.Instance.Activate (towerBtn.Sprite);
		}
	}  

	public void BuyTower() {
		if (Currency >= ClickedBtn.Price) {
			Currency = Currency - ClickedBtn.Price;
			Hover.Instance.Deactivate ();
		}
	}

	private void HandleEscape() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Hover.Instance.Deactivate ();
		}
	}

}
