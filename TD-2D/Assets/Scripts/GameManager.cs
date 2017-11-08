using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{


    public TowerBtn ClickedBtn { get; set; }

    private int currency;

    private int wave = 0;



    public int Currency
    {
        get { return currency; }
        set
        {
            this.currency = value;
            this.currencyText.text = value.ToString() + "<color=lime>$</color>";
        }
    }

    [SerializeField]
    private Text currencyText;

    [SerializeField]
    private GameObject waveBtn;

    List<Monster> activeMonsters = new List<Monster>();

    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get { return activeMonsters.Count > 0; }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Use this for initialization
    void Start()
    {
        Currency = 50;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerBtn towerBtn)
    {

        if (Currency >= towerBtn.Price && !WaveActive)
        {
            this.ClickedBtn = towerBtn;
            //Hover.Instance.Activate (towerBtn.Sprite);
        }
    }

    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency = Currency - ClickedBtn.Price;
            //Hover.Instance.Deactivate ();
        }
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    // functia asta se apeleaza cand apesi pe butonu' next wave
    public void StartWave()
    {
        wave++;
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    // tipu' inamicului
    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();

        for (int i = 0; i < wave; i++)
        {
            int monsterIndex = Random.Range(0, 3);

            string type = string.Empty;

            switch (monsterIndex)
            {
                case 0:
                    type = "Jerry";
                    break;
                case 1:
                    type = "BirdPerson";
                    break;
                case 2:
                    type = "MrNeedful";
                    break;
                default:
                    break;
            }
            //Pool.GetObject(type);
            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn();

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }

    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive)
        {
            waveBtn.SetActive(true);
        }
    }

}